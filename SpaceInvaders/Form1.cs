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

        private int enemyspeed;
        int enemiesLeft, enemiesRight,enemiesBottom;
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
            enemiesBottom = 0;
            enemyspeed = 5;
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
                        enemies.Add(new Enemy(new Point(x, y),enemyspeed, bufferGraphics, Properties.Resources.enemy_ship));
                        //if(index%4 == 3)
                        //{
                        //    enemies[index].CanShoot = true;
                        //}
                    
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
            enemiesLeft = enemies[0].Position.X - enemyspeed;
            enemiesRight = enemies[enemies.Count - 1].Position.X + 32 + enemyspeed;
            enemiesBottom = enemies[enemies.Count - 1].Position.Y + 32;
            if (enemies.Count == 0 || enemiesBottom >= mothership.Picturebox.Top)
            {
                timer1.Enabled = false;

                MessageBox.Show("Game Over +");

            }

            if (enemies.Count <= 40 && enemies.Count > 30)
            {
                enemyspeed = 24;
            }
            else if (enemies.Count <= 30 && enemies.Count > 20)
            {
                enemyspeed = 6;
            }
            else if (enemies.Count <= 20 && enemies.Count > 10)
            {
                enemyspeed = 8;
            }
            else if (enemies.Count <= 10 && enemies.Count > 5)
            {
                enemyspeed = 9;
            }
            else if (enemies.Count <= 5 && enemies.Count > 1)
            {
                enemyspeed = 10;
            }
            else if (enemies.Count == 1)
            {
                enemyspeed = 15;
            }
            textBox1.Text = mothership.Picturebox.Top.ToString();
            textBox2.Text = enemiesBottom.ToString();
            foreach (Missile missile in missiles.ToList())
            {

                missile.Draw();
                missile.Move();

            }
             if(enemiesRight > ClientRectangle.Right || enemiesLeft < ClientRectangle.Left)
            {
                foreach(Enemy enemy in enemies)
                {
                    enemy.ShiftLevel();
                    enemy.Direction = -enemy.Direction;
                }
                 
            }
            foreach(Enemy enemy in enemies.ToList())
            {
                enemy.Velocity = enemyspeed;
                    
                enemy.Draw();



                //if(i < 36)
                //{
                //    if (enemies[i + 1].Destroyed == true)
                //    {
                //        enemies[i].CanShoot = true;
                //    }
                //    i++;
                //}

                int test_Y = enemy.Position.Y; //test the y position adding the gap between each enemy
                bool test_y_empty = true; //Is the test_y position free?
                foreach(Enemy test in enemies) //loop through enemies again and test if the column is free, the offset counts 3 forwards
                {
                    if((test.Position.Y == test_Y +50 || test.Position.Y == test_Y + 100 || test.Position.Y == test_Y + 150)  && test.Position.X == enemy.Position.X)
                    {
                        test_y_empty = false;


                    }

                }
                if (test_y_empty)
                {
                    enemy.CanShoot = true;
                }
                enemy.Move();
                foreach (Missile missile in missiles.ToList())
                {
                    if (missile.Collider.IntersectsWith(enemy.Collider) && enemy.Destroyed == false)
                    {
                        
                        missile.Destroy();
                        enemies.Remove(enemy);
                    }

                }
            }

            graphics.DrawImage(bufferImage, 0, 0);

        }
    }
    
}