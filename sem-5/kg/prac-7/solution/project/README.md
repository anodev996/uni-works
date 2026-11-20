# Практика #7, вариант 2

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace project
{
    // ============================================================================
    // МОДУЛЬ 1: RectangularParallelepiped.cs
    // Назначение: Геометрическая модель прямоугольного параллелепипеда (согласно
    // рисунку варианта задания) и его построение из простейших примитивов OpenGL
    // (GL_QUADS) с обязательным заданием векторов нормалей для каждой грани.
    // ============================================================================
    public class RectangularParallelepiped
    {
        // Половины линейных размеров параллелепипеда вдоль осей X, Y и Z.
        // Размещение центра фигуры в точке (0, 0, 0) гарантирует, что повороты
        // вокруг координатных осей будут происходить строго относительно центра масс.
        private readonly float _halfWidth;
        private readonly float _halfHeight;
        private readonly float _halfDepth;

        public RectangularParallelepiped(float halfWidth = 1.25f, float halfHeight = 0.32f, float halfDepth = 0.65f)
        {
            _halfWidth = halfWidth;
            _halfHeight = halfHeight;
            _halfDepth = halfDepth;
        }

        // Метод отрисовки объёмного тела с использованием примитива PrimitiveType.Quads (GL_QUADS).
        // Важное правило модели освещения OpenGL: для корректного расчёта освещённости
        // перед вершинами каждой грани необходимо указать единичный вектор нормали (GL.Normal3),
        // направленный строго наружу от поверхности, а вершины обходить против часовой стрелки (CCW).
        public void Draw()
        {
            float w = _halfWidth;
            float h = _halfHeight;
            float d = _halfDepth;

            GL.Begin(PrimitiveType.Quads);

            // 1. Верхняя грань (+Y): нормаль направлена вертикально вверх (0, 1, 0).
            GL.Normal3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(new Vector3(-w, h, d));
            GL.Vertex3(new Vector3(w, h, d));
            GL.Vertex3(new Vector3(w, h, -d));
            GL.Vertex3(new Vector3(-w, h, -d));

            // 2. Нижняя грань (-Y): нормаль направлена вертикально вниз (0, -1, 0).
            GL.Normal3(0.0f, -1.0f, 0.0f);
            GL.Vertex3(new Vector3(-w, -h, -d));
            GL.Vertex3(new Vector3(w, -h, -d));
            GL.Vertex3(new Vector3(w, -h, d));
            GL.Vertex3(new Vector3(-w, -h, d));

            // 3. Передняя грань (+Z): нормаль направлена к наблюдателю (0, 0, 1).
            GL.Normal3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(new Vector3(-w, -h, d));
            GL.Vertex3(new Vector3(w, -h, d));
            GL.Vertex3(new Vector3(w, h, d));
            GL.Vertex3(new Vector3(-w, h, d));

            // 4. Задняя грань (-Z): нормаль направлена от наблюдателя (0, 0, -1).
            GL.Normal3(0.0f, 0.0f, -1.0f);
            GL.Vertex3(new Vector3(w, -h, -d));
            GL.Vertex3(new Vector3(-w, -h, -d));
            GL.Vertex3(new Vector3(-w, h, -d));
            GL.Vertex3(new Vector3(w, h, -d));

            // 5. Правая боковая грань (+X): нормаль направлена вправо (1, 0, 0).
            GL.Normal3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(new Vector3(w, -h, d));
            GL.Vertex3(new Vector3(w, -h, -d));
            GL.Vertex3(new Vector3(w, h, -d));
            GL.Vertex3(new Vector3(w, h, d));

            // 6. Левая боковая грань (-X): нормаль направлена влево (-1, 0, 0).
            GL.Normal3(-1.0f, 0.0f, 0.0f);
            GL.Vertex3(new Vector3(-w, -h, -d));
            GL.Vertex3(new Vector3(-w, -h, d));
            GL.Vertex3(new Vector3(-w, h, d));
            GL.Vertex3(new Vector3(-w, h, -d));

            GL.End();
        }
    }

    // ============================================================================
    // МОДУЛЬ 2: LightingMaterialController.cs
    // Назначение: Инкапсуляция параметров источника света GL_LIGHT0 и оптических
    // свойств материала поверхности (GL_AMBIENT, GL_DIFFUSE, GL_SPECULAR,
    // GL_EMISSION, GL_SHININESS), а также управление положением источника света.
    // ============================================================================
    public enum MaterialMode
    {
        Ambient,   // Фоновое (рассеянное в тени) отражение
        Diffuse,   // Диффузное (матовое) отражение по закону Ламберта
        Specular,  // Зеркальное отражение (глянцевый блик по закону Фонга)
        Emission   // Собственное излучение света поверхностью объекта
    }

    public enum LightAxis
    {
        X,
        Y,
        Z
    }

    public class LightingMaterialController
    {
        private const float DefaultCoordinate = 2.5f;
        private const float SceneDepthOffset = -4.5f;

        public MaterialMode CurrentMaterial { get; private set; }
        public LightAxis CurrentLightAxis { get; private set; }
        public float LightCoordinate { get; private set; }
        public bool IsPositionalLight { get; private set; }

        public LightingMaterialController()
        {
            Reset();
        }

        // Сброс настроек света и материала в исходное состояние.
        public void Reset()
        {
            CurrentMaterial = MaterialMode.Diffuse;
            CurrentLightAxis = LightAxis.X;
            LightCoordinate = DefaultCoordinate;
            IsPositionalLight = true;
        }

        public void SetMaterialMode(MaterialMode mode)
        {
            CurrentMaterial = mode;
        }

        // Переключение оси расположения источника света (пункты меню "Свет -> X / Y / Z").
        public void SetLightAxis(LightAxis axis)
        {
            CurrentLightAxis = axis;
            LightCoordinate = DefaultCoordinate;
        }

        // Смещение источника света вдоль выбранной оси с клавиатуры.
        public void MoveLight(float delta)
        {
            LightCoordinate = Math.Max(-6.0f, Math.Min(6.0f, LightCoordinate + delta));
        }

        // Переключение однородной координаты w в векторе GL_POSITION:
        // w = 1.0f — точечный источник в заданной точке пространства (x, y, z);
        // w = 0.0f — бесконечно удалённый направленный источник (учитывается только направление).
        public void ToggleLightType()
        {
            IsPositionalLight = !IsPositionalLight;
        }

        // Установка координат источника света GL_LIGHT0.
        // Метод должен вызываться после сброса видовой матрицы (GL.LoadIdentity),
        // чтобы источник света оставался зафиксированным в мировой системе координат,
        // пока сама фигура вращается.
        public void ApplyLightPosition()
        {
            float w = IsPositionalLight ? 1.0f : 0.0f;
            float zBase = IsPositionalLight ? SceneDepthOffset : 0.0f;
            float[] position;

            switch (CurrentLightAxis)
            {
                case LightAxis.X:
                    position = new[] { LightCoordinate, 0.0f, zBase, w };
                    break;
                case LightAxis.Y:
                    position = new[] { 0.0f, LightCoordinate, zBase, w };
                    break;
                default:
                    position = new[] { 0.0f, 0.0f, zBase + LightCoordinate, w };
                    break;
            }

            GL.Light(LightName.Light0, LightParameter.Position, position);
        }

        // Переопределение свойств материала объекта (GL_FRONT_AND_BACK)
        // и световых характеристик источника GL_LIGHT0 в соответствии с выбранным режимом.
        public void ApplyLightAndMaterial()
        {
            float[] lightAmbient;
            float[] lightDiffuse;
            float[] lightSpecular;

            float[] matAmbient;
            float[] matDiffuse;
            float[] matSpecular;
            float[] matEmission;
            float matShininess;

            switch (CurrentMaterial)
            {
                case MaterialMode.Ambient:
                    // Режим Ambient: повышаем фоновую составляющую, чтобы даже теневые
                    // грани получали мягкое рассеянное освещение и не уходили в чёрный цвет.
                    lightAmbient = new[] { 0.45f, 0.45f, 0.45f, 1.0f };
                    lightDiffuse = new[] { 0.45f, 0.45f, 0.45f, 1.0f };
                    lightSpecular = new[] { 0.0f, 0.0f, 0.0f, 1.0f };

                    matAmbient = new[] { 0.15f, 0.40f, 0.95f, 1.0f };
                    matDiffuse = new[] { 0.25f, 0.50f, 1.0f, 1.0f };
                    matSpecular = new[] { 0.0f, 0.0f, 0.0f, 1.0f };
                    matEmission = new[] { 0.0f, 0.0f, 0.0f, 1.0f };
                    matShininess = 0.0f;
                    break;

                case MaterialMode.Specular:
                    // Режим Specular: задаём белый зеркальный компонент отражения
                    // и степень концентрации блика GL_SHININESS = 64 (из диапазона 0..128).
                    lightAmbient = new[] { 0.10f, 0.10f, 0.10f, 1.0f };
                    lightDiffuse = new[] { 0.85f, 0.85f, 0.85f, 1.0f };
                    lightSpecular = new[] { 1.0f, 1.0f, 1.0f, 1.0f };

                    matAmbient = new[] { 0.05f, 0.12f, 0.30f, 1.0f };
                    matDiffuse = new[] { 0.12f, 0.38f, 0.90f, 1.0f };
                    matSpecular = new[] { 1.0f, 1.0f, 1.0f, 1.0f };
                    matEmission = new[] { 0.0f, 0.0f, 0.0f, 1.0f };
                    matShininess = 64.0f;
                    break;

                case MaterialMode.Emission:
                    // Режим Emission: объект сам излучает свет (GL_EMISSION), благодаря чему
                    // все грани окрашиваются равномерно независимо от ориентации к лампе.
                    lightAmbient = new[] { 0.10f, 0.10f, 0.10f, 1.0f };
                    lightDiffuse = new[] { 0.70f, 0.70f, 0.70f, 1.0f };
                    lightSpecular = new[] { 0.0f, 0.0f, 0.0f, 1.0f };

                    matAmbient = new[] { 0.0f, 0.0f, 0.0f, 1.0f };
                    matDiffuse = new[] { 0.08f, 0.22f, 0.65f, 1.0f };
                    matSpecular = new[] { 0.0f, 0.0f, 0.0f, 1.0f };
                    matEmission = new[] { 0.06f, 0.16f, 0.95f, 1.0f };
                    matShininess = 0.0f;
                    break;

                default:
                    // Режим Diffuse (по умолчанию): естественное матовое рассеяние света,
                    // яркость каждой грани пропорциональна косинусу угла падения лучей.
                    lightAmbient = new[] { 0.05f, 0.05f, 0.05f, 1.0f };
                    lightDiffuse = new[] { 0.95f, 0.95f, 0.95f, 1.0f };
                    lightSpecular = new[] { 0.0f, 0.0f, 0.0f, 1.0f };

                    matAmbient = new[] { 0.05f, 0.10f, 0.25f, 1.0f };
                    matDiffuse = new[] { 0.12f, 0.42f, 0.98f, 1.0f };
                    matSpecular = new[] { 0.0f, 0.0f, 0.0f, 1.0f };
                    matEmission = new[] { 0.0f, 0.0f, 0.0f, 1.0f };
                    matShininess = 0.0f;
                    break;
            }

            // Применяем параметры источника света GL_LIGHT0.
            GL.Light(LightName.Light0, LightParameter.Ambient, lightAmbient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightDiffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, lightSpecular);

            // Применяем свойства материала для лицевых и обратных граней объекта.
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, matAmbient);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, matDiffuse);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, matSpecular);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, matEmission);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, matShininess);
        }
    }

    // ============================================================================
    // МОДУЛЬ 3: RotationController.cs
    // Назначение: Управление углами поворота фигуры по таймеру и с клавиатуры,
    // а также обработка горячих клавиш перемещения источника света.
    // ============================================================================
    public class RotationController
    {
        private const float ManualStep = 5.0f;     // Шаг ручного поворота фигуры (в градусах)
        private const float TimerStepY = 1.0f;     // Шаг вращения по таймеру за кадр (в градусах)
        private const float LightMoveStep = 0.35f; // Шаг сдвига источника света вдоль выбранной оси

        public float AngleX { get; private set; }
        public float AngleY { get; private set; }
        public float AngleZ { get; private set; }
        public bool IsAutoRotating { get; private set; }

        public RotationController()
        {
            Reset();
        }

        // Начальный изометрический ракурс, при котором видны три грани параллелепипеда.
        public void Reset()
        {
            AngleX = 30.0f;
            AngleY = -35.0f;
            AngleZ = 0.0f;
            IsAutoRotating = true;
        }

        // Обновление угла при срабатывании таймера.
        public void OnTimerTick()
        {
            if (!IsAutoRotating)
            {
                return;
            }

            AngleY = NormalizeAngle(AngleY + TimerStepY);
        }

        // Обработка нажатий клавиш управления сценой:
        // Стрелки / PageUp / PageDown — вращение фигуры вокруг осей X, Y, Z;
        // W / S или + / - — перемещение источника света вдоль активной оси;
        // L — переключение между точечным и направленным источником света;
        // Space — пауза/запуск автоповорота; R — полный сброс сцены.
        public bool HandleKeyDown(Keys keyCode, LightingMaterialController lightingController)
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
                case Keys.W:
                case Keys.Oemplus:
                case Keys.Add:
                    lightingController.MoveLight(LightMoveStep);
                    return true;
                case Keys.S:
                case Keys.OemMinus:
                case Keys.Subtract:
                    lightingController.MoveLight(-LightMoveStep);
                    return true;
                case Keys.L:
                    lightingController.ToggleLightType();
                    return true;
                case Keys.Space:
                    IsAutoRotating = !IsAutoRotating;
                    return true;
                case Keys.R:
                    Reset();
                    lightingController.Reset();
                    return true;
                default:
                    return false;
            }
        }

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
    // МОДУЛЬ 4: SceneRenderer.cs
    // Назначение: Инициализация графического конвейера OpenGL (буфер глубины,
    // включение освещения, нормализации нормалей и двусторонней модели света),
    // настройка перспективной проекции и отрисовка кадра.
    // ============================================================================
    public class SceneRenderer
    {
        private readonly RectangularParallelepiped _parallelepiped;

        public SceneRenderer(RectangularParallelepiped parallelepiped)
        {
            _parallelepiped = parallelepiped;
        }

        // Однократная настройка состояний OpenGL при запуске программы.
        public void Initialize()
        {
            // Задаём белый цвет фона окна.
            GL.ClearColor(Color.White);

            // Включаем Z-буфер (тест глубины) для корректного отсечения невидимых граней.
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            // Используем плавную интерполяцию освещённости по методу Гуро.
            GL.ShadeModel(ShadingModel.Smooth);

            // Шаги 2–5 алгоритма методических указаний:
            // 1) Включаем систему расчёта освещения OpenGL;
            // 2) Активируем нулевой источник света GL_LIGHT0;
            // 3) Включаем автоматическую нормализацию векторов нормалей;
            // 4) Включаем двусторонний расчёт освещения для лицевых и нелицевых граней;
            // 5) Уточняем положение наблюдателя для точного расчёта зеркальных бликов.
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Normalize);
            GL.LightModel(LightModelParameter.LightModelTwoSide, 1.0f);
            GL.LightModel(LightModelParameter.LightModelLocalViewer, 1.0f);
        }

        // Пересчёт области вывода и матрицы перспективной проекции при изменении размера окна.
        public void UpdateProjection(int width, int height)
        {
            if (height <= 0)
            {
                height = 1;
            }

            GL.Viewport(0, 0, width, height);

            double aspectRatio = (double)width / height;
            double near = 1.5;
            double far = 20.0;
            double top = 1.0;
            double bottom = -1.0;
            double left = -top * aspectRatio;
            double right = top * aspectRatio;

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Frustum(left, right, bottom, top, near, far);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        // Построение одного кадра сцены.
        public void Render(RotationController rotationController, LightingMaterialController lightingController)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Устанавливаем положение источника света и текущие свойства материала
            // до применения матриц вращения самой фигуры.
            lightingController.ApplyLightPosition();
            lightingController.ApplyLightAndMaterial();

            // Сохраняем единичную видовую матрицу в стек перед трансформациями объекта.
            GL.PushMatrix();

            // Отодвигаем фигуру вдоль отрицательной оси Z внутрь пирамиды видимости
            // и поворачиваем её на накопленные углы вокруг осей X, Y и Z.
            GL.Translate(0.0f, 0.0f, -4.5f);
            GL.Rotate(rotationController.AngleX, 1.0f, 0.0f, 0.0f);
            GL.Rotate(rotationController.AngleY, 0.0f, 1.0f, 0.0f);
            GL.Rotate(rotationController.AngleZ, 0.0f, 0.0f, 1.0f);

            _parallelepiped.Draw();

            // Восстанавливаем матрицу из стека.
            GL.PopMatrix();
        }
    }

    // ============================================================================
    // МОДУЛЬ 5: MainForm.cs
    // Назначение: Главная форма Windows Forms, связывающая холст OpenTK.GLControl,
    // верхнее меню выбора материала и оси света ("Материал", "Свет"),
    // таймер анимации и обработчик событий клавиатуры.
    // ============================================================================
    public partial class MainForm : Form
    {
        private readonly SceneRenderer _renderer;
        private readonly RotationController _rotationController;
        private readonly LightingMaterialController _lightingController;
        private readonly Timer _animationTimer;

        public MainForm()
        {
            InitializeComponent();

            RectangularParallelepiped parallelepiped = new RectangularParallelepiped();
            _renderer = new SceneRenderer(parallelepiped);
            _rotationController = new RotationController();
            _lightingController = new LightingMaterialController();

            _animationTimer = new Timer();
            _animationTimer.Interval = 30;
            _animationTimer.Tick += AnimationTimer_Tick;

            glControl.Load += GlControl_Load;
            glControl.Resize += GlControl_Resize;
            glControl.Paint += GlControl_Paint;
            glControl.PreviewKeyDown += GlControl_PreviewKeyDown;
            KeyDown += MainForm_KeyDown;
            FormClosing += MainForm_FormClosing;

            // Привязка пунктов меню "Материал" к переключению режимов отражения.
            ambientMenuItem.Click += (s, e) => SelectMaterial(MaterialMode.Ambient);
            diffuseMenuItem.Click += (s, e) => SelectMaterial(MaterialMode.Diffuse);
            specularMenuItem.Click += (s, e) => SelectMaterial(MaterialMode.Specular);
            emissionMenuItem.Click += (s, e) => SelectMaterial(MaterialMode.Emission);

            // Привязка пунктов меню "Свет" к выбору оси расположения источника света.
            lightXMenuItem.Click += (s, e) => SelectLightAxis(LightAxis.X);
            lightYMenuItem.Click += (s, e) => SelectLightAxis(LightAxis.Y);
            lightZMenuItem.Click += (s, e) => SelectLightAxis(LightAxis.Z);

            UpdateMenuChecks();
        }

        private void SelectMaterial(MaterialMode mode)
        {
            _lightingController.SetMaterialMode(mode);
            UpdateMenuChecks();
            glControl.Invalidate();
        }

        private void SelectLightAxis(LightAxis axis)
        {
            _lightingController.SetLightAxis(axis);
            UpdateMenuChecks();
            glControl.Invalidate();
        }

        // Отображение галочек напротив выбранного свойства материала и активной оси света.
        private void UpdateMenuChecks()
        {
            ambientMenuItem.Checked = _lightingController.CurrentMaterial == MaterialMode.Ambient;
            diffuseMenuItem.Checked = _lightingController.CurrentMaterial == MaterialMode.Diffuse;
            specularMenuItem.Checked = _lightingController.CurrentMaterial == MaterialMode.Specular;
            emissionMenuItem.Checked = _lightingController.CurrentMaterial == MaterialMode.Emission;

            lightXMenuItem.Checked = _lightingController.CurrentLightAxis == LightAxis.X;
            lightYMenuItem.Checked = _lightingController.CurrentLightAxis == LightAxis.Y;
            lightZMenuItem.Checked = _lightingController.CurrentLightAxis == LightAxis.Z;
        }

        private void GlControl_Load(object sender, EventArgs e)
        {
            glControl.MakeCurrent();
            _renderer.Initialize();
            _renderer.UpdateProjection(glControl.Width, glControl.Height);
            _animationTimer.Start();
        }

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

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            if (!glControl.IsHandleCreated)
            {
                return;
            }

            glControl.MakeCurrent();
            _renderer.Render(_rotationController, _lightingController);
            glControl.SwapBuffers();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            _rotationController.OnTimerTick();
            glControl.Invalidate();
        }

        // Разрешаем передачу клавиш-стрелок в событие KeyDown формы.
        private void GlControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
                return;
            }

            if (_rotationController.HandleKeyDown(e.KeyCode, _lightingController))
            {
                UpdateMenuChecks();
                glControl.Invalidate();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _animationTimer.Stop();
            _animationTimer.Dispose();
        }
    }

    // ============================================================================
    // МОДУЛЬ 6: MainForm.Designer.cs
    // Назначение: Инициализация визуальных компонентов формы (OpenTK.GLControl
    // и главного меню MenuStrip с разделами "Материал" и "Свет").
    // ============================================================================
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.glControl = new OpenTK.GLControl();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.materialMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ambientMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diffuseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specularMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emissionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightXMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightYMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightZMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();

            this.glControl.BackColor = System.Drawing.Color.Transparent;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(0, 24);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(1024, 552);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;

            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.materialMenuItem,
            this.lightMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1024, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";

            this.materialMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ambientMenuItem,
            this.diffuseMenuItem,
            this.specularMenuItem,
            this.emissionMenuItem});
            this.materialMenuItem.Name = "materialMenuItem";
            this.materialMenuItem.Size = new System.Drawing.Size(74, 20);
            this.materialMenuItem.Text = "Материал";

            this.ambientMenuItem.Name = "ambientMenuItem";
            this.ambientMenuItem.Size = new System.Drawing.Size(121, 22);
            this.ambientMenuItem.Text = "Ambient";

            this.diffuseMenuItem.Name = "diffuseMenuItem";
            this.diffuseMenuItem.Size = new System.Drawing.Size(121, 22);
            this.diffuseMenuItem.Text = "Diffuse";

            this.specularMenuItem.Name = "specularMenuItem";
            this.specularMenuItem.Size = new System.Drawing.Size(121, 22);
            this.specularMenuItem.Text = "Specular";

            this.emissionMenuItem.Name = "emissionMenuItem";
            this.emissionMenuItem.Size = new System.Drawing.Size(121, 22);
            this.emissionMenuItem.Text = "Emission";

            this.lightMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightXMenuItem,
            this.lightYMenuItem,
            this.lightZMenuItem});
            this.lightMenuItem.Name = "lightMenuItem";
            this.lightMenuItem.Size = new System.Drawing.Size(45, 20);
            this.lightMenuItem.Text = "Свет";

            this.lightXMenuItem.Name = "lightXMenuItem";
            this.lightXMenuItem.Size = new System.Drawing.Size(82, 22);
            this.lightXMenuItem.Text = "X";

            this.lightYMenuItem.Name = "lightYMenuItem";
            this.lightYMenuItem.Size = new System.Drawing.Size(82, 22);
            this.lightYMenuItem.Text = "Y";

            this.lightZMenuItem.Name = "lightZMenuItem";
            this.lightZMenuItem.Size = new System.Drawing.Size(82, 22);
            this.lightZMenuItem.Text = "Z";

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 576);
            this.Controls.Add(this.glControl);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lighting";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private OpenTK.GLControl glControl;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem materialMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ambientMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diffuseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem specularMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emissionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightXMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightYMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightZMenuItem;
    }
}
```