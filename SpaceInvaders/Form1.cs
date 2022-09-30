namespace SpaceInvaders
{
    public partial class Form1 : Form
    {
        private Bitmap bufferImage;

        private Graphics graphics;
        private Graphics bufferGraphics;
        private List<Enemy> enemies;
        private int enemyspeed = 15;
        int enemiesLeft, enemiesRight;
        private Mothership mothership;

        public Form1()
        {
            InitializeComponent();
            graphics = CreateGraphics();
            int index = 0;
            bufferImage = new Bitmap(Width, Height);
            bufferGraphics = Graphics.FromImage(bufferImage);     
            enemies = new List<Enemy>();
            mothership = new Mothership(pictureBox1, new Point(ClientRectangle.Width / 2, ClientRectangle.Height - 50),ClientRectangle);
            enemiesLeft = 0;
            enemiesRight = 0;
            timer1.Enabled = true;
            //Grid Layout
            //Picture Box For Player Graphics draw image for ( Missile, Enemies, Bomb) -Enemies in a List and add the missiles to an array or list to limit how many at once.
            for (int x = 100; x < 800; x += 75)
            {
                for (int y = 100; y < 300; y += 50)
                {
                    if (index < 40)
                    {
                        index++;
                        enemies.Add(new Enemy(new Point(x, y), bufferGraphics, (Bitmap)Properties.Resources.T0));
                    }



                }


            }


        }

     
        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show($"{e.KeyCode}");
            if(e.KeyCode == Keys.Left)
            {
                mothership.Move(-1);

            }
            else if(e.KeyCode == Keys.Right)
            {
                mothership.Move(1);

            }

        }

 



        private void timer1_Tick(object sender, EventArgs e)
        {
            bufferGraphics.FillRectangle(Brushes.White, 0, 0, Width, Height);

            enemiesLeft = enemies[0].Position.X - 5;
            enemiesRight = enemies[36].Position.X + 30;

             if(enemiesRight > ClientRectangle.Right || enemiesLeft < ClientRectangle.Left)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.ShiftLevel();
                }
                enemyspeed = -enemyspeed;
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw();

                enemy.Move(enemyspeed);
            }

            graphics.DrawImage(bufferImage, 0, 0);

        }
    }
    
}