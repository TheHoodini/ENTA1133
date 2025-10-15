using GD14_1133_A1_JuanDiego_DiceGame.Classes;
using GD14_1133_A1_JuanDiego_DiceGame.Dungeon;
using GD14_1133_A1_JuanDiego_DiceGame.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Combat
{
    public static class CombatGame
    {
        public static bool Start(Player player, int enemyID)
        {
            bool inCombat = true;
            TextPrinter textPrinter = new() { PrinterType = "1" };
            Random rng = new();
            var enemies = new List<Enemy> 
            {
                new("Steam Robot", "S. ROBOT", 30, 30, 3, ["1d10", "2d7"], 2, "eneRobot", "A Steam Robot comes from the shadows, its eyes still glow intensely."),
                new("Mechasaur", "MECHASAUR", 40, 40, 1, ["1d15", "2d10"], 5, "eneMecha", "A Mechasaur comes from the depths of the sewers!"),
                new("Evil Truck", "E. TRUCK", 100, 100, 1, ["1d20", "2d20"], 10, "eneTruck", "You hear a loud engine... It's an Evil Truck!"),
            };
            Enemy enemy = enemies[enemyID];
            Console.WriteLine(DungeonSprites.GetSprite(enemy.Sprite));
            textPrinter.Print(enemy.Description);
            int descLine = 2;

            while (inCombat)
            {
                Console.WriteLine("═════════════════════════════════════════════════════════════════════");
                Utilities.PrintHPBar(enemy.HPName, enemy.HP, enemy.MaxHP, 10);
                //Console.WriteLine("---------------------------------------------------------------------");
                Utilities.PrintHPBar("YOU", player.HP, 100, 10);
                Console.WriteLine("═════════════════════════════════════════════════════════════════════");
                foreach (var item in player.Inventory)
                {
                    if (item.Key is ItemCombat)
                    {
                        if (item.Key is ItemWeapon)
                            Console.WriteLine($"{item.Key.Name}({((ItemWeapon)item.Key).Damage} + {item.Key.EffectRoll} dmg) x{item.Value}");
                        else
                            Console.WriteLine($"{item.Key.Name}({item.Key.EffectRoll} heal) x{item.Value}");
                    }
                }
                Console.WriteLine("Fists(2d6 dmg)\n═════════════════════════════════════════════════════════════════════");
                bool isDeciding = true;
                int numCombatItems = player.Inventory
                    .Where(entry => entry.Key is ItemCombat)
                    .Count();

                string combatOption = "";
                Console.Write("\n\nWhat will you do? (item name, fists, run)\n>");
                while (isDeciding)
                {
                    string input = Console.ReadLine().ToLower().Trim();
                    if (input == "fists" || input == "f")
                    {
                        combatOption = "fists";
                        isDeciding = false;
                    }
                    else if (input == "run" || input == "r")
                    {
                        combatOption = "run";
                        isDeciding = false;
                    }
                    else if (player.Inventory.Any(entry => entry.Key.Name.ToLower() == input && entry.Key is ItemCombat))
                    {
                        combatOption = input;
                        isDeciding = false;
                    }
                    else
                    {
                        Utilities.InputText($"Invalid option '{input}'. Choose your fists, an item or run.", 4, "\nWhat will you do? (item name, fists, run)\n>");
                        continue;
                    }
                } // while (isDeciding)

                bool isPlayerTurn;
                if (enemy.Speed > 2)
                {
                    isPlayerTurn = false;
                }
                else
                {
                    isPlayerTurn = true;
                }

                // Take 2 turns (player and enemy)
                Utilities.ClearLines(9 + numCombatItems + descLine);
                for (int i = 0; i < 2; i++)
                {   // Player's turn
                    if (isPlayerTurn)
                    {
                        switch (combatOption)
                        { 
                            case "fists":
                                textPrinter.Print("You used your fists!");
                                int damage = CombatRoller.Attack(0, "2d6", true);
                                enemy.HP -= damage;
                                break;
                            case "run":
                                textPrinter.Print("You tried to run...");
                                if (rng.Next(0, 3) == 1)
                                {
                                    textPrinter.Print("You successfully ran away!");
                                    return true;
                                }
                                else
                                {
                                    textPrinter.Print("But you couldn't escape!");
                                }
                                break;
                            default:
                                Item itemToUse = ItemList.Get(combatOption);
                                if (itemToUse is ItemWeapon)
                                {
                                    enemy.HP -= (int)player.UseItem(combatOption, 1, enemy);
                                }
                                else
                                { 
                                    player.UseItem(combatOption, 1);
                                }
                                break;
                        }
                        if (enemy.HP <= 0)
                        {
                            Console.WriteLine();
                            textPrinter.Print($"You defeated the {enemy.Name}!");
                            inCombat = false;
                            var loot = ItemList.GetRandomItems(ItemList.ItemCategory.Any, (enemyID + 2) * 2);
                            player.AddItems(loot);
                            textPrinter.Print($"You found {Utilities.DescribeLoot(loot)} on the {enemy.Name}!");

                            Console.WriteLine("\nPress any key to continue.");
                            Console.ReadKey();
                            break;
                        }
                        isPlayerTurn = false;
                    }
                    // Enemy's turn
                    else
                    {
                        textPrinter.Print($"The {enemy.Name} attacked!");
                        string enemyAttack = enemy.RollAttacks[rng.Next(0, enemy.RollAttacks.Count)];
                        int enemyDamage = CombatRoller.Attack(enemy.AttackDamage, enemyAttack, false);
                        player.HP -= enemyDamage;
                        if (player.HP <= 0)
                        {
                            Console.WriteLine();
                            textPrinter.Print("You have been defeated...");
                            Console.WriteLine("\nPress any key to continue.");
                            Console.ReadKey();
                            return false;
                        }
                        isPlayerTurn = true;
                    }
                } // combat turns
                descLine = 5;
            }// while (inCombat)

            return false;
        }
    }
}
