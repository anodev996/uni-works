using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace lab_3
{
    public partial class MainForm : Form
    {
        private bool _drawFigure = false;
        private double[,] _points;

        private readonly double[,] _initialPoints = new double[,]
        {
            { -4, -4, 1 },
            { -4,  3, 1 },
            {  3,  3, 1 },
            { -1,  1, 1 }
        };

        private const string InitialMatrixText =
            "1 0 0\r\n0 1 0\r\n0 0 1";

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

            int panelW = splitContainer1.Panel1.Width;
            int panelH = splitContainer1.Panel1.Height;

            int centerX = panelW / 2;
            int centerY = panelH / 2;

            float scale = 30f;

            using (Pen axisPen = new Pen(Color.Black, 1f))
            {
                g.DrawLine(axisPen, 0, centerY, panelW, centerY);
                g.DrawLine(axisPen, centerX, 0, centerX, panelH);
            }

            using (Font font = new Font("Microsoft Sans Serif", 10f))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.DrawString("0", font, brush, centerX + 3, centerY + 3);

                g.DrawString("1", font, brush, centerX + scale + 3, centerY + 3);
            }

            GraphicsState state = g.Save();
            g.TranslateTransform(centerX, centerY);
            g.ScaleTransform(scale, -scale);

            PointF[] points = new PointF[_points.GetLength(0)];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new PointF((float)_points[i, 0], (float)_points[i, 1]);
            }

            using (Pen pen = new Pen(Color.Black, 2 / scale))
            {
                g.DrawPolygon(pen, points);
            }

            g.Restore(state);
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            _points = (double[,])_initialPoints.Clone();
            txtBxCoordMatrix.Text = MatrixToString(_initialPoints);
            _drawFigure = true;
            splitContainer1.Panel1.Invalidate();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            double[,] transform;
            if (!TryParseMatrix(txtBxEnterMatrix.Text, 3, 3, out transform))
            {
                MessageBox.Show(
                    "Неправильный ввод матрицы преобразований.\n" +
                    "Ожидается 3 строки по 3 числа, разделённые пробелами.",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (Math.Abs(transform[0, 2]) > 1e-9 ||
                Math.Abs(transform[1, 2]) > 1e-9 ||
                Math.Abs(transform[2, 2] - 1) > 1e-9)
            {
                MessageBox.Show(
                    "Последняя строка матрицы преобразования должна быть 0 0 1.",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            double[,] result = Multiply(_points, transform);
            _points = result;

            txtBxCoordMatrix.Text = MatrixToString(_points);

            _drawFigure = true;
            splitContainer1.Panel1.Invalidate();
        }

        private void btnResetMatrix_Click(object sender, EventArgs e)
        {
            txtBxEnterMatrix.Text = InitialMatrixText;
        }


        private static double[,] Multiply(double[,] a, double[,] b)
        {
            int rowsA = a.GetLength(0);
            int colsA = a.GetLength(1);
            int rowsB = b.GetLength(0);
            int colsB = b.GetLength(1);

            if (colsA != rowsB)
                throw new InvalidOperationException("Несогласованные размеры матриц.");

            double[,] result = new double[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < colsA; k++)
                        sum += a[i, k] * b[k, j];
                    result[i, j] = sum;
                }
            }
            return result;
        }

        private static bool TryParseMatrix(string text, int rows, int cols, out double[,] matrix)
        {
            matrix = null;

            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            if (lines.Length != rows)
                return false;

            double[,] m = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string line = lines[i];

                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != cols)
                    return false;

                for (int j = 0; j < cols; j++)
                {
                    if (!double.TryParse(parts[j], NumberStyles.Float, CultureInfo.InvariantCulture, out m[i, j]))
                        return false;
                }
            }

            matrix = m;
            return true;
        }

        private static string MatrixToString(double[,] m)
        {
            int rows = m.GetLength(0);
            int cols = m.GetLength(1);

            StringBuilder sb = new StringBuilder();
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