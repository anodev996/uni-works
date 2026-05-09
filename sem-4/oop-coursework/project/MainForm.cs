using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project;

public partial class MainForm : Form
{
    // Rendering constants for responsive design
    private const float RodARelativeX = 0.175f;
    private const float RodBRelativeX = 0.5f;
    private const float RodCRelativeX = 0.825f;
    private const float BaseRelativeY = 0.913f;
    private const float MaxDiskRelativeWidth = 0.29f;
    private const float DiskRelativeHeight = 0.092f;
    private const int SolversDelayMs = 1500;

    // enum for state machine approach
    private enum GameState
    {
        Setup,
        Playing,
        AutoSolving,
        Finished
    }
    private GameState _currentState;

    // Business logic 
    private HanoiGame? _game;
    
    // Aux vars
    private int? _selectedRodIndex;
    private bool _isSolving;

    // Images
    private readonly Image _rodsImg;
    private readonly float _rodsAspectRatio;
    private readonly Image[] _diskImgs;

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
                _isSolving = false;
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
                _isSolving = false;
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
                _isSolving = true;
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
                _isSolving = false;
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

    private void btnDrawDisks_Click(object sender, EventArgs e)
    {
        if (_currentState == GameState.Playing)
        {
            SetState(GameState.Setup);
            return;
        }

        if (int.TryParse(txtBxDisks.Text, out int disks) && disks is >= 1 and <= 9)
        {
            _game = new HanoiGame(disks);
            SetState(GameState.Playing);
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
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // Calculate render dimensions preserving aspect ratio
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

        float[] rodCentersX =
        [
            x + (renderWidth * RodARelativeX),
            x + (renderWidth * RodBRelativeX),
            x + (renderWidth * RodCRelativeX)
        ];

        float baseY = y + (renderHeight * BaseRelativeY);

        using Pen highlightPen = new Pen(Color.White, 3f)
        {
            DashStyle = System.Drawing.Drawing2D.DashStyle.Dash,
            DashCap = System.Drawing.Drawing2D.DashCap.Round
        };

        for (int i = 0; i < 3; i++)
        {
            var disks = _game.Rods[i].Disks.ToArray();

            for (int j = 0; j < disks.Length; j++)
            {
                int currentDiskSize = disks[j].Number;

                // Determine disk dimensions
                float currentDiskWidth = renderWidth * MaxDiskRelativeWidth * (1 - ((currentDiskSize - 1) * 0.1f));
                float currentDiskHeight = renderHeight * DiskRelativeHeight;

                float diskX = rodCentersX[i] - (currentDiskWidth / 2f);
                int positionFromBottom = disks.Length - j;
                float diskY = baseY - (positionFromBottom * currentDiskHeight * 0.98f) - 1;

                g.DrawImage(_diskImgs[currentDiskSize - 1], diskX, diskY, currentDiskWidth, currentDiskHeight);

                // Draw highlight if selected (top disk of the selected rod)
                if (_selectedRodIndex == i && j == 0)
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

    private void pnlGame_MouseClick(object sender, MouseEventArgs e)
    {
        if (_game == null) return;

        int thirdOfWidth = pnlGame.Width / 3;
        int clickedRodIndex = e.X / thirdOfWidth;

        // Ensure index doesn't exceed 2 (in case of edge clicks)
        clickedRodIndex = Math.Min(clickedRodIndex, 2);

        // Pick up a disk
        if (_selectedRodIndex == null)
        {
            if (_game.Rods[clickedRodIndex].Disks.Count > 0)
            {
                _selectedRodIndex = clickedRodIndex;
                pnlGame.Invalidate();
            }
            return;
        }

        // Drop a disk
        bool isMoveSuccessful = _game.DoMove(_selectedRodIndex.Value, clickedRodIndex);
        _selectedRodIndex = null;

        if (isMoveSuccessful)
        {
            lblCurrentMoveCount.Text = _game.MoveHistory.Count.ToString();

            SetState(GameState.Playing);

            if (_game.IsGameWon)
            {
                SetState(GameState.Finished);

                MessageBox.Show(
                    $"Поздравляем! Вы решили головоломку за {_game.MoveHistory.Count} ходов!\n\nОптимальное количество: {_game.OptimalMoveCount}",
                    "Победа!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                SetState(GameState.Setup);
            }
        }
    }

    private void btnUndoMove_Click(object sender, EventArgs e)
    {
        if (_game == null) return;

        _game.UndoMove();
        SetState(GameState.Playing);
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
        _isSolving = false;
    }

    private async void btnSolve_Click(object sender, EventArgs e)
    {
        if (_game == null) return;

        int disks = _game.DisksCount;

        _game = new HanoiGame(disks);
        lblCurrentMoveCount.Text = "0";

        SetState(GameState.AutoSolving);

        List<Move> solutionMoves = HanoiSolver.GetSolution(disks);

        foreach (Move move in solutionMoves)
        {
            if (!_isSolving) break;

            await Task.Delay(SolversDelayMs);

            if (!_isSolving) break;

            _game.DoMove(move.FromRod, move.ToRod);
            lblCurrentMoveCount.Text = _game.MoveHistory.Count.ToString();
            pnlGame.Invalidate();
        }

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