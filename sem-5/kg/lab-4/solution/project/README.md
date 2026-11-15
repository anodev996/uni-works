# Лабораторная #4, вариант 2

```csharp
// ============================================================================
// Файл: Figure.cs
// Назначение: Хранение исходных однородных координат вершин 3D-фигуры (гексаэдра)
// ============================================================================
namespace project
{
    internal static class Figure
    {
        // Матрица 8x4 исходных координат вершин правильного гексаэдра (куба) со стороной 4,
        // центрированного в начале координат (0, 0, 0).
        // Каждая строка имеет формат [X, Y, Z, W], где W = 1 — однородная координата.
        public static readonly double[,] InitialPoints =
        {
            // Передняя грань (Z = +2): 4 вершины по часовой стрелке
            { -2, -2,  2, 1 }, // Вершина 0: левая-нижняя-передняя
            { -2,  2,  2, 1 }, // Вершина 1: левая-верхняя-передняя
            {  2,  2,  2, 1 }, // Вершина 2: правая-верхняя-передняя
            {  2, -2,  2, 1 }, // Вершина 3: правая-нижняя-передняя

            // Задняя грань (Z = -2): 4 вершины в том же порядке обхода
            { -2, -2, -2, 1 }, // Вершина 4: левая-нижняя-задняя
            { -2,  2, -2, 1 }, // Вершина 5: левая-верхняя-задняя
            {  2,  2, -2, 1 }, // Вершина 6: правая-верхняя-задняя
            {  2, -2, -2, 1 }  // Вершина 7: правая-нижняя-задняя
        };
    }
}

// ============================================================================
// Файл: MatrixMath.cs
// Назначение: Матричная алгебра, матрицы поворотов, матрица диметрического
//             проецирования и вычисление композиции преобразований
// ============================================================================
using System;
using System.Globalization;
using System.Text;

namespace project
{
    internal static class MatrixMath
    {
        // Строковое представление единичной матрицы 4x4 для сброса поля ввода
        public const string IdentityText =
            "1 0 0 0\r\n0 1 0 0\r\n0 0 1 0\r\n0 0 0 1";

        // Классическое умножение двух матриц A (rowsA x colsA) и B (rowsB x colsB).
        // Используется для применения преобразований к вершинам (8x4 * 4x4)
        // и для формирования композиции матриц (4x4 * 4x4).
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

        // Формирование матрицы композиции двух преобразований: M_comp = M_first * M_second.
        // При умножении вектора-строки V на M_comp сначала выполняется first, затем second.
        public static double[,] ComposeTransform(double[,] first, double[,] second)
        {
            return Multiply(first, second);
        }

        // Парсинг пользовательской матрицы из многострочного текстового поля
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

        // Форматирование двумерного массива в читаемую строку с округлением до 3 знаков
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

        // Матрица поворота относительно плоскости YOZ на угол a (в радианах).
        // Плоскость YOZ задаётся уравнением X = 0, её нормаль — ось OX.
        // Поворот параллельно плоскости YOZ изменяет координаты Y и Z и эквивалентен повороту вокруг OX.
        public static double[,] RotationYOZ(double a)
        {
            return RotationX(a);
        }

        // Матрица поворота вокруг оси OX на угол a (в радианах) для векторов-строк
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

        // Матрица поворота вокруг оси OY на угол a (в радианах)
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

        // Матрица поворота вокруг оси OZ на угол a (в радианах)
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

        // Матрица аксонометрического диметрического проецирования (ГОСТ 2.317-2011).
        // Коэффициенты искажения: kx = ky = 2*sqrt(2)/3 (~0.943), kz = sqrt(2)/3 (~0.471),
        // что даёт стандартное соотношение 1 : 1 : 0.5.
        // Получается произведением поворота вокруг OY (psi ~ 20.70°), поворота вокруг OX (phi ~ -19.47°)
        // и ортографического проецирования на плоскость Z = 0 (третий столбец обнулён).
        public static double[,] DimetricProjection()
        {
            double sinPhi = -1.0 / 3.0;
            double cosPhi = 2.0 * Math.Sqrt(2.0) / 3.0;
            double sinPsi = 1.0 / (2.0 * Math.Sqrt(2.0));
            double cosPsi = Math.Sqrt(7.0 / 8.0);

            return new double[,]
            {
                {  cosPsi,  sinPsi * sinPhi, 0, 0 },
                {  0,       cosPhi,          0, 0 },
                { -sinPsi,  cosPsi * sinPhi, 0, 0 },
                {  0,       0,               0, 1 }
            };
        }
    }
}

// ============================================================================
// Файл: DimetricProjector.cs
// Назначение: Перевод спроецированных координат в экранные координаты WinForms
//             и расчёт экранных векторов осей координат
// ============================================================================
using System.Drawing;

namespace project
{
    internal static class DimetricProjector
    {
        // Переводит математические 2D-координаты (sx, sy) в пиксели панели отрисовки.
        // Ось Y на экране направлена вниз, поэтому координата sy вычитается из центра cy.
        public static PointF ToScreen(double sx, double sy, int cx, int cy, float scale)
        {
            return new PointF(cx + (float)(sx * scale), cy - (float)(sy * scale));
        }

        // Проецирует матрицу 3D-точек через матрицу диметрии и сразу переводит в экранные точки
        public static PointF[] Project3DPoints(double[,] points3D, int cx, int cy, float scale)
        {
            double[,] projected = MatrixMath.Multiply(points3D, MatrixMath.DimetricProjection());
            return ProjectedMatrixToScreen(projected, cx, cy, scale);
        }

        // Преобразует уже спроецированную матрицу Nx4 в массив экранных точек PointF[]
        public static PointF[] ProjectedMatrixToScreen(double[,] projectedMatrix, int cx, int cy, float scale)
        {
            int n = projectedMatrix.GetLength(0);
            var result = new PointF[n];
            for (int i = 0; i < n; i++)
            {
                // Нормализация на однородную координату W (для аффинных и параллельных проекций W = 1)
                double w = projectedMatrix[i, 3] != 0 ? projectedMatrix[i, 3] : 1.0;
                double sx = projectedMatrix[i, 0] / w;
                double sy = projectedMatrix[i, 1] / w;
                result[i] = ToScreen(sx, sy, cx, cy, scale);
            }
            return result;
        }

        // Возвращает экранный вектор единичного отрезка для выбранной оси (0 - X, 1 - Y, 2 - Z),
        // вычисленный строго по матрице диметрического проецирования.
        public static PointF GetAxisVector(int axisIndex, float scale)
        {
            double[,] dim = MatrixMath.DimetricProjection();
            return new PointF((float)(dim[axisIndex, 0] * scale), -(float)(dim[axisIndex, 1] * scale));
        }
    }
}

// ============================================================================
// Файл: MainForm.cs
// Назначение: Логика пользовательского интерфейса, отрисовка осей и гексаэдра,
//             обработка кнопок поворота и композиции преобразований
// ============================================================================
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace project
{
    public partial class MainForm : Form
    {
        // Масштаб: количество пикселей в одном единичном отрезке
        private const float Scale = 35f;
        // Отступ концов осей от краёв области рисования
        private const float Margin = 25f;

        private bool _drawFigure;
        // Накопленный угол поворота относительно плоскости YOZ (в радианах)
        private double _accumulatedAngle;
        // Текущие 3D-координаты вершин гексаэдра (матрица 8x4)
        private double[,] _points;
        // Спроецированные координаты вершин гексаэдра после применения композиции (матрица 8x4)
        private double[,] _projectedPoints;

        public MainForm()
        {
            InitializeComponent();
            ResetFigureState();
        }

        // Сброс гексаэдра в исходное состояние и вычисление начальной диметрической проекции
        private void ResetFigureState()
        {
            _accumulatedAngle = 0;
            _points = (double[,])Figure.InitialPoints.Clone();
            _projectedPoints = MatrixMath.Multiply(_points, MatrixMath.DimetricProjection());
            txtBxCoordMatrix.Text = MatrixMath.MatrixToString(_points);
        }

        // Клик по области рисования отображает фигуру
        private void splitContainer1_Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            _drawFigure = true;
            splitContainer1.Panel1.Invalidate();
        }

        // Основной обработчик перерисовки левой панели (оси + каркас гексаэдра)
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (!_drawFigure || _projectedPoints == null)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int cx = splitContainer1.Panel1.Width / 2;
            int cy = splitContainer1.Panel1.Height / 2;

            // Рисуем диметрические оси координат X, Y, Z
            DrawAxes(g, cx, cy, Scale);

            // Переводим спроецированные вершины в пиксели экрана
            PointF[] screenPts = DimetricProjector.ProjectedMatrixToScreen(_projectedPoints, cx, cy, Scale);

            // Отрисовка 12 рёбер гексаэдра: передняя грань, задняя грань и 4 боковых ребра
            using (Pen frontPen = new Pen(Color.Black, 2f))
            using (Pen backPen = new Pen(Color.DimGray, 1.5f) { DashStyle = DashStyle.Dash })
            {
                g.DrawPolygon(frontPen, new[] { screenPts[0], screenPts[1], screenPts[2], screenPts[3] });
                g.DrawPolygon(backPen, new[] { screenPts[4], screenPts[5], screenPts[6], screenPts[7] });

                for (int i = 0; i < 4; i++)
                    g.DrawLine(frontPen, screenPts[i], screenPts[i + 4]);
            }
        }

        // Отрисовка координатных осей X, Y, Z и единичных меток с учётом диметрического искажения
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

            // Подписи начала координат (0) и единичных отрезков (1) вдоль каждой оси
            using (Font font = new Font("Microsoft Sans Serif", 8.5f))
            using (Brush br = new SolidBrush(Color.Black))
            {
                g.DrawString("0", font, br, cx + 4, cy + 4);
                g.DrawString("1", font, br, cx + vecX.X, cy + vecX.Y + 3);
                g.DrawString("1", font, br, cx + vecY.X + 4, cy + vecY.Y - 12);
                g.DrawString("1", font, br, cx + vecZ.X - 14, cy + vecZ.Y + 2);
            }
        }

        // Вычисляет максимальную длину луча от центра (cx, cy) до границы панели с учётом отступа
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

        // Кнопка "Построить/сбросить гексаэдр"
        private void BtnDraw_Click(object sender, EventArgs e)
        {
            ResetFigureState();
            _drawFigure = true;
            splitContainer1.Panel1.Invalidate();
        }

        // Кнопка "Композиция: поворот YOZ + диметрия" — ключевая операция варианта 2.
        // Формирует единую матрицу композиции M_comp = R_YOZ * M_dim, выводит её в текстовое поле
        // и применяет напрямую к исходным координатам гексаэдра.
        private void btnComposition_Click(object sender, EventArgs e)
        {
            _accumulatedAngle += GetAngle();

            double[,] rotationYOZ = MatrixMath.RotationYOZ(_accumulatedAngle);
            double[,] dimetric = MatrixMath.DimetricProjection();
            double[,] composition = MatrixMath.ComposeTransform(rotationYOZ, dimetric);

            // Отображаем результирующую матрицу композиции 4x4 на форме
            txtBxEnterMatrix.Text = MatrixMath.MatrixToString(composition);

            // Обновляем 3D-координаты для отображения в верхнем поле и вычисляем проекцию через композицию
            _points = MatrixMath.Multiply(Figure.InitialPoints, rotationYOZ);
            _projectedPoints = MatrixMath.Multiply(Figure.InitialPoints, composition);

            txtBxCoordMatrix.Text = MatrixMath.MatrixToString(_points);
            _drawFigure = true;
            splitContainer1.Panel1.Invalidate();
        }

        // Кнопка "Применить матрицу из поля" — позволяет вручную ввести любую матрицу 4x4
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

            // Проверяем, является ли введённая матрица проекционной (третий столбец обнулён)
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

        // Сброс поля ввода матрицы к единичной матрице
        private void btnResetMatrix_Click(object sender, EventArgs e)
        {
            txtBxEnterMatrix.Text = MatrixMath.IdentityText;
        }

        // Пошаговый поворот гексаэдра относительно плоскости YOZ
        private void btnRotYOZ_Click(object sender, EventArgs e)
        {
            double angle = GetAngle();
            _accumulatedAngle += angle;
            ApplyAffineTransform(MatrixMath.RotationYOZ(angle));
        }

        // Дополнительный поворот вокруг оси Y для обзора модели
        private void btnRotY_Click(object sender, EventArgs e)
        {
            ApplyAffineTransform(MatrixMath.RotationY(GetAngle()));
        }

        // Дополнительный поворот вокруг оси Z для обзора модели
        private void btnRotZ_Click(object sender, EventArgs e)
        {
            ApplyAffineTransform(MatrixMath.RotationZ(GetAngle()));
        }

        // Применяет аффинное преобразование к 3D-вершинам и перестраивает диметрическую проекцию
        private void ApplyAffineTransform(double[,] transform)
        {
            _points = MatrixMath.Multiply(_points, transform);
            _projectedPoints = MatrixMath.Multiply(_points, MatrixMath.DimetricProjection());
            txtBxCoordMatrix.Text = MatrixMath.MatrixToString(_points);
            _drawFigure = true;
            splitContainer1.Panel1.Invalidate();
        }

        // Считывает угол из текстового поля (в градусах) и переводит в радианы
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

// ============================================================================
// Файл: Program.cs
// Назначение: Точка входа в приложение Windows Forms
// ============================================================================
using System;
using System.Windows.Forms;

namespace project
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
```