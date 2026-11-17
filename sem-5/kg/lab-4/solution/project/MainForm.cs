using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace project
{
    public partial class MainForm : Form
    {
        private const float Scale = 35f;
        private const float Margin = 25f;

        private bool _drawFigure;
        private double _accumulatedAngle;
        private double[,] _points;
        private double[,] _inputBoxPoints;
        private double[,] _projectedPoints;

        public MainForm()
        {
            InitializeComponent();
            ResetFigureState();
            _inputBoxPoints = (double[,])Points.InitialInputBoxPoints.Clone();
            txtBxEnterMatrix.Text = MatrixMath.MatrixToString(_inputBoxPoints);
        }

        private void ResetFigureState()
        {
            _accumulatedAngle = 0;
            _points = (double[,])Points.InitialPoints.Clone();
            _projectedPoints = MatrixMath.Multiply(_points, MatrixMath.DimetricProjection());
            txtBxCoordMatrix.Text = MatrixMath.MatrixToString(_points);
        }

        private void splitContainer1_Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            _drawFigure = true;
            splitContainer1.Panel1.Invalidate();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (!_drawFigure || _projectedPoints == null)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int cx = splitContainer1.Panel1.Width / 2;
            int cy = splitContainer1.Panel1.Height / 2;

            DrawAxes(g, cx, cy, Scale);

            PointF[] screenPts = DimetricProjector.ProjectedMatrixToScreen(_projectedPoints, cx, cy, Scale);

            using (Pen frontPen = new Pen(Color.Black, 2f))
            using (Pen backPen = new Pen(Color.DimGray, 1.5f) { DashStyle = DashStyle.Dash })
            {
                g.DrawPolygon(frontPen, new[] { screenPts[0], screenPts[1], screenPts[2], screenPts[3] });
                g.DrawPolygon(backPen, new[] { screenPts[4], screenPts[5], screenPts[6], screenPts[7] });

                for (int i = 0; i < 4; i++)
                    g.DrawLine(frontPen, screenPts[i], screenPts[i + 4]);
            }
        }

        private void DrawAxes(Graphics g, int cx, int cy, float scale)
        {
            int panelW = splitContainer1.Panel1.Width;
            int panelH = splitContainer1.Panel1.Height;

            PointF vecX = DimetricProjector.GetAxisVector(0, scale);
            PointF vecY = DimetricProjector.GetAxisVector(1, scale);
            PointF vecZ = DimetricProjector.GetAxisVector(2, scale);

            float tX = GetRayLength(cx, cy, vecX.X, vecX.Y, panelW, panelH);
            float tY = GetRayLength(cx, cy, vecY.X, vecY.Y, panelW, panelH);
            float tZ = GetRayLength(cx, cy, vecZ.X, vecZ.Y, panelW, panelH);

            var origin = new PointF(cx, cy);
            var endX = new PointF(cx + vecX.X * tX, cy + vecX.Y * tX);
            var endY = new PointF(cx + vecY.X * tY, cy + vecY.Y * tY);
            var endZ = new PointF(cx + vecZ.X * tZ, cy + vecZ.Y * tZ);

            using (Pen penX = new Pen(Color.Red, 1.5f))
            using (Pen penY = new Pen(Color.Green, 1.5f))
            using (Pen penZ = new Pen(Color.Blue, 1.5f))
            {
                g.DrawLine(penX, origin, endX);
                g.DrawLine(penY, origin, endY);
                g.DrawLine(penZ, origin, endZ);
            }

            using (Font font = new Font("Microsoft Sans Serif", 11f, FontStyle.Bold))
            {
                g.DrawString("X", font, Brushes.Red, endX.X - 16, endX.Y + 4);
                g.DrawString("Y", font, Brushes.Green, endY.X + 5, endY.Y - 4);
                g.DrawString("Z", font, Brushes.Blue, endZ.X + 4, endZ.Y + 4);
            }

            using (Font font = new Font("Microsoft Sans Serif", 8.5f))
            using (Brush br = new SolidBrush(Color.Black))
            {
                g.DrawString("0", font, br, cx + 4, cy + 4);
                g.DrawString("1", font, br, cx + vecX.X, cy + vecX.Y + 3);
                g.DrawString("1", font, br, cx + vecY.X + 4, cy + vecY.Y - 12);
                g.DrawString("1", font, br, cx + vecZ.X - 14, cy + vecZ.Y + 2);
            }
        }

        private static float GetRayLength(int cx, int cy, float dx, float dy, int width, int height)
        {
            float tx = float.MaxValue;
            float ty = float.MaxValue;

            if (dx > 1e-6f) tx = (width - cx - Margin) / dx;
            else if (dx < -1e-6f) tx = (cx - Margin) / -dx;

            if (dy > 1e-6f) ty = (height - cy - Margin) / dy;
            else if (dy < -1e-6f) ty = (cy - Margin) / -dy;

            return Math.Max(1f, Math.Min(tx, ty));
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            ResetFigureState();
            _drawFigure = true;
            splitContainer1.Panel1.Invalidate();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (!MatrixMath.TryParseMatrix(txtBxEnterMatrix.Text, 4, 4, out double[,] transform))
            {
                Warn("Неправильный ввод матрицы преобразований.\nОжидается 4 строки по 4 числа, разделённые пробелами.");
                return;
            }

            if (Math.Abs(transform[0, 3]) > 1e-9 ||
                Math.Abs(transform[1, 3]) > 1e-9 ||
                Math.Abs(transform[2, 3]) > 1e-9 ||
                Math.Abs(transform[3, 3] - 1) > 1e-9)
            {
                Warn("Последний столбец матрицы преобразования должен быть 0 0 0 1.");
                return;
            }

            bool isProjectionMatrix =
                Math.Abs(transform[0, 2]) < 1e-9 &&
                Math.Abs(transform[1, 2]) < 1e-9 &&
                Math.Abs(transform[2, 2]) < 1e-9 &&
                Math.Abs(transform[3, 2]) < 1e-9;

            if (isProjectionMatrix)
            {
                _projectedPoints = MatrixMath.Multiply(_points, transform);
                _drawFigure = true;
                splitContainer1.Panel1.Invalidate();
            }
            else
            {
                ApplyAffineTransform(transform);
            }
        }

        private void btnResetMatrix_Click(object sender, EventArgs e)
        {
            txtBxEnterMatrix.Text = MatrixMath.MatrixToString(_inputBoxPoints);
        }

        private void btnRotYOZ_Click(object sender, EventArgs e)
        {
            double angle = GetAngle();
            _accumulatedAngle += angle;
            ApplyAffineTransform(MatrixMath.RotationYOZ(angle));
        }

        private void btnRotY_Click(object sender, EventArgs e)
        {
            ApplyAffineTransform(MatrixMath.RotationY(GetAngle()));
        }

        private void btnRotZ_Click(object sender, EventArgs e)
        {
            ApplyAffineTransform(MatrixMath.RotationZ(GetAngle()));
        }

        private void ApplyAffineTransform(double[,] transform)
        {
            _points = MatrixMath.Multiply(_points, transform);
            _projectedPoints = MatrixMath.Multiply(_points, MatrixMath.DimetricProjection());
            txtBxCoordMatrix.Text = MatrixMath.MatrixToString(_points);
            _drawFigure = true;
            splitContainer1.Panel1.Invalidate();
        }

        private double GetAngle()
        {
            if (!double.TryParse(txtAngle.Text, NumberStyles.Float,
                CultureInfo.InvariantCulture, out double a))
                a = 15;
            return a * Math.PI / 180.0;
        }

        private static void Warn(string message) =>
            MessageBox.Show(message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}