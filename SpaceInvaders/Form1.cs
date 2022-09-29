namespace SpaceInvaders
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private List<PictureBox> pictureBoxes;
        public Form1()
        {
            InitializeComponent();
            graphics = CreateGraphics();
            pictureBoxes = new List<PictureBox>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Grid Layout
            //Picture Box for All Objects (Player, Missile, Enemies, Bomb) -Enemies in a List
            for (int x = 100; x < 1000; x += 100)
            {
                for (int y = 100; y < 400; y += 100)
                {
                    graphics.FillEllipse(Brushes.Blue, x, y, 25, 25);
                }
            }
        }
    }
}