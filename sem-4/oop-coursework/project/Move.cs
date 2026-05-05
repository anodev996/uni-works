namespace project
{
    public struct Move
    {
        public int FromRod { get; }
        public int ToRod { get; }

        public Move (int fromRod, int toRod)
        {
            FromRod = fromRod;
            ToRod = toRod;
        }
    }
}
