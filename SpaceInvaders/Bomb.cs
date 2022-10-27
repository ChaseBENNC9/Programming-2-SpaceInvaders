using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//This is the bomb, it has a similar behaviour to the missile except it moves downwards from an enemies position and does not have a set limit. After a random ammount of ticks
//Or after a collision with the player it will be destroyed
namespace SpaceInvaders
{
    public class Bomb
    {
        private Point position;
        private int velocity;
        private Graphics graphics;
        private List<Bomb> bombs;
        private Rectangle collider;
        private Rectangle boundary;
        private int life;
        private Random rand;


        public Bomb(Point position, int velocity, Graphics graphics, List<Bomb> bombs, Random rand,Rectangle boundary)
        {
            this.boundary = boundary;
            this.position = position;
            this.velocity = velocity;
            this.graphics = graphics;
            this.bombs = bombs;
            collider = new Rectangle(position.X, position.Y, 8, 8);
            this.rand = rand;
            life = rand.Next(1, 70);
        }

        public Point Position { get => position; set => position = value; }
        public Rectangle Collider { get => collider; set => collider = value; }
        public int Life { get => life; set => life = value; }

        public void Draw()
        {
            graphics.FillEllipse(Brushes.Blue, position.X, position.Y, 8, 8);
        }
        public void Move()
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

        public void Destroy()
        {
            //graphics.FillEllipse(Brushes.Blue, position.X, position.Y, 8, 8);

            bombs.Remove(this);

        }


    }
}
