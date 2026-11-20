using System;
using System.Windows.Forms;

namespace project
{
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

            _animationTimer = new Timer();
            _animationTimer.Interval = 30;
            _animationTimer.Tick += AnimationTimer_Tick;

            glControl.Load += GlControl_Load;
            glControl.Resize += GlControl_Resize;
            glControl.Paint += GlControl_Paint;
            glControl.PreviewKeyDown += GlControl_PreviewKeyDown;
            KeyDown += MainForm_KeyDown;
            FormClosing += MainForm_FormClosing;
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
            _renderer.Render(_controller);
            glControl.SwapBuffers();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            _controller.OnTimerTick();
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

            if (_controller.HandleKeyDown(e.KeyCode))
            {
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