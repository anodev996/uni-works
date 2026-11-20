using System.Windows.Forms;

namespace project
{
    public class RotationController
    {
        private const float ManualStep = 5.0f;
        private const float TimerStepY = 1.0f;
        private const float LightMoveStep = 0.35f;

        public float AngleX { get; private set; }
        public float AngleY { get; private set; }
        public float AngleZ { get; private set; }
        public bool IsAutoRotating { get; private set; }

        public RotationController()
        {
            Reset();
        }

        public void Reset()
        {
            AngleX = 30.0f;
            AngleY = -35.0f;
            AngleZ = 0.0f;
            IsAutoRotating = true;
        }

        public void OnTimerTick()
        {
            if (!IsAutoRotating)
            {
                return;
            }

            AngleY = NormalizeAngle(AngleY + TimerStepY);
        }

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
}