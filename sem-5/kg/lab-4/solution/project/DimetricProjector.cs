using System.Drawing;

namespace project
{
    internal static class DimetricProjector
    {
        public static PointF ToScreen(double sx, double sy, int cx, int cy, float scale)
        {
            return new PointF(cx + (float)(sx * scale), cy - (float)(sy * scale));
        }

        public static PointF[] ProjectedMatrixToScreen(double[,] projectedMatrix, int cx, int cy, float scale)
        {
            int n = projectedMatrix.GetLength(0);
            var result = new PointF[n];
            for (int i = 0; i < n; i++)
            {
                double w = projectedMatrix[i, 3] != 0 ? projectedMatrix[i, 3] : 1.0;
                double sx = projectedMatrix[i, 0] / w;
                double sy = projectedMatrix[i, 1] / w;
                result[i] = ToScreen(sx, sy, cx, cy, scale);
            }
            return result;
        }

        public static PointF GetAxisVector(int axisIndex, float scale)
        {
            double[,] dim = MatrixMath.DimetricProjection();
            return new PointF((float)(dim[axisIndex, 0] * scale), -(float)(dim[axisIndex, 1] * scale));
        }
    }
}