using GD14_1133_A1_JuanDiego_DiceGame.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Scripts
{
    internal class DiceGameManager(Player player1)
    {
        TextPrinter textPrinter = new TextPrinter();
        Random rng = new Random();

        bool curseOfTheFrog = false;
        int angerLevel = 0;

        // ------------------- Start the game -------------------
        public void Start()
        {
            Play();
        }

        // ------------------- Start a game round -------------------
        public bool Play()
        {
            textPrinter.PrinterType = "1";
            var roller = new DieRoller();
            bool wantsToPlay = true;
            string summary = "";
            // Create a CPU player
            Player playerCpu = new Player("Mechasaur", false);
            playerCpu.AddDice(new List<string> { "d4", "d6", "d8", "d12", "d20" });

            // Decide turns
            textPrinter.Print($"\n[You start a battle against a {playerCpu.Name}]");
            textPrinter.Print("\n[Flipping a coin to decide who starts...]");
            int coinFlip = rng.Next(1, 3);
            bool isPlayerTurn;
            if (coinFlip == 1)
            {
                textPrinter.Print("You start!");
                isPlayerTurn = true;
            }
            else
            {
                textPrinter.Print($"The {playerCpu.Name} starts!");
                isPlayerTurn = false;
            }

            // Start the turns
            for (int i = 0; i < 2; i++)
            {
                textPrinter.Dialogue($"Round {i + 1}", "Start!");
                if (isPlayerTurn)
                {
                    // Player's turn
                    // Choose the die Dizarius will roll
                    textPrinter.Print($"\nPick the die the {playerCpu.Name} will roll: " + string.Join(", ", playerCpu.Dice));
                    Console.Write($"[{player1.Name.ToUpper()}] ");
                    string cpuDie = Console.ReadLine();
                    while (!playerCpu.Dice.Contains(cpuDie.ToLower()))
                    {
                        textPrinter.Print("Please type one of the options: " + string.Join(", ", playerCpu.Dice));
                        Console.Write($"[{player1.Name.ToUpper()}] ");
                        cpuDie = Console.ReadLine();
                    }

                    // Choose the die the player will roll
                    textPrinter.Print("Pick the die you will roll: " + string.Join(", ", player1.Dice));
                    Console.Write($"[{player1.Name.ToUpper()}] ");
                    string yourDie = Console.ReadLine();
                    while (!player1.Dice.Contains(yourDie.ToLower()))
                    {
                        textPrinter.Print("Please type one of the options: " + string.Join(", ", player1.Dice));
                        Console.Write($"[{player1.Name.ToUpper()}] ");
                        yourDie = Console.ReadLine();
                    }

                    Console.WriteLine("");
                    player1.UseDie(yourDie, textPrinter);
                    playerCpu.UseDie(cpuDie, textPrinter);
                    isPlayerTurn = false;

                    textPrinter.Print($"\n[The round ends with you having a score of {player1.Score} and {playerCpu.Name} \nwith a score of {playerCpu.Score}]");
                }
                else
                {
                    // CPU's turn
                    // Choose the die the player will roll
                    string[] playerOptions = player1.Dice.ToArray();
                    string cpuChosenDie = playerOptions[rng.Next(playerOptions.Length)];
                    textPrinter.Print($"\n[The {playerCpu.Name} has picked your die: {cpuChosenDie}]");

                    // Choose the die Dizarius will roll
                    string[] cpuOptions = playerCpu.Dice.ToArray();
                    string cpuDie = cpuOptions[rng.Next(cpuOptions.Length)];
                    textPrinter.Print($"[The {playerCpu.Name} has picked their own die: {cpuDie}]");

                    Console.WriteLine("");
                    player1.UseDie(cpuChosenDie, textPrinter);
                    playerCpu.UseDie(cpuDie, textPrinter);
                    isPlayerTurn = true;

                    textPrinter.Print($"\n[The round ends with you having a score of {player1.Score} and the {playerCpu.Name} \nwith a score of {playerCpu.Score}]");
                }
            }

            if (player1.Score > playerCpu.Score)
            {
                string[] newDice = ["d" + rng.Next(5, 16), "d" + rng.Next(15, 26)];
                summary += $"You won the duel! You got a {newDice[0]} and a {newDice[1]}\n";
                player1.AddDice(newDice);
            }
            else if (player1.Score < playerCpu.Score)
            {
                summary += $"The {playerCpu.Name} won the duel! You recieve 15 damage!\n";
                player1.TakeDamage(10);
            }
            else
            {
                summary += $"It's a tie! The {playerCpu.Name} still roams around the room!\n";
            }

            summary += player1.Summary;
            textPrinter.Dialogue("Stats Summary", summary);
            textPrinter.Print("Press any key to continue");
            Console.ReadKey();

            if (player1.Score > playerCpu.Score)
            {
                player1.Reset();
                return false;
            } 
            else
            {
                player1.Reset();
                return true;
            }

                
        }

        public void CurseOfTheFrog()
        {
            curseOfTheFrog = true;
            textPrinter.Dialogue("Dizarius", "You have tested my patience too long... By my hand, you now bear the curse of the frog!");
            textPrinter.Print("\n[You have received the curse of the frog. Your inventory \nwas filled with d7 dice]\n\nPress ENTER to continue");
            player1.ChangeName("Frog");
            Console.ReadLine();
        }

    }
}
