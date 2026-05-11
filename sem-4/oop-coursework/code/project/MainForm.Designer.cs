using System;
using System.Drawing;
using System.Windows.Forms;

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
            this.btnDrawDisks = new System.Windows.Forms.Button();
            this.tblLtPnlGame = new System.Windows.Forms.TableLayoutPanel();
            this.pnlGame = new System.Windows.Forms.Panel();
            this.pnlGameControls = new System.Windows.Forms.Panel();
            this.lblSettings = new System.Windows.Forms.Label();
            this.lblDisks = new System.Windows.Forms.Label();
            this.txtBxDisks = new System.Windows.Forms.TextBox();
            this.lblStatistics = new System.Windows.Forms.Label();
            this.lblCurrentMove = new System.Windows.Forms.Label();
            this.lblCurrentMoveCount = new System.Windows.Forms.Label();
            this.lblOptimal = new System.Windows.Forms.Label();
            this.lblOptimalCount = new System.Windows.Forms.Label();
            this.lblControls = new System.Windows.Forms.Label();
            this.btnSolve = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnUndoMove = new System.Windows.Forms.Button();
            this.btnRules = new System.Windows.Forms.Button();
            this.pnlGreet = new System.Windows.Forms.Panel();
            this.lblGreet = new System.Windows.Forms.Label();
            this.btnBegin = new System.Windows.Forms.Button();
            this.pnlRules = new System.Windows.Forms.Panel();
            this.lblRulesTitle = new System.Windows.Forms.Label();
            this.lblRules = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tblLtPnlGame.SuspendLayout();
            this.pnlGameControls.SuspendLayout();
            this.pnlGreet.SuspendLayout();
            this.pnlRules.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDrawDisks
            // 
            this.btnDrawDisks.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDrawDisks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(211)))), ((int)(((byte)(109)))));
            this.btnDrawDisks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrawDisks.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDrawDisks.ForeColor = System.Drawing.Color.White;
            this.btnDrawDisks.Location = new System.Drawing.Point(42, 120);
            this.btnDrawDisks.Name = "btnDrawDisks";
            this.btnDrawDisks.Size = new System.Drawing.Size(170, 35);
            this.btnDrawDisks.TabIndex = 0;
            this.btnDrawDisks.Text = "Нарисовать";
            this.btnDrawDisks.UseVisualStyleBackColor = false;
            this.btnDrawDisks.Click += new System.EventHandler(this.BtnDrawDisks_Click);
            // 
            // tblLtPnlGame
            // 
            this.tblLtPnlGame.BackColor = System.Drawing.Color.White;
            this.tblLtPnlGame.ColumnCount = 2;
            this.tblLtPnlGame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tblLtPnlGame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLtPnlGame.Controls.Add(this.pnlGame, 0, 0);
            this.tblLtPnlGame.Controls.Add(this.pnlGameControls, 1, 0);
            this.tblLtPnlGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLtPnlGame.Location = new System.Drawing.Point(0, 0);
            this.tblLtPnlGame.MinimumSize = new System.Drawing.Size(1152, 648);
            this.tblLtPnlGame.Name = "tblLtPnlGame";
            this.tblLtPnlGame.RowCount = 1;
            this.tblLtPnlGame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLtPnlGame.Size = new System.Drawing.Size(1152, 648);
            this.tblLtPnlGame.TabIndex = 2;
            this.tblLtPnlGame.Visible = false;
            // 
            // pnlGame
            // 
            this.pnlGame.BackColor = System.Drawing.Color.Transparent;
            this.pnlGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGame.Location = new System.Drawing.Point(17, 17);
            this.pnlGame.Margin = new System.Windows.Forms.Padding(17);
            this.pnlGame.Name = "pnlGame";
            this.pnlGame.Size = new System.Drawing.Size(830, 614);
            this.pnlGame.TabIndex = 2;
            this.pnlGame.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlGame_Paint);
            this.pnlGame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PnlGame_MouseClick);
            // 
            // pnlGameControls
            // 
            this.pnlGameControls.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlGameControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
            this.pnlGameControls.Controls.Add(this.lblSettings);
            this.pnlGameControls.Controls.Add(this.lblDisks);
            this.pnlGameControls.Controls.Add(this.txtBxDisks);
            this.pnlGameControls.Controls.Add(this.btnDrawDisks);
            this.pnlGameControls.Controls.Add(this.lblStatistics);
            this.pnlGameControls.Controls.Add(this.lblCurrentMove);
            this.pnlGameControls.Controls.Add(this.lblCurrentMoveCount);
            this.pnlGameControls.Controls.Add(this.lblOptimal);
            this.pnlGameControls.Controls.Add(this.lblOptimalCount);
            this.pnlGameControls.Controls.Add(this.lblControls);
            this.pnlGameControls.Controls.Add(this.btnSolve);
            this.pnlGameControls.Controls.Add(this.btnStop);
            this.pnlGameControls.Controls.Add(this.btnUndoMove);
            this.pnlGameControls.Controls.Add(this.btnRules);
            this.pnlGameControls.Location = new System.Drawing.Point(881, 17);
            this.pnlGameControls.Margin = new System.Windows.Forms.Padding(17);
            this.pnlGameControls.Name = "pnlGameControls";
            this.pnlGameControls.Size = new System.Drawing.Size(254, 614);
            this.pnlGameControls.TabIndex = 1;
            // 
            // lblSettings
            // 
            this.lblSettings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSettings.AutoSize = true;
            this.lblSettings.BackColor = System.Drawing.Color.Transparent;
            this.lblSettings.Font = new System.Drawing.Font("Calibri Light", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettings.Location = new System.Drawing.Point(38, 55);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(95, 23);
            this.lblSettings.TabIndex = 1;
            this.lblSettings.Text = "Настройки";
            // 
            // lblDisks
            // 
            this.lblDisks.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDisks.AutoSize = true;
            this.lblDisks.BackColor = System.Drawing.Color.Transparent;
            this.lblDisks.Font = new System.Drawing.Font("Calibri Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisks.Location = new System.Drawing.Point(38, 88);
            this.lblDisks.Name = "lblDisks";
            this.lblDisks.Size = new System.Drawing.Size(64, 23);
            this.lblDisks.TabIndex = 2;
            this.lblDisks.Text = "Колец:";
            // 
            // txtBxDisks
            // 
            this.txtBxDisks.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBxDisks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBxDisks.Font = new System.Drawing.Font("Calibri Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxDisks.Location = new System.Drawing.Point(110, 88);
            this.txtBxDisks.Name = "txtBxDisks";
            this.txtBxDisks.Size = new System.Drawing.Size(45, 24);
            this.txtBxDisks.TabIndex = 3;
            this.txtBxDisks.Text = "0";
            this.txtBxDisks.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblStatistics
            // 
            this.lblStatistics.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatistics.AutoSize = true;
            this.lblStatistics.BackColor = System.Drawing.Color.Transparent;
            this.lblStatistics.Font = new System.Drawing.Font("Calibri Light", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatistics.Location = new System.Drawing.Point(38, 190);
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.Size = new System.Drawing.Size(96, 23);
            this.lblStatistics.TabIndex = 4;
            this.lblStatistics.Text = "Статистика";
            // 
            // lblCurrentMove
            // 
            this.lblCurrentMove.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCurrentMove.AutoSize = true;
            this.lblCurrentMove.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentMove.Font = new System.Drawing.Font("Calibri Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentMove.Location = new System.Drawing.Point(38, 221);
            this.lblCurrentMove.Name = "lblCurrentMove";
            this.lblCurrentMove.Size = new System.Drawing.Size(113, 23);
            this.lblCurrentMove.TabIndex = 7;
            this.lblCurrentMove.Text = "Текущий ход:";
            // 
            // lblCurrentMoveCount
            // 
            this.lblCurrentMoveCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCurrentMoveCount.AutoSize = true;
            this.lblCurrentMoveCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentMoveCount.Font = new System.Drawing.Font("Calibri Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentMoveCount.Location = new System.Drawing.Point(158, 221);
            this.lblCurrentMoveCount.Name = "lblCurrentMoveCount";
            this.lblCurrentMoveCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurrentMoveCount.Size = new System.Drawing.Size(20, 23);
            this.lblCurrentMoveCount.TabIndex = 5;
            this.lblCurrentMoveCount.Text = "0";
            // 
            // lblOptimal
            // 
            this.lblOptimal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOptimal.AutoSize = true;
            this.lblOptimal.BackColor = System.Drawing.Color.Transparent;
            this.lblOptimal.Font = new System.Drawing.Font("Calibri Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptimal.Location = new System.Drawing.Point(38, 249);
            this.lblOptimal.Name = "lblOptimal";
            this.lblOptimal.Size = new System.Drawing.Size(114, 23);
            this.lblOptimal.TabIndex = 6;
            this.lblOptimal.Text = "Оптимально:";
            // 
            // lblOptimalCount
            // 
            this.lblOptimalCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOptimalCount.AutoSize = true;
            this.lblOptimalCount.BackColor = System.Drawing.Color.Transparent;
            this.lblOptimalCount.Font = new System.Drawing.Font("Calibri Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptimalCount.Location = new System.Drawing.Point(158, 249);
            this.lblOptimalCount.Name = "lblOptimalCount";
            this.lblOptimalCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOptimalCount.Size = new System.Drawing.Size(20, 23);
            this.lblOptimalCount.TabIndex = 8;
            this.lblOptimalCount.Text = "0";
            // 
            // lblControls
            // 
            this.lblControls.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblControls.AutoSize = true;
            this.lblControls.BackColor = System.Drawing.Color.Transparent;
            this.lblControls.Font = new System.Drawing.Font("Calibri Light", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControls.Location = new System.Drawing.Point(38, 307);
            this.lblControls.Name = "lblControls";
            this.lblControls.Size = new System.Drawing.Size(105, 23);
            this.lblControls.TabIndex = 9;
            this.lblControls.Text = "Управление";
            // 
            // btnSolve
            // 
            this.btnSolve.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSolve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(211)))), ((int)(((byte)(109)))));
            this.btnSolve.Enabled = false;
            this.btnSolve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSolve.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolve.ForeColor = System.Drawing.Color.White;
            this.btnSolve.Location = new System.Drawing.Point(42, 339);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(170, 35);
            this.btnSolve.TabIndex = 10;
            this.btnSolve.Text = "Решить";
            this.btnSolve.UseVisualStyleBackColor = false;
            this.btnSolve.Click += new System.EventHandler(this.BtnSolve_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(211)))), ((int)(((byte)(109)))));
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(42, 387);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(170, 35);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "Стоп";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnUndoMove
            // 
            this.btnUndoMove.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUndoMove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(211)))), ((int)(((byte)(109)))));
            this.btnUndoMove.Enabled = false;
            this.btnUndoMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndoMove.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUndoMove.ForeColor = System.Drawing.Color.White;
            this.btnUndoMove.Location = new System.Drawing.Point(42, 433);
            this.btnUndoMove.Name = "btnUndoMove";
            this.btnUndoMove.Size = new System.Drawing.Size(170, 35);
            this.btnUndoMove.TabIndex = 12;
            this.btnUndoMove.Text = "Отменить ход";
            this.btnUndoMove.UseVisualStyleBackColor = false;
            this.btnUndoMove.Click += new System.EventHandler(this.BtnUndoMove_Click);
            // 
            // btnRules
            // 
            this.btnRules.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(211)))), ((int)(((byte)(109)))));
            this.btnRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRules.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRules.ForeColor = System.Drawing.Color.White;
            this.btnRules.Location = new System.Drawing.Point(42, 513);
            this.btnRules.Name = "btnRules";
            this.btnRules.Size = new System.Drawing.Size(170, 35);
            this.btnRules.TabIndex = 13;
            this.btnRules.Text = "Справка";
            this.btnRules.UseVisualStyleBackColor = false;
            this.btnRules.Click += new System.EventHandler(this.BtnRules_Click);
            // 
            // pnlGreet
            // 
            this.pnlGreet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlGreet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
            this.pnlGreet.Controls.Add(this.lblGreet);
            this.pnlGreet.Controls.Add(this.btnBegin);
            this.pnlGreet.Location = new System.Drawing.Point(257, 154);
            this.pnlGreet.Margin = new System.Windows.Forms.Padding(0);
            this.pnlGreet.Name = "pnlGreet";
            this.pnlGreet.Size = new System.Drawing.Size(639, 341);
            this.pnlGreet.TabIndex = 4;
            // 
            // lblGreet
            // 
            this.lblGreet.AutoSize = true;
            this.lblGreet.Font = new System.Drawing.Font("Calibri Light", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreet.Location = new System.Drawing.Point(50, 36);
            this.lblGreet.Name = "lblGreet";
            this.lblGreet.Size = new System.Drawing.Size(538, 195);
            this.lblGreet.TabIndex = 3;
            this.lblGreet.Text = "Курсовая работа\r\n\r\nРазработка игры \"Ханойские башни\"\r\n\r\nВыполнил: студент Бадулин" +
    " И.А., 424-1";
            this.lblGreet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBegin
            // 
            this.btnBegin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(211)))), ((int)(((byte)(109)))));
            this.btnBegin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBegin.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBegin.ForeColor = System.Drawing.Color.White;
            this.btnBegin.Location = new System.Drawing.Point(237, 254);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(165, 49);
            this.btnBegin.TabIndex = 3;
            this.btnBegin.Text = "Начать";
            this.btnBegin.UseVisualStyleBackColor = false;
            this.btnBegin.Click += new System.EventHandler(this.BtnBegin_Click);
            // 
            // pnlRules
            // 
            this.pnlRules.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlRules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
            this.pnlRules.Controls.Add(this.lblRulesTitle);
            this.pnlRules.Controls.Add(this.lblRules);
            this.pnlRules.Controls.Add(this.btnClose);
            this.pnlRules.Location = new System.Drawing.Point(111, 48);
            this.pnlRules.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRules.Name = "pnlRules";
            this.pnlRules.Size = new System.Drawing.Size(931, 552);
            this.pnlRules.TabIndex = 5;
            this.pnlRules.Visible = false;
            // 
            // lblRulesTitle
            // 
            this.lblRulesTitle.AutoSize = true;
            this.lblRulesTitle.Font = new System.Drawing.Font("Calibri Light", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRulesTitle.Location = new System.Drawing.Point(399, 18);
            this.lblRulesTitle.Name = "lblRulesTitle";
            this.lblRulesTitle.Size = new System.Drawing.Size(126, 39);
            this.lblRulesTitle.TabIndex = 4;
            this.lblRulesTitle.Text = "Справка";
            // 
            // lblRules
            // 
            this.lblRules.AutoSize = true;
            this.lblRules.Font = new System.Drawing.Font("Calibri Light", 13F);
            this.lblRules.Location = new System.Drawing.Point(63, 68);
            this.lblRules.Name = "lblRules";
            this.lblRules.Size = new System.Drawing.Size(804, 396);
            this.lblRules.TabIndex = 3;
            this.lblRules.Text = resources.GetString("lblRules.Text");
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(211)))), ((int)(((byte)(109)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(371, 480);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(165, 49);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1152, 648);
            this.Controls.Add(this.tblLtPnlGame);
            this.Controls.Add(this.pnlGreet);
            this.Controls.Add(this.pnlRules);
            this.MinimumSize = new System.Drawing.Size(1168, 687);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ханойские башни";
            this.tblLtPnlGame.ResumeLayout(false);
            this.pnlGameControls.ResumeLayout(false);
            this.pnlGameControls.PerformLayout();
            this.pnlGreet.ResumeLayout(false);
            this.pnlGreet.PerformLayout();
            this.pnlRules.ResumeLayout(false);
            this.pnlRules.PerformLayout();
            this.ResumeLayout(false);

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
