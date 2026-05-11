using System.Collections.Generic;

namespace project
{
    // Utility class to automatically solve the Tower of Hanoi puzzle
    public static class HanoiSolver
    {
        // Returns a list of optimal moves to solve the puzzle for a given number of disks
        public static List<Move> GetSolution(int disksCount)
        {
            List<Move> solution = new List<Move>();

            // Start the recursive solving process if there is at least 1 disk
            // 0 is the start rod, 2 is the target rod, 1 is the auxiliary (helper) rod
            if (disksCount > 0)
            {
                CalculateMoves(disksCount, 0, 2, 1, solution);
            }

            return solution;
        }

        // Recursive function to calculate the moves
        private static void CalculateMoves(int n, int fromRod, int toRod, int auxRod, List<Move> moves)
        {
            // Base case: if there is only 1 disk, just move it directly to the target rod
            if (n == 1)
            {
                moves.Add(new Move(fromRod, toRod));
                return;
            }

            // Step 1: Move (n-1) disks from the starting rod to the helper rod
            CalculateMoves(n - 1, fromRod, auxRod, toRod, moves);

            // Step 2: Move the largest disk directly to the target rod
            moves.Add(new Move(fromRod, toRod));

            // Step 3: Move the (n-1) disks from the helper rod to the target rod
            CalculateMoves(n - 1, auxRod, toRod, fromRod, moves);
        }
    }
}