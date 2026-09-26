namespace prac_3
{
    partial class Form1
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
            this.btnDraw = new System.Windows.Forms.Button();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblZero = new System.Windows.Forms.Label();
            this.lblOne = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(143, 323);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(99, 23);
            this.btnDraw.TabIndex = 0;
            this.btnDraw.Text = "Построить";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.BtnDraw_Click);
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(200, 9);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 13);
            this.lblY.TabIndex = 1;
            this.lblY.Text = "Y";
            this.lblY.Visible = false;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(365, 163);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 13);
            this.lblX.TabIndex = 2;
            this.lblX.Text = "X";
            this.lblX.Visible = false;
            // 
            // lblZero
            // 
            this.lblZero.AutoSize = true;
            this.lblZero.Location = new System.Drawing.Point(201, 163);
            this.lblZero.Name = "lblZero";
            this.lblZero.Size = new System.Drawing.Size(13, 13);
            this.lblZero.TabIndex = 3;
            this.lblZero.Text = "0";
            this.lblZero.Visible = false;
            // 
            // lblOne
            // 
            this.lblOne.AutoSize = true;
            this.lblOne.Location = new System.Drawing.Point(229, 163);
            this.lblOne.Name = "lblOne";
            this.lblOne.Size = new System.Drawing.Size(13, 13);
            this.lblOne.TabIndex = 4;
            this.lblOne.Text = "1";
            this.lblOne.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.lblOne);
            this.Controls.Add(this.lblZero);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.btnDraw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblZero;
        private System.Windows.Forms.Label lblOne;
    }
}

