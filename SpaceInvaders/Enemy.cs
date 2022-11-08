
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
        private int shootChance;
        private bool canShoot;
        private int velocity;
        private int size;
        private const int DROPCHANCE = 100;
        private const int SPEEDCHANGE = 2;
        public Enemy(Point position, int size, int velocity, Graphics graphics, Image image, List<Bomb> bombs, Random rand, Rectangle boundary, SoundPlayer sound, List<Enemy> enemies) :
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
            shootChance = 0;
            direction = eDirection.LEFT;

        }


        public Point Position { get => position; set => position = value; }
        public Rectangle Collider { get => collider; set => collider = value; }
        public bool CanShoot { get => canShoot; set => canShoot = value; }
        public int Velocity { get => velocity; set => velocity = value; }
        public int ShootChance { get => shootChance; set => shootChance = value; }
        public eDirection Direction { get => direction; set => direction = value; }
        public int Size { get => size; set => size = value; }

        public override void Draw()
        {

            graphics.DrawImage(image, position.X, position.Y, size, size);


        }

        public override void Move()
        {
            //The enemies move in a grid formation. Every time the function is called, it generates a new random number between 0 and 100.
            shootChance = rand.Next(DROPCHANCE);
            collider.X = position.X;
            collider.Y = position.Y;
            switch (direction) //reads the Enum: direction and changes the enemy direction depending on it's value
            {
                case eDirection.LEFT:
                    position.X -= velocity;
                    break;
                case eDirection.RIGHT:
                    position.X += velocity;
                    break;
            }

        }


        public void ShiftLevel() //When the enemy reaches each side. The grid will drop one level, and increase it's velocity slightly
        {
            position.Y += size / 2;
            velocity += SPEEDCHANGE;
        }

        public override void Destroy()
        {
            enemies.Remove(this); //When the enemy is destroyed it is removed from the list and will stop being drawn to the screen.
        }

        public void Shoot() //Create a new bomb at the enemy position and play the sound for a bomb
        {
            bombs.Add(new Bomb(new Point(position.X + size / 2, position.Y + size), 32, graphics, bombs, rand, Properties.Resources.bomb1, boundary));
            sound.Play();
        }
    }
}