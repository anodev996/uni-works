using System.Collections.Generic;

namespace project;

/// TODO
/// Поправить выбор диска. Пересобрать проект под win7
public class HanoiGame
{
    public Rod[] Rods { get; }
    public int DisksCount { get; }
    public List<Move> MoveHistory { get; } = new();

    // Bitwise shift is mathematically equivalent to 2^n and much faster than Math.Pow
    public int OptimalMoveCount => (1 << DisksCount) - 1;
    public bool IsGameWon => Rods[2].Disks.Count == DisksCount;

    public HanoiGame(int disksCount)
    {
        DisksCount = disksCount;
        Rods = [new Rod(), new Rod(), new Rod()];

        // Fill the first rod (1 is the largest disk at the bottom)
        for (int i = 1; i <= disksCount; i++)
            Rods[0].Push(new Disk(i));
    }

    public bool DoMove(int fromIndex, int toIndex)
    {
        // Validate indices using C# pattern matching
        if (fromIndex is < 0 or > 2 || toIndex is < 0 or > 2 || fromIndex == toIndex)
            return false;

        var fromRod = Rods[fromIndex];
        var toRod = Rods[toIndex];

        // Check if source rod has disks
        if (!fromRod.Disks.TryPeek(out Disk diskToMove))
            return false;

        // Rule: Cannot place a larger disk (smaller number) on a smaller disk (larger number)
        if (toRod.Disks.TryPeek(out Disk topDisk) && diskToMove.Number < topDisk.Number)
            return false;

        toRod.Push(fromRod.Pop());

        MoveHistory.Add(new Move(fromIndex, toIndex));

        return true;
    }

    public void UndoMove()
    {
        if (MoveHistory.Count == 0) return;

        // Use index from end operator (^1) to get the last element
        Move lastMove = MoveHistory[^1];

        Disk disk = Rods[lastMove.ToRod].Pop();
        Rods[lastMove.FromRod].Push(disk);

        MoveHistory.RemoveAt(MoveHistory.Count - 1);
    }
}