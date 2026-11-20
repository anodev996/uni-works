using System;
using OpenTK.Graphics.OpenGL;

namespace project
{
    public enum MaterialMode
    {
        Ambient,
        Diffuse,
        Specular,
        Emission
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

        public void SetLightAxis(LightAxis axis)
        {
            CurrentLightAxis = axis;
            LightCoordinate = DefaultCoordinate;
        }

        public void MoveLight(float delta)
        {
            LightCoordinate = Math.Max(-6.0f, Math.Min(6.0f, LightCoordinate + delta));
        }

        public void ToggleLightType()
        {
            IsPositionalLight = !IsPositionalLight;
        }

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

            GL.Light(LightName.Light0, LightParameter.Ambient, lightAmbient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightDiffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, lightSpecular);

            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, matAmbient);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, matDiffuse);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, matSpecular);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, matEmission);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, matShininess);
        }
    }
}