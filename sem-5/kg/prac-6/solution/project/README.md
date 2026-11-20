# Практика #6, вариант 2

```csharp
// ============================================================================
// МОДУЛЬ 1: TruncatedPyramid.cs
// Назначение: Геометрическая модель усечённой треугольной пирамиды (вариант 2)
// и её построение из простейших 2D-примитивов OpenGL (Triangles, Quads, Lines).
// ============================================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace project
{
    public class TruncatedPyramid
    {
        // Массивы трёхмерных вершин (X, Y, Z) для нижнего и верхнего треугольных оснований.
        // Хранение вершин в полях класса избавляет от пересчёта тригонометрии при каждом кадре.
        private readonly Vector3[] _bottomVertices;
        private readonly Vector3[] _topVertices;

        // Конструктор задаёт габариты фигуры:
        // bottomRadius — радиус описанной окружности нижнего (большого) основания;
        // topRadius    — радиус описанной окружности верхнего (малого) основания;
        // halfHeight   — половина высоты пирамиды (центр фигуры располагается в (0, 0, 0),
        //                благодаря чему вращение происходит строго вокруг центра объекта).
        public TruncatedPyramid(float bottomRadius = 1.3f, float topRadius = 0.55f, float halfHeight = 0.8f)
        {
            _bottomVertices = CreateTriangleRing(bottomRadius, -halfHeight);
            _topVertices = CreateTriangleRing(topRadius, halfHeight);
        }

        // Вычисление координат вершин правильного треугольника, лежащего в плоскости Y = const.
        private static Vector3[] CreateTriangleRing(float radius, float y)
        {
            Vector3[] vertices = new Vector3[3];
            // Углы 90°, 210° и 330° формируют равносторонний треугольник,
            // симметричный относительно оси Z.
            double[] anglesDeg = { 90.0, 210.0, 330.0 };

            for (int i = 0; i < 3; i++)
            {
                double rad = anglesDeg[i] * Math.PI / 180.0;
                float x = (float)(radius * Math.Cos(rad));
                float z = (float)(radius * Math.Sin(rad));
                vertices[i] = new Vector3(x, y, z);
            }

            return vertices;
        }

        // Основной метод отрисовки объёмного тела.
        public void Draw()
        {
            // Включаем смещение полигонов (PolygonOffsetFill), чтобы при отрисовке
            // чёрных рёбер поверх залитых граней не возникало «мерцания» (Z-fighting)
            // из-за совпадения значений глубины в Z-буфере.
            GL.Enable(EnableCap.PolygonOffsetFill);
            GL.PolygonOffset(1.0f, 1.0f);

            DrawFilledFaces();

            GL.Disable(EnableCap.PolygonOffsetFill);

            DrawEdges();
        }

        // Отрисовка всех 5 граней усечённой пирамиды с помощью 2D-примитивов OpenGL.
        private void DrawFilledFaces()
        {
            // 1. Верхнее и нижнее основания строятся из треугольных примитивов (GL_TRIANGLES).
            // Порядок обхода вершин задаётся против часовой стрелки (CCW) при взгляде снаружи.
            GL.Begin(PrimitiveType.Triangles);

            // Верхнее основание
            SetTopColor();
            GL.Vertex3(_topVertices[0]);
            GL.Vertex3(_topVertices[1]);
            GL.Vertex3(_topVertices[2]);

            // Нижнее основание
            SetBottomColor();
            GL.Vertex3(_bottomVertices[0]);
            GL.Vertex3(_bottomVertices[2]);
            GL.Vertex3(_bottomVertices[1]);

            GL.End();

            // 2. Три боковые грани (равнобедренные трапеции) строятся из четырёхугольников (GL_QUADS).
            // Разный цвет у нижних и верхних вершин при включённой интерполяции (ShadingModel.Smooth)
            // создаёт плавный светотеневой градиент, подчёркивающий объём граней.
            GL.Begin(PrimitiveType.Quads);
            for (int i = 0; i < 3; i++)
            {
                int next = (i + 1) % 3;

                SetBottomColor();
                GL.Vertex3(_bottomVertices[i]);
                GL.Vertex3(_bottomVertices[next]);

                SetTopColor();
                GL.Vertex3(_topVertices[next]);
                GL.Vertex3(_topVertices[i]);
            }
            GL.End();
        }

        // Отрисовка рёбер многогранника линиями для чёткого выделения геометрии.
        private void DrawEdges()
        {
            GL.LineWidth(2.0f);
            GL.Color3(0.12f, 0.12f, 0.12f);

            // Замкнутый контур верхнего основания (GL_LINE_LOOP)
            GL.Begin(PrimitiveType.LineLoop);
            for (int i = 0; i < 3; i++)
            {
                GL.Vertex3(_topVertices[i]);
            }
            GL.End();

            // Замкнутый контур нижнего основания (GL_LINE_LOOP)
            GL.Begin(PrimitiveType.LineLoop);
            for (int i = 0; i < 3; i++)
            {
                GL.Vertex3(_bottomVertices[i]);
            }
            GL.End();

            // Три боковых ребра, соединяющих нижние и верхние вершины (GL_LINES)
            GL.Begin(PrimitiveType.Lines);
            for (int i = 0; i < 3; i++)
            {
                GL.Vertex3(_bottomVertices[i]);
                GL.Vertex3(_topVertices[i]);
            }
            GL.End();
        }

        private static void SetTopColor()
        {
            GL.Color3(0.84f, 0.16f, 0.16f);
        }

        private static void SetBottomColor()
        {
            GL.Color3(0.96f, 0.56f, 0.54f);
        }
    }

    // ============================================================================
    // МОДУЛЬ 2: SceneRenderer.cs
    // Назначение: Инициализация состояния OpenGL, работа с Z-буфером, настройка
    // перспективной проекции glFrustum и управление матричным стеком.
    // ============================================================================
    public class SceneRenderer
    {
        private readonly TruncatedPyramid _pyramid;

        public SceneRenderer(TruncatedPyramid pyramid)
        {
            _pyramid = pyramid;
        }

        // Начальная конфигурация графического конвейера OpenGL (вызывается один раз при загрузке).
        public void Initialize()
        {
            // Цвет очистки буфера кадра (белый фон сцены).
            GL.ClearColor(Color.White);

            // Включение теста глубины (алгоритм Z-буфера):
            // каждый пиксель проверяется по координате глубины, и невидимые задние грани отсекаются.
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            // Плавная интерполяция цветов между вершинами примитива (закраска по Гуро).
            GL.ShadeModel(ShadingModel.Smooth);
            GL.Enable(EnableCap.LineSmooth);
        }

        // Настройка области вывода и матрицы перспективной проекции при создании/изменении окна.
        public void UpdateProjection(int width, int height)
        {
            if (height <= 0)
            {
                height = 1;
            }

            // Привязка области вывода OpenGL к размерам клиентской области GLControl.
            GL.Viewport(0, 0, width, height);

            // Вычисляем соотношение сторон окна, чтобы при растягивании формы
            // геометрические пропорции пирамиды не искажались.
            double aspectRatio = (double)width / height;
            double near = 1.5;   // Расстояние до ближней плоскости отсечения (> 0)
            double far = 20.0;   // Расстояние до дальней плоскости отсечения (> near)
            double top = 1.0;
            double bottom = -1.0;
            double left = -top * aspectRatio;
            double right = top * aspectRatio;

            // Переключаемся на матрицу проекций и сбрасываем её в единичную перед заданием объёма видимости.
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            // Задаём усечённую пирамиду видимости (перспективная проекция).
            GL.Frustum(left, right, bottom, top, near, far);

            // Возвращаемся в режим видовой матрицы (GL_MODELVIEW) для дальнейшей работы со сценой.
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        // Отрисовка одного кадра сцены.
        public void Render(RotationController controller)
        {
            // Очищаем одновременно буфер цвета и Z-буфер (буфер глубины) перед рисованием нового кадра.
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Сохраняем текущую видовую матрицу в стек (glPushMatrix),
            // чтобы преобразования текущего объекта не накапливались между кадрами.
            GL.PushMatrix();

            // Переносим систему координат вглубь экрана (по отрицательной оси Z),
            // чтобы фигура попала внутрь усечённой пирамиды видимости [ -far; -near ].
            GL.Translate(0.0f, 0.0f, -4.5f);

            // Выполняем последовательные повороты системы координат вокруг осей X, Y и Z
            // на углы, накопленные в контроллере вращения.
            GL.Rotate(controller.AngleX, 1.0f, 0.0f, 0.0f);
            GL.Rotate(controller.AngleY, 0.0f, 1.0f, 0.0f);
            GL.Rotate(controller.AngleZ, 0.0f, 0.0f, 1.0f);

            // Отрисовываем пирамиду в преобразованной системе координат.
            _pyramid.Draw();

            // Восстанавливаем исходную матрицу из стека (glPopMatrix).
            GL.PopMatrix();
        }
    }

    // ============================================================================
    // МОДУЛЬ 3: RotationController.cs
    // Назначение: Хранение углов ориентации объекта в пространстве, обработка
    // шага анимации по таймеру и обработка нажатий клавиш управления курсором.
    // ============================================================================
    public class RotationController
    {
        private const float ManualStep = 5.0f;  // Шаг поворота при нажатии клавиши (в градусах)
        private const float TimerStepY = 1.5f;  // Шаг автоповорота за один тик таймера (в градусах)

        public float AngleX { get; private set; }
        public float AngleY { get; private set; }
        public float AngleZ { get; private set; }
        public bool IsAutoRotating { get; private set; }

        public RotationController()
        {
            Reset();
        }

        // Установка начального ракурса, при котором хорошо видны верхнее основание и боковые грани.
        public void Reset()
        {
            AngleX = 32.0f;
            AngleY = -28.0f;
            AngleZ = 0.0f;
            IsAutoRotating = true;
        }

        // Обновление угла при срабатывании события Timer.Tick.
        public void OnTimerTick()
        {
            if (!IsAutoRotating)
            {
                return;
            }

            AngleY = NormalizeAngle(AngleY + TimerStepY);
        }

        // Обработка клавиш управления курсором и вспомогательных клавиш управления сценой.
        public bool HandleKeyDown(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.Up:
                    AngleX = NormalizeAngle(AngleX - ManualStep);
                    return true;
                case Keys.Down:
                    AngleX = NormalizeAngle(AngleX + ManualStep);
                    return true;
                case Keys.Left:
                    AngleY = NormalizeAngle(AngleY - ManualStep);
                    return true;
                case Keys.Right:
                    AngleY = NormalizeAngle(AngleY + ManualStep);
                    return true;
                case Keys.PageUp:
                    AngleZ = NormalizeAngle(AngleZ + ManualStep);
                    return true;
                case Keys.PageDown:
                    AngleZ = NormalizeAngle(AngleZ - ManualStep);
                    return true;
                case Keys.Space:
                    // Пауза / возобновление вращения по таймеру
                    IsAutoRotating = !IsAutoRotating;
                    return true;
                case Keys.R:
                    // Возврат фигуры в исходное положение
                    Reset();
                    return true;
                default:
                    return false;
            }
        }

        // Нормализация угла в диапазон [0; 360) для предотвращения переполнения значений.
        private static float NormalizeAngle(float angle)
        {
            angle %= 360.0f;
            if (angle < 0.0f)
            {
                angle += 360.0f;
            }
            return angle;
        }
    }

    // ============================================================================
    // МОДУЛЬ 4: MainForm.cs
    // Назначение: Главная форма Windows Forms, связывающая компонент OpenTK.GLControl,
    // компонент System.Windows.Forms.Timer и события клавиатуры.
    // ============================================================================
    public partial class MainForm : Form
    {
        private readonly SceneRenderer _renderer;
        private readonly RotationController _controller;
        private readonly Timer _animationTimer;

        public MainForm()
        {
            InitializeComponent();

            TruncatedPyramid pyramid = new TruncatedPyramid();
            _renderer = new SceneRenderer(pyramid);
            _controller = new RotationController();

            // Настройка таймера анимации (интервал 30 мс обеспечивает плавные ~33 кадра/с).
            _animationTimer = new Timer();
            _animationTimer.Interval = 30;
            _animationTimer.Tick += AnimationTimer_Tick;

            // Подписка на события жизненного цикла OpenGL-холста и клавиатуры.
            glControl.Load += GlControl_Load;
            glControl.Resize += GlControl_Resize;
            glControl.Paint += GlControl_Paint;
            glControl.PreviewKeyDown += GlControl_PreviewKeyDown;
            KeyDown += MainForm_KeyDown;
            FormClosing += MainForm_FormClosing;
        }

        // Срабатывает при создании OpenGL-контекста элемента управления glControl.
        private void GlControl_Load(object sender, EventArgs e)
        {
            glControl.MakeCurrent();
            _renderer.Initialize();
            _renderer.UpdateProjection(glControl.Width, glControl.Height);
            _animationTimer.Start();
        }

        // Срабатывает при изменении размеров окна формы.
        private void GlControl_Resize(object sender, EventArgs e)
        {
            if (!glControl.IsHandleCreated)
            {
                return;
            }

            glControl.MakeCurrent();
            _renderer.UpdateProjection(glControl.Width, glControl.Height);
            glControl.Invalidate();
        }

        // Срабатывает при перерисовке элемента glControl.
        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            if (!glControl.IsHandleCreated)
            {
                return;
            }

            glControl.MakeCurrent();
            _renderer.Render(_controller);

            // Переключение переднего и заднего буферов (двойная буферизация устраняет мерцание).
            glControl.SwapBuffers();
        }

        // Обработчик тика таймера: обновляет угол поворота и запрашивает перерисовку окна.
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            _controller.OnTimerTick();
            glControl.Invalidate();
        }

        // Разрешаем передачу нажатий клавиш-стрелок в стандартное событие KeyDown
        // (по умолчанию WinForms использует стрелки для навигации фокуса между контролами).
        private void GlControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }

        // Обработчик нажатий клавиш клавиатуры.
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Выход из программы по клавише Escape (согласно примеру в методических указаниях).
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
                return;
            }

            if (_controller.HandleKeyDown(e.KeyCode))
            {
                glControl.Invalidate();
            }
        }

        // Корректная остановка и освобождение ресурсов таймера при закрытии формы.
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _animationTimer.Stop();
            _animationTimer.Dispose();
        }
    }
}
```