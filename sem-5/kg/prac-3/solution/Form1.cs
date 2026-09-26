using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace prac_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            this.Text = "Practice 3. Variant 2";
            this.Size = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            
            btnDraw.Click += new EventHandler(BtnDraw_Click);
            this.MouseClick += new MouseEventHandler(Form1_MouseClick);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            BtnDraw_Click(null, EventArgs.Empty);
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                int centerX = this.ClientSize.Width / 2;
                int centerY = this.ClientSize.Height / 2;

                float scale = 30f; 

                // Смещаем и отражаем ось Y (в GDI+ Y растет вниз, а нам нужно вверх)
                g.TranslateTransform(centerX, centerY);
                g.ScaleTransform(scale, -scale);

                DrawAxes(g, scale);

                PointF[] points = new PointF[]
                {
                    new PointF(-4f, -4f),  
                    new PointF(-4f, 3f),   
                    new PointF(3f, 3f),    
                    new PointF(-1f, 1f)    
                };
                
                using (Pen pen = new Pen(Color.Black, 2 / scale))
                {
                    g.DrawPolygon(pen, points);
                }
            }
        }

        private void DrawAxes(Graphics g, float scale)
        {
            float axisThickness = 1 / scale;

            using (Pen axisPen = new Pen(Color.Black, axisThickness))
            {
                g.DrawLine(axisPen, -6f, 0f, 6f, 0f);
                g.DrawLine(axisPen, 0f, -4.5f, 0f, 5.5f);
            }

            lblY.Visible = true;
            lblX.Visible = true;
            lblZero.Visible = true;
            lblOne.Visible = true;
        }
    }
}
