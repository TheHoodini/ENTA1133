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
            string printType = ChooseTextType();
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
            textPrinter.Print(printType, "\nWelcome " + playerName + "! Let's play Crazy Dice!\n" + todayDate + "\n");
            Play(playerName, printType);
        }

        // Start a game round
        public void Play(string playerName, string printType)
        {
            Console.WriteLine("Game start");
        }

        // Function to choose the type of text printer 
        static string ChooseTextType()
        {
            Console.WriteLine("Choose the type of text printer (type the number):\n1. Typewriter Effect\n2. Regular Print");
            string typeChoice = Console.ReadLine();
            if (typeChoice == "1" || typeChoice == "2")
            {
                return typeChoice; 
            }
            else
            {
                // Ask again in a recursive way if the input is invalid
                Console.WriteLine("Invalid answer, please choose between 1 or 2.\n");
                return ChooseTextType(); 
            }
        }

        // Function to choose the player's name and check if it's not blank
        static string ChoosePlayerName()
        {
            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();
            // Ask again in a recursive way if the input is blank
            if (string.IsNullOrWhiteSpace(name))
            {                Console.WriteLine("Please type a name!");
                return ChoosePlayerName();
            }
            else
            {
                return name;
            }
        }
    }
}
