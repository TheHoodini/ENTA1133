using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Scripts
{
    internal class TextPrinter
    {
        public void Print(string printerType, string text)
        {
            if (printerType == "1")
            {
                foreach (char c in text)
                {
                    Console.Write(c);
                    Thread.Sleep(20);
                }
                Console.WriteLine();
                Thread.Sleep(400);
            }
            else
            {
                Console.WriteLine(text);
            }
        }
    }
}
