using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace prac_4
{
    public partial class MainForm : Form
    {
        private const string InitialMatrixText = "1 0 0 0\r\n0 1 0 0\r\n0 0 1 0\r\n0 0 0 1";
        private const float Scale = 30f;
        private const float Margin = 25f;

        private static readonly double Cos30 = Math.Cos(Math.PI / 6);
        private static readonly double Sin30 = 0.5;

        private bool _drawFigure;
        private double[,] _points;

        private readonly double[,] _initialPoints =
        {
            { -4, -4,  1, 1 },
            { -4,  3,  1, 1 },
            {  3,  3,  1, 1 },
            { -1,  1,  1, 1 },
            { -4, -4, -1, 1 },
            { -4,  3, -1, 1 },
            {  3,  3, -1, 1 },
            { -1,  1, -1, 1 }
        };

        public MainForm()
        {
            InitializeComponent();
            _points = (double[,])_initialPoints.Clone();
            txtBxCoordMatrix.Text = MatrixToString(_initialPoints);
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

            PointF[] projected = ProjectPoints(_points, cx, cy, Scale);

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
            float dxZ = -(float)(Cos30 * scale);
            float dyZ = (float)(Sin30 * scale);

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

        private static PointF Project3D(double x, double y, double z, int cx, int cy, float scale)
        {
            double sx = x - z * Cos30;
            double sy = y - z * Sin30;
            return new PointF(cx + (float)(sx * scale), cy - (float)(sy * scale));
        }

        private static PointF[] ProjectPoints(double[,] pts, int cx, int cy, float scale)
        {
            int n = pts.GetLength(0);
            var result = new PointF[n];
            for (int i = 0; i < n; i++)
                result[i] = Project3D(pts[i, 0], pts[i, 1], pts[i, 2], cx, cy, scale);
            return result;
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            _points = (double[,])_initialPoints.Clone();
            txtBxCoordMatrix.Text = MatrixToString(_points);
            _drawFigure = true;
            splitContainer1.Panel1.Invalidate();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (!TryParseMatrix(txtBxEnterMatrix.Text, 4, 4, out double[,] transform))
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
            txtBxEnterMatrix.Text = InitialMatrixText;
        }

        private void btnRotX_Click(object sender, EventArgs e) => ApplyTransform(RotationX(GetAngle()));
        private void btnRotY_Click(object sender, EventArgs e) => ApplyTransform(RotationY(GetAngle()));
        private void btnRotZ_Click(object sender, EventArgs e) => ApplyTransform(RotationZ(GetAngle()));

        private void ApplyTransform(double[,] transform)
        {
            _points = Multiply(_points, transform);
            txtBxCoordMatrix.Text = MatrixToString(_points);
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

        private static double[,] RotationX(double a)
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

        private static double[,] RotationY(double a)
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

        private static double[,] RotationZ(double a)
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

        private static double[,] Multiply(double[,] a, double[,] b)
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

        private static bool TryParseMatrix(string text, int rows, int cols, out double[,] matrix)
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

        private static string MatrixToString(double[,] m)
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
    }
}