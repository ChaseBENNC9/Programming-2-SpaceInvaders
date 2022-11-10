using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    //This is a Missile, It is created when the player clicks the mouse. It will start at the top of the Mothership in the center and will only move Up. 
    //If it collides with an enemy It will be destroyed
    public class Missile : GameObject
    {

        private int velocity;
        private const int WIDTH = 16;
        private const int HEIGHT = 32;
        private const int MAXLIFESPAN = 70;
        private List<Missile> missiles;
        private Rectangle collider;

        private int life;
        private Random rand;




        public Missile(Point position, int velocity, Graphics graphics, List<Missile> missiles, Random rand, Image image, Rectangle boundary) :
            base(position, image, graphics, boundary)

        {
            this.velocity = velocity;
            this.missiles = missiles;
            collider = new Rectangle(position.X, position.Y, WIDTH, HEIGHT);
            this.rand = rand;
            life = rand.Next(1, MAXLIFESPAN);
        }

        public Rectangle Collider { get => collider; set => collider = value; }

        public override void Draw() //Draw's the image of the missile
        {
            graphics.DrawImage(image, position.X, position.Y, WIDTH, HEIGHT);
        }
        public override void Move() //Move the missile while it still has life, when life reaches 0 it is destroyed
        {
            if (life > 0)
            {
                life--;
                if (position.Y < boundary.Top)
                {
                    Destroy();
                }
                else
                {
                    position.Y -= velocity;
                    collider.Y = position.Y;
                    collider.X = position.X;

                }

            }
            else if (life == 0)
            {
                Destroy();
            }

        }

        public override void Destroy() //Removes the missile from the list and therefore stops drawing to the screen.
        {

            missiles.Remove(this);

        }


    }
}
