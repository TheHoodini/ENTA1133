using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Scripts
{
    internal class InputManager
    {
        // Function to choose the die type and check if it's valid
        public string ChooseDie(DieRoller dieRoller, bool isPlayer)
        {
            // Link die names to their availability
            var dieAvailability = new Dictionary<string, bool>
            {
                { "d4", dieRoller.IsD4Available },
                { "d6", dieRoller.IsD6Available },
                { "d8", dieRoller.IsD8Available },
                { "d12", dieRoller.IsD12Available },
                { "d20", dieRoller.IsD20Available }
            };
            string dieOption = "";

            Console.WriteLine("Choose a die to roll:\nd4  d6  d8  d12  d20");
            while (true)
            {
                if (isPlayer)
                {
                    Console.Write("> ");
                    dieOption = Console.ReadLine()?.ToLower();
                }
                else
                {
                    // CPU randomly selects a die
                    string[] options = { "d4", "d6", "d8", "d12", "d20" };
                    Random random = new Random();
                    dieOption = options[random.Next(options.Length)];
                }

                // Validate the chosen die
                if (dieAvailability.ContainsKey(dieOption))
                {
                    if (dieAvailability[dieOption])
                    {
                        return dieOption;
                    }
                    else if (isPlayer)
                    {
                        Console.WriteLine("This die has already been used, please choose another one.\n");
                    }
                }
                else if (isPlayer)
                {
                    Console.WriteLine("Invalid answer, please choose between d4, d6, d8, d12 or d20\n");
                }

            }
        }


    }
}
