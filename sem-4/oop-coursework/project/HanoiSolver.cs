using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;

namespace project
{
    public static class HanoiSolver
    {
        public static List<Move> GetSolution(int disksCount)
        {
            List<Move> solution = new List<Move>();
            CalculateMoves(disksCount, 0, 2, 1, solution);
            return solution;
        }

        private static void CalculateMoves(int n, int fromRod, int toRod, int auxRod, List<Move> moves)
        {
            if (n == 1)
            {
                moves.Add(new Move(fromRod, toRod));
            }

            CalculateMoves(n - 1, fromRod, auxRod, toRod, moves);
            moves.Add(new Move(fromRod, toRod));
            CalculateMoves(n - 1, auxRod, toRod, fromRod, moves);
        }
    }
}
