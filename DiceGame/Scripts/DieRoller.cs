using GD14_1133_A1_JuanDiego_DiceGame.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame
{
    internal class DieRoller(string textType)
    {
        Random random = new Random();
        TextPrinter textPrinter = new TextPrinter();
        bool isD4Available = true;
        bool isD6Available = true;
        bool isD8Available = true;
        bool isD12Available = true;
        bool isD20Available = true;

        // Properties to check if a die is available
        public bool IsD4Available => isD4Available;
        public bool IsD6Available => isD6Available;
        public bool IsD8Available => isD8Available;
        public bool IsD12Available => isD12Available;
        public bool IsD20Available => isD20Available;
        public void Roll(string dieType)
        {
            int roll = 0;
            textPrinter.Print(textType, "Rolling the " + dieType + "...");
            // Roll the selected die and mark it as unavailable
            if (dieType == "d4")
            {
                roll = random.Next(1, 5);
                isD4Available = false;
            }
            else if (dieType == "d6")
            {
                roll = random.Next(1, 7);
                isD6Available = false;
            }
            else if (dieType == "d8")
            {
                roll = random.Next(1, 9);
                isD8Available = false;
            }
            else if (dieType == "d12")
            {
                roll = random.Next(1, 13);
                isD12Available = false;
            }
            else if (dieType == "d20")
            {
                roll = random.Next(1, 21);
                isD20Available = false;
            }
            textPrinter.Print(textType, "You rolled a " + roll);
        }
    }
}
