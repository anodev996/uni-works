namespace project
{
    // Represents a single move from one rod to another
    public struct Move
    {
        // Index of the starting rod
        public int FromRod { get; private set; }
        // Index of the destination rod
        public int ToRod { get; private set; }

        // Constructor to set the move details
        public Move(int fromRod, int toRod)
        {
            FromRod = fromRod;
            ToRod = toRod;
        }
    }
}