using System.Collections.Generic;

namespace project
{
    public class HanoiGame
    {
        public Rod[] Rods { get; private set; }
        public int DisksCount { get; private set; }
        public List<Move> MoveHistory { get; private set; }

        public int OptimalMoveCount
        {
            get { return (1 << DisksCount) - 1; }
        }

        public bool IsGameWon
        {
            get { return Rods[2].Disks.Count == DisksCount; }
        }

        public HanoiGame(int disksCount)
        {
            DisksCount = disksCount;
            MoveHistory = new List<Move>();
            Rods = new Rod[] { new Rod(), new Rod(), new Rod() };

            for (int i = 1; i <= disksCount; i++)
            {
                Rods[0].Push(new Disk(i));
            }
        }

        public bool DoMove(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex > 2 || toIndex < 0 || toIndex > 2 || fromIndex == toIndex)
                return false;

            Rod fromRod = Rods[fromIndex];
            Rod toRod = Rods[toIndex];

            if (fromRod.Disks.Count == 0)
                return false;

            Disk diskToMove = fromRod.Disks.Peek();

            if (toRod.Disks.Count > 0 && diskToMove.Number < toRod.Disks.Peek().Number)
                return false;

            toRod.Push(fromRod.Pop());

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
        }
    }
}