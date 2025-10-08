using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Scripts
{
    internal class GameManager
    {
        TextPrinter textPrinter = new TextPrinter();
        Random random = new Random();

        string playerName;
        PlayerClass player1;

        bool curseOfTheFrog = false;
        int angerLevel = 0;

        // ------------------- Start the game -------------------
        public void Start()
        {
            Welcome();
            Play();
        }
        public void Welcome()
        {
            // Display the game title
            Console.WriteLine("              _                            _                 \n" +
                              "             | |                          | |                \n" +
                              "__      _____| | ___ ___  _ __ ___   ___  | |_ ___          \n" +
                              "\\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ | __/ _ \\         \n" +
                              " \\ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) |  _ _ _ \n" +
                              "  \\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|  \\__\\___/  (_|_|_)\n");
            Console.WriteLine(" __       __                        __      __                  _______             __  __ \n" +
                                "/  \\     /  |                      /  |    /  |                /       \\           /  |/  |\n" +
                                "$$  \\   /$$ | __    __   _______  _$$ |_   $$/   _______       $$$$$$$  |  ______  $$ |$$ |\n" +
                                "$$$  \\ /$$$ |/  |  /  | /       |/ $$   |  /  | /       |      $$ |__$$ | /      \\ $$ |$$ |\n" +
                                "$$$$  /$$$$ |$$ |  $$ |/$$$$$$$/ $$$$$$/   $$ |/$$$$$$$/       $$    $$< /$$$$$$  |$$ |$$ |\n" +
                                "$$ $$ $$/$$ |$$ |  $$ |$$      \\   $$ | __ $$ |$$ |            $$$$$$$  |$$ |  $$ |$$ |$$ |\n" +
                                "$$ |$$$/ $$ |$$ \\__$$ | $$$$$$  |  $$ |/  |$$ |$$ \\_____       $$ |  $$ |$$ \\__$$ |$$ |$$ |\n" +
                                "$$ | $/  $$ |$$    $$ |/     $$/   $$  $$/ $$ |$$       |      $$ |  $$ |$$    $$/ $$ |$$ |\n" +
                                "$$/      $$/  $$$$$$$ |$$$$$$$/     $$$$/  $$/  $$$$$$$/       $$/   $$/  $$$$$$/  $$/ $$/ \n" +
                                "             /  \\__$$ |                                                                    \n" +
                                "             $$    $$/                                                                    \n" +
                                "              $$$$$$/                                                                     \n\n");
            // Welcome the player
            Console.WriteLine("                                     Press any key to start\n");
            Console.ReadKey();
            textPrinter.Dialogue("Dizarius", "Welcome brave adventurer, I'm Dizarius the dice wizard. " +
                "Before we start our recreation, please tell me the type of text printer you desire (type the number):\n1. Typewriter Effect\n2. Regular Print", true);

            // Ask for the type of text printer
            Console.Write("[YOU] ");
            textPrinter.PrinterType = Console.ReadLine();
            while (textPrinter.PrinterType != "1" && textPrinter.PrinterType != "2")
            {
                // Increase anger level if the input is invalid
                if (angerLevel == 0)
                {
                    textPrinter.Dialogue("Dizarius", "Take this seriously, answer 1 if you want a Typewriter Effect or 2 for a normal print");
                }
                else if (angerLevel == 1)
                {
                    textPrinter.Dialogue("Dizarius", "Are you paying attention, young person? Just type 1 or 2 for your typing effect");
                }
                else if (angerLevel == 2)
                {
                    textPrinter.Dialogue("Dizarius", "...You are trying my patience, last chance to answer");
                }
                else
                {
                    CurseOfTheFrog();
                    break;
                }
                angerLevel++;
                Console.Write("[YOU] ");
                textPrinter.PrinterType = Console.ReadLine();
            }


            // Ask for the player's name and type of text printer to start the game
            if (!curseOfTheFrog)
            {
                textPrinter.Dialogue("Dizarius", "Great to know. Now, what is your name?", true);
                do
                {
                    Console.Write("[YOU] ");
                    playerName = Console.ReadLine();

                    // If the input is invalid, increase the anger level
                    if (string.IsNullOrWhiteSpace(playerName))
                    {
                        if (angerLevel == 0)
                        {
                            textPrinter.Dialogue("Dizarius", "Please talk young person, what is your name?");
                        }
                        else if (angerLevel == 1)
                        {
                            textPrinter.Dialogue("Dizarius", "Surely, you must have a name, don't be shy and tell me what it is.");
                        }
                        else if (angerLevel == 2)
                        {
                            textPrinter.Dialogue("Dizarius", "This is getting ridiculous, I need your name to proceed!");
                        }
                        else
                        {
                            CurseOfTheFrog();
                            break;
                        }
                        angerLevel++;
                    }

                } while (string.IsNullOrWhiteSpace(playerName));

                if (!curseOfTheFrog)
                {
                    player1 = new PlayerClass(playerName); // Create a player object with the name entered
                }
            }

            string rulesIntro = "";
            if (curseOfTheFrog)
            {
                rulesIntro += "Just because I'm not a completely evil wizard I'll explain the rules:\n";
            } else
            {
                rulesIntro += "\nOkay " + player1.Name + ", these are the game rules:\n";
            }
            // Print rules
            textPrinter.Dialogue("Dizarius", rulesIntro +
                    "1. First I flip a coin to decide the turn order, the game lasts 3 rounds\n" +
                    "2. The first player picks the die they will roll and the die their oponent will roll\n" +
                    "3. The next turn the other player picks their own roll and the die their oponent will roll\n" +
                    "4. The player with the highest score wins\n", true);

            textPrinter.Print("\n[Are you ready to start?]\n> Yes\n> No\n");

            // Ask if the player is ready to start
            bool isReady = false;
            do
            {
                Console.Write($"[{player1.Name.ToUpper()}] ");
                string readyOption = Console.ReadLine().ToLower();

                if (readyOption == "y" || readyOption == "yes")
                {
                    isReady = true;
                }
                else if (readyOption == "n" || readyOption == "no")
                {
                    textPrinter.Dialogue("Dizarius", "Patience is important, let me know when you're ready.");
                    textPrinter.Print("\n[Are you ready now?]\n> Yes\n> No\n");
                }
                else
                {
                    // Increase anger level if the input is invalid
                    if (!curseOfTheFrog)
                    {
                        string message = string.Empty;
                        if (angerLevel == 0)
                        {
                            textPrinter.Dialogue("Dizarius", $"Take this seriously {player1.Name}, answer if you're ready or not");
                        }
                        else if (angerLevel == 1)
                        {
                            textPrinter.Dialogue("Dizarius", $"Are you paying attention, {player1.Name}? Just answer yes or no!");
                        }
                        else if (angerLevel == 2)
                        {
                            textPrinter.Dialogue("Dizarius", "This is getting ridiculous, I need a proper answer to proceed!");
                        }
                        else
                        {
                            CurseOfTheFrog();
                            break;
                        }
                        angerLevel++;
                    }
                    else
                    {
                        textPrinter.Dialogue("Dizarius", "...");
                    }
                }
            } while (!isReady);


            // Greet the player 
            if (curseOfTheFrog) {
                textPrinter.Dialogue("Dizarius", "Very well, frog... Let us see how you fare in Mystic Roll...", true);
                player1.AddDice(new List<string> { "d7", "d7", "d7", "d7", "d7" });
            }
            else
            {
                textPrinter.Dialogue("Dizarius", $"Excellent {player1.Name}! Let's begin the Mystic Roll!", true);
                player1.AddDice(new List<string> { "d4", "d6", "d8", "d12", "d20" });
                textPrinter.Print($"\n[Your inventory was filled with a " + string.Join(", ", player1.Dice) + "]");
            }
        }

        // ------------------- Start a game round -------------------
        public void Play()
        {
            var roller = new DieRoller();
            bool wantsToPlay = true;
            int playerRoll;
            int cpuRoll;
            string tieMessage ="";
            do 
            {
                string summary = "";
                // Create a CPU player
                PlayerClass playerCpu = new PlayerClass("Dizarius", false);
                playerCpu.AddDice(new List<string> { "d4", "d6", "d8", "d12", "d20" });

                // Decide turns
                textPrinter.Print("\n[Dizarius flips a coin to decide who starts...]");
                int coinFlip = random.Next(1, 3);
                bool isPlayerTurn;
                if (coinFlip == 1)
                {
                    textPrinter.Print("You start!");
                    isPlayerTurn = true;
                }
                else
                {
                    textPrinter.Print("Dizarius starts!");
                    textPrinter.Dialogue("Dizarius", "It seems that luck is on my side again!");
                    isPlayerTurn = false;
                }

                // Start the turns
                for (int i = 0; i < 3; i++)
                {
                    textPrinter.Dialogue($"Round {i + 1}", "Start!");
                    if (isPlayerTurn)
                    {
                        // Player's turn
                        // Choose the die Dizarius will roll
                        textPrinter.Print("\nPick the die Dizarius will roll: " + string.Join(", ", playerCpu.Dice));
                        Console.Write($"[{player1.Name.ToUpper()}] ");
                        string cpuDie = Console.ReadLine().ToLower();
                        while (!playerCpu.Dice.Contains(cpuDie))
                        {
                            textPrinter.Print("Please type one of the options: " + string.Join(", ", playerCpu.Dice));
                            Console.Write($"[{player1.Name.ToUpper()}] ");
                            cpuDie = Console.ReadLine().ToLower();
                        }

                        // Choose the die the player will roll
                        textPrinter.Print("Pick the die you will roll: " + string.Join(", ", player1.Dice));
                        Console.Write($"[{player1.Name.ToUpper()}] ");
                        string yourDie = Console.ReadLine().ToLower();
                        while (!player1.Dice.Contains(yourDie))
                        {
                            textPrinter.Print("Please type one of the options: " + string.Join(", ", player1.Dice));
                            Console.Write($"[{player1.Name.ToUpper()}] ");
                            yourDie = Console.ReadLine().ToLower();
                        }

                        Console.WriteLine("");
                        playerRoll = player1.UseDie(yourDie, textPrinter);
                        cpuRoll = playerCpu.UseDie(cpuDie, textPrinter);
                        isPlayerTurn = false;
                    }
                    else
                    {
                        // CPU's turn
                        // Choose the die the player will roll
                        string[] playerOptions = player1.Dice.ToArray();
                        string cpuChosenDie = playerOptions[random.Next(playerOptions.Length)];
                        textPrinter.Print($"\n[Dizarius has picked your die: {cpuChosenDie}]");

                        // Choose the die Dizarius will roll
                        string[] cpuOptions = playerCpu.Dice.ToArray();
                        string cpuDie = cpuOptions[random.Next(cpuOptions.Length)];
                        textPrinter.Print($"[Dizarius has picked his own die: {cpuDie}]");

                        Console.WriteLine("");
                        cpuRoll = playerCpu.UseDie(cpuDie, textPrinter);
                        playerRoll = player1.UseDie(cpuChosenDie, textPrinter);
                        isPlayerTurn = true;
                    }
                    if (playerRoll > cpuRoll)
                    {
                        player1.addScore(1);
                    }
                    else if (playerRoll < cpuRoll)
                    {
                        playerCpu.addScore(1);
                    }
                    else
                    {
                        tieMessage = "It's a tie, nobody gets points!\n";
                    }
                    textPrinter.Print($"\n{tieMessage}[The round ends with you having a score of {player1.Score} and Dizarius \nwith a score of {playerCpu.Score}]");
                    textPrinter.Print("\n[Press any key to continue]");
                    Console.ReadKey();
                    tieMessage = "";
                }

                if (player1.Score > playerCpu.Score)
                {
                    summary += $"Congratulations {player1.Name}, you won this round!\n";
                }
                else if (player1.Score < playerCpu.Score)
                {
                    summary += "Dizarius won this round!\n";
                }
                else
                {
                    summary += "It's a tie!\n";
                }

                summary += player1.Summary;
                // Print the summary of the round
                textPrinter.Dialogue("Stats Summary", summary, true);

                // Ask if the player wants to play again
                textPrinter.Dialogue("Dizarius", $"That was wonderful! An amazing duel! Do you wish to play again, {player1.Name}?");
                textPrinter.Print("\n> Yes\n> No\n");
                string playAgainOption;
                do 
                {
                    Console.Write($"[{player1.Name.ToUpper()}] ");
                    playAgainOption = Console.ReadLine().ToLower();
                    if (playAgainOption == "y" || playAgainOption == "yes")
                    {
                        // Reset scores and dice for a new round
                        player1.Reset();
                        playerCpu.Reset();
                        if (curseOfTheFrog)
                        {
                            player1.AddDice(new List<string> { "d7", "d7", "d7", "d7", "d7" });
                        }
                        else
                        {
                            player1.AddDice(new List<string> { "d4", "d6", "d8", "d12", "d20" });
                        }
                        playerCpu.AddDice(new List<string> { "d4", "d6", "d8", "d12", "d20" });
                    }
                    else if (playAgainOption == "n" || playAgainOption == "no")
                    {
                        wantsToPlay = false;
                        textPrinter.Dialogue("Dizarius", "It was a pleasure playing with you, until we meet again!");
                    }
                    else
                    {
                        // Increase anger level if the input is invalid
                        if (!curseOfTheFrog)
                        {
                            if (angerLevel == 0)
                            {
                                textPrinter.Dialogue("Dizarius", $"Take this seriously {player1.Name}, tell me if you want to try again or not");
                            }
                            else if (angerLevel == 1)
                            {
                                textPrinter.Dialogue("Dizarius", $"Are you paying attention, {player1.Name}? Just type yes or no to answer");
                            }
                            else if (angerLevel == 2)
                            {
                                textPrinter.Dialogue("Dizarius", "This is getting ridiculous, I need a proper answer to proceed!");
                            }
                            else
                            {
                                CurseOfTheFrog();
                                player1.Reset();
                                playerCpu.Reset();
                                playerCpu.AddDice(new List<string> { "d4", "d6", "d8", "d12", "d20" });
                                player1.AddDice(new List<string> { "d7", "d7", "d7", "d7", "d7" });
                                break;
                            }
                            angerLevel++;
                        }
                        else
                        {
                            textPrinter.Dialogue("Dizarius", "...");
                        }
                    }
                } while (playAgainOption != "yes" && playAgainOption != "y" && playAgainOption != "no" && playAgainOption != "n");
                
            } while (wantsToPlay);
            
        }

        public void CurseOfTheFrog()
        {
            curseOfTheFrog = true;
            textPrinter.Dialogue("Dizarius", "You have tested my patience too long... By my hand, you now bear the curse of the frog!");
            textPrinter.Print("\n[You have received the curse of the frog. Your inventory \nwas filled with d7 dice]\n\nPress any key to continue");
            player1 = new PlayerClass("Frog");
            Console.ReadKey();
        }

    }
}
