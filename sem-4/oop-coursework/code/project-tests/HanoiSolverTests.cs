using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using project;

namespace project_tests
{
    [TestClass]
    public class HanoiSolverTests
    {
        [TestMethod]
        public void GetSolution_For3Disks_Returns7Moves()
        {
            List<Move> solution = HanoiSolver.GetSolution(3);
            Assert.AreEqual(7, solution.Count);
        }

        [TestMethod]
        public void GetSolution_MovesActuallySolveTheGame()
        {
            int disks = 4;
            HanoiGame game = new HanoiGame(disks);

            List<Move> solution = HanoiSolver.GetSolution(disks);
            foreach (Move move in solution)
            {
                game.DoMove(move.FromRod, move.ToRod);
            }

            Assert.IsTrue(game.IsGameWon);
            Assert.AreEqual(15, game.MoveHistory.Count);
        }
    }
}