namespace SpaceInvaders
{
    public partial class Form1 : Form
    {
        private Bitmap bufferImage;
        private bool canShoot;
        private Graphics graphics;
        private Graphics bufferGraphics;
        private List<Enemy> enemies;
        private List<Missile> missiles;

        private int enemyspeed = 8;
        int enemiesLeft, enemiesRight;
        private Mothership mothership;

        public Form1()
        {
            InitializeComponent();
            missiles = new List<Missile>();
            canShoot = true;
            graphics = CreateGraphics();
            int index = -1;
            bufferImage = new Bitmap(Width, Height);
            bufferGraphics = Graphics.FromImage(bufferImage);     
            enemies = new List<Enemy>();
            

            mothership = new Mothership(pictureBox1,ClientRectangle,new Point(ClientSize.Width/2,ClientSize.Height-150),bufferGraphics,missiles);
            enemiesLeft = 0;
            enemiesRight = 0;
            timer1.Enabled = true;
            
            //Grid Layout
            //Picture Box For Player Graphics draw image for ( Missile, Enemies, Bomb) -Enemies in a List and add the missiles to an array or list to limit how many at once.
            for (int x = 100; x < 800; x += 75)
            {
                for (int y = 10; y < 200; y += 50)
                {
                    if (index < 40)
                    {
                        index++;
                        enemies.Add(new Enemy(new Point(x, y), bufferGraphics, Properties.Resources.T0));
                        if(index%4 == 3)
                        {
                            enemies[index].CanShoot = true;
                        }
                    
                    }



                }


            }


        }

     
        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ////MessageBox.Show($"{e.KeyCode}");
            //if(e.KeyCode == Keys.Left)
            //{
            //    mothership.Move(-1);

            //}
            //else if(e.KeyCode == Keys.Right)
            //{
            //    mothership.Move(1);

            //}

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
                mothership.Move(e.X);
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {


            if(missiles.Count < 10 && canShoot)
            {
                mothership.Shoot();
            }
            canShoot = !canShoot;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bufferGraphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            //mothership.Draw();
            enemiesLeft = enemies[0].Position.X - 5;
            enemiesRight = enemies[36].Position.X + 30;

            foreach(Missile missile in missiles.ToList())
            {

                missile.Draw();
                missile.Move();

            }
             if(enemiesRight > ClientRectangle.Right || enemiesLeft < ClientRectangle.Left)
            {
                foreach(Enemy enemy in enemies)
                {
                    enemy.ShiftLevel();
                }
                enemyspeed = -enemyspeed;
            }
            int i = 0;
            foreach(Enemy enemy in enemies.ToList())
            {
                if(enemy.Destroyed == false)
                {
                    enemy.Draw();


                }
                if(i < enemies.Count-1)
                {
                    if (enemies[i + 1].Destroyed == true)
                    {
                        enemies[i].CanShoot = true;
                    }
                    i++;
                }
                
                enemy.Move(enemyspeed);
                foreach (Missile missile in missiles.ToList())
                {
                    if (missile.Collider.IntersectsWith(enemy.Collider) && enemy.Destroyed == false)
                    {
                        
                        missile.Destroy();
                        enemy.Destroy();
                    }

                }
            }

            graphics.DrawImage(bufferImage, 0, 0);

            textBox1.Text = canShoot.ToString();
        }
    }
    
}