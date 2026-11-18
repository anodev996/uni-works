using OpenTK.Graphics.OpenGL;
using System;

namespace project
{
    public class SceneRenderer
    {
        public void Init(int width, int height)
        {
            GL.ClearColor(255 / 255.0f, 255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.LineSmooth);
            Resize(width, height);
        }

        public void Resize(int width, int height)
        {
            if (height == 0)
            {
                height = 1;
            }

            GL.Viewport(0, 0, width, height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            double aspect = (double)width / height;
            GL.Ortho(-2.4 * aspect, 2.4 * aspect, -2.4, 2.4, -1.0, 1.0);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        public void Render(SceneState state)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.LoadIdentity();

            DrawTransformedFigureB(state);

            if (state.LoopMode)
            {
                int count = 28;
                float stepAngle = 10.0f;

                for (int i = 0; i < count; i++)
                {
                    DrawTransformedFigureA(state, state.OrbitAngle + i * stepAngle);
                }
            }
            else
            {
                DrawTransformedFigureA(state, state.OrbitAngle);
            }
        }

        private void DrawTransformedFigureA(SceneState state, float currentOrbitAngle)
        {
            GL.PushMatrix();
            GL.Rotate(currentOrbitAngle, 0.0f, 0.0f, 1.0f);
            GL.Translate(state.Ax, state.Ay, 0.0f);
            GL.Rotate(state.AngleA, 0.0f, 0.0f, 1.0f);
            GL.Scale(state.ScaleA, state.ScaleA, 1.0f);
            DrawFigureA();
            GL.PopMatrix();
        }

        private void DrawTransformedFigureB(SceneState state)
        {
            GL.PushMatrix();
            GL.Translate(state.Bx, state.By, 0.0f);
            GL.Rotate(state.AngleB, 0.0f, 0.0f, 1.0f);
            GL.Scale(state.ScaleB, state.ScaleB, 1.0f);
            DrawFigureB();
            GL.PopMatrix();
        }

        private void DrawFigureA()
        {
            int n = 8;
            float radius = 0.55f;
            float[] vx = new float[n];
            float[] vy = new float[n];

            for (int i = 0; i < n; i++)
            {
                double angle = Math.PI / 2.0 + i * (2.0 * Math.PI / n);
                vx[i] = (float)(radius * Math.Cos(angle));
                vy[i] = (float)(radius * Math.Sin(angle));
            }

            GL.Color3(0.92f, 0.88f, 0.98f);
            GL.Begin(PrimitiveType.Polygon);
            for (int i = 0; i < n; i++)
            {
                GL.Vertex2(vx[i], vy[i]);
            }
            GL.End();

            GL.Color3(0.96f, 0.68f, 0.68f);
            GL.Begin(PrimitiveType.Polygon);
            GL.Vertex2(vx[0], vy[0]);
            GL.Vertex2(vx[2], vy[2]);
            GL.Vertex2(vx[4], vy[4]);
            GL.Vertex2(vx[6], vy[6]);
            GL.End();

            GL.LineWidth(1.2f);
            GL.Color3(0.15f, 0.15f, 0.15f);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(vx[0], vy[0]);
            GL.Vertex2(vx[4], vy[4]);
            GL.Vertex2(vx[2], vy[2]);
            GL.Vertex2(vx[6], vy[6]);
            GL.End();

            GL.LineWidth(3.0f);
            GL.Color3(0.45f, 0.10f, 0.78f);
            GL.Begin(PrimitiveType.LineLoop);
            for (int i = 0; i < n; i++)
            {
                GL.Vertex2(vx[i], vy[i]);
            }
            GL.End();

            GL.LineWidth(3.0f);
            GL.Color3(0.92f, 0.25f, 0.15f);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(vx[0], vy[0]);
            GL.Vertex2(vx[2], vy[2]);
            GL.Vertex2(vx[4], vy[4]);
            GL.Vertex2(vx[6], vy[6]);
            GL.End();

            GL.PointSize(5.0f);
            GL.Color3(0.2f, 0.2f, 0.2f);
            GL.Begin(PrimitiveType.Points);
            for (int i = 0; i < n; i++)
            {
                GL.Vertex2(vx[i], vy[i]);
            }
            GL.End();

            GL.PointSize(6.0f);
            GL.Color3(0.0f, 0.15f, 0.75f);
            GL.Begin(PrimitiveType.Points);
            GL.Vertex2(0.0f, 0.0f);
            GL.End();
        }

        private void DrawFigureB()
        {
            GL.LineWidth(2.5f);
            GL.Color3(0.0f, 0.0f, 0.0f);

            GL.Begin(PrimitiveType.Lines);

            GL.Vertex2(-0.35f, 0.55f);
            GL.Vertex2(-0.35f, -0.55f);

            GL.Vertex2(0.0f, 0.375f);
            GL.Vertex2(0.0f, -0.375f);

            GL.Vertex2(-0.35f, 0.375f);
            GL.Vertex2(0.0f, 0.375f);

            GL.Vertex2(-0.35f, -0.375f);
            GL.Vertex2(0.0f, -0.375f);

            GL.Vertex2(-0.35f, 0.55f);
            GL.Vertex2(0.40f, 0.175f);

            GL.Vertex2(0.40f, 0.175f);
            GL.Vertex2(0.0f, 0.0f);

            GL.Vertex2(0.0f, 0.0f);
            GL.Vertex2(0.40f, -0.175f);

            GL.Vertex2(0.40f, -0.175f);
            GL.Vertex2(-0.35f, -0.55f);

            GL.End();

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