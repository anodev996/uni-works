using System.Collections.Generic;

namespace project
{
    // Contains the main logic and state of the Tower of Hanoi game
    public class HanoiGame
    {
        // Array of 3 rods used in the game
        public Rod[] Rods { get; private set; }
        // Total number of disks in the current game
        public int DisksCount { get; private set; }
        // Keeps track of all moves made by the player or solver
        public List<Move> MoveHistory { get; private set; }

        // Calculates the minimum number of moves required to solve the puzzle (2^n - 1)
        public int OptimalMoveCount
        {
            get { return (1 << DisksCount) - 1; }
        }

        // Checks if the game is won (all disks are successfully moved to the last rod)
        public bool IsGameWon
        {
            get { return Rods[2].Disks.Count == DisksCount; }
        }

        // Constructor to initialize a new game with a specific number of disks
        public HanoiGame(int disksCount)
        {
            DisksCount = disksCount;
            MoveHistory = new List<Move>();
            // Initialize the 3 rods
            Rods = new Rod[] { new Rod(), new Rod(), new Rod() };

            // Put all disks on the first rod (rod index 0)
            // Disk size increases from top to bottom
            for (int i = 1; i <= disksCount; i++)
            {
                Rods[0].Push(new Disk(i));
            }
        }

        // Attempts to move a disk from one rod to another
        public bool DoMove(int fromIndex, int toIndex)
        {
            // Check if indices are valid (between 0 and 2) and not the same
            if (fromIndex < 0 || fromIndex > 2 || toIndex < 0 || toIndex > 2 || fromIndex == toIndex)
                return false;

            Rod fromRod = Rods[fromIndex];
            Rod toRod = Rods[toIndex];

            // Cannot move a disk from an empty rod
            if (fromRod.Disks.Count == 0)
                return false;

            Disk diskToMove = fromRod.Disks.Peek();

            // Rule check: Cannot place a larger disk on a smaller disk
            if (toRod.Disks.Count > 0 && diskToMove.Number < toRod.Disks.Peek().Number)
                return false;

            // Move the disk: pop from source, push to destination
            toRod.Push(fromRod.Pop());

            // Record the move in history
            MoveHistory.Add(new Move(fromIndex, toIndex));

            return true; // Move was successful
        }

        // Undoes the last move made in the game
        public void UndoMove()
        {
            // Do nothing if there is no move history
            if (MoveHistory.Count == 0) return;

            // Get the last move from the list
            Move lastMove = MoveHistory[MoveHistory.Count - 1];

            // Move the disk back from the destination rod to the source rod
            Disk disk = Rods[lastMove.ToRod].Pop();
            Rods[lastMove.FromRod].Push(disk);

            // Remove the last move from history
            MoveHistory.RemoveAt(MoveHistory.Count - 1);
        }
    }
}