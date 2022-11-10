//This is the bomb, it has a similar behaviour to the missile except it moves downwards from an enemies position and does not have a set limit. After a random ammount of ticks
//Or after a collision with the player it will be destroyed

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class Bomb : GameObject
    {
        private const int SIZE = 16;
        private const int MAXLIFESPAN = 70;

        private int velocity;
        private List<Bomb> bombs;
        private Rectangle collider;
        private int life;
        private Random rand;


        public Bomb(Point position, int velocity, Graphics graphics, List<Bomb> bombs, Random rand, Image image, Rectangle boundary) :
            base(position, image, graphics, boundary)
        {
            this.velocity = velocity;
            this.bombs = bombs;
            collider = new Rectangle(position.X, position.Y, SIZE, SIZE);
            this.rand = rand;
            life = rand.Next(1, MAXLIFESPAN);
        }

        public Point Position { get => position; set => position = value; }
        public Rectangle Collider { get => collider; set => collider = value; }

        public override void Draw() //Draw the image of the missile
        {
            graphics.DrawImage(image, position.X, position.Y, SIZE, SIZE);

        }
        public override void Move() //Moves the bomb downwards at a constant speed until it reaches the bottom of the screen.
        {
            if (life > 0)
            {
                life--;
                if (position.Y >= boundary.Height)
                {
                    Destroy();
                }
                else
                {
                    position.Y += velocity;
                    collider.Y = position.Y;
                    collider.X = position.X;

                }

            }
            else if (life == 0)
            {
                Destroy();
            }

        }

        public override void Destroy() //Destroys the bomb by removing it from it's list.
        {

            bombs.Remove(this);

        }


    }
}
