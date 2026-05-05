using System;
using System.Collections.Generic;
using System.Text;

namespace project
{
    public class Rod
    {
        public Stack<Disk> Disks { get; private set; }
        
        public Rod()
        {
            Disks = new Stack<Disk>();
        }

        public void Push(Disk disk)
        {
            if (Disks.Count > 0 && Disks.Peek().Size <= disk.Size)
            {
                throw new InvalidOperationException("Нельзя класть больший диск на меньший!");
            }
            Disks.Push(disk);
        }

        public Disk Pop()
        {
            if (Disks.Count == 0)
            {
                throw new InvalidOperationException("Стержень пуст!");
            }
            return Disks.Pop();
        }
    }
}
