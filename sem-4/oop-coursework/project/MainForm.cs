using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace project
{
    public partial class MainForm : Form
    {
        private HanoiGame _game;
        Image rodsImg;
        float rodsAspectRatio;
        private Image[] _diskImgs;

        public MainForm()
        {
            InitializeComponent();

            //tblLtPnlGame.Visible = false;

            _game = null;

            rodsImg = Properties.Resources.rods;
            rodsAspectRatio = (float)rodsImg.Width / rodsImg.Height;

            _diskImgs =
            [
                Properties.Resources.disk1,
                Properties.Resources.disk2,
                Properties.Resources.disk3,
                Properties.Resources.disk4,
                Properties.Resources.disk5,
                Properties.Resources.disk6,
                Properties.Resources.disk7,
                Properties.Resources.disk8,
                Properties.Resources.disk9
            ];


        }

        private void btnDrawDisks_Click(object sender, EventArgs e)
        {
            // На всякий случай добавим простую защиту, чтобы программа не вылетала 
            // если поле пустое или там введен текст
            if (int.TryParse(txtBxDisks.Text, out int disks) && disks >= 1 && disks <= 9)
            {
                _game = new HanoiGame(disks);

                // КРИТИЧЕСКИ ВАЖНО: говорим панели, что данные обновились и нужно нарисовать кадр заново
                pnlGame.Invalidate();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите число от 1 до 9.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            pnlGreet.Visible = false;
            tblLtPnlGame.Visible = true;
        }

        private void btnRules_Click(object sender, EventArgs e)
        {
            tblLtPnlGame.Visible = false;
            pnlRules.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlRules.Visible = false;
            tblLtPnlGame.Visible = true;
        }

        private void pnlGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Включаем сглаживание для более красивой картинки
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int renderWidth = pnlGame.Width;
            int renderHeight = (int)(renderWidth / rodsAspectRatio);

            if (renderHeight > pnlGame.Height)
            {
                renderHeight = pnlGame.Height;
                renderWidth = (int)(renderHeight * rodsAspectRatio);
            }

            int x = (pnlGame.Width - renderWidth) / 2;
            int y = (pnlGame.Height - renderHeight) / 2;

            g.DrawImage(rodsImg, x, y, renderWidth, renderHeight);

            // Если игра еще не создана, выходим (рисовать нечего)
            if (_game == null) return;

            // 1. Вычисляем центры трёх стержней по оси X
            float[] rodCentersX =
            [
                x + (renderWidth * 0.175f), // Центр стержня A
                x + (renderWidth * 0.5f), // Центр стержня B
                x + (renderWidth * 0.825f), // Центр стержня C
            ];

            // 2. База (земля) для стержней
            float baseY = y + (renderHeight * 0.913f);

            for (int i = 0; i < 3; i++)
            {
                var disks = _game.Rods[i].Disks.ToArray();

                for (int j = 0; j < disks.Length; j++)
                {
                    int currentDiskSize = disks[j].Number;

                    float currentDiskWidth = renderWidth * 0.29f * (1 - (j * 0.1f));
                    float currentDiskHeight = renderHeight * 0.092f;

                    // Считаем координаты X и Y для текущего диска
                    float diskX = rodCentersX[i] - (currentDiskWidth / 2f);

                    int positionFromBottom = j + 1;
                    float diskY = baseY - (positionFromBottom * currentDiskHeight * 0.98f) - 1;

                    // Рисуем!
                    g.DrawImage(_diskImgs[j], diskX, diskY, currentDiskWidth, currentDiskHeight);
                }
            }
        }


    }
}