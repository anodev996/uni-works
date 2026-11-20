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

        private void InitializeComponent()
        {
            this.glControl = new OpenTK.GLControl();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.materialMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ambientMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diffuseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specularMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emissionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightXMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightYMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightZMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Transparent;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(0, 24);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(1024, 552);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.materialMenuItem,
            this.lightMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1024, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // materialMenuItem
            // 
            this.materialMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ambientMenuItem,
            this.diffuseMenuItem,
            this.specularMenuItem,
            this.emissionMenuItem});
            this.materialMenuItem.Name = "materialMenuItem";
            this.materialMenuItem.Size = new System.Drawing.Size(74, 20);
            this.materialMenuItem.Text = "Материал";
            // 
            // ambientMenuItem
            // 
            this.ambientMenuItem.Name = "ambientMenuItem";
            this.ambientMenuItem.Size = new System.Drawing.Size(121, 22);
            this.ambientMenuItem.Text = "Ambient";
            // 
            // diffuseMenuItem
            // 
            this.diffuseMenuItem.Name = "diffuseMenuItem";
            this.diffuseMenuItem.Size = new System.Drawing.Size(121, 22);
            this.diffuseMenuItem.Text = "Diffuse";
            // 
            // specularMenuItem
            // 
            this.specularMenuItem.Name = "specularMenuItem";
            this.specularMenuItem.Size = new System.Drawing.Size(121, 22);
            this.specularMenuItem.Text = "Specular";
            // 
            // emissionMenuItem
            // 
            this.emissionMenuItem.Name = "emissionMenuItem";
            this.emissionMenuItem.Size = new System.Drawing.Size(121, 22);
            this.emissionMenuItem.Text = "Emission";
            // 
            // lightMenuItem
            // 
            this.lightMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightXMenuItem,
            this.lightYMenuItem,
            this.lightZMenuItem});
            this.lightMenuItem.Name = "lightMenuItem";
            this.lightMenuItem.Size = new System.Drawing.Size(44, 20);
            this.lightMenuItem.Text = "Свет";
            // 
            // lightXMenuItem
            // 
            this.lightXMenuItem.Name = "lightXMenuItem";
            this.lightXMenuItem.Size = new System.Drawing.Size(81, 22);
            this.lightXMenuItem.Text = "X";
            // 
            // lightYMenuItem
            // 
            this.lightYMenuItem.Name = "lightYMenuItem";
            this.lightYMenuItem.Size = new System.Drawing.Size(81, 22);
            this.lightYMenuItem.Text = "Y";
            // 
            // lightZMenuItem
            // 
            this.lightZMenuItem.Name = "lightZMenuItem";
            this.lightZMenuItem.Size = new System.Drawing.Size(81, 22);
            this.lightZMenuItem.Text = "Z";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 576);
            this.Controls.Add(this.glControl);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(1040, 615);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KgPrac7";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private OpenTK.GLControl glControl;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem materialMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ambientMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diffuseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem specularMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emissionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightXMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightYMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightZMenuItem;
    }
}