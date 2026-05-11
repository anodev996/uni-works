namespace project
{
    // Represents a single disk in the game
    public struct Disk
    {
        // The size or ID of the disk (smaller number = smaller disk)
        public int Number { get; private set; }

        // Constructor to set the disk size
        public Disk(int number)
        {
            Number = number;
        }
    }
}