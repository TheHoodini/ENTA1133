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
            Console.WriteLine("Rolling the d"+ numberOfSidesRoll +"...");
            Thread.Sleep(800);
            rollNumber = random.Next(1, numberOfSidesRoll + 1);
            Console.WriteLine("First roll:  " + rollNumber);
            result += rollNumber;
            rollNumber = random.Next(1, numberOfSidesRoll + 1);
            Thread.Sleep(800);
            Console.WriteLine("Second roll: " + rollNumber);
            result += rollNumber;
            rollNumber = random.Next(1, numberOfSidesRoll + 1);
            Thread.Sleep(800);
            Console.WriteLine("Third roll:  " + rollNumber);
            result += rollNumber;
            rollNumber = random.Next(1, numberOfSidesRoll + 1);
            Thread.Sleep(800);
            Console.WriteLine("Fourth roll: " + rollNumber);
            result += rollNumber;
            Thread.Sleep(800);

            Console.WriteLine("-----------------\nYour total:  "+ result);
        }
    }
}
