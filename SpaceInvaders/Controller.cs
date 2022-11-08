
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
        private SoundPlayer enemyKilled;
        private SoundPlayer destroySound;
        private SoundPlayer winSound;

        private Rectangle boundary;
        private Graphics graphics;
        private Random rand;
        private Mothership mothership;
        private List<Missile> missiles;
        private List<Bomb> bombs;
        private bool gameOver;
        private bool gameWon;
        private bool columnFree;
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
            winSound = new SoundPlayer(Properties.Resources.win);
            enemyKilled = new SoundPlayer(Properties.Resources.invaderkilled);
            missiles = new List<Missile>();
            bombs = new List<Bomb>();  
            enemies = new List<Enemy>();
            this.boundary = boundary;
            this.graphics = graphics;
            this.rand = rand;
            mothership = new Mothership(picturebox, boundary, graphics, missiles, rand, missileSound);
            enemiesLeft = 0;
            enemiesRight = 0;
            enemiesBottom = 0;
            enemyspeed = 4;
            score = 0;
        gameOver = false;
        gameWon = false;
        columnFree = false;



        int index = 0;
            //Loop that populates the Enemy List and initialises their x and y position
            for (int x = 100; x < 800; x += OFFSET)
            {
                for (int y = 10; y < 200; y += OFFSET)
                {
                    if (index < MAX_ENEMIES)
                    {

                        enemies.Add(new Enemy(new Point(x, y),48, enemyspeed, graphics, Properties.Resources.enemy_ship, bombs, rand, boundary, bombSound,enemies));

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

            if (enemies.Count > 0) //When the enemies list contains at least one. Set the variables

            {

                enemiesLeft = enemies[0].Position.X - enemyspeed; //The Leftmost side of the grid is the first Enemy. 
                //Offset by enemyspeed so the enemies are not drawn offscreen.
                enemiesRight = enemies[enemies.Count - 1].Position.X + enemies[0].Size + enemyspeed;
             
                enemiesBottom = enemies[enemies.Count - 1].Position.Y + enemies[enemies.Count - 1].Size; //The bottom of the grid is the last enemy's y position plus the size of the enemy
            }


            foreach (Bomb bomb in bombs.ToList())
            {
                if (bomb.Position.Y > boundary.Height) //When the bomb reaches the bottom of the screen it is destroyed
                {
                    bomb.Destroy();
                }
                if (bomb.Position.Y >= mothership.Picturebox.Top && bomb.Position.X >= mothership.Picturebox.Left && bomb.Position.X <= mothership.Picturebox.Right)
                {
                    //If the bomb touches the mothership. The game is over and the player has lost the game
                    gameOver = true;
                    destroySound.Play();
                   
                }
                foreach (Missile missile in missiles.ToList())
                {
                    if (bomb.Collider.IntersectsWith(missile.Collider))
                    {
                        //If a bomb and a missile collide with eachother. Both are destroyed and the score is increased by 5.
                        missile.Destroy();
                        bomb.Destroy();
                        score += 5;
                        enemyKilled.Play();
                    }
                }

            }







            if (enemies.Count == 0)
            {
                winSound.Play(); //WINNING SCENARIO
                GameWon = true;

                //if the enemies count reaches 0. The player has Won the game.

            }
            else if (enemiesBottom >= /*boundary.Bottom*/mothership.Picturebox.Top) //or bottom of form.
            {
                //If the bottom of the enemy grid is the same as the top of the mothership. The player loses the game.
                gameOver = true;
                destroySound.Play();
            }
            //When the enemy list reaches a certain size. The enemyspeed is set to different speeds.
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


            //If the enemy grid reaches either side of the screen. The direction of the grid changes
            //and the enemy grid drops one level.
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



                

                columnFree = true; //Is the next position in the column free?
                foreach (Enemy enemyPos in enemies.ToList()) //loop through enemies again and test if the column is free, the offset counts 3 forwards
                {
                    if ((enemyPos.Position.Y == enemy.Position.Y + OFFSET || enemyPos.Position.Y == enemy.Position.Y + 2*OFFSET || enemyPos.Position.Y == enemy.Position.Y + 3*OFFSET) && enemyPos.Position.X == enemy.Position.X)
                        //Tests the next 3 positions in the column if any of them are occupied by another enemy. The columnFree variable is set to false
                    {
                        columnFree = false;
                    }
  
                }
                if (columnFree) //If the column is free. the CanShoot property of the enemy is true.
                {
                    enemy.CanShoot = true;
                }

                enemy.Move();
                if (enemy.CanShoot)
                {
                    if (enemy.ShootChance == 99) //a 1/100 chance of dropping a bomb
                    {
                        enemy.Shoot();
                    }
                }
                foreach (Missile missile in missiles.ToList())
                {

                    if (missile.Collider.IntersectsWith(enemy.Collider))
                    {
                        //if a missile hits an enemy. Both are destroyed and the score is increased by 50.
                        missile.Destroy();
                        enemy.Destroy();
                        score += 50;
                        enemyKilled.Play();
                    }



                }
            }
        }


        public void MovePlayer(int x) //Moves the player to a given x position - The Mouse X position
        {
            //set the variable to be x
            mothership.Move(x); //mothership.Move()
        }





    }
}
