namespace project
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCoordMatrix = new System.Windows.Forms.Label();
            this.txtBxCoordMatrix = new System.Windows.Forms.TextBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.lblAngle = new System.Windows.Forms.Label();
            this.txtAngle = new System.Windows.Forms.TextBox();
            this.btnRotYOZ = new System.Windows.Forms.Button();
            this.btnRotY = new System.Windows.Forms.Button();
            this.btnRotZ = new System.Windows.Forms.Button();
            this.lblEnterMatrix = new System.Windows.Forms.Label();
            this.txtBxEnterMatrix = new System.Windows.Forms.TextBox();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnResetMatrix = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.splitContainer1.Panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel1_MouseClick);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(1024, 576);
            this.splitContainer1.SplitterDistance = 773;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 9;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.lblCoordMatrix);
            this.flowLayoutPanel1.Controls.Add(this.txtBxCoordMatrix);
            this.flowLayoutPanel1.Controls.Add(this.btnDraw);
            this.flowLayoutPanel1.Controls.Add(this.lblAngle);
            this.flowLayoutPanel1.Controls.Add(this.txtAngle);
            this.flowLayoutPanel1.Controls.Add(this.btnRotYOZ);
            this.flowLayoutPanel1.Controls.Add(this.btnRotY);
            this.flowLayoutPanel1.Controls.Add(this.btnRotZ);
            this.flowLayoutPanel1.Controls.Add(this.lblEnterMatrix);
            this.flowLayoutPanel1.Controls.Add(this.txtBxEnterMatrix);
            this.flowLayoutPanel1.Controls.Add(this.btnModify);
            this.flowLayoutPanel1.Controls.Add(this.btnResetMatrix);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(233, 576);
            this.flowLayoutPanel1.TabIndex = 7;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // lblCoordMatrix
            // 
            this.lblCoordMatrix.AutoSize = true;
            this.lblCoordMatrix.Location = new System.Drawing.Point(20, 20);
            this.lblCoordMatrix.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblCoordMatrix.Name = "lblCoordMatrix";
            this.lblCoordMatrix.Size = new System.Drawing.Size(161, 13);
            this.lblCoordMatrix.TabIndex = 6;
            this.lblCoordMatrix.Text = "Матрица исходных координат:";
            // 
            // txtBxCoordMatrix
            // 
            this.txtBxCoordMatrix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.txtBxCoordMatrix.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBxCoordMatrix.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxCoordMatrix.Location = new System.Drawing.Point(20, 41);
            this.txtBxCoordMatrix.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.txtBxCoordMatrix.Multiline = true;
            this.txtBxCoordMatrix.Name = "txtBxCoordMatrix";
            this.txtBxCoordMatrix.ReadOnly = true;
            this.txtBxCoordMatrix.Size = new System.Drawing.Size(200, 125);
            this.txtBxCoordMatrix.TabIndex = 7;
            this.txtBxCoordMatrix.WordWrap = false;
            // 
            // btnDraw
            // 
            this.btnDraw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnDraw.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnDraw.ForeColor = System.Drawing.Color.White;
            this.btnDraw.Location = new System.Drawing.Point(20, 182);
            this.btnDraw.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(200, 30);
            this.btnDraw.TabIndex = 0;
            this.btnDraw.Text = "Построить / сбросить фигуру";
            this.btnDraw.UseVisualStyleBackColor = false;
            this.btnDraw.Click += new System.EventHandler(this.BtnDraw_Click);
            // 
            // lblAngle
            // 
            this.lblAngle.AutoSize = true;
            this.lblAngle.Location = new System.Drawing.Point(20, 228);
            this.lblAngle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(133, 13);
            this.lblAngle.TabIndex = 9;
            this.lblAngle.Text = "Угол поворота (градусы)";
            // 
            // txtAngle
            // 
            this.txtAngle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtAngle.Location = new System.Drawing.Point(20, 249);
            this.txtAngle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.txtAngle.Multiline = true;
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(200, 20);
            this.txtAngle.TabIndex = 10;
            this.txtAngle.Text = "15";
            this.txtAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRotYOZ
            // 
            this.btnRotYOZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnRotYOZ.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnRotYOZ.ForeColor = System.Drawing.Color.White;
            this.btnRotYOZ.Location = new System.Drawing.Point(20, 273);
            this.btnRotYOZ.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnRotYOZ.Name = "btnRotYOZ";
            this.btnRotYOZ.Size = new System.Drawing.Size(200, 30);
            this.btnRotYOZ.TabIndex = 11;
            this.btnRotYOZ.Text = "Поворот отн. плоскости YOZ";
            this.btnRotYOZ.UseVisualStyleBackColor = false;
            this.btnRotYOZ.Click += new System.EventHandler(this.btnRotYOZ_Click);
            // 
            // btnRotY
            // 
            this.btnRotY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnRotY.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnRotY.ForeColor = System.Drawing.Color.White;
            this.btnRotY.Location = new System.Drawing.Point(20, 307);
            this.btnRotY.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnRotY.Name = "btnRotY";
            this.btnRotY.Size = new System.Drawing.Size(200, 30);
            this.btnRotY.TabIndex = 12;
            this.btnRotY.Text = "Поворот вокруг Y";
            this.btnRotY.UseVisualStyleBackColor = false;
            this.btnRotY.Click += new System.EventHandler(this.btnRotY_Click);
            // 
            // btnRotZ
            // 
            this.btnRotZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnRotZ.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnRotZ.ForeColor = System.Drawing.Color.White;
            this.btnRotZ.Location = new System.Drawing.Point(20, 341);
            this.btnRotZ.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.btnRotZ.Name = "btnRotZ";
            this.btnRotZ.Size = new System.Drawing.Size(200, 30);
            this.btnRotZ.TabIndex = 13;
            this.btnRotZ.Text = "Поворот вокруг Z";
            this.btnRotZ.UseVisualStyleBackColor = false;
            this.btnRotZ.Click += new System.EventHandler(this.btnRotZ_Click);
            // 
            // lblEnterMatrix
            // 
            this.lblEnterMatrix.AutoSize = true;
            this.lblEnterMatrix.Location = new System.Drawing.Point(20, 387);
            this.lblEnterMatrix.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblEnterMatrix.Name = "lblEnterMatrix";
            this.lblEnterMatrix.Size = new System.Drawing.Size(170, 13);
            this.lblEnterMatrix.TabIndex = 4;
            this.lblEnterMatrix.Text = "Ввод матрицы преобразований:";
            // 
            // txtBxEnterMatrix
            // 
            this.txtBxEnterMatrix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBxEnterMatrix.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txtBxEnterMatrix.Location = new System.Drawing.Point(20, 408);
            this.txtBxEnterMatrix.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.txtBxEnterMatrix.Multiline = true;
            this.txtBxEnterMatrix.Name = "txtBxEnterMatrix";
            this.txtBxEnterMatrix.Size = new System.Drawing.Size(200, 70);
            this.txtBxEnterMatrix.TabIndex = 5;
            this.txtBxEnterMatrix.WordWrap = false;
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnModify.ForeColor = System.Drawing.Color.White;
            this.btnModify.Location = new System.Drawing.Point(20, 482);
            this.btnModify.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(200, 30);
            this.btnModify.TabIndex = 2;
            this.btnModify.Text = "Применить ввод";
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnResetMatrix
            // 
            this.btnResetMatrix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(153)))));
            this.btnResetMatrix.ForeColor = System.Drawing.Color.White;
            this.btnResetMatrix.Location = new System.Drawing.Point(20, 516);
            this.btnResetMatrix.Margin = new System.Windows.Forms.Padding(0);
            this.btnResetMatrix.Name = "btnResetMatrix";
            this.btnResetMatrix.Size = new System.Drawing.Size(200, 30);
            this.btnResetMatrix.TabIndex = 8;
            this.btnResetMatrix.Text = "Сбросить ввод";
            this.btnResetMatrix.UseVisualStyleBackColor = false;
            this.btnResetMatrix.Click += new System.EventHandler(this.btnResetMatrix_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(1024, 576);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(1040, 615);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KgLab4";
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Label lblCoordMatrix;
        private System.Windows.Forms.TextBox txtBxCoordMatrix;
        private System.Windows.Forms.Label lblEnterMatrix;
        private System.Windows.Forms.TextBox txtBxEnterMatrix;
        private System.Windows.Forms.Button btnResetMatrix;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.TextBox txtAngle;
        private System.Windows.Forms.Button btnRotYOZ;
        private System.Windows.Forms.Button btnRotY;
        private System.Windows.Forms.Button btnRotZ;
    }
}