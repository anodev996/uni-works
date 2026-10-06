namespace lab_3
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Click += new System.EventHandler(this.splitContainer1_Panel1_Click);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel2MinSize = 210;
            this.splitContainer1.Size = new System.Drawing.Size(784, 561);
            this.splitContainer1.SplitterDistance = 570;
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
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(210, 561);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // btnDraw
            // 
            this.btnDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDraw.Location = new System.Drawing.Point(20, 20);
            this.btnDraw.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(164, 39);
            this.btnDraw.TabIndex = 0;
            this.btnDraw.Text = "Построить / Сбросить";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.BtnDraw_Click);
            // 
            // btnModify
            // 
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModify.Location = new System.Drawing.Point(20, 69);
            this.btnModify.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(164, 39);
            this.btnModify.TabIndex = 1;
            this.btnModify.Text = "Выполнить аффинные преобразования";
            this.btnModify.UseVisualStyleBackColor = true;
            // 
            // lblCoordMatrix
            // 
            this.lblCoordMatrix.AutoSize = true;
            this.lblCoordMatrix.Location = new System.Drawing.Point(20, 138);
            this.lblCoordMatrix.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.lblCoordMatrix.Name = "lblCoordMatrix";
            this.lblCoordMatrix.Size = new System.Drawing.Size(164, 13);
            this.lblCoordMatrix.TabIndex = 6;
            this.lblCoordMatrix.Text = "Матрица начальных координат";
            // 
            // txtBxCoordMatrix
            // 
            this.txtBxCoordMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxCoordMatrix.Location = new System.Drawing.Point(20, 161);
            this.txtBxCoordMatrix.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.txtBxCoordMatrix.Multiline = true;
            this.txtBxCoordMatrix.Name = "txtBxCoordMatrix";
            this.txtBxCoordMatrix.ReadOnly = true;
            this.txtBxCoordMatrix.Size = new System.Drawing.Size(164, 110);
            this.txtBxCoordMatrix.TabIndex = 7;
            this.txtBxCoordMatrix.Text = "-4 -4 1\r\n-4 3 1\r\n3 3 1\r\n-1 1 1";
            this.txtBxCoordMatrix.WordWrap = false;
            // 
            // lblEnterMatrix
            // 
            this.lblEnterMatrix.AutoSize = true;
            this.lblEnterMatrix.Location = new System.Drawing.Point(20, 291);
            this.lblEnterMatrix.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.lblEnterMatrix.Name = "lblEnterMatrix";
            this.lblEnterMatrix.Size = new System.Drawing.Size(167, 13);
            this.lblEnterMatrix.TabIndex = 4;
            this.lblEnterMatrix.Text = "Ввод матрицы преобразований";
            // 
            // txtBxEnterMatrix
            // 
            this.txtBxEnterMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxEnterMatrix.Location = new System.Drawing.Point(20, 314);
            this.txtBxEnterMatrix.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.txtBxEnterMatrix.Multiline = true;
            this.txtBxEnterMatrix.Name = "txtBxEnterMatrix";
            this.txtBxEnterMatrix.Size = new System.Drawing.Size(164, 82);
            this.txtBxEnterMatrix.TabIndex = 5;
            this.txtBxEnterMatrix.Text = "1 0 0\r\n0 1 0\r\n0 0 1";
            this.txtBxEnterMatrix.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "424-1. Badulin Ilya. Laboratory #3. Variant #2.";
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
    }
}

