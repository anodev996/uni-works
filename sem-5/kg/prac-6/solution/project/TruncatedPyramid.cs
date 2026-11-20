using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace project
{
    public class TruncatedPyramid
    {
        private readonly Vector3[] _bottomVertices;
        private readonly Vector3[] _topVertices;

        public TruncatedPyramid(float bottomRadius = 1.3f, float topRadius = 0.55f, float halfHeight = 0.8f)
        {
            _bottomVertices = CreateTriangleRing(bottomRadius, -halfHeight);
            _topVertices = CreateTriangleRing(topRadius, halfHeight);
        }

        private static Vector3[] CreateTriangleRing(float radius, float y)
        {
            Vector3[] vertices = new Vector3[3];
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

        public void Draw()
        {
            GL.Enable(EnableCap.PolygonOffsetFill);
            GL.PolygonOffset(1.0f, 1.0f);

            DrawFilledFaces();

            GL.Disable(EnableCap.PolygonOffsetFill);

            DrawEdges();
        }

        private void DrawFilledFaces()
        {
            GL.Begin(PrimitiveType.Triangles);

            SetTopColor();
            GL.Vertex3(_topVertices[0]);
            GL.Vertex3(_topVertices[1]);
            GL.Vertex3(_topVertices[2]);

            SetBottomColor();
            GL.Vertex3(_bottomVertices[0]);
            GL.Vertex3(_bottomVertices[2]);
            GL.Vertex3(_bottomVertices[1]);

            GL.End();

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

        private void DrawEdges()
        {
            GL.LineWidth(2.0f);
            GL.Color3(0.12f, 0.12f, 0.12f);

            GL.Begin(PrimitiveType.LineLoop);
            for (int i = 0; i < 3; i++)
            {
                GL.Vertex3(_topVertices[i]);
            }
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            for (int i = 0; i < 3; i++)
            {
                GL.Vertex3(_bottomVertices[i]);
            }
            GL.End();

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
}