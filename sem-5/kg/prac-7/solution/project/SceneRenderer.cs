using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace project
{
    public class SceneRenderer
    {
        private readonly RectangularParallelepiped _parallelepiped;

        public SceneRenderer(RectangularParallelepiped parallelepiped)
        {
            _parallelepiped = parallelepiped;
        }

        public void Initialize()
        {
            GL.ClearColor(Color.White);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.ShadeModel(ShadingModel.Smooth);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Normalize);
            GL.LightModel(LightModelParameter.LightModelTwoSide, 1.0f);
            GL.LightModel(LightModelParameter.LightModelLocalViewer, 1.0f);
        }

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

        public void Render(RotationController rotationController, LightingMaterialController lightingController)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            lightingController.ApplyLightPosition();
            lightingController.ApplyLightAndMaterial();

            GL.PushMatrix();

            GL.Translate(0.0f, 0.0f, -4.5f);
            GL.Rotate(rotationController.AngleX, 1.0f, 0.0f, 0.0f);
            GL.Rotate(rotationController.AngleY, 0.0f, 1.0f, 0.0f);
            GL.Rotate(rotationController.AngleZ, 0.0f, 0.0f, 1.0f);

            _parallelepiped.Draw();

            GL.PopMatrix();
        }
    }
}