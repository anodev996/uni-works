using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace project
{
    public class RectangularParallelepiped
    {
        private readonly float _halfWidth;
        private readonly float _halfHeight;
        private readonly float _halfDepth;

        public RectangularParallelepiped(float halfWidth = 1.25f, float halfHeight = 0.32f, float halfDepth = 0.65f)
        {
            _halfWidth = halfWidth;
            _halfHeight = halfHeight;
            _halfDepth = halfDepth;
        }

        public void Draw()
        {
            float w = _halfWidth;
            float h = _halfHeight;
            float d = _halfDepth;

            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(new Vector3(-w, h, d));
            GL.Vertex3(new Vector3(w, h, d));
            GL.Vertex3(new Vector3(w, h, -d));
            GL.Vertex3(new Vector3(-w, h, -d));

            GL.Normal3(0.0f, -1.0f, 0.0f);
            GL.Vertex3(new Vector3(-w, -h, -d));
            GL.Vertex3(new Vector3(w, -h, -d));
            GL.Vertex3(new Vector3(w, -h, d));
            GL.Vertex3(new Vector3(-w, -h, d));

            GL.Normal3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(new Vector3(-w, -h, d));
            GL.Vertex3(new Vector3(w, -h, d));
            GL.Vertex3(new Vector3(w, h, d));
            GL.Vertex3(new Vector3(-w, h, d));

            GL.Normal3(0.0f, 0.0f, -1.0f);
            GL.Vertex3(new Vector3(w, -h, -d));
            GL.Vertex3(new Vector3(-w, -h, -d));
            GL.Vertex3(new Vector3(-w, h, -d));
            GL.Vertex3(new Vector3(w, h, -d));

            GL.Normal3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(new Vector3(w, -h, d));
            GL.Vertex3(new Vector3(w, -h, -d));
            GL.Vertex3(new Vector3(w, h, -d));
            GL.Vertex3(new Vector3(w, h, d));

            GL.Normal3(-1.0f, 0.0f, 0.0f);
            GL.Vertex3(new Vector3(-w, -h, -d));
            GL.Vertex3(new Vector3(-w, -h, d));
            GL.Vertex3(new Vector3(-w, h, d));
            GL.Vertex3(new Vector3(-w, h, -d));

            GL.End();
        }
    }
}