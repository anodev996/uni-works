using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace project
{
    public partial class MainForm : Form
    {
        private const float RodARelativeX = 0.175f;
        private const float RodBRelativeX = 0.5f;
        private const float RodCRelativeX = 0.825f;
        private const float BaseRelativeY = 0.913f;
        private const float MaxDiskRelativeWidth = 0.29f;
        private const float DiskRelativeHeight = 0.092f;
        private const int SolversDelayMs = 1500;

        private enum GameState
        {
            Setup,
            Playing,
            AutoSolving,
            Finished
        }

        private GameState _currentState;

        private HanoiGame _game;
        private int? _selectedRodIndex;

        private readonly Image _rodsImg;
        private readonly float _rodsAspectRatio;
        private readonly Image[] _diskImgs;

        // Переменные для таймера авторешения (заменяют async/await)
        private readonly Timer _solveTimer;
        private List<Move> _solutionMoves;
        private int _currentSolveMoveIndex;

        public MainForm()
        {
            InitializeComponent();

            _rodsImg = Properties.Resources.rods;
            _rodsAspectRatio = (float)_rodsImg.Width / _rodsImg.Height;
            _diskImgs = new Image[]
            {
                Properties.Resources.disk1, Properties.Resources.disk2,
                Properties.Resources.disk3, Properties.Resources.disk4,
                Properties.Resources.disk5, Properties.Resources.disk6,
                Properties.Resources.disk7, Properties.Resources.disk8,
                Properties.Resources.disk9
            };

            // Настройка таймера для авторешения
            _solveTimer = new Timer
            {
                Interval = SolversDelayMs
            };
            _solveTimer.Tick += new EventHandler(SolveTimer_Tick);
        }

        private void SetState(GameState newState)
        {
            _currentState = newState;

            switch (_currentState)
            {
                case GameState.Setup:
                    {
                        _game = null;
                        _selectedRodIndex = null;
                        txtBxDisks.Enabled = true;
                        btnDrawDisks.Text = "Нарисовать";
                        btnDrawDisks.Enabled = true;
                        lblCurrentMoveCount.Text = "0";
                        lblOptimalCount.Text = "0";
                        btnSolve.Enabled = false;
                        btnStop.Enabled = false;
                        btnUndoMove.Enabled = false;
                        pnlGame.Enabled = false;
                        break;
                    }

                case GameState.Playing:
                    {
                        _selectedRodIndex = null;
                        txtBxDisks.Enabled = false;
                        btnDrawDisks.Text = "Заново";
                        btnDrawDisks.Enabled = true;
                        if (_game != null)
                        {
                            lblCurrentMoveCount.Text = _game.MoveHistory.Count.ToString();
                            lblOptimalCount.Text = _game.OptimalMoveCount.ToString();
                        }
                        btnSolve.Enabled = true;
                        btnStop.Enabled = false;
                        btnUndoMove.Enabled = _game != null && _game.MoveHistory.Count > 0;
                        pnlGame.Enabled = true;
                        break;
                    }

                case GameState.AutoSolving:
                    {
                        _selectedRodIndex = null;
                        txtBxDisks.Enabled = false;
                        btnDrawDisks.Enabled = false;
                        if (_game != null)
                        {
                            lblOptimalCount.Text = _game.OptimalMoveCount.ToString();
                        }
                        btnSolve.Enabled = false;
                        btnStop.Enabled = true;
                        btnUndoMove.Enabled = false;
                        pnlGame.Enabled = false;
                        break;
                    }
                case GameState.Finished:
                    {
                        _selectedRodIndex = null;
                        txtBxDisks.Enabled = false;
                        btnDrawDisks.Enabled = false;
                        btnSolve.Enabled = false;
                        btnStop.Enabled = false;
                        btnUndoMove.Enabled = false;
                        pnlGame.Enabled = false;
                        break;
                    }
            }

            pnlGame.Invalidate();
        }

        private void BtnDrawDisks_Click(object sender, EventArgs e)
        {
            if (_currentState == GameState.Playing)
            {
                SetState(GameState.Setup);
                return;
            }

            if (int.TryParse(txtBxDisks.Text, out int disks) && disks >= 1 && disks <= 9)
            {
                _game = new HanoiGame(disks);
                SetState(GameState.Playing);
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите число от 1 до 9.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnBegin_Click(object sender, EventArgs e)
        {
            pnlGreet.Visible = false;
            tblLtPnlGame.Visible = true;
        }

        private void BtnRules_Click(object sender, EventArgs e)
        {
            tblLtPnlGame.Visible = false;
            pnlRules.Visible = true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            pnlRules.Visible = false;
            tblLtPnlGame.Visible = true;
        }

        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int renderWidth = pnlGame.Width;
            int renderHeight = (int)(renderWidth / _rodsAspectRatio);

            if (renderHeight > pnlGame.Height)
            {
                renderHeight = pnlGame.Height;
                renderWidth = (int)(renderHeight * _rodsAspectRatio);
            }

            int x = (pnlGame.Width - renderWidth) / 2;
            int y = (pnlGame.Height - renderHeight) / 2;

            g.DrawImage(_rodsImg, x, y, renderWidth, renderHeight);

            if (_game == null) return;

            float[] rodCentersX = new float[]
            {
                x + (renderWidth * RodARelativeX),
                x + (renderWidth * RodBRelativeX),
                x + (renderWidth * RodCRelativeX)
            };

            float baseY = y + (renderHeight * BaseRelativeY);

            // В C# 3.0 using нужно писать с фигурными скобками
            using (Pen highlightPen = new Pen(Color.White, 3f))
            {
                highlightPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                highlightPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;

                for (int i = 0; i < 3; i++)
                {
                    Disk[] disks = _game.Rods[i].Disks.ToArray();

                    for (int j = 0; j < disks.Length; j++)
                    {
                        int currentDiskSize = disks[j].Number;

                        float currentDiskWidth = renderWidth * MaxDiskRelativeWidth * (1 - ((currentDiskSize - 1) * 0.1f));
                        float currentDiskHeight = renderHeight * DiskRelativeHeight;

                        float diskX = rodCentersX[i] - (currentDiskWidth / 2f);
                        int positionFromBottom = disks.Length - j;
                        float diskY = baseY - (positionFromBottom * currentDiskHeight * 0.98f) - 1;

                        g.DrawImage(_diskImgs[currentDiskSize - 1], diskX, diskY, currentDiskWidth, currentDiskHeight);

                        if (_selectedRodIndex != null && _selectedRodIndex.Value == i && j == 0)
                        {
                            float offset = 2f;
                            g.DrawRectangle(highlightPen,
                                diskX + offset,
                                diskY + offset - 1,
                                currentDiskWidth - (offset * 2),
                                currentDiskHeight - (offset * 2));
                        }
                    }
                }
            }
        }

        private void PnlGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (_game == null) return;

            int thirdOfWidth = pnlGame.Width / 3;
            int clickedRodIndex = e.X / thirdOfWidth;

            clickedRodIndex = Math.Min(clickedRodIndex, 2);

            if (_selectedRodIndex == null)
            {
                if (_game.Rods[clickedRodIndex].Disks.Count > 0)
                {
                    _selectedRodIndex = clickedRodIndex;
                    pnlGame.Invalidate();
                }
                return;
            }

            bool isMoveSuccessful = _game.DoMove(_selectedRodIndex.Value, clickedRodIndex);
            _selectedRodIndex = null;
            pnlGame.Invalidate();

            if (isMoveSuccessful)
            {
                SetState(GameState.Playing);

                if (_game.IsGameWon)
                {
                    SetState(GameState.Finished);

                    MessageBox.Show(
                        string.Format("Поздравляем! Вы решили головоломку за {0} ходов!\n\nОптимальное количество: {1}", 
                                      _game.MoveHistory.Count, _game.OptimalMoveCount),
                        "Победа!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    SetState(GameState.Setup);
                }
            }
        }

        private void BtnUndoMove_Click(object sender, EventArgs e)
        {
            if (_game == null) return;

            _game.UndoMove();
            SetState(GameState.Playing);
        }

        // --- ЛОГИКА АВТОРЕШЕНИЯ ЧЕРЕЗ ТАЙМЕР ---

        private void BtnSolve_Click(object sender, EventArgs e)
        {
            if (_game == null) return;

            int disks = _game.DisksCount;

            _game = new HanoiGame(disks);
            lblCurrentMoveCount.Text = "0";

            SetState(GameState.AutoSolving);

            _solutionMoves = HanoiSolver.GetSolution(disks);
            _currentSolveMoveIndex = 0;

            _solveTimer.Start();
        }

        private void SolveTimer_Tick(object sender, EventArgs e)
        {
            // Проверка: вдруг таймер сработал, а мы уже не в режиме решения
            if (_currentState != GameState.AutoSolving)
            {
                _solveTimer.Stop();
                return;
            }

            if (_currentSolveMoveIndex < _solutionMoves.Count)
            {
                Move move = _solutionMoves[_currentSolveMoveIndex];
                _game.DoMove(move.FromRod, move.ToRod);
                
                lblCurrentMoveCount.Text = _game.MoveHistory.Count.ToString();
                pnlGame.Invalidate();
                
                _currentSolveMoveIndex++;
            }
            else
            {
                // Решение закончено
                _solveTimer.Stop();
                SetState(GameState.Playing);

                if (_game != null && _game.IsGameWon)
                {
                    SetState(GameState.Finished);
                    MessageBox.Show(
                        "Автоматическое решение успешно завершено!",
                        "Решено",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    SetState(GameState.Setup);
                }
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            // Просто останавливаем таймер и возвращаем интерфейс к игре
            if (_currentState == GameState.AutoSolving)
            {
                _solveTimer.Stop();
                SetState(GameState.Playing);
            }
        }
    }
}