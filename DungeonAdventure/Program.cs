using GD14_1133_A1_JuanDiego_DiceGame.Scripts;

namespace GD14_1133_A1_JuanDiego_DiceGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DungeonGameManager gameManager = new();
            gameManager.StartGame();
        }
    }
}
