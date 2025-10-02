using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Scripts
{
    internal class GameManager
    {
        TextPrinter textPrinter = new TextPrinter();
        InputManager inputManager = new InputManager();
        Random random = new Random();

        string playerName;
        PlayerClass player1;
        string printType;

        public void Start()
        {
            Welcome();
            Play();
        }
        public void Welcome()
        {
            // Display the game title
            Console.WriteLine("              _                            _                 \n" +
                              "             | |                          | |                \n" +
                              "__      _____| | ___ ___  _ __ ___   ___  | |_ ___          \n" +
                              "\\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ | __/ _ \\         \n" +
                              " \\ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) |  _ _ _ \n" +
                              "  \\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|  \\__\\___/  (_|_|_)\n");
            Console.WriteLine("  ______                                                      __  __                     \n" +
                          " /      \\                                                    /  |/  |                    \n" +
                          "/$$$$$$  |  ______   ______   ________  __    __         ____$$ |$$/   _______   ______  \n" +
                          "$$ |  $$/  /      \\ /      \\ /        |/  |  /  |       /    $$ |/  | /       | /      \\ \n" +
                          "$$ |      /$$$$$$  |$$$$$$  |$$$$$$$$/ $$ |  $$ |      /$$$$$$$ |$$ |/$$$$$$$/ /$$$$$$  | \n" +
                          "$$ |   __ $$ |  $$/ /    $$ |  /  $$/  $$ |  $$ |      $$ |  $$ |$$ |$$ |      $$    $$ | \n" +
                          "$$ \\__/  |$$ |     /$$$$$$$ | /$$$$/__ $$ \\__$$ |      $$ \\__$$ |$$ |$$ \\_____ $$$$$$$$/ \n" +
                          "$$    $$/ $$ |     $$    $$ |/$$      |$$    $$ |      $$    $$ |$$ |$$       |$$       | \n" +
                          " $$$$$$/  $$/       $$$$$$$/ $$$$$$$$/  $$$$$$$ |       $$$$$$$/ $$/  $$$$$$$/  $$$$$$$/ \n" +
                          "                                       /  \\__$$ |                                       \n" +
                          "                                       $$    $$/                                          \n" +
                          "                                        $$$$$$/                                           ");
            // Ask for the player's name and type of text printer to start the game
            Console.Write("What is your name?\n>");
            do
            {
                playerName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(playerName))
                {
                    Console.WriteLine("Please type a name!");
                    Console.Write("> ");
                }
            } while (string.IsNullOrWhiteSpace(playerName));
            player1 = new PlayerClass(playerName); // Create a player object with the name entered

            // Ask for the type of text printer
            Console.Write("Choose the type of text printer (type the number):\n1. Typewriter Effect\n2. Regular Print\n>");
            do
            {
                printType = Console.ReadLine();
                if (printType != "1" && printType != "2")
                {
                    Console.WriteLine("Invalid answer, please choose between 1 or 2.\n");
                    Console.Write("> ");
                }
            } while (printType != "1" && printType != "2");

            // Ask if the player wants to read the rules
            string rulesOption;
            Console.Write("Do you want to read the rules? (type the number):\n1. Yes\n2. No\n>");
            do
            {
                rulesOption = Console.ReadLine();
                if (rulesOption != "1" && rulesOption != "2")
                {
                    Console.WriteLine("Invalid answer, please choose between 1 or 2.\n");
                    Console.Write(">");
                }
            } while (rulesOption != "1" && rulesOption != "2");
            if (rulesOption == "1")
            {
                textPrinter.Print(printType, "\nRules:\n" +
                    "1. First we flip a coin to decide the turn order\n" +
                    "2. The first player picks the die the other player will roll and then the die they will roll\n" +
                    "3. The second one picks the die of the first player and then their own die\n" +
                    "4. The player with the highest score wins");
            }

            // Greet the player and show the current date
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
            textPrinter.Print(printType, "\n" + todayDate + "\nWelcome " + player1.Name + "! Let's play Crazy Dice!\n");
        }

        // ------------------- Start a game round -------------------
        public void Play()
        {
            DieRoller dieRoller = new DieRoller(printType);
            // Create a CPU player
            PlayerClass playerCpu = new PlayerClass("Player 2");
            // Decide turns
            textPrinter.Print(printType, "Flipping the coin to decide who starts...");
            int coinFlip = random.Next(1, 3);
            int roll = 0;
            // First turn
            if (coinFlip == 1)
            {
                textPrinter.Print(printType, player1.Name + " starts!\n");
                // check the die the player wants to roll and roll it
                string dieType = inputManager.ChooseDie(dieRoller, true);
                roll = dieRoller.Roll(dieType);
                // Add the score
                player1.addScore(roll);
            }
            else
            {
                textPrinter.Print(printType, "The CPU starts!\n");
                textPrinter.Print(printType, "The CPU is picking their die...");
                string dieType = inputManager.ChooseDie(dieRoller, false);
                roll = dieRoller.Roll(dieType);
                // Add the score
                playerCpu.addScore(roll);
            }

            // Second turn
            if (coinFlip != 1) // If player 2 started, player 1 goes now
            {
                // check the die the player wants to roll and roll it
                textPrinter.Print(printType, "\nNow it's your turn!");
                string dieType = inputManager.ChooseDie(dieRoller, true);
                roll = dieRoller.Roll(dieType);
                // Add the score
                player1.addScore(roll);
            }
            else // If player 1 started, player 2 goes now
            {
                textPrinter.Print(printType, "\nNow it's CPU's turn to pick their die...");
                string dieType = inputManager.ChooseDie(dieRoller, false);
                roll = dieRoller.Roll(dieType);
                // Add the score
                playerCpu.addScore(roll);
            }

            if (player1.Score > playerCpu.Score)
            {
                textPrinter.Print(printType, "\n" + player1.Name + " wins with a score of " + player1.Score + "! Congratulations!");
            }
            else if (player1.Score < playerCpu.Score)
            {
                textPrinter.Print(printType, "\n The CPU wins with a score of " + playerCpu.Score + "! Better luck next time!");
            }
            else
            {
                textPrinter.Print(printType, "\nIt's a tie! Another round will start!");
                Play(); // Start another round in case of a tie
            }
        }

    }
}
