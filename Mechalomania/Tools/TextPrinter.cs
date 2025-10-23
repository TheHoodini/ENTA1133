using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Tools
{
    internal class TextPrinter
    {
        private string printerType = "2";

        // Property to get or set the printer type
        public string PrinterType
        {
            get { return printerType; }
            set { printerType = value; }
        }
        public void Print(string text)
        {
            if (printerType == "1")
            {
                foreach (char c in text)
                {
                    if (c == '\n')
                    {
                        Console.Write(c);           
                        Thread.Sleep(400);         
                    }
                    else
                    {
                        Console.Write(c);          
                        Thread.Sleep(15);          
                    }
                }
                Thread.Sleep(200);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(text);
            }
        }

        public void Dialogue(string character, string text, bool clearConsole = false)
        { 
            if (clearConsole) { Console.Clear(); } else { Console.WriteLine("\n"); }
            const int dBoxWidth = 69; 
            string upperCharacter = character.ToUpper();

            // Centered top line
            string topLine = new string('═', 34 - upperCharacter.Length / 2);
            Console.WriteLine($"{topLine}╣{upperCharacter}╠{topLine}");

            // Prepare wrapped lines
            List<string> wrappedLines = WrapText(text, dBoxWidth);

            // Print each wrapped line using animation
            foreach (string line in wrappedLines)
            {
                Print(line);
            }

            // Draw bottom line
            Console.WriteLine(new string('═', dBoxWidth + 2));

        }

        // Helper method to wrap text into lines to fit within a specified width
        private List<string> WrapText(string text, int maxLineLength)
        {
            var lines = new List<string>();
            var paragraphs = text.Split('\n'); // Respect manual line breaks

            foreach (var paragraph in paragraphs)
            {
                var words = paragraph.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var currentLine = new StringBuilder();

                foreach (var word in words)
                {
                    if (currentLine.Length + word.Length + 1 > maxLineLength)
                    {
                        lines.Add(currentLine.ToString().TrimEnd());
                        currentLine.Clear();
                    }
                    currentLine.Append(word + " ");
                }

                if (currentLine.Length > 0)
                {
                    lines.Add(currentLine.ToString().TrimEnd());
                }
            }

            return lines;
        }


    }
}
