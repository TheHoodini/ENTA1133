using GD14_1133_A1_JuanDiego_DiceGame.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Combat
{
    public static class CombatRoller
    {
        private static readonly Random _random = new();
        private static readonly TextPrinter textPrinter = new()
        {
            PrinterType = "1"
        };
        private static TextPrinter printer = textPrinter;

        private static (int total, bool isCritical) RollDice(string dice)
        {
            int total = 0;
            bool isCritical = true;

            var diceParts = dice.Split(',');

            foreach (var part in diceParts)
            {
                var trimmed = part.Trim();
                var split = trimmed.ToLower().Split('d');

                if (split.Length != 2 ||
                    !int.TryParse(split[0], out int count) ||
                    !int.TryParse(split[1], out int sides) ||
                    count <= 0 || sides <= 0)
                {
                    Console.WriteLine($"Invalid dice '{trimmed}'");
                    continue;
                }

                for (int i = 0; i < count; i++)
                {
                    int roll = _random.Next(1, sides + 1);
                    total += roll;

                    if (roll != sides)
                    {
                        isCritical = false;
                    }
                }
            }

            return (total, isCritical);
        }

        public static int Heal(string dice)
        {
            var (result, _) = RollDice(dice);
            printer.Print($"You healed {result} HP!");
            return result;
        }

        public static int Attack(int damage, string dice, bool isPlayer)
        {
            var (result, isCritical) = RollDice(dice);
            result += damage;

            if (isCritical)
            {
                result *= 2;
                if (isPlayer)
                    printer.Print($"Critical hit! You did {result} damage!");
                else
                    printer.Print($"Oh no, a critical hit! You took {result} damage!");
            }
            else
            {
                if (isPlayer)
                    printer.Print($"You did {result} damage!");
                else
                    printer.Print($"You took {result} damage!");
            }

            return result;
        }
    }


}
