namespace project
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnDrawDisks_Click(object sender, EventArgs e)
        {
            int disks = int.Parse(textBoxDisks.Text);
            HanoiGame _game = new HanoiGame(disks);
        }
    }
}
