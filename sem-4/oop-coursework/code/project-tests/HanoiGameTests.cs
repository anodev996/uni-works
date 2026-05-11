using Microsoft.VisualStudio.TestTools.UnitTesting;
using project;

namespace project_tests
{
    [TestClass]
    public class HanoiGameTests
    {
        [TestMethod]
        public void Constructor_InitializesGameCorrectly()
        {
            HanoiGame game = new HanoiGame(3);

            Assert.AreEqual(3, game.DisksCount);
            Assert.AreEqual(7, game.OptimalMoveCount);
            Assert.AreEqual(3, game.Rods[0].Disks.Count);
            Assert.AreEqual(0, game.MoveHistory.Count);
            Assert.IsFalse(game.IsGameWon);
        }

        [TestMethod]
        public void DoMove_ValidMove_ReturnsTrueAndMovesDisk()
        {
            HanoiGame game = new HanoiGame(3);

            bool result = game.DoMove(0, 1);

            Assert.IsTrue(result);
            Assert.AreEqual(2, game.Rods[0].Disks.Count);
            Assert.AreEqual(1, game.Rods[1].Disks.Count);
            Assert.AreEqual(1, game.MoveHistory.Count);
        }

        [TestMethod]
        public void DoMove_EmptySourceRod_ReturnsFalse()
        {
            HanoiGame game = new HanoiGame(3);

            bool result = game.DoMove(1, 2);

            Assert.IsFalse(result);
            Assert.AreEqual(0, game.MoveHistory.Count);
        }

        [TestMethod]
        public void DoMove_LargerOnSmaller_ReturnsFalse()
        {
            HanoiGame game = new HanoiGame(3);
            game.DoMove(0, 1);

            bool result = game.DoMove(0, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UndoMove_RevertsLastMove()
        {
            HanoiGame game = new HanoiGame(3);
            game.DoMove(0, 1);

            game.UndoMove();

            Assert.AreEqual(3, game.Rods[0].Disks.Count);
            Assert.AreEqual(0, game.Rods[1].Disks.Count);
            Assert.AreEqual(0, game.MoveHistory.Count);
        }
    }
}