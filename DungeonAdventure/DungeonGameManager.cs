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
        private Player player = new Player("Hero");

        // Static global references for RefreshDungeonGame
        public static Room[,]? CurrentDungeon { get; private set; }
        public static Room? CurrentRoom { get; private set; }
        public static Player? CurrentPlayer { get; private set; }
        int DungeonRows;

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
            // Only generate a new dungeon if it doesn’t exist already
            if (CurrentDungeon == null || CurrentPlayer == null)
            {
                Random rng = new Random();
                int rows = rng.Next(3, 6);
                int cols = rng.Next(3, 9);

                player.TakeDamage(67); // test

                player.AddDice(new List<string> { "d4", "d6", "d8", "d12", "d20" });
                var dungeon = DungeonMaker.GenerateDungeon(rows, cols);

                // Random starting position
                int startRow = rng.Next(rows);
                int startCol = rng.Next(cols);
                Room startRoom = dungeon[startRow, startCol];

                // Store 
                CurrentDungeon = dungeon;
                CurrentRoom = startRoom;
                CurrentPlayer = player;

                DungeonRows = rows;
            }

            // Continue game from last known position
            Room currentRoom = CurrentRoom!;
            Room[,] dungeonRef = CurrentDungeon!;
            Player playerRef = CurrentPlayer!;

            currentRoom.OnRoomEntered(dungeonRef, currentRoom, playerRef);

            bool playing = true;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (playing)
            {
                string input = Console.ReadLine()?.ToLower() ?? "";
                switch (input)
                {
                    case "north":
                    case "n":
                        MoveTo(currentRoom.North, ref currentRoom, dungeonRef, playerRef, "north");
                        break;

                    case "south":
                    case "s":
                        MoveTo(currentRoom.South, ref currentRoom, dungeonRef, playerRef, "south");
                        break;

                    case "east":
                    case "e":
                        MoveTo(currentRoom.East, ref currentRoom, dungeonRef, playerRef, "east");
                        break;

                    case "west":
                    case "w":
                        MoveTo(currentRoom.West, ref currentRoom, dungeonRef, playerRef, "west");
                        break;

                    case "inspect":
                    case "ins":
                        currentRoom.OnRoomSearched(playerRef);
                        break;

                    case "inventory":
                    case "inv":
                        playerRef.OpenInventory();
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

        private void MoveTo(Room? target, ref Room currentRoom, Room[,] dungeon, Player player, string direction)
        {
            if (target != null)
            {
                string exitMsg = currentRoom.OnRoomExit();
                currentRoom = target;
                CurrentRoom = currentRoom; 
                currentRoom.OnRoomEntered(dungeon, currentRoom, player, exitMsg);
            }
            else
            {
                Utilities.OverwritePrompt($"You can’t go {direction}.");
            }
        }
    }


}
