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

        public string Name => name;
        public int Score => score;
        internal void addScore(int points) { score += points; } 
    }
}
