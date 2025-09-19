using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame
{
    internal class DieRoller(int numberOfSides)
    {
        int numberOfSidesRoll = numberOfSides;
        Random random = new Random();

        public void Roll()
        {
            int result = 0;
            int rollNumber;
            Console.WriteLine("Rolling the die...");
            Thread.Sleep(800);
            rollNumber = random.Next(1, numberOfSidesRoll + 1);
            Console.WriteLine("Your first roll was a " + rollNumber + "!");
            result += rollNumber;
            rollNumber = random.Next(1, numberOfSidesRoll + 1);
            Thread.Sleep(800);
            Console.WriteLine("Your second roll was a " + rollNumber + "!");
            result += rollNumber;
            rollNumber = random.Next(1, numberOfSidesRoll + 1);
            Thread.Sleep(800);
            Console.WriteLine("Your third roll was a " + rollNumber + "!");
            result += rollNumber;
            rollNumber = random.Next(1, numberOfSidesRoll + 1);
            Thread.Sleep(800);
            Console.WriteLine("Your fourth roll was a " + rollNumber + "!");
            result += rollNumber;
            Thread.Sleep(800);

            Console.WriteLine("Your total: "+ result);
        }
    }
}
