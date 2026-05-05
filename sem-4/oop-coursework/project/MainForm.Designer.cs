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
            btnDrawDisks = new Button();
            textBoxDisks = new TextBox();
            btnTest = new Button();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // btnDrawDisks
            // 
            btnDrawDisks.Location = new Point(273, 121);
            btnDrawDisks.Name = "btnDrawDisks";
            btnDrawDisks.Size = new Size(114, 36);
            btnDrawDisks.TabIndex = 0;
            btnDrawDisks.Text = "Нарисовать";
            btnDrawDisks.UseVisualStyleBackColor = true;
            btnDrawDisks.Click += btnDrawDisks_Click;
            // 
            // textBoxDisks
            // 
            textBoxDisks.Location = new Point(416, 129);
            textBoxDisks.Name = "textBoxDisks";
            textBoxDisks.Size = new Size(100, 23);
            textBoxDisks.TabIndex = 1;
            // 
            // btnTest
            // 
            btnTest.Location = new Point(229, 247);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(75, 23);
            btnTest.TabIndex = 2;
            btnTest.Text = "somthing";
            btnTest.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Location = new Point(537, 273);
            panel1.Name = "panel1";
            panel1.Size = new Size(257, 145);
            panel1.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 450);
            Controls.Add(panel1);
            Controls.Add(btnTest);
            Controls.Add(textBoxDisks);
            Controls.Add(btnDrawDisks);
            Name = "MainForm";
            Text = "Ханойские башни";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDrawDisks;
        private TextBox textBoxDisks;
        private Button btnTest;
        private Panel panel1;
    }
}
