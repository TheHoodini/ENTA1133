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
            Console.WriteLine("                                     Press ENTER to start\n");
            Console.ReadLine();
            textPrinter.Dialogue("Dizarius", "Welcome young person, I'm Dizarius the dice wizard. " +
                "Please tell me the type of text printer you desire (type the number):\n1. Typewriter Effect\n2. Regular Print", true);

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
                    "1. First we flip a coin to decide the turn order\n" +
                    "2. The first player picks the die the other player will roll and then the die they will roll\n" +
                    "3. The second one picks the die of the first player and then their own die\n" +
                    "4. The player with the highest score wins\n", true);

            textPrinter.Print("\n[Are you ready to start?]\n1. Yes\n2. No\n");

            // Ask if the player is ready to start
            bool isReady = false;
            do
            {
                Console.Write($"[{player1.Name.ToUpper()}] ");
                string readyOption = Console.ReadLine();

                if (readyOption == "1")
                {
                    isReady = true;
                }
                else if (readyOption == "2")
                {
                    textPrinter.Dialogue("Dizarius", "Patience is important, let me know when you're ready.");
                    textPrinter.Print("\n[Are you ready now?]\n1. Yes\n2. No\n");
                }
                else
                {
                    // Increase anger level if the input is invalid
                    if (!curseOfTheFrog)
                    {
                        string message = string.Empty;
                        if (angerLevel == 0)
                        {
                            textPrinter.Dialogue("Dizarius", $"Take this seriously {player1.Name}, answer 1 if you're ready or 2 if not");
                        }
                        else if (angerLevel == 1)
                        {
                            textPrinter.Dialogue("Dizarius", $"Are you paying attention, {player1.Name}? Just type 1 or 2 to answer");
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
                textPrinter.Dialogue("Dizarius", $"Excellent {player1.Name}! Let's begin the Mystic Roll!");
                player1.AddDice(new List<string> { "d4", "d6", "d8", "d12", "d20" });
                textPrinter.Print($"\n[Your inventory was filled with a " + string.Join(", ", player1.Dice) + "]");
            }
        }

        // ------------------- Start a game round -------------------
        public void Play()
        {
            var roller = new DieRoller();
            bool wantsToPlay = true;
            do 
            {
                string summary = "";
                // Create a CPU player
                PlayerClass playerCpu = new PlayerClass("Dizarius");
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
                for (int i = 0; i < 2; i++)
                {
                    textPrinter.Dialogue($"Round {i + 1}", "Start!");
                    if (isPlayerTurn)
                    {
                        // Player's turn
                        // Choose the die Dizarius will roll
                        textPrinter.Print("\nPick the die Dizarius will roll: " + string.Join(", ", playerCpu.Dice));
                        Console.Write($"[{player1.Name.ToUpper()}] ");
                        string cpuDie = Console.ReadLine();
                        while (!playerCpu.Dice.Contains(cpuDie.ToLower()))
                        {
                            textPrinter.Print("Please type one of the options: " + string.Join(", ", playerCpu.Dice));
                            Console.Write($"[{player1.Name.ToUpper()}] ");
                            cpuDie = Console.ReadLine();
                        }
                        playerCpu.RemoveDie(cpuDie);

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
                        player1.RemoveDie(yourDie);

                        Console.WriteLine("");
                        player1.addScore(roller.Roll(yourDie, textPrinter));
                        playerCpu.addScore(roller.Roll(cpuDie, textPrinter, false));
                        isPlayerTurn = false;

                        textPrinter.Print($"\n[The round ends with you having a score of {player1.Score} and Dizarius with a score of {playerCpu.Score}]");
                    }
                    else
                    {
                        // CPU's turn
                        // Choose the die the player will roll
                        string[] playerOptions = player1.Dice.ToArray();
                        string cpuChosenDie = playerOptions[random.Next(playerOptions.Length)];
                        textPrinter.Print($"\n[Dizarius has picked your die: {cpuChosenDie}]");
                        player1.RemoveDie(cpuChosenDie);

                        // Choose the die Dizarius will roll
                        string[] cpuOptions = playerCpu.Dice.ToArray();
                        string cpuDie = cpuOptions[random.Next(cpuOptions.Length)];
                        textPrinter.Print($"[Dizarius has picked his own die: {cpuDie}]");
                        playerCpu.RemoveDie(cpuDie);

                        Console.WriteLine("");
                        playerCpu.addScore(roller.Roll(cpuDie, textPrinter, false));
                        player1.addScore(roller.Roll(cpuChosenDie, textPrinter));
                        isPlayerTurn = true;

                        textPrinter.Print($"\n[The round ends with you having a score of {player1.Score} and Dizarius with a score of {playerCpu.Score}]");
                    }
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

                // Print the summary of the round
                textPrinter.Dialogue("Summary", summary);

                // Ask if the player wants to play again
                textPrinter.Dialogue("Dizarius", $"That was wonderful! An amazing duel! Do you wish to play again, {player1.Name}?");
                textPrinter.Print("\n1. Yes\n2. No\n");
                Console.Write($"[{player1.Name.ToUpper()}] ");
                string playAgainOption = Console.ReadLine();
                do 
                {
                    Console.Write($"[{player1.Name.ToUpper()}] ");
                    playAgainOption = Console.ReadLine();
                    if (playAgainOption == "1")
                    {
                        // Reset scores and dice for a new round
                        player1.Reset();
                        playerCpu.Reset();
                        player1.AddDice(new List<string> { "d4", "d6", "d8", "d12", "d20" });
                        playerCpu.AddDice(new List<string> { "d4", "d6", "d8", "d12", "d20" });
                    }
                    else if (playAgainOption == "2")
                    {
                        wantsToPlay = false;
                        textPrinter.Dialogue("Dizarius", "It was a pleasure playing with you, until next time!");
                    }
                    else
                    {
                        // Increase anger level if the input is invalid
                        if (!curseOfTheFrog)
                        {
                            string message = string.Empty;
                            if (angerLevel == 0)
                            {
                                textPrinter.Dialogue("Dizarius", $"Take this seriously {player1.Name}, answer 1 if you want to play again or 2 if not");
                            }
                            else if (angerLevel == 1)
                            {
                                textPrinter.Dialogue("Dizarius", $"Are you paying attention, {player1.Name}? Just type 1 or 2 to answer");
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
                } while (playAgainOption != "1" && playAgainOption != "2");
                
            } while (wantsToPlay);
            
        }

        public void CurseOfTheFrog()
        {
            curseOfTheFrog = true;
            textPrinter.Dialogue("Dizarius", "You have tested my patience too long... By my hand, you now bear the curse of the frog!");
            textPrinter.Print("\n[You have received the curse of the frog. Your inventory \nwas filled with d7 dice]\n\nPress ENTER to continue");
            player1 = new PlayerClass("Frog");
            Console.ReadLine();
        }

    }
}
