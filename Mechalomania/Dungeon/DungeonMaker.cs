using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Dungeon
{
    public class DungeonMaker
    {
        private static Random rng = new Random();

        public static Room[,] GenerateDungeon(int rows = 3, int cols = 3)
        {
            Room[,] dungeon = new Room[rows, cols];
            int index = 0;

            // ------------------- Room Probabilities -------------------
            var roomChances = new Dictionary<string, int>
            {
                { "e", 50 }, // Empty 
                { "t", 14 }, // Treasure 
                { "c", 10 },  // Combat 
                { "tt", 10 }, // Trap 
                { "f", 8 },   // Fountain
                { "l", 8 }   // Locked door
            };

            // Create rooms
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    string type = GetRandomRoom(roomChances);
                    Room r = type switch
                    {
                        "e" => new RoomEmpty(index, i, j),
                        "t" => new RoomTreasure(index, i, j),
                        "c" => new RoomCombat(index, i, j),
                        "tt" => new RoomTrap(index, i, j),
                        "f" => new RoomHealing(index, i, j),
                        "l" => new RoomLocked(index, i, j),
                        _ => new RoomEmpty(index, i, j)
                    };
                    dungeon[i, j] = r;
                    index++;
                }
            }

            // Link the rooms they have around
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Room current = dungeon[i, j];
                    if (i > 0) current.North = dungeon[i - 1, j];
                    if (i < rows - 1) current.South = dungeon[i + 1, j];
                    if (j > 0) current.West = dungeon[i, j - 1];
                    if (j < cols - 1) current.East = dungeon[i, j + 1];
                }
            }

            return dungeon;
        }

        // ---------------------- Get the room's probability to appear ----------------------
        private static string GetRandomRoom(Dictionary<string, int> roomChances)
        {
            int total = 0;
            foreach (var chance in roomChances.Values)
            { total += chance; }

            int roll = rng.Next(total);
            foreach (var key in roomChances)
            {
                if (roll < key.Value) return key.Key;
                roll -= key.Value;
            }

            // fallback (shouldn't happen if total > 0)
            foreach (var key in roomChances) return key.Key;
            return "e";
        }
    }


}
