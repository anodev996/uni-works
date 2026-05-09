using System.Collections.Generic;

namespace project;

public static class HanoiSolver
{
    public static List<Move> GetSolution(int disksCount)
    {
        var solution = new List<Move>();

        if (disksCount > 0)
            CalculateMoves(disksCount, 0, 2, 1, solution);

        return solution;
    }

    private static void CalculateMoves(int n, int fromRod, int toRod, int auxRod, List<Move> moves)
    {
        // Base case: Move a single disk
        if (n == 1)
        {
            moves.Add(new Move(fromRod, toRod));
            return;
        }

        // Recursive steps
        CalculateMoves(n - 1, fromRod, auxRod, toRod, moves);
        moves.Add(new Move(fromRod, toRod));
        CalculateMoves(n - 1, auxRod, toRod, fromRod, moves);
    }
}