using GD14_1133_A1_JuanDiego_DiceGame.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame
{
    internal class DieRoller
    {
        private readonly Random random = new();
        public int Roll(string dieType, TextPrinter textPrinter, bool isPlayer)
        {
            // Extract the maximum roll from the die type 
            int maxRoll = int.Parse(dieType[1..]);

            // Generate a random roll between 1 and the max roll
            int rollResult = random.Next(1, maxRoll + 1);

            string name;
            if (isPlayer) { name = "You"; } else { name = "Dizarius"; }
            textPrinter.Print($"{name} rolled a {dieType}... The result was a {rollResult}!");

            // Comment based on roll
            string comment = rollResult switch
            {
                int r when r == maxRoll => "Excellent, a maximum roll!!!",
                1 => "A critical fail...",
                int r when r == (maxRoll / 2) => "Well, the average",
                int r when r > (maxRoll / 2) => "Above average!",
                _ => "Below average..."
            };

            textPrinter.Print(comment);
            return rollResult;
        }
    }



}
