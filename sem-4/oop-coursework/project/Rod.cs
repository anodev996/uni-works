using System;
using System.Collections.Generic;

namespace project;

public class Rod
{
    public Stack<Disk> Disks { get; } = new();

    public void Push(Disk disk)
    {
        // TryPeek safely checks the top element without throwing exceptions
        if (Disks.TryPeek(out Disk topDisk) && topDisk.Number > disk.Number)
            throw new InvalidOperationException("Нельзя класть больший диск на меньший!");

        Disks.Push(disk);
    }

    public Disk Pop()
    {
        // TryPop attempts to remove and return the top element
        if (!Disks.TryPop(out Disk disk))
            throw new InvalidOperationException("Стержень пуст!");

        return disk;
    }
}