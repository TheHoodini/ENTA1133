using GD14_1133_A1_JuanDiego_DiceGame.Classes;
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
        private Player player = new Player("Engineer");
        private int DungeonRows;

        // Static global references for RefreshDungeonGame
        public static Room[,]? CurrentDungeon { get; private set; }
        public static Room? CurrentRoom { get; private set; }
        public static Player? CurrentPlayer { get; private set; }
        

        public void StartMenu()
        {
            Console.Write(DungeonSprites.GetSprite("uiTitle"));
            Console.ReadKey();
            StartGame();
        }

        public void StartGame()
        {
            // Only generate a new dungeon if it doesn’t exist already
            if (CurrentDungeon == null || CurrentPlayer == null)
            {
                Random rng = new Random();
                // Dungeon size
                int rows = rng.Next(3, 6);
                int cols = rng.Next(3, 9);

                // damage test
                //player.HP -= 10; 

                player.AddItems(new Dictionary<string, int>
                {
                    { "hammer", 3 },
                    { "screwdriver", 1 },
                    { "canteen", 2 },
                });  

                var dungeon = DungeonMaker.GenerateDungeon(rows, cols);

                // Random starting position
                int startRow = rng.Next(rows);
                int startCol = rng.Next(cols);
                Room startRoom = dungeon[startRow, startCol];

                // store location references
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

            while (playerRef.IsPlaying)
            {
                string input = Console.ReadLine()?.ToLower() ?? "";
                switch (input)
                {
                    case "north":
                    case "w":
                        MoveTo(currentRoom.North, ref currentRoom, dungeonRef, playerRef, "north");
                        break;

                    case "south":
                    case "s":
                        MoveTo(currentRoom.South, ref currentRoom, dungeonRef, playerRef, "south");
                        break;

                    case "east":
                    case "d":
                        MoveTo(currentRoom.East, ref currentRoom, dungeonRef, playerRef, "east");
                        break;

                    case "west":
                    case "a":
                        MoveTo(currentRoom.West, ref currentRoom, dungeonRef, playerRef, "west");
                        break;

                    case "check":
                    case "c":
                        currentRoom.OnRoomSearched(playerRef);
                        break;

                    case "inventory":
                    case "i":
                        playerRef.OpenInventory();
                        break;

                    case "quit":
                        Console.WriteLine("[Exiting dungeon...]");
                        playerRef.IsPlaying = false;
                        break;

                    default:
                        Utilities.InputText($"Unknown command '{input}'");
                        break;
                }
                if (playerRef.HP <= 0)
                {
                    playerRef.IsPlaying = false;
                    GameOver();
                }
            }
        }

        public void GameOver()
        {
            Utilities.FullClear();
            Console.WriteLine(DungeonSprites.GetSprite("uiGameOver"));
            Console.Write("                                          ");
            string wantToRestart = Console.ReadLine()?.ToLower() ?? "";

            while (wantToRestart != "y" && wantToRestart != "yes" && wantToRestart != "n" && wantToRestart != "no")
            {
                Utilities.ClearLines(1);
                Console.Write("                                          ");
                wantToRestart = Console.ReadLine()?.ToLower() ?? "";
            }

            if (wantToRestart == "y" || wantToRestart == "yes")
            {
                // Reset 
                CurrentDungeon = null;
                CurrentRoom = null;
                CurrentPlayer = null;
                player = new Player("Engineer");
                StartGame();
            }
            else
            {
                Utilities.ClearLines(1);
                Console.WriteLine("                                 Thanks for playing!");
            }
        }

        private void MoveTo(Room? targetRoom, ref Room currentRoom, Room[,] dungeon, Player player, string direction)
        {
            if (targetRoom != null)
            {
                string exitMsg = currentRoom.OnRoomExit();
                currentRoom = targetRoom;
                CurrentRoom = currentRoom; 
                currentRoom.OnRoomEntered(dungeon, currentRoom, player, exitMsg);
            }
            else
            {
                Utilities.InputText($"You can’t go {direction}.");
            }
        }
    }


}
