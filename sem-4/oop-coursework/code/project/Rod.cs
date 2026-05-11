using System;
using System.Collections.Generic;

namespace project
{
    // Represents a single rod (peg) that holds a stack of disks
    public class Rod
    {
        // Stack to store disks. The top disk is the first to be popped.
        public Stack<Disk> Disks { get; private set; }

        // Constructor initializes an empty stack of disks
        public Rod()
        {
            Disks = new Stack<Disk>();
        }

        // Adds a new disk to the top of the rod
        public void Push(Disk disk)
        {
            // Check if the rod is not empty and the new disk is larger than the top disk
            // Rule: You cannot place a larger disk on top of a smaller one
            if (Disks.Count > 0 && Disks.Peek().Number > disk.Number)
            {
                throw new InvalidOperationException("Нельзя класть больший диск на меньший!");
            }

            // Add the disk to the stack
            Disks.Push(disk);
        }

        // Removes and returns the top disk from the rod
        public Disk Pop()
        {
            // Check if the rod is empty before trying to remove a disk
            if (Disks.Count == 0)
            {
                throw new InvalidOperationException("Стержень пуст!");
            }

            // Remove and return the top disk
            return Disks.Pop();
        }
    }
}