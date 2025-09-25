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
        public void Start()
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
            string playerName = inputManager.ChoosePlayerName();
            PlayerClass player1 = new PlayerClass(playerName); // Create a player object with the name entered
            string printType = inputManager.ChooseOption("Choose the type of text printer (type the number):\n1. Typewriter Effect\n2. Regular Print\n>");
            string rulesOption = inputManager.ChooseOption("Do you want to read the rules? (type the number):\n1. Yes\n2. No\n>");
            if (rulesOption == "1")
            {
                textPrinter.Print(printType, "\nRules:\n" +
                    "1. First we flip a coin to decide the turn order\n" +
                    "2. The first player chooses a die to roll between some options and rolls it\n" +
                    "3. The second one chooses a different die and rolls it\n" +
                    "4. The player with the highest score wins");
            }
            // Greet the player and show the current date
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
            textPrinter.Print(printType, "\nWelcome " + player1.Name + "! Let's play Crazy Dice!\n" + todayDate + "\n");
            Play(printType, player1);
        }

        // ------------------- Start a game round -------------------
        public void Play(string printType, PlayerClass player)
        {
            DieRoller dieRoller = new DieRoller(printType);
            // Decide turns
            textPrinter.Print(printType, "Flipping the coin to decide who starts...");
            int coinFlip = random.Next(1, 3); 
            if (coinFlip == 1)
            {
                textPrinter.Print(printType, player.Name + " starts!\n");
                string dieType = inputManager.ChooseDie(dieRoller);
                dieRoller.Roll(dieType);
            }
            else
            {
                textPrinter.Print(printType, "Player 2 starts!\n");
            }


        }

    }
}
