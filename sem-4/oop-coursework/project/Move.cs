namespace project
{
    public struct Move
    {
        public int FromRod { get; private set; }
        public int ToRod { get; private set; }

        public Move(int fromRod, int toRod)
        {
            FromRod = fromRod;
            ToRod = toRod;
        }
    }
}