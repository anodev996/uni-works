using project;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace project
{
    public partial class MainForm : Form
    {
        private readonly SceneState state;
        private readonly SceneRenderer renderer;
        private Point lastMousePos;
        private bool glReady;

        public MainForm()
        {
            InitializeComponent();

            state = new SceneState();
            renderer = new SceneRenderer();

            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;

            glControl.Paint += glControl_Paint;
            glControl.Resize += glControl_Resize;
            glControl.MouseDown += glControl_MouseDown;
            glControl.MouseMove += glControl_MouseMove;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            glControl.MakeCurrent();
            renderer.Init(glControl.Width, glControl.Height);
            glReady = true;
        }

        private void glControl_Resize(object sender, EventArgs e)
        {
            if (!glReady)
            {
                return;
            }

            glControl.MakeCurrent();
            renderer.Resize(glControl.Width, glControl.Height);
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
            glControl.SwapBuffers();
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            lastMousePos = e.Location;
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                return;
            }

            float dx = (e.X - lastMousePos.X) * 0.01f;
            float dy = (lastMousePos.Y - e.Y) * 0.01f;

            if (e.Button == MouseButtons.Left)
            {
                state.Ax += dx;
                state.Ay += dy;
            }
            else if (e.Button == MouseButtons.Right)
            {
                state.Bx += dx;
                state.By += dy;
            }

            lastMousePos = e.Location;
            glControl.Invalidate();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            float moveStep = 0.08f;
            float rotateStep = 5.0f;
            float scaleStep = 0.08f;

            switch (e.KeyCode)
            {
                case Keys.Q:
                    state.OrbitAngle += rotateStep;
                    break;
                case Keys.E:
                    state.OrbitAngle -= rotateStep;
                    break;

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
                    if (state.ScaleA > 0.2f)
                    {
                        state.ScaleA -= scaleStep;
                    }
                    break;

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

                case Keys.Space:
                    state.LoopMode = !state.LoopMode;
                    break;
                case Keys.C:
                    state.Reset();
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
            }

            glControl.Invalidate();
        }
    }
}