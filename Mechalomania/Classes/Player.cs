using GD14_1133_A1_JuanDiego_DiceGame.Dungeon;
using GD14_1133_A1_JuanDiego_DiceGame.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Classes
{
    public class Player(string playerName, bool isPlayer = true)
    {
        // Player attributes
        private string name = playerName;
        private int score = 0;
        private List<string> dice = new();
        private Dictionary<Item, int> inventory = new();
        private string summary = "";
        private int hp = 100;
        private int money = 0;
        private bool isPlaying = true;

        private List<int> pastRolls = new(); // Track all roll results
        private List<string> diceUsedHistory = new(); // Track the types of dice used

        public int HP
        {
            get { return hp; }
            set { hp = Math.Clamp(value, 0, 100); }
        }
        public int Money
        {
            get { return money; }
            set { money = Math.Max(0, value); } 
        }
        public bool IsPlaying
        {
            get { return isPlaying; }
            set { isPlaying = value; }
        }
        public string Name => name;
        public int Score => score;
        public string Summary => summary;
        internal List<string> Dice => new List<string>(dice);
        internal Dictionary<Item, int> Inventory => new Dictionary<Item, int>(inventory);

        internal void ChangeName(string newName)
        {
            name = newName;
        }

        internal void addScore(int points)
        {
            score += points;
        }

        internal void AddDice(IEnumerable<string> diceToAdd)
        {
            dice.AddRange(diceToAdd);
        }

        internal void AddItem(string itemName, int amount = 1)
        {
            Item item = ItemList.Get(itemName);

            if (inventory.ContainsKey(item))
            {
                inventory[item] += amount;
            }
            else
            {
                inventory[item] = amount;
            }
        }

        internal void AddItems(Dictionary<string, int> items)
        {
            foreach (var itemElement in items)
            {
                AddItem(itemElement.Key, itemElement.Value);
            }
        }

        private Enemy voidEnemy = new ("Void", "VOID", 1, 1, 1, ["1d1"], 5, "", "");
        internal int? UseItem(string itemName, int amount = 1, Enemy? enemy = null)
        {
            if (enemy == null) enemy = voidEnemy;
            Item item = ItemList.Get(itemName);

            int? result = null;

            if (item is ItemConsumable consumable)
            {
                consumable.Use(this); // void return
            }
            else if (item is ItemWeapon weapon)
            {
                result = weapon.Use(this, enemy); // weapon damage
            }

            inventory[item] -= amount;
            if (inventory[item] <= 0)
            {
                inventory.Remove(item);
            }

            return result;
        }

        public bool HasItem(string itemName, int requiredAmount = 1)
        {
            Item item = ItemList.Get(itemName);

            return inventory.TryGetValue(item, out int amount) && amount >= requiredAmount;
        }

        internal void PrintInventory()
        {
            Console.WriteLine(DungeonSprites.GetSprite("uiInv"));
            Console.WriteLine("                            INVENTORY");
            Console.WriteLine("═════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"Name: The {name}");
            Console.WriteLine($"HP: {hp}/100");
            Console.WriteLine($"Coins: ${money}\n");
            Console.WriteLine("Items:");
            if (inventory.Count > 0)
            {
                foreach (var item in inventory)
                {
                    Console.WriteLine($"- {item.Key.Name} x{item.Value}");
                }
            }
            else
            {
                Console.WriteLine("- None.");
            }
            Console.WriteLine("═════════════════════════════════════════════════════════════════════");
        }
        internal void OpenInventory()
        {
            bool openInventory = true;
            string input;
            bool showingInfo = false;
            Utilities.FullClear();
            PrintInventory();
            Console.Write($"\n\nWhat will you do? (use, info, close):\n>");
            while (openInventory) 
            {
                input = Console.ReadLine()?.ToLower() ?? "";

                if (string.IsNullOrWhiteSpace(input))
                {
                    Utilities.InputText("Please enter a command.", question: "\nWhat will you do? (use, info, close):\n>");
                    continue;
                }

                string[] commandParts = input.Split(' ', 2); 
                string command = commandParts[0].ToLower();

                switch (command) 
                { 
                    case "use":
                    case "u":
                        // No item specified
                        if (commandParts.Length < 2 || string.IsNullOrWhiteSpace(commandParts[1]))
                        {
                            Utilities.InputText("No item specified. Type 'use <item name>'", question: "\nWhat will you do? (use, info, close):\n>");
                            continue;
                        }
                        // Check if item is in inventory
                        if (HasItem(commandParts[1].Trim().ToLower()))
                        {
                            // Check if item is consumable
                            Item itemToUse = ItemList.Get(commandParts[1].Trim().ToLower());
                            if (itemToUse is not ItemConsumable consumable)
                            {
                                Utilities.InputText($"You can't use that item here", question: "\nWhat will you do? (use, info, close):\n>");
                                continue;
                            }
                            
                            if (hp == 100)
                            {
                                Utilities.InputText("You are already at full health!", question: "\nWhat will you do? (use, info, close):\n>");
                                continue;
                            }
                            // Use the item
                            if (showingInfo)
                                Utilities.ClearLines(7);
                            else
                                Utilities.ClearLines(4);
                            //consumable.Use(this);
                            UseItem(itemToUse.Name.ToLower());
                            Console.WriteLine("═════════════════════════════════════════════════════════════════════");
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            Utilities.FullClear();
                            PrintInventory();
                            showingInfo = false;
                            Console.Write($"\n\nWhat will you do? (use, info, close):\n>");
                        }
                        // Does not have the item
                        else
                        {
                            Utilities.InputText($"The item '{commandParts[1].Trim()}' is not in your inventory", question: "\nWhat will you do? (use, info, close):\n>");
                        }
                        break;

                    case "info":
                    case "i":
                        // No item specified
                        if (commandParts.Length < 2 || string.IsNullOrWhiteSpace(commandParts[1]))
                        {
                            Utilities.InputText("No item specified. Type 'info <item name>'.", question: "\nWhat will you do? (use, info, close):\n>");
                            continue;
                        }
                        // Show item info
                        if (HasItem(commandParts[1].Trim().ToLower()))
                        {
                            if (showingInfo)
                                Utilities.ClearLines(7);
                            else
                                Utilities.ClearLines(4);
                            Item infoItem = ItemList.Get(commandParts[1].Trim().ToLower());
                            infoItem.Info();
                            showingInfo = true;
                            Console.WriteLine("═════════════════════════════════════════════════════════════════════");
                            Console.Write($"\n\nWhat will you do? (use, info, close):\n>");
                        }
                        // Does not have the item
                        else
                        {
                            Utilities.InputText($"The item '{commandParts[1].Trim()}' is not in your inventory", question: "\nWhat will you do? (use, info, close):\n>");
                        }
                        break;
                    
                    case "close":
                    case "c":
                        openInventory = false;
                        break;

                    default:
                        Utilities.InputText($"Invalid command '{input}'.", question: "\nWhat will you do? (close):\n>");
                        break;
                }
            }
            Utilities.RefreshDungeonGame();
        }

        internal void Reset()
        {
            score = 0;
            summary = "";
            pastRolls.Clear();
            diceUsedHistory.Clear();
        }
    }

}
