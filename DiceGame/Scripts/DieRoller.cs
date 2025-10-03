using GD14_1133_A1_JuanDiego_DiceGame.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame
{
    internal class DieRoller()
    {
        private readonly Random random = new();
        private readonly TextPrinter textPrinter = new();

        // Die configuration and availability list
        private readonly Dictionary<string, (int MaxRoll, bool Available)> dice = new()
        {
            { "d4",  (4, true) },
            { "d6",  (6, true) },
            { "d8",  (8, true) },
            { "d12", (12, true) },
            { "d20", (20, true) }
        };

        // Properties to expose availability
        public bool IsD4Available => dice["d4"].Available;
        public bool IsD6Available => dice["d6"].Available;
        public bool IsD8Available => dice["d8"].Available;
        public bool IsD12Available => dice["d12"].Available;
        public bool IsD20Available => dice["d20"].Available;

        public int Roll(string dieType)
        {
            var (maxRoll, isAvailable) = dice[dieType];
            textPrinter.Print($"Rolling the {dieType}...");

            int roll = random.Next(1, maxRoll + 1);

            // Mark die as unavailable
            dice[dieType] = (maxRoll, false);

            textPrinter.Print($"The {dieType} rolled a {roll}");

            // Commentary based on roll
            string comment = roll switch
            {
                int r when r == maxRoll => "Excellent, a maximum roll!!!",
                1 => "A critical fail...",
                int r when r == (maxRoll / 2) => "Well, the average",
                int r when r > (maxRoll / 2) => "Above average!",
                _ => "Below average..."
            };

            textPrinter.Print(comment);
            return roll;
        }
    }

}
