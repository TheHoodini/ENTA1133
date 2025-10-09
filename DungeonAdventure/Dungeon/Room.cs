using GD14_1133_A1_JuanDiego_DiceGame.Classes;
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

        public Room(int index, int row, int col, string sprite = "roomE")
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
                Console.WriteLine($"{exitMsg}You enter room #{Index + 1}{RoomDescription()}");
                Visited = true;
            }
            else
            {
                Console.WriteLine($"{exitMsg}You return to room #{Index + 1}{RoomDescription()}");
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
        public RoomEmpty(int index, int row, int col) : base(index, row, col, "roomE") { }

        public override string RoomDescription() => " and see an empty, quiet hallway.";

        public override void OnRoomSearched(Player player)
        {
            Utilities.OverwritePrompt("You search, but find nothing of interest.");
        }

        public override string MapSymbol() => " ";
    }

    public class RoomTreasure : Room
    {
        private bool HasTreasure = true;

        public RoomTreasure(int index, int row, int col) : base(index, row, col, "roomT") { }

        public override string RoomDescription()
        {
            return HasTreasure
                ? " and you find a treasure!"
                : ", where there used to be a treasure";
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
                Sprite = "roomTE";
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
        public RoomCombat(int index, int row, int col) : base(index, row, col, "roomC") { }

        public override string RoomDescription() 
        {
            return HasCombat
                ? " and you find an enemy, be careful!"
                : ". It feels safe now.";
        }

        public override void OnRoomSearched(Player player)
        {
            if (HasCombat)
            {
                DiceGameManager diceGame = new(player);
                Utilities.FullClear();
                HasCombat = diceGame.Play();
                if (!HasCombat) {
                    Sprite = "roomE";
                }
                Utilities.RefreshDungeonGame();
            }
            else
            {
                Utilities.OverwritePrompt("You see the defeated body of the enemy on the floor.");
            }

        }

        public override string MapSymbol() => HasCombat ? "C" : " ";
    }

    public class RoomTrap : Room
    {
        private bool HasBeenSearched = false;

        public RoomTrap(int index, int row, int col) : base(index, row, col, "roomT") { }

        public override string RoomDescription()
        {
            return HasBeenSearched
                ? ". Be careful, something feels wrong here."
                : "and you find a treasure!";
        }

        public override void OnRoomSearched(Player player)
        {
            HasBeenSearched = true;
            Utilities.FullClear();
            player.TakeDamage(10);
            Utilities.RefreshDungeonGame();
            Utilities.OverwritePrompt("You tried to search... But you found a trap! you received 10 damage.", 3);
        }

        public override string MapSymbol() => "t";
    }

    public class RoomHealing : Room
    {
        private bool HasHealing = true;

        public RoomHealing(int index, int row, int col) : base(index, row, col, "roomF") { }

        public override string RoomDescription()
        {
            return HasHealing
                ? " and you find a healing fountain!"
                : ", where the fountain healed you.";
        }

        public override void OnRoomSearched(Player player)
        {
            string searchMessage;
            int clearLines = 4;
            if (HasHealing)
            {
                if (player.HP == 100)
                {
                    searchMessage = "You are already at full health, no need to heal.";
                    Utilities.OverwritePrompt(searchMessage, clearLines);
                    return;
                }
                clearLines = 3;
                searchMessage = $"You drink the water and get healed by 20!";
                player.Heal(20);
                HasHealing = false;
                Sprite = "roomFE";
                Utilities.FullClear();
                Utilities.RefreshDungeonGame();
            }
            else
            {
                searchMessage = "You already drank from the fountain.";
            }
            Utilities.OverwritePrompt(searchMessage, clearLines);
        }

        public override string MapSymbol() => HasHealing ? "F" : " ";
    }


}
