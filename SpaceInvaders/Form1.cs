namespace SpaceInvaders
{
    public partial class Form1 : Form
    {//All functionality will be moved to the Controller class//
        private Bitmap bufferImage;
        private bool canShoot;
        private Graphics graphics;
        private Graphics bufferGraphics;
        private Controller controller;
        //All of theese will eventually become part of a Fleet Object
        private List<Enemy> enemies; //2D array? or keep as a list? new var[4,10] (r,c)
        private List<Bomb> bombs; //List
        private List<Missile> missiles; //Array
        /**********************************/
        //Fleet has different fields for each // 3 constructors?
        private Random rand;
        private Rectangle boundary;

        private Mothership mothership;



        public Form1()
        {
            InitializeComponent();
            rand = new Random();

            canShoot = true;
            graphics = CreateGraphics();
            bufferImage = new Bitmap(Width, Height);
            bufferGraphics = Graphics.FromImage(bufferImage);     
            enemies = new List<Enemy>();
            boundary = ClientRectangle;
            controller = new Controller(pictureBox1,boundary, bufferGraphics, rand);
            mothership = new Mothership(pictureBox1,boundary,new Point(ClientSize.Width/2,ClientSize.Height-150),bufferGraphics,missiles,rand);

            timer1.Enabled = true;
            
            //Grid Layout
            //Picture Box For Player Graphics draw image for ( Missile, Enemies, Bomb) -Enemies in a List and add the missiles to an array or list to limit how many at once.
            


        }

     
        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show($"{e.KeyCode}");
            if (e.KeyCode == Keys.NumPad5)
            {
                enemies.Clear();

            }
           

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (timer1.Enabled)
            {
                mothership.Move(e.X);

            }


        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {


            if(canShoot && controller.MissileCount() < 15)
            {
                controller.FireMissile();
            }
            canShoot = !canShoot;

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = controller.MissileCount().ToString();

            bufferGraphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            controller.RunGame();
            controller.DrawObjects();
            if (controller.GameOver == true)
            {
                timer1.Enabled = false;
                MessageBox.Show("Game Over");
            }
          //  if (enemies.Count > 0)
          //  {
          //      enemiesLeft = enemies[0].Position.X - enemyspeed;
          //      enemiesRight = enemies[enemies.Count - 1].Position.X + 32 + enemyspeed;
          //      enemiesBottom = enemies[enemies.Count - 1].Position.Y + 32;
          //  }


          //  foreach (Missile bomb in bombs.ToList())
          //  {
          //      if(bomb.Position.Y > ClientSize.Height)
          //      {
          //          bomb.Destroy();
          //      }
          //      if (bomb.Position.Y >= mothership.Picturebox.Top && bomb.Position.X >= mothership.Picturebox.Left && bomb.Position.X <= mothership.Picturebox.Right)
          //      {
          //          timer1.Enabled = false;
                    
          //      }

          //  }







          //  if (enemies.Count == 0 || enemiesBottom >= mothership.Picturebox.Top || mothership.Picturebox.Visible == false)
          //  {
                
          //      GameOver();

          //     // MessageBox.Show("Game Over +");

          //  }

          //  if (enemies.Count <= 40 && enemies.Count > 30)
          //  {
          //      enemyspeed = 4;
          //  }
          //  else if (enemies.Count <= 30 && enemies.Count > 20)
          //  {

                
          //      enemyspeed = 6;
          //  }
          //  else if (enemies.Count <= 20 && enemies.Count > 10)
          //  {
          //      enemyspeed = 8;
          //  }
          //  else if (enemies.Count <= 10 && enemies.Count > 5)
          //  {
          //      enemyspeed = 9;
          //  }
          //  else if (enemies.Count <= 5 && enemies.Count > 1)
          //  {
          //      enemyspeed = 10;
          //  }
          //  else if (enemies.Count == 1)
          //  {
          //      enemyspeed = 15;
          //  }
          // //textBox1.Text = mothership.Picturebox.Top.ToString();
          //// textBox2.Text = enemiesBottom.ToString();
          //  foreach (Missile missile in missiles.ToList())
          //  {
          //      missile.Draw();
          //      missile.Move();

          //  }
          //  foreach(Missile bomb in bombs.ToList())
          //  {
          //      bomb.Draw();
          //      bomb.Move();
          //  }
          ////  textBox1.Text = bombs.Count.ToString();
          //   if(enemiesRight > ClientRectangle.Right || enemiesLeft < ClientRectangle.Left)
          //  {
          //      foreach(Enemy enemy in enemies)
          //      {
          //          enemy.ShiftLevel();
          //          enemy.Direction = -enemy.Direction;
          //      }
                 
          //  }
          //  foreach(Enemy enemy in enemies.ToList())
          //  {
          //      enemy.Velocity = enemyspeed;
                    
          //      enemy.Draw();



          //      //if(i < 36)
          //      //{
          //      //    if (enemies[i + 1].Destroyed == true)
          //      //    {
          //      //        enemies[i].CanShoot = true;
          //      //    }
          //      //    i++;
          //      //}

          //      int test_Y = enemy.Position.Y; //test the y position adding the gap between each enemy
          //      bool test_y_empty = true; //Is the test_y position free?
          //      foreach(Enemy test in enemies) //loop through enemies again and test if the column is free, the offset counts 3 forwards
          //      {
          //          if((test.Position.Y == test_Y +50 || test.Position.Y == test_Y + 100 || test.Position.Y == test_Y + 150)  && test.Position.X == enemy.Position.X)
          //          {
          //              test_y_empty = false;


          //          }

          //      }
          //      if (test_y_empty)
          //      {
          //          enemy.CanShoot = true;
          //      }

          //      enemy.Move();
          //      if (enemy.CanShoot)
          //      {
          //          if(enemy.ShootNum == 99)
          //          {
          //              enemy.Shoot();
          //          }
          //         // textBox1.Text = bombs.Count.ToString();
          //      }
          //      foreach (Missile missile in missiles.ToList())
          //      {
                    
          //          if (missile.Collider.IntersectsWith(enemy.Collider) /*&& enemy.Destroyed == false*/)
          //          {
                        
          //              missile.Destroy(); //alive = false
          //              enemies.Remove(enemy);
          //          }
                    

          //      }
          //  }



            graphics.DrawImage(bufferImage, 0, 0);

        }

        //public void GameOver()
        //{

        //   // bombs.Clear();
        //    //missiles.Clear();

        //    timer1.Enabled = false;
        //    //gameFinished = true;
        //    textBox1.Text = "Game Over";
        //}
    }
    
}