
using System.Media;

namespace SpaceInvaders
{
    //This is the enemy, It moves in a grid in a steplock motion and drops a Bomb at random intervals if it is active. 
    //If the enemy collides with a missile it will be destroyed, If the bottom row of enemies reaches the y position of the Mothership
    //the game will end.

    //As the amount of enemies left decreases to certain ammounts the speed they move will increase at set intervals.
    public class Enemy : GameObject
    {
        private SoundPlayer sound;

        private eDirection direction;
        private Rectangle collider;
        private List<Bomb> bombs;
        private Random rand;
        private int shootNum;
        private bool destroyed;
        private bool canShoot;
        private int velocity;
        private const int SIZE = 48;
        //private int direction;
        public Enemy(Point position, int velocity, Graphics graphics, Image image, List<Bomb> bombs,Random rand,Rectangle boundary,SoundPlayer sound) :
            base(position,image,graphics,boundary)
        {
            this.rand = rand;
            this.boundary = boundary;
            this.position = position;
            this.graphics = graphics;
            this.sound = sound;
            this.velocity = velocity;
            this.image = image;
            this.bombs = bombs;
            collider = new Rectangle(position.X, position.Y, SIZE, SIZE);
            destroyed = false;
            canShoot = false;
            shootNum = 0;
            direction = eDirection.LEFT;
            
        }

        public Point Position { get => position; set => position = value; }
        public Rectangle Collider { get => collider; set => collider = value; }
        public bool Destroyed { get => destroyed; set => destroyed = value; }
        public bool CanShoot { get => canShoot; set => canShoot = value; }
        public int Velocity { get => velocity; set => velocity = value; }
        public int ShootNum { get => shootNum; set => shootNum = value; }
        public eDirection Direction { get => direction; set => direction = value; }

        public override void Draw()
        {
            graphics.DrawImage(image, position.X, position.Y, SIZE, SIZE);
        //    if (canShoot)
        //    {
        //        graphics.DrawRectangle(Pens.Green, position.X, position.Y, 48, 48);

        //    }
        //    else
        //    {
        //        graphics.DrawRectangle(Pens.Red, position.X, position.Y, 48, 48);

        //    }

        }

        public override void Move()
        {
            shootNum = rand.Next(1, 101);
            collider.X = position.X;
            collider.Y = position.Y;
            switch (direction)
            {
                case eDirection.LEFT:
                    position.X -= velocity;
                    break;
                case eDirection.RIGHT:
                    position.X += velocity;
                    break;
            }
            
        }

        public void ShiftLevel()
        {
            position.Y += 24;
            velocity += 2;
        }

        public void Destroy()
        {
            destroyed = true;
        }

        public void Shoot()
        {
            bombs.Add(new Bomb(new Point(position.X+16,position.Y+32), 32, graphics, bombs, rand, Properties.Resources.bomb1,boundary));
            sound.Play();
        }
    }
}