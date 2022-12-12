using NUnit.Framework;
using TicTacToeApp;

namespace TicTacToeTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestXWinsFirstRow()
        {
            TicTacToe game = new TicTacToe();
            TestContext.WriteLine(game.IsPlayAgainstComputer.ToString());
            game.StartGame();
            game.TakeSpot(0);
            game.TakeSpot(3);
            game.TakeSpot(1);
            game.TakeSpot(6);
            game.TakeSpot(2);
            Assert.IsTrue(game.GameActive == false && game.Winner == TicTacToe.PlayerEnum.X, "Game.GameActive = " + game.GameActive + "Game.Message = " + game.Message);
            TestContext.WriteLine(game.SpotsReport);
            TestContext.WriteLine(game.Message);
        }
        [TestCase (0,3,1,6,2)]
        [TestCase (3,0,4,6,5)]
        [TestCase (6,3,7,0,8)]
        public void TestXWinsHorizontal(int x0, int o0, int x1, int o1, int x2)
        {
            TicTacToe game = new TicTacToe();
            TestContext.WriteLine(game.IsPlayAgainstComputer.ToString());
            game.StartGame();
            TestContext.WriteLine(game.IsPlayAgainstComputer.ToString());
            game.TakeSpot(x0);
            game.TakeSpot(o0);
            game.TakeSpot(x1);
            game.TakeSpot(o1);
            game.TakeSpot(x2);
            Assert.IsTrue(game.GameActive == false && game.Winner == TicTacToe.PlayerEnum.X, "Game.GameActive = " + game.GameActive + "Game.Message = " + game.Message);
            TestContext.WriteLine(game.SpotsReport);
            TestContext.WriteLine(game.Message);
        }
    }
}