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

        public void Dialogue(string character, string text)
        { 
            Console.Clear();
            const int dBoxWidth = 56; // Max characters per line
            string upperCharacter = character.ToUpper();

            // Centered top line
            string topLine = new string('=', 28 - upperCharacter.Length / 2);
            Console.WriteLine($"{topLine}[{upperCharacter}]{topLine}");

            // Prepare wrapped lines
            List<string> wrappedLines = WrapText(text, dBoxWidth);

            // Print blank lines to reserve space
            foreach (var _ in wrappedLines)
            {
                Console.WriteLine();
            }

            // Draw bottom line
            Console.WriteLine(new string('=', dBoxWidth + 2));

            // Move cursor back up to start printing lines
            int currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, currentLine - wrappedLines.Count - 1);

            // Print each wrapped line using animation
            foreach (string line in wrappedLines)
            {
                Print(line);
            }

            // Reset cursor to below the dialogue box
            Console.SetCursorPosition(0, currentLine);
        }

        // Helper method to wrap text into lines of diLalog box
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
