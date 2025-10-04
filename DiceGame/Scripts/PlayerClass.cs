using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Scripts
{
    internal class PlayerClass(string playerName, bool isPlayer = true)
    {
        private string name = playerName;
        private int score = 0;
        private List<string> dice = new();
        private string summary = "";
        private readonly DieRoller dieRoller = new();
        private List<int> pastRolls = new(); // Track all roll results
        private List<string> diceUsedHistory = new(); // Track the types of dice used

        public string Name => name;
        public int Score => score;
        public string Summary => summary;
        internal List<string> Dice => new List<string>(dice);

        internal void addScore(int points)
        {
            score += points;
        }

        internal void AddDice(IEnumerable<string> diceToAdd)
        {
            dice.AddRange(diceToAdd);
        }

        internal int UseDie(string die, TextPrinter printer)
        {
            // Roll the die
            int roll = dieRoller.Roll(die, printer, isPlayer);
            pastRolls.Add(roll);
            diceUsedHistory.Add(die);
            //addScore(roll);
            dice.Remove(die);

            int total = pastRolls.Sum();
            int highest = pastRolls.Max();
            int evens = pastRolls.Count(r => r % 2 == 0);
            int odds = pastRolls.Count - evens;

            // Calculate expected average total
            int expectedTotal = 0;
            foreach (var usedDie in pastRolls.Zip(diceUsedHistory, (rollValue, dieType) => dieType))
            {
                if (int.TryParse(usedDie[1..], out int sides))
                {
                    expectedTotal += (int)Math.Round((sides + 1) / 2.0);
                }
            }

            // Comment based on total vs expected
            string totalComment;
            if (total < expectedTotal)
            {
                totalComment = "That was the best you got?";
            }
            else if (total > expectedTotal)
            {
                totalComment = "You're truly a dice master!";
            }
            else
            {
                totalComment = "Not bad!";
            }

            summary = $"You rolled {pastRolls.Count} dice in total.\n" +
                      $"Your total score was {total}. {totalComment}\n" +
                      $"Your highest roll was {highest}.\n" +
                      $"You had {evens} even rolls and {odds} odd rolls.";

            return roll;
        }

        internal void Reset()
        {
            dice.Clear();
            score = 0;
            summary = "";
            pastRolls.Clear();
            diceUsedHistory.Clear();
        }
    }

}
