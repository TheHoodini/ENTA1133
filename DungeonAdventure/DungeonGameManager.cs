using GD14_1133_A1_JuanDiego_DiceGame.Dungeon;
using GD14_1133_A1_JuanDiego_DiceGame.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame
{
    internal class DungeonGameManager
    {
        Player player = new Player("Hero");

        public void StartMenu()
        {
            Utilities.FullClear();
            Console.WriteLine("Welcome to the Dungeon Game!");
            Console.WriteLine("Press Enter to start...");
            Console.ReadLine();
            StartGame();
        }

        public void StartGame()
        {
            Random rng = new Random();

            int rows = rng.Next(3, 6);
            int cols = rng.Next(3, 9);

            player.TakeDamage(67);
            var dungeon = DungeonMaker.GenerateDungeon(rows, cols);

            // random starting position
            int startRow = rng.Next(rows);
            int startCol = rng.Next(cols);
            Room currentRoom = dungeon[startRow, startCol];

            currentRoom.OnRoomEntered(dungeon, currentRoom, player);

            bool playing = true;

            // Enable for the console to display special characters
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (playing)
            {
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "north":
                    case "n":
                        if (currentRoom.North != null)
                        {
                            currentRoom.OnRoomExit();
                            currentRoom = currentRoom.North;
                            currentRoom.OnRoomEntered(dungeon, currentRoom, player);
                        }
                        else Utilities.OverwritePrompt("You can’t go north.");
                        break;

                    case "south":
                    case "s":
                        if (currentRoom.South != null)
                        {
                            currentRoom.OnRoomExit();
                            currentRoom = currentRoom.South;
                            currentRoom.OnRoomEntered(dungeon, currentRoom, player);
                        }
                        else Utilities.OverwritePrompt("You can’t go south.");
                        break;

                    case "east":
                    case "e":
                        if (currentRoom.East != null)
                        {
                            currentRoom.OnRoomExit();
                            currentRoom = currentRoom.East;
                            currentRoom.OnRoomEntered(dungeon, currentRoom, player);
                        }
                        else Utilities.OverwritePrompt("You can’t go east.");
                        break;

                    case "west":
                    case "w":
                        if (currentRoom.West != null)
                        {
                            currentRoom.OnRoomExit();
                            currentRoom = currentRoom.West;
                            currentRoom.OnRoomEntered(dungeon, currentRoom, player);
                        }
                        else Utilities.OverwritePrompt("You can’t go west.");
                        break;

                    case "inspect":
                    case "ins":
                        currentRoom.OnRoomSearched(player);
                        break;

                    case "quit":
                        Console.WriteLine("[Exiting dungeon...]");
                        playing = false;
                        break;

                    default:
                        Utilities.OverwritePrompt($"Unknown command '{input}'");
                        break;
                }
            }
        }


    }

}
