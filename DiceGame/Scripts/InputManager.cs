using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Scripts
{
    internal class InputManager
    {
        // Function to validate options (1 or 2)
        public string ChooseOption(string text)
        {
            Console.Write(text);
            string typeChoice = Console.ReadLine();
            if (typeChoice == "1" || typeChoice == "2")
            {
                return typeChoice;
            }
            else
            {
                // Ask again in a recursive way if the input is invalid
                Console.WriteLine("Invalid answer, please choose between 1 or 2.\n");
                return ChooseOption(text);
            }
        }

        // Function to choose the player's name and check if it's not blank
        public string ChoosePlayerName()
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

        // Function to choose the die type and check if it's valid
        public string ChooseDie(DieRoller dieRoller, bool isPlayer)
        {
            string dieOption = "";
            if (isPlayer) 
            {
                Console.Write("Choose a die to roll:\nd4  d6  d8  d12  d20\n>");
                dieOption = Console.ReadLine()?.ToLower();
            } else
            {
                string[] options = { "d4", "d6", "d8", "d12", "d20" };
                Random random = new Random();
                dieOption = options[random.Next(options.Length)];
            }
            // Check if the input is valid and if the die is available
            if (dieOption == "d4")
            {
                if (dieRoller.IsD4Available) return "d4";
            }
            else if (dieOption == "d6")
            {
                if (dieRoller.IsD6Available) return "d6";
            }
            else if (dieOption == "d8")
            {
                if (dieRoller.IsD8Available) return "d8";
            }
            else if (dieOption == "d12")
            {
                if (dieRoller.IsD12Available) return "d12";
            }
            else if (dieOption == "d20")
            {
                if (dieRoller.IsD20Available) return "d20";
            }
            else
            {
                if (isPlayer) { Console.WriteLine("Invalid answer, please choose between d4, d6, d8, d12 or d20\n"); }
                return ChooseDie(dieRoller, isPlayer);
            }

            if (isPlayer) { Console.WriteLine("This die has already been used, please choose another one.\n"); }
            return ChooseDie(dieRoller, isPlayer);
        }

    }
}
