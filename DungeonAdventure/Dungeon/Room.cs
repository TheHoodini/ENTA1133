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

        public virtual void OnRoomEntered(Room[,] dungeon, Room playerRoom, Player player)
        {
            // -------------------------------------------------------- Dungeon Game UI --------------------------------------------------------
            Utilities.FullClear();

            // Print room sprite
            Console.WriteLine(RoomSprites.GetSprite(this.Sprite));

            if (!Visited)
            {
                Console.WriteLine($"You enter room #{Index + 1}: {RoomDescription()}");
                Visited = true;
            }
            else
            {
                Console.WriteLine($"You return to room #{Index + 1}. {RoomDescription()}");
            }

            Utilities.PrintDungeonUI(dungeon, playerRoom, player);
        }

        public abstract string RoomDescription();
        public abstract void OnRoomSearched(Player player);

        public virtual void OnRoomExit()
        {
            Console.WriteLine($"You leave room {Index}...");
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
                : "It used to be a trasure here";
        }

        public override void OnRoomSearched(Player player)
        {
            string searchMessage;
            if (HasTreasure)
            {
                searchMessage = "You search the room and find a gem!";
                HasTreasure = false;
                Sprite = "spE";
            }
            else
            {
                searchMessage = "You already took the treasure. Nothing remains here.";
            }
            Utilities.OverwritePrompt(searchMessage);
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
            //Utilities.OverwritePrompt("Searching here feels dangerous. There are enemies around!");
            DiceGameManager diceGame = new(player);
            Utilities.FullClear();
            diceGame.Play();
        }

        public override string MapSymbol() => HasCombat ? "C" : " ";
    }


}
