
//The Controller manages the main game function. It will start the timer and can check when the game is Playing,Paused or Finished
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
namespace SpaceInvaders
{
    public class Controller
    {

        private SoundPlayer missileSound;
        private SoundPlayer bombSound;
        private SoundPlayer destroySound;

        private Rectangle boundary;
        private Graphics graphics;
        private Random rand;
        private Mothership mothership;
        private List<Missile> missiles;
        private List<Bomb> bombs;
        private bool gameOver = false;
        private bool gameWon = false;
        private int score;
        private const int OFFSET = 50;
        private const int MAX_ENEMIES = 40;
        private List<Enemy> enemies;

        private int enemiesLeft, enemiesRight, enemiesBottom, enemyspeed;

        public bool GameOver { get => gameOver; set => gameOver = value; }
        public int Score { get => score; set => score = value; }
        public bool GameWon { get => gameWon; set => gameWon = value; }

        public Controller(PictureBox picturebox, Rectangle boundary, Graphics graphics, Random rand)
        {
            missileSound = new SoundPlayer(Properties.Resources.blaster);
            bombSound = new SoundPlayer(Properties.Resources.bomb);
            destroySound = new SoundPlayer(Properties.Resources.explosion);

            missiles = new List<Missile>();
            bombs = new List<Bomb>();
            enemies = new List<Enemy>();
            this.boundary = boundary;
            this.graphics = graphics;
            this.rand = rand;
            mothership = new Mothership(picturebox, boundary, new Point(boundary.Width / 2, boundary.Height - 150), graphics, missiles, rand, missileSound);
            enemiesLeft = 0;
            enemiesRight = 0;
            enemiesBottom = 0;
            enemyspeed = 5;
            score = 0;



            int index = 0;

            for (int x = 100; x < 800; x += OFFSET)
            {
                for (int y = 10; y < 200; y += OFFSET)
                {
                    if (index < MAX_ENEMIES)
                    {

                        enemies.Add(new Enemy(new Point(x, y), enemyspeed, graphics, Properties.Resources.enemy_ship, bombs, rand, boundary, bombSound));
                        //if (index % 4 == 3) //Initially, Set canShoot for all the front Line enemies to True
                        //{
                        //    enemies[index].CanShoot = true;
                        //}
                        index++;
                    }



                }


            }


        }


        public void DrawObjects() //This is called every timer tick and draws the missiles and bombs to the screen
        {
            foreach (Missile missile in missiles.ToList())
            {
                missile.Draw();
                missile.Move();

            }
            foreach (Bomb bomb in bombs.ToList())
            {
                bomb.Draw();
                bomb.Move();
            }
        }
        public int MissileCount() //This returns the ammount of missiles so the limit can be check against
        {
            return missiles.Count;
        }
        public void FireMissile() //Fires a missile from the mothership
        {
            mothership.Shoot();
        }

        public void RunGame() //The main game loop called at each tick
        {

            if (enemies.Count > 0)

            {

                enemiesLeft = enemies[0].Position.X - enemyspeed;
                enemiesRight = enemies[enemies.Count - 1].Position.X + 32 + enemyspeed;
                enemiesBottom = enemies[enemies.Count - 1].Position.Y + 32;
            }


            foreach (Bomb bomb in bombs.ToList())
            {
                if (bomb.Position.Y > boundary.Height)
                {
                    bomb.Destroy();
                }
                if (bomb.Position.Y >= mothership.Picturebox.Top && bomb.Position.X >= mothership.Picturebox.Left && bomb.Position.X <= mothership.Picturebox.Right)
                {
                    gameOver = true;
                    //loseSound.Play();
                }
                foreach (Missile missile in missiles.ToList())
                {
                    if (bomb.Collider.IntersectsWith(missile.Collider))
                    {
                        missile.Destroy();
                        bomb.Destroy();
                        score += 5;
                        destroySound.Play();
                    }
                }

            }







            if (enemies.Count == 0)
            {
                //winSound.Play(); //WINNING SCENARIO
                GameWon = true;

                // MessageBox.Show("Game Over +");

            }
            else if (enemiesBottom >= mothership.Picturebox.Top)
            {
                //loseSound.Play();
                gameOver = true;
            }

            if (enemies.Count <= 40 && enemies.Count > 30)
            {
                enemyspeed = 4;
            }
            else if (enemies.Count <= 30 && enemies.Count > 20)
            {


                enemyspeed = 8;
            }
            else if (enemies.Count <= 20 && enemies.Count > 10)
            {
                enemyspeed = 10;
            }
            else if (enemies.Count <= 10 && enemies.Count > 5)
            {
                enemyspeed = 12;
            }
            else if (enemies.Count <= 5 && enemies.Count > 1)
            {
                enemyspeed = 15;
            }
            else if (enemies.Count == 1)
            {
                enemyspeed = 20;
            }



            if (enemiesRight > boundary.Right || enemiesLeft < boundary.Left)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.ShiftLevel();
                    if (enemy.Direction == eDirection.LEFT)
                    {
                        enemy.Direction = eDirection.RIGHT;
                    }
                    else
                    {
                        enemy.Direction = eDirection.LEFT;
                    }

                }

            }
            foreach (Enemy enemy in enemies.ToList())
            {
                enemy.Velocity = enemyspeed;

                enemy.Draw();





                int test_Y = enemy.Position.Y; //test the y position adding the gap between each enemy
                bool test_y_empty = true; //Is the test_y position free?
                foreach (Enemy test in enemies) //loop through enemies again and test if the column is free, the offset counts 3 forwards
                {
                    if ((test.Position.Y == test_Y + 50 || test.Position.Y == test_Y + 100 || test.Position.Y == test_Y + 150) && test.Position.X == enemy.Position.X)
                    {
                        test_y_empty = false;


                    }

                }
                if (test_y_empty)
                {
                    enemy.CanShoot = true;
                }

                enemy.Move();
                if (enemy.CanShoot)
                {
                    if (enemy.ShootNum == 99)
                    {
                        enemy.Shoot();
                    }
                    // textBox1.Text = bombs.Count.ToString();
                }
                foreach (Missile missile in missiles.ToList())
                {

                    if (missile.Collider.IntersectsWith(enemy.Collider) /*&& enemy.Destroyed == false*/)
                    {

                        missile.Destroy(); //alive = false
                        enemies.Remove(enemy);
                        score += 50;
                        destroySound.Play();
                    }



                }
            }
        }


        public void MovePlayer(int x) //Moves the player to a given x position - The Mouse X position
        {
            mothership.Move(x);
        }





    }
}
