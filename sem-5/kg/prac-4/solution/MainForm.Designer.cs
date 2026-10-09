namespace prac_4
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
            this.btnDraw = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.lblCoordMatrix = new System.Windows.Forms.Label();
            this.txtBxCoordMatrix = new System.Windows.Forms.TextBox();
            this.lblEnterMatrix = new System.Windows.Forms.Label();
            this.txtBxEnterMatrix = new System.Windows.Forms.TextBox();
            this.btnResetMatrix = new System.Windows.Forms.Button();
            this.lblAngle = new System.Windows.Forms.Label();
            this.txtAngle = new System.Windows.Forms.TextBox();
            this.btnRotX = new System.Windows.Forms.Button();
            this.btnRotY = new System.Windows.Forms.Button();
            this.btnRotZ = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.splitContainer1.Panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel1_MouseClick);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel2MinSize = 246;
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 542;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 9;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnDraw);
            this.flowLayoutPanel1.Controls.Add(this.btnModify);
            this.flowLayoutPanel1.Controls.Add(this.lblCoordMatrix);
            this.flowLayoutPanel1.Controls.Add(this.txtBxCoordMatrix);
            this.flowLayoutPanel1.Controls.Add(this.lblEnterMatrix);
            this.flowLayoutPanel1.Controls.Add(this.txtBxEnterMatrix);
            this.flowLayoutPanel1.Controls.Add(this.btnResetMatrix);
            this.flowLayoutPanel1.Controls.Add(this.lblAngle);
            this.flowLayoutPanel1.Controls.Add(this.txtAngle);
            this.flowLayoutPanel1.Controls.Add(this.btnRotX);
            this.flowLayoutPanel1.Controls.Add(this.btnRotY);
            this.flowLayoutPanel1.Controls.Add(this.btnRotZ);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(240, 666);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(20, 20);
            this.btnDraw.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(200, 39);
            this.btnDraw.TabIndex = 0;
            this.btnDraw.Text = "Построить/сбросить фигуру";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.BtnDraw_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(20, 69);
            this.btnModify.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(200, 39);
            this.btnModify.TabIndex = 1;
            this.btnModify.Text = "Выполнить аффинные преобразования";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // lblCoordMatrix
            // 
            this.lblCoordMatrix.AutoSize = true;
            this.lblCoordMatrix.Location = new System.Drawing.Point(20, 128);
            this.lblCoordMatrix.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblCoordMatrix.Name = "lblCoordMatrix";
            this.lblCoordMatrix.Size = new System.Drawing.Size(164, 13);
            this.lblCoordMatrix.TabIndex = 6;
            this.lblCoordMatrix.Text = "Матрица начальных координат";
            // 
            // txtBxCoordMatrix
            // 
            this.txtBxCoordMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtBxCoordMatrix.Location = new System.Drawing.Point(20, 151);
            this.txtBxCoordMatrix.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.txtBxCoordMatrix.Multiline = true;
            this.txtBxCoordMatrix.Name = "txtBxCoordMatrix";
            this.txtBxCoordMatrix.ReadOnly = true;
            this.txtBxCoordMatrix.Size = new System.Drawing.Size(200, 150);
            this.txtBxCoordMatrix.TabIndex = 7;
            this.txtBxCoordMatrix.WordWrap = false;
            // 
            // lblEnterMatrix
            // 
            this.lblEnterMatrix.AutoSize = true;
            this.lblEnterMatrix.Location = new System.Drawing.Point(20, 321);
            this.lblEnterMatrix.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.lblEnterMatrix.Name = "lblEnterMatrix";
            this.lblEnterMatrix.Size = new System.Drawing.Size(167, 13);
            this.lblEnterMatrix.TabIndex = 4;
            this.lblEnterMatrix.Text = "Ввод матрицы преобразований";
            // 
            // txtBxEnterMatrix
            // 
            this.txtBxEnterMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtBxEnterMatrix.Location = new System.Drawing.Point(20, 344);
            this.txtBxEnterMatrix.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.txtBxEnterMatrix.Multiline = true;
            this.txtBxEnterMatrix.Name = "txtBxEnterMatrix";
            this.txtBxEnterMatrix.Size = new System.Drawing.Size(200, 74);
            this.txtBxEnterMatrix.TabIndex = 5;
            this.txtBxEnterMatrix.Text = "1 0 0 0\r\n0 1 0 0\r\n0 0 1 0\r\n0 0 0 1";
            this.txtBxEnterMatrix.WordWrap = false;
            // 
            // btnResetMatrix
            // 
            this.btnResetMatrix.Location = new System.Drawing.Point(20, 428);
            this.btnResetMatrix.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.btnResetMatrix.Name = "btnResetMatrix";
            this.btnResetMatrix.Size = new System.Drawing.Size(200, 30);
            this.btnResetMatrix.TabIndex = 8;
            this.btnResetMatrix.Text = "Сбросить матрицу ввода";
            this.btnResetMatrix.UseVisualStyleBackColor = true;
            this.btnResetMatrix.Click += new System.EventHandler(this.btnResetMatrix_Click);
            // 
            // lblAngle
            // 
            this.lblAngle.AutoSize = true;
            this.lblAngle.Location = new System.Drawing.Point(20, 478);
            this.lblAngle.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(133, 13);
            this.lblAngle.TabIndex = 9;
            this.lblAngle.Text = "Угол поворота (градусы)";
            // 
            // txtAngle
            // 
            this.txtAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtAngle.Location = new System.Drawing.Point(20, 501);
            this.txtAngle.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(200, 23);
            this.txtAngle.TabIndex = 10;
            this.txtAngle.Text = "10";
            // 
            // btnRotX
            // 
            this.btnRotX.Location = new System.Drawing.Point(20, 534);
            this.btnRotX.Margin = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.btnRotX.Name = "btnRotX";
            this.btnRotX.Size = new System.Drawing.Size(200, 30);
            this.btnRotX.TabIndex = 11;
            this.btnRotX.Text = "Поворот вокруг X";
            this.btnRotX.UseVisualStyleBackColor = true;
            this.btnRotX.Click += new System.EventHandler(this.btnRotX_Click);
            // 
            // btnRotY
            // 
            this.btnRotY.Location = new System.Drawing.Point(20, 569);
            this.btnRotY.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnRotY.Name = "btnRotY";
            this.btnRotY.Size = new System.Drawing.Size(200, 30);
            this.btnRotY.TabIndex = 12;
            this.btnRotY.Text = "Поворот вокруг Y";
            this.btnRotY.UseVisualStyleBackColor = true;
            this.btnRotY.Click += new System.EventHandler(this.btnRotY_Click);
            // 
            // btnRotZ
            // 
            this.btnRotZ.Location = new System.Drawing.Point(20, 604);
            this.btnRotZ.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnRotZ.Name = "btnRotZ";
            this.btnRotZ.Size = new System.Drawing.Size(200, 30);
            this.btnRotZ.TabIndex = 13;
            this.btnRotZ.Text = "Поворот вокруг Z";
            this.btnRotZ.UseVisualStyleBackColor = true;
            this.btnRotZ.Click += new System.EventHandler(this.btnRotZ_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(816, 639);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "424-1. Badulin Ilya. Practice #4. Variant #2";
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
        private System.Windows.Forms.Button btnRotX;
        private System.Windows.Forms.Button btnRotY;
        private System.Windows.Forms.Button btnRotZ;
    }
}