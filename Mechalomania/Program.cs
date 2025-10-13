using GD14_1133_A1_JuanDiego_DiceGame.Scripts;

namespace GD14_1133_A1_JuanDiego_DiceGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            DungeonGameManager gameManager = new();
            gameManager.StartMenu();
        }
    }
}
