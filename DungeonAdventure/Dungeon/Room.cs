using GD14_1133_A1_JuanDiego_DiceGame.Scripts;
using GD14_1133_A1_JuanDiego_DiceGame.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Dungeon
{
    public abstract class Room
    {
        public bool Visited { get; private set; }
        public int Index { get; private set; }
        public int Row { get; private set; }
        public int Col { get; private set; }

        public Room North { get; set; }
        public Room South { get; set; }
        public Room East { get; set; }
        public Room West { get; set; }

        public string Sprite { get; protected set; }

        public Room(int index, int row, int col, string sprite = "spE")
        {
            Index = index;
            Row = row;
            Col = col;
            Visited = false;
            Sprite = sprite;
        }

        public virtual void OnRoomEntered(Room[,] dungeon, Room playerRoom, Player player, string exitMsg = "")
        {
            // -------------------------------------------------------- Dungeon Game UI --------------------------------------------------------
            Utilities.FullClear();

            // Print room sprite
            Console.WriteLine(DungeonSprites.GetSprite(this.Sprite));

            if (!Visited)
            {
                Console.WriteLine($"{exitMsg}You enter room #{Index + 1}: {RoomDescription()}");
                Visited = true;
            }
            else
            {
                Console.WriteLine($"{exitMsg}You return to room #{Index + 1}. {RoomDescription()}");
            }

            Utilities.PrintDungeonUI(dungeon, playerRoom, player);
        }

        public abstract string RoomDescription();
        public abstract void OnRoomSearched(Player player);

        public virtual string OnRoomExit()
        {
            return $"You left room #{Index + 1}.\n";
        }

        public abstract string MapSymbol();

    }

    // ---------------------- Subclasses ----------------------
    public class RoomEmpty : Room
    {
        public RoomEmpty(int index, int row, int col) : base(index, row, col, "spE") { }

        public override string RoomDescription() => "An empty, quiet room.";

        public override void OnRoomSearched(Player player)
        {
            Utilities.OverwritePrompt("You search, but find nothing of interest.");
        }

        public override string MapSymbol() => " ";
    }

    public class RoomTreasure : Room
    {
        private bool HasTreasure = true;

        public RoomTreasure(int index, int row, int col) : base(index, row, col, "spT") { }

        public override string RoomDescription()
        {
            return HasTreasure
                ? "You found a treasure!"
                : "There used to be a treasure here";
        }

        public override void OnRoomSearched(Player player)
        {
            string searchMessage;
            int clearLines = HasTreasure ? 3 : 4;
            Random rng = new Random();
            if (HasTreasure)
            {
                int coinflip = rng.Next(0, 2);
                string die;
                if (coinflip == 0)
                {
                    die = "d" + rng.Next(20, 31);
                }
                else
                {
                    die = "d" + rng.Next(1, 21);
                }
                player.AddDice(new List<string> { die });
                searchMessage = $"You search the room and find a {die} die!";
                HasTreasure = false;
                Sprite = "spE";
                Utilities.FullClear();
                Utilities.RefreshDungeonGame();
            }
            else
            {
                searchMessage = "You already took the treasure. Nothing remains here.";
            }
            Utilities.OverwritePrompt(searchMessage, clearLines);
        }

        public override string MapSymbol() => HasTreasure ? "T" : " ";
    }

    public class RoomCombat : Room
    {
        private bool HasCombat = true;
        public RoomCombat(int index, int row, int col) : base(index, row, col, "spC") { }

        public override string RoomDescription() 
        {
            return HasCombat
                ? "It's a room with an enemy! Be careful!"
                : "There used to be an enemy but it was defeated.";
        }

        public override void OnRoomSearched(Player player)
        {
            if (HasCombat)
            {
                DiceGameManager diceGame = new(player);
                Utilities.FullClear();
                HasCombat = diceGame.Play();
                if (!HasCombat) {
                    Sprite = "spE";
                }
                Utilities.RefreshDungeonGame();
            }
            else
            {
                Utilities.OverwritePrompt("You already defeated the enemy here. The room is safe now.");
            }

        }

        public override string MapSymbol() => HasCombat ? "C" : " ";
    }

    public class RoomTrap : Room
    {
        private bool HasBeenSearched = false;

        public RoomTrap(int index, int row, int col) : base(index, row, col, "spT") { }

        public override string RoomDescription()
        {
            return HasBeenSearched
                ? "Be careful, something feels wrong here"
                : "You found a treasure!";
        }

        public override void OnRoomSearched(Player player)
        {
            HasBeenSearched = true;
            Utilities.FullClear();
            player.TakeDamage(5);
            Utilities.RefreshDungeonGame();
            Utilities.OverwritePrompt("You tried to search... But you found a trap! you received 5 damage", 3);
        }

        public override string MapSymbol() => "t";
    }


}
