using System;
using System.Globalization;
using System.Text;

namespace prac_4
{
    internal static class MatrixMath
    {
        public const string IdentityText =
            "1 0 0 0\r\n0 1 0 0\r\n0 0 1 0\r\n0 0 0 1";

        public static double[,] Multiply(double[,] a, double[,] b)
        {
            int rowsA = a.GetLength(0), colsA = a.GetLength(1);
            int rowsB = b.GetLength(0), colsB = b.GetLength(1);

            if (colsA != rowsB)
                throw new InvalidOperationException("Несогласованные размеры матриц.");

            var result = new double[rowsA, colsB];
            for (int i = 0; i < rowsA; i++)
                for (int j = 0; j < colsB; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < colsA; k++)
                        sum += a[i, k] * b[k, j];
                    result[i, j] = sum;
                }
            return result;
        }

        public static bool TryParseMatrix(string text, int rows, int cols, out double[,] matrix)
        {
            matrix = null;

            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            if (lines.Length != rows)
                return false;

            var m = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                string[] parts = lines[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != cols)
                    return false;

                for (int j = 0; j < cols; j++)
                    if (!double.TryParse(parts[j], NumberStyles.Float, CultureInfo.InvariantCulture, out m[i, j]))
                        return false;
            }

            matrix = m;
            return true;
        }

        public static string MatrixToString(double[,] m)
        {
            int rows = m.GetLength(0), cols = m.GetLength(1);
            var sb = new StringBuilder();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(m[i, j].ToString("0.###", CultureInfo.InvariantCulture));
                    if (j < cols - 1) sb.Append(' ');
                }
                if (i < rows - 1) sb.AppendLine();
            }
            return sb.ToString();
        }

        public static double[,] RotationX(double a)
        {
            double c = Math.Cos(a), s = Math.Sin(a);
            return new double[,]
            {
                { 1, 0,  0, 0 },
                { 0, c, -s, 0 },
                { 0, s,  c, 0 },
                { 0, 0,  0, 1 }
            };
        }

        public static double[,] RotationY(double a)
        {
            double c = Math.Cos(a), s = Math.Sin(a);
            return new double[,]
            {
                {  c, 0, s, 0 },
                {  0, 1, 0, 0 },
                { -s, 0, c, 0 },
                {  0, 0, 0, 1 }
            };
        }

        public static double[,] RotationZ(double a)
        {
            double c = Math.Cos(a), s = Math.Sin(a);
            return new double[,]
            {
                { c, -s, 0, 0 },
                { s,  c, 0, 0 },
                { 0,  0, 1, 0 },
                { 0,  0, 0, 1 }
            };
        }
    }
}