using System;
using System.Windows.Forms;

namespace project
{
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

            ambientMenuItem.Click += (s, e) => SelectMaterial(MaterialMode.Ambient);
            diffuseMenuItem.Click += (s, e) => SelectMaterial(MaterialMode.Diffuse);
            specularMenuItem.Click += (s, e) => SelectMaterial(MaterialMode.Specular);
            emissionMenuItem.Click += (s, e) => SelectMaterial(MaterialMode.Emission);

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
}