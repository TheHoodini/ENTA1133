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
        private Dictionary<string, int> inventory = new();
        private string summary = "";
        private int hp = 100;
        private int money = 0;

        private readonly DieRoller dieRoller = new();
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
        public string Name => name;
        public int Score => score;
        public string Summary => summary;
        internal List<string> Dice => new List<string>(dice);
        internal Dictionary<string, int> Inventory => new Dictionary<string, int>(inventory);

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

        internal void AddToInventory(string item, int quantity = 1)
        {
            if (inventory.ContainsKey(item))
            {
                inventory[item] += quantity;
            }
            else
            {
                inventory[item] = quantity;
            }
        }

        internal void UseItem(string item, int quantity = 1)
        {
            inventory[item] -= quantity;
            if (inventory[item] <= 0)
            {
                inventory.Remove(item);
            }
        }

        internal void OpenInventory()
        {
            Utilities.FullClear();
            Console.WriteLine(DungeonSprites.GetSprite("uiInv"));
            Console.WriteLine("                            INVENTORY");
            Console.WriteLine("═════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"Name: The {name}");
            Console.WriteLine($"HP: {hp}/100");
            Console.WriteLine($"Coins: ${money}\n");
            Console.WriteLine("Dice: " + (dice.Count > 0 ? string.Join(", ", dice) : "None"));
            if (inventory.Count > 0)
            {
                Console.WriteLine("Items:");
                foreach (var item in inventory)
                {
                    Console.WriteLine($"- {item.Key} x{item.Value}");
                }
            }
            Console.WriteLine("═════════════════════════════════════════════════════════════════════");
            Console.Write($"\n\nWhat will you do? (close):\n>");
            string input = Console.ReadLine()?.ToLower() ?? "";
            while (input != "close" && input != "c")
            {
                Utilities.InputText($"Invalid command '{input}'. Type 'close' to exit inventory.", question: "\nWhat will you do? (close):\n>");
                input = Console.ReadLine()?.ToLower() ?? "";
            }
            Utilities.RefreshDungeonGame();
        }

        internal int UseDie(string die, TextPrinter printer)
        {
            // Roll the die
            int roll = dieRoller.Roll(die, printer, isPlayer);
            pastRolls.Add(roll);
            diceUsedHistory.Add(die);
            //addScore(roll);
            dice.Remove(die);

            int total = pastRolls.Sum();
            int highest = pastRolls.Max();
            int evens = pastRolls.Count(r => r % 2 == 0);
            int odds = pastRolls.Count - evens;

            // Calculate expected average total
            int expectedTotal = 0;
            foreach (var usedDie in pastRolls.Zip(diceUsedHistory, (rollValue, dieType) => dieType))
            {
                if (int.TryParse(usedDie[1..], out int sides))
                {
                    expectedTotal += (int)Math.Round((sides + 1) / 2.0);
                }
            }

            // Comment based on total vs expected
            string totalComment;
            if (total < expectedTotal)
            {
                totalComment = "That was the best you got?";
            }
            else if (total > expectedTotal)
            {
                totalComment = "You're truly a dice master!";
            }
            else
            {
                totalComment = "Not bad!";
            }

            summary = $"You rolled {pastRolls.Count} dice in total.\n" +
                      $"Your total score was {total}. {totalComment}\n" +
                      $"Your highest roll was {highest}.\n" +
                      $"You had {evens} even rolls and {odds} odd rolls.";

            return roll;
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
