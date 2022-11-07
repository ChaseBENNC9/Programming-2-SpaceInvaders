
using System.Media;

namespace SpaceInvaders
{
    //This is the enemy, It moves in a grid in a steplock motion and drops a Bomb at random intervals if it is active. 
    //If the enemy collides with a missile it will be destroyed, If the bottom row of enemies reaches the y position of the Mothership
    //the game will end.

    //As the amount of enemies left decreases to certain ammounts the speed they move will increase at set intervals. and They speed up by a constant interval when
    //they drop one level
    public class Enemy : GameObject
    {
        private SoundPlayer sound;

        private eDirection direction;
        private Rectangle collider;
        private List<Bomb> bombs;
        private List<Enemy> enemies;
        private Random rand;
        private int shootNum;
        private bool canShoot;
        private int velocity;
        private int size;
        private const int DROPCHANCE = 100;
        private const int SPEEDCHANGE = 2;
        public Enemy(Point position,int size, int velocity, Graphics graphics, Image image, List<Bomb> bombs, Random rand, Rectangle boundary, SoundPlayer sound, List<Enemy> enemies) :
            base(position, image, graphics, boundary)
        {
            this.rand = rand;
            this.boundary = boundary;
            this.position = position;
            this.graphics = graphics;
            this.sound = sound;
            this.velocity = velocity;
            this.image = image;
            this.bombs = bombs;
            this.enemies = enemies;
            this.size = size;
            collider = new Rectangle(position.X, position.Y, size, size);
            canShoot = false;
            shootNum = 0;
            direction = eDirection.LEFT;

        }

        
        public Point Position { get => position; set => position = value; }
        public Rectangle Collider { get => collider; set => collider = value; }
        public bool CanShoot { get => canShoot; set => canShoot = value; }
        public int Velocity { get => velocity; set => velocity = value; }
        public int ShootNum { get => shootNum; set => shootNum = value; }
        public eDirection Direction { get => direction; set => direction = value; }
        public int Size { get => size; set => size = value; }

        public override void Draw()
        {
            graphics.DrawImage(image, position.X, position.Y, size, size);


        }

        public override void Move()
        {
            //
            shootNum = rand.Next(DROPCHANCE);
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
            position.Y += size/2;
            velocity += SPEEDCHANGE;
        }

        public override void Destroy()
        {
            enemies.Remove(this);
        }

        public void Shoot()
        {
            bombs.Add(new Bomb(new Point(position.X+size/2,position.Y+size), 32, graphics, bombs, rand, Properties.Resources.bomb1,boundary));
            sound.Play();
        }
    }
}