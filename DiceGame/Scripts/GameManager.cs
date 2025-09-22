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
            string playerName = ChoosePlayerName();
            string printType = ChooseOption("Choose the type of text printer (type the number):\n1. Typewriter Effect\n2. Regular Print\n>");
            string rulesType = ChooseOption("Do you want to read the rules? (type the number):\n1. Yes\n2. No\n>");
            if (rulesType == "1")
            {
                textPrinter.Print(printType, "\nRules:\n" +
                    "1. Pick a type of die.\n" +
                    "2. \n" +
                    "3. \n" +
                    "4. \n");
            }
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
            textPrinter.Print(printType, "\nNow we can begin! " + playerName + ", let's play Crazy Dice!\n" + todayDate + "\n");
            // Start the game with the chosen settings
            Play(playerName, printType);
        }

        // Start a game round
        public void Play(string playerName, string printType)
        {
            // Decide who starts first
            textPrinter.Print(printType, "Deciding turn...\n");
            Random rand = new Random();
            int coinFlip = rand.Next(2);
            if (coinFlip == 1)
            {
                textPrinter.Print(printType, "You start first!\n");
                textPrinter.Print(printType, "Choose one of these dice: d4, d6, d8, d12 and d20");
                Console.ReadLine();
            }
            else
            {
                textPrinter.Print(printType, "Computer starts first!\n");
            }


        }

        // Function to choose the type of text printer 
        static string ChooseOption(string text)
        {
            Console.Write(text);
            string choice = Console.ReadLine();
            if (choice == "1" || choice == "2")
            {
                return choice; 
            }
            else
            {
                // Ask again in a recursive way if the input is invalid
                Console.WriteLine("Invalid answer, please choose between 1 or 2.\n");
                return ChooseOption(text); 
            }
        }

        // Function to choose the player's name and check if it's not blank
        static string ChoosePlayerName()
        {
            Console.Write("What is your name?\n>");
            string name = Console.ReadLine();
            // Ask again in a recursive way if the input is blank
            if (string.IsNullOrWhiteSpace(name))
            {                
                Console.WriteLine("Please type a name!");
                return ChoosePlayerName();
            }
            else
            {
                return name;
            }
        }
    }
}
