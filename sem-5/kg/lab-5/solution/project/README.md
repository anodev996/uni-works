# Лабораторная #5, вариант 2

```csharp
// ============================================================================
// МОДУЛЬ 1: Program.cs — Точка входа в приложение
// ============================================================================
using System;
using System.Windows.Forms;

namespace project
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// Атрибут [STAThread] обязателен для Windows Forms: он указывает,
        /// что приложение использует однопоточную модель апартаментов (STA),
        /// необходимую для корректной работы оконных компонентов и буфера обмена.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Включаем современное визуальное оформление элементов управления ОС
            Application.EnableVisualStyles();
            // Используем стандартный механизм отрисовки текста GDI+
            Application.SetCompatibleTextRenderingDefault(false);
            // Запускаем главный цикл обработки сообщений (Message Loop) и открываем MainForm
            Application.Run(new MainForm());
        }
    }
}


// ============================================================================
// МОДУЛЬ 2: SceneState.cs — Модель данных (состояние сцены)
// ============================================================================
// ПРИМЕЧАНИЕ ПРЕПОДАВАТЕЛЯ:
// Обратите внимание на правильную архитектуру: мы отделяем данные (состояние)
// от логики отрисовки (SceneRenderer) и обработки событий окна (MainForm).
// Это избавляет код от «каши» и позволяет легко сбросить сцену одним методом.
namespace project
{
    public class SceneState
    {
        // --- Параметры Фигуры A (восьмиугольник с вписанным квадратом) ---
        public float Ax { get; set; }         // Смещение по оси X относительно центра орбиты
        public float Ay { get; set; }         // Смещение по оси Y относительно центра орбиты
        public float AngleA { get; set; }     // Угол собственного вращения фигуры A (в градусах)
        public float ScaleA { get; set; }     // Коэффициент масштабирования фигуры A
        public float OrbitAngle { get; set; } // Угол орбитального вращения фигуры A вокруг фигуры B

        // --- Параметры Фигуры B (центральная опорная фигура) ---
        public float Bx { get; set; }         // Позиция фигуры B по оси X в мировых координатах
        public float By { get; set; }         // Позиция фигуры B по оси Y в мировых координатах
        public float AngleB { get; set; }     // Угол собственного вращения фигуры B (в градусах)
        public float ScaleB { get; set; }     // Коэффициент масштабирования фигуры B

        // --- Режим отрисовки ---
        // Если true — рисуется веер (каскад) из 28 копий фигуры A вдоль орбиты
        public bool LoopMode { get; set; }

        public SceneState()
        {
            // При создании объекта сразу инициализируем его начальными значениями
            Reset();
        }

        /// <summary>
        /// Возвращает все координаты, углы и масштабы к исходным значениям.
        /// </summary>
        public void Reset()
        {
            // Фигура A изначально сдвинута влево на 1.25 единицы от центра фигуры B
            Ax = -1.25f;
            Ay = 0.0f;
            AngleA = 0.0f;
            ScaleA = 1.0f;
            OrbitAngle = 0.0f;

            // Фигура B находится строго в начале координат (0, 0)
            Bx = 0.0f;
            By = 0.0f;
            AngleB = 0.0f;
            ScaleB = 1.0f;

            LoopMode = false;
        }
    }
}


// ============================================================================
// МОДУЛЬ 3: SceneRenderer.cs — Графический движок отрисовки (OpenGL)
// ============================================================================
using OpenTK.Graphics.OpenGL;
using System;

namespace project
{
    public class SceneRenderer
    {
        /// <summary>
        /// Первичная настройка состояния машины состояний OpenGL.
        /// Вызывается один раз после создания контекста OpenGL.
        /// </summary>
        public void Init(int width, int height)
        {
            // Задаем цвет очистки экрана (RGBA): чистый белый фон
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);

            // Включаем сглаживание (антиалиасинг) для точек и линий,
            // чтобы контуры не выглядели «лесенкой»
            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.LineSmooth);

            // Настраиваем область вывода и матрицу проекции под текущие размеры окна
            Resize(width, height);
        }

        /// <summary>
        /// Настройка области вывода (Viewport) и ортографической проекции
        /// при изменении размеров окна.
        /// </summary>
        public void Resize(int width, int height)
        {
            // Защита от деления на ноль, если окно свернули в полоску высотой 0 пикселей
            if (height == 0)
            {
                height = 1;
            }

            // Указываем OpenGL рисовать на всю площадь элемента управления
            GL.Viewport(0, 0, width, height);

            // Переключаемся на матрицу проекции, чтобы задать систему координат сцены
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity(); // Сбрасываем матрицу в единичную

            // Вычисляем соотношение сторон окна (aspect ratio).
            // Это критически важно: если умножить горизонтальные границы на aspect,
            // наши фигуры сохранят правильные пропорции и не растянутся в овалы
            // при растягивании окна по ширине.
            double aspect = (double)width / height;
            GL.Ortho(-2.4 * aspect, 2.4 * aspect, -2.4, 2.4, -1.0, 1.0);

            // Возвращаемся в режим модельно-видовой матрицы для дальнейшей работы с объектами
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        /// <summary>
        /// Главный метод отрисовки одного кадра сцены.
        /// </summary>
        public void Render(SceneState state)
        {
            // Очищаем буфер цвета перед рисованием нового кадра
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.LoadIdentity();

            // --- 1. Отрисовка Фигуры B ---
            // ВАЖНО: Оборачиваем трансформации в PushMatrix() / PopMatrix().
            // Это изолирует матрицу Фигуры B: её сдвиги, повороты и масштаб
            // не повлияют на последующую отрисовку других объектов.
            GL.PushMatrix();
            GL.Translate(state.Bx, state.By, 0.0f);         // Перенос в точку (Bx, By)
            GL.Rotate(state.AngleB, 0.0f, 0.0f, 1.0f);      // Поворот вокруг оси Z (направлена на нас)
            GL.Scale(state.ScaleB, state.ScaleB, 1.0f);     // Масштабирование по X и Y
            DrawFigureB();
            GL.PopMatrix(); // Возвращаем исходную единичную матрицу

            // --- 2. Отрисовка Фигуры A (одиночной или в режиме каскада) ---
            if (state.LoopMode)
            {
                // В циклическом режиме рисуем 28 копий фигуры A с шагом 10 градусов
                int count = 28;
                float stepAngle = 10.0f;

                for (int i = 0; i < count; i++)
                {
                    DrawTransformedFigureA(state, state.OrbitAngle + i * stepAngle);
                }
            }
            else
            {
                // Обычный режим: рисуем одну фигуру A под текущим орбитальным углом
                DrawTransformedFigureA(state, state.OrbitAngle);
            }
        }

        /// <summary>
        /// Применяет иерархию аффинных преобразований к Фигуре A и рисует её.
        /// </summary>
        private void DrawTransformedFigureA(SceneState state, float currentOrbitAngle)
        {
            GL.PushMatrix();

            // В OpenGL матрицы преобразований умножаются справа, поэтому для точки
            // они применяются в ОБРАТНОМ порядке (снизу вверх по коду):
            // 1. Сначала фигура масштабируется (Scale) и крутится вокруг своего центра (AngleA).
            // 2. Затем сдвигается на радиус орбиты (Ax, Ay).
            // 3. Затем поворачивается по орбите вокруг начала координат (currentOrbitAngle).
            // 4. И наконец, вся эта конструкция переносится в центр Фигуры B (Bx, By)!
            // Именно такой порядок создает эффект «спутника», вращающегося вокруг планеты B.
            GL.Translate(state.Bx, state.By, 0.0f);
            GL.Rotate(currentOrbitAngle, 0.0f, 0.0f, 1.0f);
            GL.Translate(state.Ax, state.Ay, 0.0f);
            GL.Rotate(state.AngleA, 0.0f, 0.0f, 1.0f);
            GL.Scale(state.ScaleA, state.ScaleA, 1.0f);

            DrawFigureA();

            GL.PopMatrix();
        }

        /// <summary>
        /// Геометрия Фигуры A: правильный восьмиугольник с вписанным квадратом.
        /// </summary>
        private void DrawFigureA()
        {
            int n = 8;              // Количество вершин восьмиугольника
            float radius = 0.55f;   // Радиус описанной окружности
            float[] vx = new float[n];
            float[] vy = new float[n];

            // Вычисляем координаты вершин по параметрическому уравнению окружности.
            // Начинаем с угла Math.PI / 2.0 (90 градусов), чтобы нулевая вершина
            // смотрела строго вертикально вверх.
            for (int i = 0; i < n; i++)
            {
                double angle = Math.PI / 2.0 + i * (2.0 * Math.PI / n);
                vx[i] = (float)(radius * Math.Cos(angle));
                vy[i] = (float)(radius * Math.Sin(angle));
            }

            // 1. Заливка внешнего восьмиугольника светло-сиреневым цветом
            GL.Color3(0.92f, 0.88f, 0.98f);
            GL.Begin(PrimitiveType.Polygon);
            for (int i = 0; i < n; i++)
            {
                GL.Vertex2(vx[i], vy[i]);
            }
            GL.End();

            // 2. Заливка вписанного квадрата (соединяем чётные вершины: 0, 2, 4, 6)
            GL.Color3(0.96f, 0.68f, 0.68f);
            GL.Begin(PrimitiveType.Polygon);
            GL.Vertex2(vx[0], vy[0]);
            GL.Vertex2(vx[2], vy[2]);
            GL.Vertex2(vx[4], vy[4]);
            GL.Vertex2(vx[6], vy[6]);
            GL.End();

            // 3. Главные диагонали квадрата (оси симметрии)
            GL.LineWidth(1.2f);
            GL.Color3(0.15f, 0.15f, 0.15f);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(vx[0], vy[0]);
            GL.Vertex2(vx[4], vy[4]);
            GL.Vertex2(vx[2], vy[2]);
            GL.Vertex2(vx[6], vy[6]);
            GL.End();

            // 4. Внешний контур восьмиугольника (замкнутая ломаная LineLoop)
            GL.LineWidth(3.0f);
            GL.Color3(0.45f, 0.10f, 0.78f);
            GL.Begin(PrimitiveType.LineLoop);
            for (int i = 0; i < n; i++)
            {
                GL.Vertex2(vx[i], vy[i]);
            }
            GL.End();

            // 5. Контур вписанного квадрата
            GL.LineWidth(3.0f);
            GL.Color3(0.92f, 0.25f, 0.15f);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(vx[0], vy[0]);
            GL.Vertex2(vx[2], vy[2]);
            GL.Vertex2(vx[4], vy[4]);
            GL.Vertex2(vx[6], vy[6]);
            GL.End();

            // 6. Узловые точки по всем 8 вершинам
            GL.PointSize(5.0f);
            GL.Color3(0.2f, 0.2f, 0.2f);
            GL.Begin(PrimitiveType.Points);
            for (int i = 0; i < n; i++)
            {
                GL.Vertex2(vx[i], vy[i]);
            }
            GL.End();

            // 7. Центральная точка (локальный центр вращения фигуры A)
            GL.PointSize(6.0f);
            GL.Color3(0.0f, 0.15f, 0.75f);
            GL.Begin(PrimitiveType.Points);
            GL.Vertex2(0.0f, 0.0f);
            GL.End();
        }

        /// <summary>
        /// Геометрия Фигуры B: стилизованный контур буквы «В» из отрезков и опорных точек.
        /// </summary>
        private void DrawFigureB()
        {
            GL.LineWidth(2.5f);
            GL.Color3(0.0f, 0.0f, 0.0f);

            // PrimitiveType.Lines воспринимает каждую пару вершин как отдельный отрезок
            GL.Begin(PrimitiveType.Lines);

            // Левая внешняя вертикаль
            GL.Vertex2(-0.35f, 0.55f);
            GL.Vertex2(-0.35f, -0.55f);

            // Внутренняя вертикальная перемычка
            GL.Vertex2(0.0f, 0.375f);
            GL.Vertex2(0.0f, -0.375f);

            // Верхняя и нижняя горизонтальные перемычки
            GL.Vertex2(-0.35f, 0.375f);
            GL.Vertex2(0.0f, 0.375f);

            GL.Vertex2(-0.35f, -0.375f);
            GL.Vertex2(0.0f, -0.375f);

            // Верхний «лепесток» (две наклонные грани)
            GL.Vertex2(-0.35f, 0.55f);
            GL.Vertex2(0.40f, 0.175f);

            GL.Vertex2(0.40f, 0.175f);
            GL.Vertex2(0.0f, 0.0f);

            // Нижний «лепесток» (две наклонные грани)
            GL.Vertex2(0.0f, 0.0f);
            GL.Vertex2(0.40f, -0.175f);

            GL.Vertex2(0.40f, -0.175f);
            GL.Vertex2(-0.35f, -0.55f);

            GL.End();

            // Выделяем жирными точками ключевые узлы вертикальных стоек
            GL.PointSize(9.0f);
            GL.Begin(PrimitiveType.Points);
            GL.Vertex2(-0.35f, 0.55f);
            GL.Vertex2(-0.35f, -0.55f);
            GL.Vertex2(0.0f, 0.375f);
            GL.Vertex2(0.0f, -0.375f);
            GL.End();
        }
    }
}


// ============================================================================
// МОДУЛЬ 4: MainForm.cs — Главная форма и обработка пользовательского ввода
// ============================================================================
using System;
using System.Drawing;
using System.Windows.Forms;

namespace project
{
    public partial class MainForm : Form
    {
        private readonly SceneState state;       // Хранит текущие координаты и углы
        private readonly SceneRenderer renderer; // Отвечает за вызовы OpenGL
        private Point lastMousePos;              // Предыдущая позиция курсора для расчёта дельты
        private bool glReady;                    // Флаг готовности контекста OpenGL

        public MainForm()
        {
            InitializeComponent();

            state = new SceneState();
            renderer = new SceneRenderer();

            // ВАЖНО: KeyPreview = true заставляет форму перехватывать нажатия клавиш
            // ДО того, как они попадут в дочерний элемент glControl. Без этого свойства
            // клавиши со стрелками могут не срабатывать в KeyDown.
            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;

            // Подписываемся на жизненный цикл и события мыши области OpenGL
            glControl.Paint += glControl_Paint;
            glControl.Resize += glControl_Resize;
            glControl.MouseDown += glControl_MouseDown;
            glControl.MouseMove += glControl_MouseMove;
        }

        /// <summary>
        /// Вызывается при загрузке формы, когда дескриптор окна и контекст OpenGL уже созданы.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Делаем контекст OpenGL текущего окна активным в данном потоке
            glControl.MakeCurrent();
            renderer.Init(glControl.Width, glControl.Height);
            glReady = true;
        }

        private void glControl_Resize(object sender, EventArgs e)
        {
            // Игнорируем событие, если OpenGL ещё не инициализирован
            if (!glReady)
            {
                return;
            }

            glControl.MakeCurrent();
            renderer.Resize(glControl.Width, glControl.Height);
            // Запрашиваем перерисовку окна после изменения размеров
            glControl.Invalidate();
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            if (!glReady)
            {
                return;
            }

            glControl.MakeCurrent();
            renderer.Render(state);

            // Используется двойная буферизация: вся отрисовка шла в скрытый задний буфер.
            // SwapBuffers() мгновенно меняет задний и передний буферы местами,
            // что полностью исключает мерцание изображения.
            glControl.SwapBuffers();
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            // Запоминаем точку начала перетаскивания
            lastMousePos = e.Location;
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            // Если ни одна кнопка мыши не зажата — ничего не делаем
            if (e.Button == MouseButtons.None)
            {
                return;
            }

            // Переводим смещение курсора из пикселей в мировые координаты OpenGL (коэф. 0.01).
            // Обратите внимание на ось Y: в Windows ось Y направлена ВНИЗ, а в OpenGL — ВВЕРХ,
            // поэтому вычитаем текущую координату из предыдущей (lastMousePos.Y - e.Y).
            float dx = (e.X - lastMousePos.X) * 0.01f;
            float dy = (lastMousePos.Y - e.Y) * 0.01f;

            if (e.Button == MouseButtons.Left)
            {
                // Левая кнопка двигает Фигуру A
                state.Ax += dx;
                state.Ay += dy;
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Правая кнопка двигает Фигуру B
                state.Bx += dx;
                state.By += dy;
            }

            lastMousePos = e.Location;

            // Сообщаем Windows Forms, что содержимое glControl устарело и нужно вызвать Paint
            glControl.Invalidate();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            float moveStep = 0.08f;   // Шаг перемещения
            float rotateStep = 5.0f;  // Шаг поворота (в градусах)
            float scaleStep = 0.08f;  // Шаг масштабирования

            switch (e.KeyCode)
            {
                // --- Орбитальное вращение Фигуры A вокруг Фигуры B ---
                case Keys.Q:
                    state.OrbitAngle += rotateStep;
                    break;
                case Keys.E:
                    state.OrbitAngle -= rotateStep;
                    break;

                // --- Управление Фигурой A (WASD + Z/X + R/F) ---
                case Keys.W:
                    state.Ay += moveStep;
                    break;
                case Keys.S:
                    state.Ay -= moveStep;
                    break;
                case Keys.A:
                    state.Ax -= moveStep;
                    break;
                case Keys.D:
                    state.Ax += moveStep;
                    break;
                case Keys.Z:
                    state.AngleA += rotateStep;
                    break;
                case Keys.X:
                    state.AngleA -= rotateStep;
                    break;
                case Keys.R:
                    state.ScaleA += scaleStep;
                    break;
                case Keys.F:
                    // Ограничиваем минимальный масштаб, чтобы фигура не вывернулась наизнанку
                    if (state.ScaleA > 0.2f)
                    {
                        state.ScaleA -= scaleStep;
                    }
                    break;

                // --- Управление Фигурой B (Стрелки + N/M + I/K) ---
                case Keys.Up:
                    state.By += moveStep;
                    break;
                case Keys.Down:
                    state.By -= moveStep;
                    break;
                case Keys.Left:
                    state.Bx -= moveStep;
                    break;
                case Keys.Right:
                    state.Bx += moveStep;
                    break;
                case Keys.N:
                    state.AngleB += rotateStep;
                    break;
                case Keys.M:
                    state.AngleB -= rotateStep;
                    break;
                case Keys.I:
                    state.ScaleB += scaleStep;
                    break;
                case Keys.K:
                    if (state.ScaleB > 0.2f)
                    {
                        state.ScaleB -= scaleStep;
                    }
                    break;

                // --- Системные клавиши ---
                case Keys.Space:
                    // Инвертируем флаг каскадной отрисовки
                    state.LoopMode = !state.LoopMode;
                    break;
                case Keys.C:
                    // Полный сброс сцены
                    state.Reset();
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
            }

            // После любого изменения параметров обязательно инициируем перерисовку кадра
            glControl.Invalidate();
        }
    }
}
```