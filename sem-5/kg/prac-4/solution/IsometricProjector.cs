using System;
using System.Drawing;

namespace prac_4
{
    internal static class IsometricProjector
    {
        private static readonly double Cos30 = Math.Cos(Math.PI / 6);
        private static readonly double Sin30 = 0.5;

        public static PointF Project(double x, double y, double z, int cx, int cy, float scale)
        {
            double sx = x - z * Cos30;
            double sy = y - z * Sin30;
            return new PointF(cx + (float)(sx * scale), cy - (float)(sy * scale));
        }

        public static PointF[] ProjectAll(double[,] pts, int cx, int cy, float scale)
        {
            int n = pts.GetLength(0);
            var result = new PointF[n];
            for (int i = 0; i < n; i++)
                result[i] = Project(pts[i, 0], pts[i, 1], pts[i, 2], cx, cy, scale);
            return result;
        }

        public static double Cos30Value => Cos30;
        public static double Sin30Value => Sin30;
    }
}