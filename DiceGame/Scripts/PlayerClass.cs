using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Scripts
{
    internal class PlayerClass(string playerName)
    {
        private string name = playerName;
        int score = 0;
        private List<string> dice = new List<string>();
        internal List<string> Dice => new List<string>(dice);
        public string Name => name;
        public int Score => score;

        internal void addScore(int points) { score += points; }

        internal void AddDice(IEnumerable<string> diceToAdd)
        {
            dice.AddRange(diceToAdd);
        }

        internal void RemoveDie(string die)
        {
            dice.Remove(die);
        }

        internal void Reset()
        {
            dice.Clear();
            score = 0;
        }
    }
}
