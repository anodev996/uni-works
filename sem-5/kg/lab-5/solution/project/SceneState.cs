namespace project
{
    public class SceneState
    {
        public float Ax { get; set; }
        public float Ay { get; set; }
        public float AngleA { get; set; }
        public float ScaleA { get; set; }
        public float OrbitAngle { get; set; }

        public float Bx { get; set; }
        public float By { get; set; }
        public float AngleB { get; set; }
        public float ScaleB { get; set; }

        public bool LoopMode { get; set; }

        public SceneState()
        {
            Reset();
        }

        public void Reset()
        {
            Ax = -1.25f;
            Ay = 0.0f;
            AngleA = 0.0f;
            ScaleA = 1.0f;
            OrbitAngle = 0.0f;

            Bx = 0.0f;
            By = 0.0f;
            AngleB = 0.0f;
            ScaleB = 1.0f;

            LoopMode = false;
        }
    }
}