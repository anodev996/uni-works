namespace project
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btnDrawDisks = new Button();
            tblLtPnlGame = new TableLayoutPanel();
            pnlGame = new Panel();
            pnlGameControls = new Panel();
            lblSettings = new Label();
            lblDisks = new Label();
            txtBxDisks = new TextBox();
            lblStatistics = new Label();
            lblCurrentMove = new Label();
            lblCurrentMoveCount = new Label();
            lblOptimal = new Label();
            lblOptimalCount = new Label();
            lblControls = new Label();
            btnSolve = new Button();
            btnStop = new Button();
            btnUndoMove = new Button();
            btnRules = new Button();
            pnlGreet = new Panel();
            lblGreet = new Label();
            btnBegin = new Button();
            pnlRules = new Panel();
            lblRulesTitle = new Label();
            lblRules = new Label();
            btnClose = new Button();
            tblLtPnlGame.SuspendLayout();
            pnlGameControls.SuspendLayout();
            pnlGreet.SuspendLayout();
            pnlRules.SuspendLayout();
            SuspendLayout();
            // 
            // btnDrawDisks
            // 
            btnDrawDisks.Anchor = AnchorStyles.None;
            btnDrawDisks.BackColor = Color.FromArgb(133, 211, 109);
            btnDrawDisks.FlatStyle = FlatStyle.Flat;
            btnDrawDisks.Font = new Font("Calibri", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDrawDisks.ForeColor = Color.White;
            btnDrawDisks.Location = new Point(34, 103);
            btnDrawDisks.Name = "btnDrawDisks";
            btnDrawDisks.Size = new Size(187, 35);
            btnDrawDisks.TabIndex = 0;
            btnDrawDisks.Text = "Нарисовать";
            btnDrawDisks.UseVisualStyleBackColor = false;
            btnDrawDisks.Click += btnDrawDisks_Click;
            // 
            // tblLtPnlGame
            // 
            tblLtPnlGame.BackColor = Color.White;
            tblLtPnlGame.ColumnCount = 2;
            tblLtPnlGame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tblLtPnlGame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblLtPnlGame.Controls.Add(pnlGame, 0, 0);
            tblLtPnlGame.Controls.Add(pnlGameControls, 1, 0);
            tblLtPnlGame.Dock = DockStyle.Fill;
            tblLtPnlGame.Location = new Point(0, 0);
            tblLtPnlGame.MinimumSize = new Size(1152, 648);
            tblLtPnlGame.Name = "tblLtPnlGame";
            tblLtPnlGame.RowCount = 1;
            tblLtPnlGame.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblLtPnlGame.Size = new Size(1152, 648);
            tblLtPnlGame.TabIndex = 2;
            tblLtPnlGame.Visible = false;
            // 
            // pnlGame
            // 
            pnlGame.BackColor = Color.Transparent;
            pnlGame.Dock = DockStyle.Fill;
            pnlGame.Location = new Point(20, 20);
            pnlGame.Margin = new Padding(20);
            pnlGame.Name = "pnlGame";
            pnlGame.Size = new Size(824, 608);
            pnlGame.TabIndex = 2;
            pnlGame.Paint += pnlGame_Paint;
            pnlGame.MouseClick += pnlGame_MouseClick;
            // 
            // pnlGameControls
            // 
            pnlGameControls.Anchor = AnchorStyles.None;
            pnlGameControls.BackColor = Color.FromArgb(235, 235, 239);
            pnlGameControls.Controls.Add(lblSettings);
            pnlGameControls.Controls.Add(lblDisks);
            pnlGameControls.Controls.Add(txtBxDisks);
            pnlGameControls.Controls.Add(btnDrawDisks);
            pnlGameControls.Controls.Add(lblStatistics);
            pnlGameControls.Controls.Add(lblCurrentMove);
            pnlGameControls.Controls.Add(lblCurrentMoveCount);
            pnlGameControls.Controls.Add(lblOptimal);
            pnlGameControls.Controls.Add(lblOptimalCount);
            pnlGameControls.Controls.Add(lblControls);
            pnlGameControls.Controls.Add(btnSolve);
            pnlGameControls.Controls.Add(btnStop);
            pnlGameControls.Controls.Add(btnUndoMove);
            pnlGameControls.Controls.Add(btnRules);
            pnlGameControls.Location = new Point(884, 20);
            pnlGameControls.Margin = new Padding(20);
            pnlGameControls.Name = "pnlGameControls";
            pnlGameControls.Size = new Size(248, 608);
            pnlGameControls.TabIndex = 1;
            // 
            // lblSettings
            // 
            lblSettings.Anchor = AnchorStyles.None;
            lblSettings.AutoSize = true;
            lblSettings.BackColor = Color.Transparent;
            lblSettings.Font = new Font("Calibri Light", 14.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            lblSettings.Location = new Point(34, 32);
            lblSettings.Name = "lblSettings";
            lblSettings.Size = new Size(95, 23);
            lblSettings.TabIndex = 1;
            lblSettings.Text = "Настройки";
            // 
            // lblDisks
            // 
            lblDisks.Anchor = AnchorStyles.None;
            lblDisks.AutoSize = true;
            lblDisks.BackColor = Color.Transparent;
            lblDisks.Font = new Font("Calibri Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDisks.Location = new Point(34, 70);
            lblDisks.Name = "lblDisks";
            lblDisks.Size = new Size(64, 23);
            lblDisks.TabIndex = 2;
            lblDisks.Text = "Колец:";
            // 
            // txtBxDisks
            // 
            txtBxDisks.Anchor = AnchorStyles.None;
            txtBxDisks.BorderStyle = BorderStyle.None;
            txtBxDisks.Font = new Font("Calibri Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBxDisks.Location = new Point(104, 70);
            txtBxDisks.Name = "txtBxDisks";
            txtBxDisks.Size = new Size(52, 24);
            txtBxDisks.TabIndex = 3;
            txtBxDisks.Text = "0";
            txtBxDisks.TextAlign = HorizontalAlignment.Center;
            // 
            // lblStatistics
            // 
            lblStatistics.Anchor = AnchorStyles.None;
            lblStatistics.AutoSize = true;
            lblStatistics.BackColor = Color.Transparent;
            lblStatistics.Font = new Font("Calibri Light", 14.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            lblStatistics.Location = new Point(34, 168);
            lblStatistics.Name = "lblStatistics";
            lblStatistics.Size = new Size(96, 23);
            lblStatistics.TabIndex = 4;
            lblStatistics.Text = "Статистика";
            // 
            // lblCurrentMove
            // 
            lblCurrentMove.Anchor = AnchorStyles.None;
            lblCurrentMove.AutoSize = true;
            lblCurrentMove.BackColor = Color.Transparent;
            lblCurrentMove.Font = new Font("Calibri Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCurrentMove.Location = new Point(34, 204);
            lblCurrentMove.Name = "lblCurrentMove";
            lblCurrentMove.Size = new Size(113, 23);
            lblCurrentMove.TabIndex = 7;
            lblCurrentMove.Text = "Текущий ход:";
            // 
            // lblCurrentMoveCount
            // 
            lblCurrentMoveCount.Anchor = AnchorStyles.None;
            lblCurrentMoveCount.AutoSize = true;
            lblCurrentMoveCount.BackColor = Color.Transparent;
            lblCurrentMoveCount.Font = new Font("Calibri Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCurrentMoveCount.Location = new Point(153, 204);
            lblCurrentMoveCount.Name = "lblCurrentMoveCount";
            lblCurrentMoveCount.RightToLeft = RightToLeft.No;
            lblCurrentMoveCount.Size = new Size(20, 23);
            lblCurrentMoveCount.TabIndex = 5;
            lblCurrentMoveCount.Text = "0";
            // 
            // lblOptimal
            // 
            lblOptimal.Anchor = AnchorStyles.None;
            lblOptimal.AutoSize = true;
            lblOptimal.BackColor = Color.Transparent;
            lblOptimal.Font = new Font("Calibri Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOptimal.Location = new Point(34, 237);
            lblOptimal.Name = "lblOptimal";
            lblOptimal.Size = new Size(114, 23);
            lblOptimal.TabIndex = 6;
            lblOptimal.Text = "Оптимально:";
            // 
            // lblOptimalCount
            // 
            lblOptimalCount.Anchor = AnchorStyles.None;
            lblOptimalCount.AutoSize = true;
            lblOptimalCount.BackColor = Color.Transparent;
            lblOptimalCount.Font = new Font("Calibri Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOptimalCount.Location = new Point(154, 237);
            lblOptimalCount.Name = "lblOptimalCount";
            lblOptimalCount.RightToLeft = RightToLeft.No;
            lblOptimalCount.Size = new Size(20, 23);
            lblOptimalCount.TabIndex = 8;
            lblOptimalCount.Text = "0";
            // 
            // lblControls
            // 
            lblControls.Anchor = AnchorStyles.None;
            lblControls.AutoSize = true;
            lblControls.BackColor = Color.Transparent;
            lblControls.Font = new Font("Calibri Light", 14.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            lblControls.Location = new Point(34, 296);
            lblControls.Name = "lblControls";
            lblControls.Size = new Size(105, 23);
            lblControls.TabIndex = 9;
            lblControls.Text = "Управление";
            // 
            // btnSolve
            // 
            btnSolve.Anchor = AnchorStyles.None;
            btnSolve.BackColor = Color.FromArgb(133, 211, 109);
            btnSolve.Enabled = false;
            btnSolve.FlatStyle = FlatStyle.Flat;
            btnSolve.Font = new Font("Calibri", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSolve.ForeColor = Color.White;
            btnSolve.Location = new Point(34, 332);
            btnSolve.Name = "btnSolve";
            btnSolve.Size = new Size(187, 35);
            btnSolve.TabIndex = 10;
            btnSolve.Text = "Решить";
            btnSolve.UseVisualStyleBackColor = false;
            btnSolve.Click += btnSolve_Click;
            // 
            // btnStop
            // 
            btnStop.Anchor = AnchorStyles.None;
            btnStop.BackColor = Color.FromArgb(133, 211, 109);
            btnStop.Enabled = false;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new Font("Calibri", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStop.ForeColor = Color.White;
            btnStop.Location = new Point(34, 382);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(187, 35);
            btnStop.TabIndex = 11;
            btnStop.Text = "Стоп";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // btnUndoMove
            // 
            btnUndoMove.Anchor = AnchorStyles.None;
            btnUndoMove.BackColor = Color.FromArgb(133, 211, 109);
            btnUndoMove.Enabled = false;
            btnUndoMove.FlatStyle = FlatStyle.Flat;
            btnUndoMove.Font = new Font("Calibri", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUndoMove.ForeColor = Color.White;
            btnUndoMove.Location = new Point(34, 429);
            btnUndoMove.Name = "btnUndoMove";
            btnUndoMove.Size = new Size(187, 35);
            btnUndoMove.TabIndex = 12;
            btnUndoMove.Text = "Отменить ход";
            btnUndoMove.UseVisualStyleBackColor = false;
            btnUndoMove.Click += btnUndoMove_Click;
            // 
            // btnRules
            // 
            btnRules.Anchor = AnchorStyles.None;
            btnRules.BackColor = Color.FromArgb(133, 211, 109);
            btnRules.FlatStyle = FlatStyle.Flat;
            btnRules.Font = new Font("Calibri", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRules.ForeColor = Color.White;
            btnRules.Location = new Point(34, 521);
            btnRules.Name = "btnRules";
            btnRules.Size = new Size(187, 35);
            btnRules.TabIndex = 13;
            btnRules.Text = "Справка";
            btnRules.UseVisualStyleBackColor = false;
            btnRules.Click += btnRules_Click;
            // 
            // pnlGreet
            // 
            pnlGreet.Anchor = AnchorStyles.None;
            pnlGreet.BackColor = Color.FromArgb(235, 235, 239);
            pnlGreet.Controls.Add(lblGreet);
            pnlGreet.Controls.Add(btnBegin);
            pnlGreet.Location = new Point(256, 146);
            pnlGreet.Margin = new Padding(0);
            pnlGreet.Name = "pnlGreet";
            pnlGreet.Size = new Size(641, 356);
            pnlGreet.TabIndex = 4;
            // 
            // lblGreet
            // 
            lblGreet.AutoSize = true;
            lblGreet.Font = new Font("Calibri Light", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGreet.Location = new Point(51, 41);
            lblGreet.Name = "lblGreet";
            lblGreet.Size = new Size(538, 195);
            lblGreet.TabIndex = 3;
            lblGreet.Text = "Курсовая работа\r\n\r\nРазработка игры \"Ханойские башни\"\r\n\r\nВыполнил: студент Бадулин И.А., 424-1";
            lblGreet.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnBegin
            // 
            btnBegin.BackColor = Color.FromArgb(133, 211, 109);
            btnBegin.FlatStyle = FlatStyle.Flat;
            btnBegin.Font = new Font("Calibri", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBegin.ForeColor = Color.White;
            btnBegin.Location = new Point(224, 269);
            btnBegin.Name = "btnBegin";
            btnBegin.Size = new Size(193, 57);
            btnBegin.TabIndex = 3;
            btnBegin.Text = "Начать";
            btnBegin.UseVisualStyleBackColor = false;
            btnBegin.Click += btnBegin_Click;
            // 
            // pnlRules
            // 
            pnlRules.Anchor = AnchorStyles.None;
            pnlRules.BackColor = Color.FromArgb(235, 235, 239);
            pnlRules.Controls.Add(lblRulesTitle);
            pnlRules.Controls.Add(lblRules);
            pnlRules.Controls.Add(btnClose);
            pnlRules.Location = new Point(89, 42);
            pnlRules.Margin = new Padding(0);
            pnlRules.Name = "pnlRules";
            pnlRules.Size = new Size(974, 565);
            pnlRules.TabIndex = 5;
            pnlRules.Visible = false;
            // 
            // lblRulesTitle
            // 
            lblRulesTitle.AutoSize = true;
            lblRulesTitle.Font = new Font("Calibri", 24F);
            lblRulesTitle.Location = new Point(423, 18);
            lblRulesTitle.Name = "lblRulesTitle";
            lblRulesTitle.Size = new Size(128, 39);
            lblRulesTitle.TabIndex = 4;
            lblRulesTitle.Text = "Справка";
            // 
            // lblRules
            // 
            lblRules.AutoSize = true;
            lblRules.Font = new Font("Calibri Light", 13F);
            lblRules.Location = new Point(85, 68);
            lblRules.Name = "lblRules";
            lblRules.Size = new Size(804, 396);
            lblRules.TabIndex = 3;
            lblRules.Text = resources.GetString("lblRules.Text");
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(133, 211, 109);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Calibri", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(391, 488);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(193, 57);
            btnClose.TabIndex = 3;
            btnClose.Text = "Закрыть";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1152, 648);
            Controls.Add(tblLtPnlGame);
            Controls.Add(pnlGreet);
            Controls.Add(pnlRules);
            MinimumSize = new Size(1168, 687);
            Name = "MainForm";
            Text = "Ханойские башни";
            tblLtPnlGame.ResumeLayout(false);
            pnlGameControls.ResumeLayout(false);
            pnlGameControls.PerformLayout();
            pnlGreet.ResumeLayout(false);
            pnlGreet.PerformLayout();
            pnlRules.ResumeLayout(false);
            pnlRules.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnDrawDisks;
        private TableLayoutPanel tblLtPnlGame;
        private Panel pnlGameControls;
        private Label lblSettings;
        private Label lblDisks;
        private TextBox txtBxDisks;
        private Label lblStatistics;
        private Label lblCurrentMoveCount;
        private Label lblOptimal;
        private Label lblCurrentMove;
        private Label lblOptimalCount;
        private Label lblControls;
        private Button btnSolve;
        private Button btnStop;
        private Button btnUndoMove;
        private Button btnRules;
        private Panel pnlGame;
        private Panel pnlGreet;
        private Label lblGreet;
        private Button btnBegin;
        private Panel pnlRules;
        private Label lblRules;
        private Button btnClose;
        private Label lblRulesTitle;
    }
}
