using GD14_1133_A1_JuanDiego_DiceGame.Classes;
using GD14_1133_A1_JuanDiego_DiceGame.Combat;
using GD14_1133_A1_JuanDiego_DiceGame.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static GD14_1133_A1_JuanDiego_DiceGame.Classes.ItemList;

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
                Console.WriteLine($"{exitMsg}You enter chamber #{Index + 1}{RoomDescription()}");
                Visited = true;
            }
            else
            {
                Console.WriteLine($"{exitMsg}You return to chamber #{Index + 1}{RoomDescription()}");
            }

            Utilities.PrintDungeonUI(dungeon, playerRoom, player);
        }

        public abstract string RoomDescription();
        public abstract void OnRoomSearched(Player player);

        public virtual string OnRoomExit()
        {
            return $"You left chamber #{Index + 1}.\n";
        }

        public abstract string MapSymbol();

    }

    // ---------------------- Subclasses ----------------------
    public class RoomEmpty : Room
    {
        public RoomEmpty(int index, int row, int col) : base(index, row, col, "roomE") { }

        public override string RoomDescription() => ". Just another empty hallway in the factory.";

        public override void OnRoomSearched(Player player)
        {
            Utilities.InputText("You search... And find nothing but the cold wall's bricks and tubes.");
        }

        public override string MapSymbol() => " ";
    }

    public class RoomTreasure : Room
    {
        private bool HasTreasure = true;
        private Random rng = new();
        private int treasureID;

        public RoomTreasure(int index, int row, int col) : base(index, row, col, "roomT") 
        { 
            treasureID = rng.Next(3);
        }

        public override string RoomDescription()
        {
            return HasTreasure
                ? " and you find a treasure!"
                : ", where there used to be a treasure";
        }

        public override void OnRoomSearched(Player player)
        {
            string searchMessage;
            int clearLines;
            if (HasTreasure)
            {
                switch (treasureID)
                {
                    case 0:
                        var loot = ItemList.GetRandomItems(ItemCategory.Loot, 2);
                        player.AddItems(loot);
                        searchMessage = $"You search... And find {Utilities.DescribeLoot(loot)}!";
                        break;
                    case 1:
                        searchMessage = "You search... And find 10 coins!";
                        player.Money += 10;
                        break;
                    default:
                        var loot2 = ItemList.GetRandomItems(ItemCategory.Combat, 2);
                        player.AddItems(loot2);
                        searchMessage = $"You search... And find {Utilities.DescribeLoot(loot2)}!";
                        break;
                }
                Sprite = "roomTE";
                HasTreasure = false;
                Utilities.RefreshDungeonGame();
                clearLines = 3;
            }
            else
            {
                searchMessage = "You already took the treasure. Nothing remains here.";
                clearLines = 4;
            }
            Utilities.InputText(searchMessage, clearLines);
        }

        public override string MapSymbol() => HasTreasure ? "T" : " ";
    }

    public class RoomCombat : Room
    {
        private bool HasCombat = true;
        private Random rng = new();
        private int enemyID;

        public RoomCombat(int index, int row, int col) : base(index, row, col, "roomC")
        {
            enemyID = rng.Next(3);
        }

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
                //DiceGameManager diceGame = new(player);
                Utilities.FullClear();
                HasCombat = CombatGame.Start(player, enemyID);
                if (!HasCombat) {
                    Sprite = "roomE";
                }
                Utilities.RefreshDungeonGame();
            }
            else
            {
                Utilities.InputText("You see the defeated body of the enemy on the floor.");
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
                : " and you find a treasure!";
        }

        public override void OnRoomSearched(Player player)
        {
            HasBeenSearched = true;
            Utilities.FullClear();
            player.HP -= 10;
            Utilities.RefreshDungeonGame();
            Utilities.InputText("You tried to search... But you found a trap! you received 10 damage.", 3);
        }

        public override string MapSymbol() => "T";
    }

    public class RoomHealing : Room
    {
        private bool HasHealing = true;

        public RoomHealing(int index, int row, int col) : base(index, row, col, "roomF") { }

        public override string RoomDescription()
        {
            return HasHealing
                ? " and find a healing fountain that still works."
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
                    Utilities.InputText(searchMessage, clearLines);
                    return;
                }
                clearLines = 3;
                searchMessage = $"You drink the water and get healed by 20!";
                player.HP += 20;
                HasHealing = false;
                Sprite = "roomFE";
                Utilities.FullClear();
                Utilities.RefreshDungeonGame();
            }
            else
            {
                searchMessage = "The fountain doesn't seem to have more purified water.";
            }
            Utilities.InputText(searchMessage, clearLines);
        }

        public override string MapSymbol() => HasHealing ? "F" : " ";
    }

    public class RoomLocked : Room
    {
        private bool IsLocked = true;

        public RoomLocked(int index, int row, int col) : base(index, row, col, "roomL") { }

        public override string RoomDescription()
        {
            return IsLocked
                ? " and find a door made of a heavy metal."
                : ". The door remains open due its weight.";
        }

        public override void OnRoomSearched(Player player)
        {
            string searchMessage;
            int clearLines = 4;
            if (IsLocked)
            {
                if (!player.HasItem("rusted key"))
                {
                    searchMessage = "You need a key to open this door.";
                    Utilities.InputText(searchMessage, clearLines);
                    return;
                }
                clearLines = 3;
                searchMessage = $"You used the rusted key. Behind the door you find 30 coins!";
                player.UseItem("rusted key");
                player.Money += 30;
                IsLocked = false;
                Sprite = "roomLO";
                Utilities.FullClear();
                Utilities.RefreshDungeonGame();
            }
            else
            {
                searchMessage = "There is nothing more behind the door.";
            }
            Utilities.InputText(searchMessage, clearLines);

        }

        public override string MapSymbol() => IsLocked ? "L" : " ";
    }




}
