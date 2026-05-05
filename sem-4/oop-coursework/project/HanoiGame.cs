using System;
using System.Collections.Generic;

namespace project
{
    public class HanoiGame
    {
        public Rod[] Rods { get; private set; }
        public int DisksCount { get; }
        public int MoveCount { get; private set; }
        public List<Move> MoveHistory { get; private set; }
        public int OptimalMoveCount => (int)Math.Pow(2, DisksCount) - 1;

        public HanoiGame(int disksCount)
        {
            DisksCount = disksCount;
            MoveCount = 0;
            MoveHistory = new List<Move>();
            Rods = new Rod[3] { new Rod(), new Rod(), new Rod() };

            for (int i = disksCount; i >= 1; i--)
            {
                Rods[0].Push(new Disk(i));
            }
        }

        public bool DoMove(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex > 2 || toIndex < 0 || toIndex > 2) return false;
            if (Rods[fromIndex].Disks.Count == 0) return false;

            if (Rods[toIndex].Disks.Count > 0)
            {
                Disk diskToMove = Rods[fromIndex].Disks.Peek();
                Disk topDiskAtDestination = Rods[toIndex].Disks.Peek();

                if (diskToMove.Size > topDiskAtDestination.Size)
                {
                    return false;
                }
            }

            Disk disk = Rods[fromIndex].Pop();
            Rods[toIndex].Push(disk);

            MoveCount++;
            MoveHistory.Add(new Move(fromIndex, toIndex));

            return true;
        }

        public void UndoMove()
        {
            if (MoveHistory.Count == 0) return;

            Move lastMove = MoveHistory[MoveHistory.Count - 1];

            Disk disk = Rods[lastMove.ToRod].Pop();
            Rods[lastMove.FromRod].Push(disk);

            MoveHistory.RemoveAt(MoveHistory.Count - 1);
            MoveCount--;
        }
    }
}