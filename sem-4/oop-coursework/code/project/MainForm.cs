using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace project
{
    public partial class MainForm : Form
    {
        // Constants for calculating relative positions and sizes when drawing the game
        private const float RodARelativeX = 0.175f;
        private const float RodBRelativeX = 0.5f;
        private const float RodCRelativeX = 0.825f;
        private const float BaseRelativeY = 0.913f;
        private const float MaxDiskRelativeWidth = 0.29f;
        private const float DiskRelativeHeight = 0.092f;
        private const int SolversDelayMs = 1500; // Delay in milliseconds for the auto-solver

        // Represents the different states the application can be in
        private enum GameState
        {
            Setup,       // Waiting for user to input disk count
            Playing,     // User is actively playing
            AutoSolving, // Auto-solver is currently making moves
            Finished     // The game has been won
        }

        private GameState _currentState; // Stores the current state of the game

        private HanoiGame _game;         // The main game logic instance
        private int? _selectedRodIndex;  // The index of the currently selected rod (for moving disks)

        // Graphics and images
        private readonly Image _rodsImg;
        private readonly float _rodsAspectRatio;
        private readonly Image[] _diskImgs;

        // Timer and variables used for the auto-solving animation
        private readonly Timer _solveTimer;
        private List<Move> _solutionMoves;
        private int _currentSolveMoveIndex;

        public MainForm()
        {
            InitializeComponent();

            // Load resources (images) for rods and disks
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

            // Initialize the timer for the auto-solver
            _solveTimer = new Timer
            {
                Interval = SolversDelayMs
            };
            _solveTimer.Tick += new EventHandler(SolveTimer_Tick);
        }

        // Updates the UI elements based on the current game state
        private void SetState(GameState newState)
        {
            _currentState = newState;

            switch (_currentState)
            {
                case GameState.Setup:
                    {
                        // Reset game and UI for a new setup
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
                        // Enable controls for manual playing
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
                        // Only enable Undo button if there are moves in the history
                        btnUndoMove.Enabled = _game != null && _game.MoveHistory.Count > 0;
                        pnlGame.Enabled = true;
                        break;
                    }

                case GameState.AutoSolving:
                    {
                        // Disable most controls while the auto-solver is running
                        _selectedRodIndex = null;
                        txtBxDisks.Enabled = false;
                        btnDrawDisks.Enabled = false;
                        if (_game != null)
                        {
                            lblOptimalCount.Text = _game.OptimalMoveCount.ToString();
                        }
                        btnSolve.Enabled = false;
                        btnStop.Enabled = true; // Allow the user to stop the solver
                        btnUndoMove.Enabled = false;
                        pnlGame.Enabled = false; // Prevent manual moves during auto-solve
                        break;
                    }
                case GameState.Finished:
                    {
                        // Disable all gameplay controls when the game is won
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

            // Request a redraw of the game panel to reflect changes
            pnlGame.Invalidate();
        }

        // Event handler for the "Draw Disks" (Start/Restart) button
        private void BtnDrawDisks_Click(object sender, EventArgs e)
        {
            // If playing, restart the game setup
            if (_currentState == GameState.Playing)
            {
                SetState(GameState.Setup);
                return;
            }

            // Parse the number of disks from the text box
            if (int.TryParse(txtBxDisks.Text, out int disks) && disks >= 1 && disks <= 9)
            {
                // Create a new game and switch to Playing state
                _game = new HanoiGame(disks);
                SetState(GameState.Playing);
            }
            else
            {
                // Show error message if input is invalid
                MessageBox.Show("Пожалуйста, введите число от 1 до 9.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Event handler for the "Begin" button on the greeting screen
        private void BtnBegin_Click(object sender, EventArgs e)
        {
            pnlGreet.Visible = false;      // Hide greeting panel
            tblLtPnlGame.Visible = true;   // Show the main game layout
        }

        // Event handler for the "Rules" (Help) button
        private void BtnRules_Click(object sender, EventArgs e)
        {
            tblLtPnlGame.Visible = false;  // Hide game layout
            pnlRules.Visible = true;       // Show rules panel
        }

        // Event handler for the "Close" button on the rules screen
        private void BtnClose_Click(object sender, EventArgs e)
        {
            pnlRules.Visible = false;      // Hide rules panel
            tblLtPnlGame.Visible = true;   // Show game layout again
        }

        // Event handler for drawing the game field and disks on the panel
        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Smooth edges for drawing

            // Calculate the proper size to keep the aspect ratio of the rods image
            int renderWidth = pnlGame.Width;
            int renderHeight = (int)(renderWidth / _rodsAspectRatio);

            if (renderHeight > pnlGame.Height)
            {
                renderHeight = pnlGame.Height;
                renderWidth = (int)(renderHeight * _rodsAspectRatio);
            }

            // Center the image in the panel
            int x = (pnlGame.Width - renderWidth) / 2;
            int y = (pnlGame.Height - renderHeight) / 2;

            // Draw the background rods image
            g.DrawImage(_rodsImg, x, y, renderWidth, renderHeight);

            if (_game == null) return; // Exit if game is not started

            // Calculate the X coordinates for the center of each rod
            float[] rodCentersX = new float[]
            {
                x + (renderWidth * RodARelativeX),
                x + (renderWidth * RodBRelativeX),
                x + (renderWidth * RodCRelativeX)
            };

            // Calculate the Y coordinate for the base (bottom) of the rods
            float baseY = y + (renderHeight * BaseRelativeY);

            // Pen used to draw a highlight border around the selected disk
            using (Pen highlightPen = new Pen(Color.White, 3f))
            {
                highlightPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                highlightPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;

                // Loop through all 3 rods
                for (int i = 0; i < 3; i++)
                {
                    Disk[] disks = _game.Rods[i].Disks.ToArray();

                    // Loop through all disks on the current rod
                    for (int j = 0; j < disks.Length; j++)
                    {
                        int currentDiskSize = disks[j].Number;

                        // Calculate the width and height of the current disk
                        float currentDiskWidth = renderWidth * MaxDiskRelativeWidth * (1 - ((currentDiskSize - 1) * 0.1f));
                        float currentDiskHeight = renderHeight * DiskRelativeHeight;

                        // Calculate X and Y coordinates to place the disk
                        float diskX = rodCentersX[i] - (currentDiskWidth / 2f);
                        int positionFromBottom = disks.Length - j;
                        float diskY = baseY - (positionFromBottom * currentDiskHeight * 0.98f) - 1;

                        // Draw the disk image
                        g.DrawImage(_diskImgs[currentDiskSize - 1], diskX, diskY, currentDiskWidth, currentDiskHeight);

                        // If this rod is selected and this is the top disk, draw a highlight border
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

        // Event handler for mouse clicks on the game panel (to select and move disks)
        private void PnlGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (_game == null) return;

            // Divide the panel width by 3 to determine which rod was clicked
            int thirdOfWidth = pnlGame.Width / 3;
            int clickedRodIndex = e.X / thirdOfWidth;

            // Ensure the index doesn't exceed 2 (in case of clicking the exact right edge)
            clickedRodIndex = Math.Min(clickedRodIndex, 2);

            // If no rod is currently selected, select the clicked rod (if it has disks)
            if (_selectedRodIndex == null)
            {
                if (_game.Rods[clickedRodIndex].Disks.Count > 0)
                {
                    _selectedRodIndex = clickedRodIndex;
                    pnlGame.Invalidate(); // Redraw to show the highlight
                }
                return;
            }

            // Attempt to move a disk from the selected rod to the clicked rod
            bool isMoveSuccessful = _game.DoMove(_selectedRodIndex.Value, clickedRodIndex);

            // Clear the selection
            _selectedRodIndex = null;
            pnlGame.Invalidate(); // Redraw the panel

            // If the move was valid and successful
            if (isMoveSuccessful)
            {
                // Refresh the UI state (e.g., enable Undo button)
                SetState(GameState.Playing);

                // Check if the game is won after this move
                if (_game.IsGameWon)
                {
                    SetState(GameState.Finished);

                    // Show victory message
                    MessageBox.Show(
                        string.Format("Поздравляем! Вы решили головоломку за {0} ходов!\n\nОптимальное количество: {1}",
                                      _game.MoveHistory.Count, _game.OptimalMoveCount),
                        "Победа!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Reset to setup state after clicking OK
                    SetState(GameState.Setup);
                }
            }
        }

        // Event handler for the "Undo Move" button
        private void BtnUndoMove_Click(object sender, EventArgs e)
        {
            if (_game == null) return;

            // Undo the last move in the game logic
            _game.UndoMove();

            // Refresh the UI state
            SetState(GameState.Playing);
        }

        // Event handler for the "Solve" (Auto-Solve) button
        private void BtnSolve_Click(object sender, EventArgs e)
        {
            if (_game == null) return;

            int disks = _game.DisksCount;

            // Reset the game to the initial state with the same number of disks
            _game = new HanoiGame(disks);
            lblCurrentMoveCount.Text = "0";

            // Switch to auto-solving state
            SetState(GameState.AutoSolving);

            // Get the list of optimal moves from the solver algorithm
            _solutionMoves = HanoiSolver.GetSolution(disks);
            _currentSolveMoveIndex = 0; // Reset the current move counter

            // Start the timer to animate the solution
            _solveTimer.Start();
        }

        // Timer tick event for animating the auto-solver moves step by step
        private void SolveTimer_Tick(object sender, EventArgs e)
        {
            // Check if the user manually stopped the auto-solver
            if (_currentState != GameState.AutoSolving)
            {
                _solveTimer.Stop();
                return;
            }

            // If there are still moves left in the solution list
            if (_currentSolveMoveIndex < _solutionMoves.Count)
            {
                Move move = _solutionMoves[_currentSolveMoveIndex];
                // Execute the next move
                _game.DoMove(move.FromRod, move.ToRod);

                // Update the move counter text
                lblCurrentMoveCount.Text = _game.MoveHistory.Count.ToString();

                // Redraw the panel to show the disk in its new position
                pnlGame.Invalidate();

                _currentSolveMoveIndex++; // Move to the next index for the next tick
            }
            else
            {
                // All solution moves are completed
                _solveTimer.Stop();
                SetState(GameState.Playing);

                // Double check if the game is won
                if (_game != null && _game.IsGameWon)
                {
                    SetState(GameState.Finished);

                    // Show success message
                    MessageBox.Show(
                        "Автоматическое решение успешно завершено!",
                        "Решено",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Reset to setup state after clicking OK
                    SetState(GameState.Setup);
                }
            }
        }

        // Event handler for the "Stop" button (interrupts auto-solver)
        private void BtnStop_Click(object sender, EventArgs e)
        {
            // Stop the timer and return control to the player
            if (_currentState == GameState.AutoSolving)
            {
                _solveTimer.Stop();
                SetState(GameState.Playing);
            }
        }
    }
}