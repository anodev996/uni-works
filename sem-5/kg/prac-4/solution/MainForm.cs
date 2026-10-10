using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace prac_4
{
    public partial class MainForm : Form
    {
        private const float Scale = 30f;
        private const float Margin = 25f;

        private bool _drawFigure;
        private double[,] _points;

        public MainForm()
        {
            InitializeComponent();
            _points = (double[,])Figure.InitialPoints.Clone();
            txtBxCoordMatrix.Text = MatrixMath.MatrixToString(_points);
        }

        private void splitContainer1_Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            _drawFigure = true;
            splitContainer1.Panel1.Invalidate();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (!_drawFigure || _points == null)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int cx = splitContainer1.Panel1.Width / 2;
            int cy = splitContainer1.Panel1.Height / 2;

            DrawAxes(g, cx, cy, Scale);

            PointF[] projected = IsometricProjector.ProjectAll(_points, cx, cy, Scale);

            using (Pen pen = new Pen(Color.Black, 2f))
            using (Pen dashPen = new Pen(Color.Gray, 1.5f) { DashStyle = DashStyle.Dash })
            {
                g.DrawPolygon(pen, new[] { projected[0], projected[1], projected[2], projected[3] });
                g.DrawPolygon(pen, new[] { projected[4], projected[5], projected[6], projected[7] });

                for (int i = 0; i < 4; i++)
                    g.DrawLine(dashPen, projected[i], projected[i + 4]);
            }
        }

        private void DrawAxes(Graphics g, int cx, int cy, float scale)
        {
            int panelW = splitContainer1.Panel1.Width;
            int panelH = splitContainer1.Panel1.Height;

            float dxX = scale;
            float dyY = -scale;
            float dxZ = -(float)(IsometricProjector.Cos30Value * scale);
            float dyZ = (float)(IsometricProjector.Sin30Value * scale);

            float lenX = panelW - cx - Margin;
            float lenY = cy - Margin;
            float tZ = Math.Min(
                dxZ != 0 ? (cx - Margin) / -dxZ : float.MaxValue,
                dyZ != 0 ? (panelH - cy - Margin) / dyZ : float.MaxValue);

            var origin = new PointF(cx, cy);
            var endX = new PointF(cx + dxX * (lenX / Math.Abs(dxX)), cy);
            var endY = new PointF(cx, cy + dyY * (lenY / Math.Abs(dyY)));
            var endZ = new PointF(cx + dxZ * tZ, cy + dyZ * tZ);

            using (Pen penX = new Pen(Color.Red, 1.5f))
            using (Pen penY = new Pen(Color.Green, 1.5f))
            using (Pen penZ = new Pen(Color.Blue, 1.5f))
            {
                g.DrawLine(penX, origin, endX);
                g.DrawLine(penY, origin, endY);
                g.DrawLine(penZ, origin, endZ);
            }

            using (Font font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold))
            {
                g.DrawString("X", font, Brushes.Red, endX.X + 3, endX.Y - 8);
                g.DrawString("Y", font, Brushes.Green, endY.X + 5, endY.Y - 5);
                g.DrawString("Z", font, Brushes.Blue, endZ.X - 18, endZ.Y + 3);
            }

            using (Font font = new Font("Microsoft Sans Serif", 9f))
            using (Brush br = new SolidBrush(Color.Black))
            {
                g.DrawString("0", font, br, cx + 4, cy + 4);
                g.DrawString("1", font, br, cx + scale + 2, cy + 4);
                g.DrawString("1", font, br, cx + 4, cy - scale - 14);
                g.DrawString("1", font, br, cx + dxZ - 16, cy + dyZ + 2);
            }
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            _points = (double[,])Figure.InitialPoints.Clone();
            txtBxCoordMatrix.Text = MatrixMath.MatrixToString(_points);
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

            ApplyTransform(transform);
        }

        private void btnResetMatrix_Click(object sender, EventArgs e)
        {
            txtBxEnterMatrix.Text = MatrixMath.IdentityText;
        }

        private void btnRotX_Click(object sender, EventArgs e) => ApplyTransform(MatrixMath.RotationX(GetAngle()));
        private void btnRotY_Click(object sender, EventArgs e) => ApplyTransform(MatrixMath.RotationY(GetAngle()));
        private void btnRotZ_Click(object sender, EventArgs e) => ApplyTransform(MatrixMath.RotationZ(GetAngle()));

        private void ApplyTransform(double[,] transform)
        {
            _points = MatrixMath.Multiply(_points, transform);
            txtBxCoordMatrix.Text = MatrixMath.MatrixToString(_points);
            _drawFigure = true;
            splitContainer1.Panel1.Invalidate();
        }

        private double GetAngle()
        {
            if (!double.TryParse(txtAngle.Text, NumberStyles.Float,
                CultureInfo.InvariantCulture, out double a))
                a = 10;
            return a * Math.PI / 180.0;
        }

        private static void Warn(string message) =>
            MessageBox.Show(message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}