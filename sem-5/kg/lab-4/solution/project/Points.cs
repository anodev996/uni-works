namespace project
{
    internal static class Points
    {
        public static readonly double[,] InitialPoints =
        {
            { -2, -2,  2, 1 },
            { -2,  2,  2, 1 },
            {  2,  2,  2, 1 },
            {  2, -2,  2, 1 },
            { -2, -2, -2, 1 },
            { -2,  2, -2, 1 },
            {  2,  2, -2, 1 },
            {  2, -2, -2, 1 }
        };

        public static readonly double[,] InitialInputBoxPoints =
        {
            { 1, 0, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 0, 1 },
        };
    }
}