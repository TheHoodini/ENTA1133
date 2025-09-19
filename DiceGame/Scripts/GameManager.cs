using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Scripts
{
    internal class GameManager
    {
        public void Play()
        {
            // Welcome the player with their name and date
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            Console.WriteLine("Welcome "+ playerName +" to the Dice Game!\n"+ todayDate +"\n");
            Thread.Sleep(800);

            // Ask for the type of die
            Console.Write("Enter the number of sides your die will have: ");
            int numberOfSides = int.Parse(Console.ReadLine());

            // Create a DieRoller and call the Roll method
            DieRoller dieRoller = new DieRoller(numberOfSides);
            Thread.Sleep(500);
            dieRoller.Roll();

            // Aritmetic operations explanation
            Thread.Sleep(1000);
            Console.WriteLine("\nHere is an explanation about the different types of arithmetic operators in programming:");
            Thread.Sleep(1000);
            Console.WriteLine("Addition (+): Adds two values. ex: 4 + 7 = 11");
            Thread.Sleep(1000);
            Console.WriteLine("Subtraction (-): Subtracts one value from another. ex: 10 - 3 = 7");
            Thread.Sleep(1000);
            Console.WriteLine("Division (/): Divides one value by another. ex: 20 / 4 = 5");
            Thread.Sleep(1000);
            Console.WriteLine("Multiplication (*): Multiplies two values. ex: 5 * 6 = 30");
            Thread.Sleep(1000);
            Console.WriteLine("Increment (++): increases the value of the variable by 1. ex: x = 5 -> x++ -> x = 6");
            Thread.Sleep(1000);
            Console.WriteLine("Decrement (--): decreases the value of the variable by 1. ex: x = 5 -> x-- -> x = 4");
            Thread.Sleep(1000);
            Console.WriteLine("Modulus (%): Returns the remainder of a division operation. ex: 10 % 3 = 1");

            Thread.Sleep(1000);
            Console.WriteLine("\nThanks " + playerName + " for playing! Goodbye!");
        }
    }
}
