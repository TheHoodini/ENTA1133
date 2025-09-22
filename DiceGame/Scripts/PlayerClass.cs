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
        internal string GetName() { return name; }
    }
}
