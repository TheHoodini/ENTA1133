using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GD14_1133_A1_JuanDiego_DiceGame.Classes;
using GD14_1133_A1_JuanDiego_DiceGame.Dungeon; // Needed for Room
using GD14_1133_A1_JuanDiego_DiceGame.Tools;   // In case of cross-refs like Player

namespace GD14_1133_A1_JuanDiego_DiceGame.Tools
{
    public static class Utilities
    {
        // Fully clear the console
        public static void FullClear()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.Clear();
        }

        // Clear n lines above in the console
        public static void ClearLines(int n)
        {
            int currentLine = Console.CursorTop;

            for (int i = 1; i <= n; i++)
            {
                int lineToClear = currentLine - i;
                if (lineToClear < 0)
                    break;

                Console.SetCursorPosition(0, lineToClear);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, currentLine - n);
        }

        // Print the question again 
        public static void InputText(string message, int clearLines = 4, string question = "\nWhat will you do? (n/s/e/w, check, inventory):\n>")
        {
            ClearLines(clearLines);
            Console.WriteLine($"{message}");
            Console.Write(question);
        }

        public static void PrintMap(Room[,] dungeon, Room playerRoom)
        {
            int rows = dungeon.GetLength(0);
            int cols = dungeon.GetLength(1);
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Room r = dungeon[i, j];
                    if (r == playerRoom)
                    {
                        Console.Write("[■]");
                    }
                    else if (r.Visited)
                    {
                        Console.Write($"[{r.MapSymbol()}]");
                    }
                    else
                    {
                        Console.Write("[ ]");
                    }
                }
                Console.WriteLine();
            }
        }

        public static void PrintDungeonUI(Room[,] dungeon, Room playerRoom, Player player)
        {
            string hpBar = "";
            int NumHpBars = 15;
            int maxHp = 100;
            int filledBars = (int)Math.Ceiling((player.HP / (double)maxHp) * NumHpBars);
            hpBar = new string('█', filledBars) + new string('░', NumHpBars - filledBars);

            Console.WriteLine("═════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"HP   {player.HP}/{maxHp} {hpBar}");
            Console.WriteLine($"$$   {player.Money}");
            Console.WriteLine("MAP");
            PrintMap(dungeon, playerRoom);
            Console.WriteLine("═════════════════════════════════════════════════════════════════════");
            Console.Write($"\n\nWhat will you do? (n/s/e/w, check, inventory):\n>");
        }

        public static void RefreshDungeonGame()
        {
            var dungeon = DungeonGameManager.CurrentDungeon;
            var current = DungeonGameManager.CurrentRoom;
            var player = DungeonGameManager.CurrentPlayer;

            if (dungeon == null || current == null || player == null)
            {
                Console.WriteLine("dungeon not initialized");
                return;
            }

            FullClear();
            current.OnRoomEntered(dungeon, current, player);
        }

    }
}
