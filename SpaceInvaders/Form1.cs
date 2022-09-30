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


        public Form1()
        {
            InitializeComponent();
            graphics = CreateGraphics();
            bufferImage = new Bitmap(Width, Height);
            bufferGraphics = Graphics.FromImage(bufferImage);     
            enemies = new List<Enemy>();
            enemiesLeft = 0;
            enemiesRight = 0;


        }

        int index = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            //Grid Layout
            //Picture Box for All Objects (Player, Missile, Enemies, Bomb) -Enemies in a List
            for (int x = 100; x < 600; x += 50)
            {
                for (int y = 100; y < 300; y += 50)
                {

                    enemies.Add(new Enemy(new Point(x, y), bufferGraphics, (Bitmap)Properties.Resources.T0));



                    if (index < 40)
                    {
                        index++;
                    }
                }


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