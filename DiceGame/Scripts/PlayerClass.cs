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
        private List<string> dice = new List<string> { "d6", "d8", "d10", "d12", "d20" };

        public string Name => name;
        public int Score => score;
        internal void addScore(int points) { score += points; } 

        internal string GetDie(string die)
        {
            if (dice.Contains(die))
            {
                dice.Remove(die);
                return die;
            }
            else
            {
                return null;
            }
        }
    }
}
