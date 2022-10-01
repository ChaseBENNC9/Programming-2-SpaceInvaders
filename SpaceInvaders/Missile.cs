using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class Missile
    {
        private Point position;
        private int velocity;
        private Graphics graphics;
        private List<Missile> missiles;
        private Rectangle collider;


        public Missile(Point position, int velocity,Graphics graphics, List<Missile> missiles)
        {
            this.position = position;
            this.velocity = velocity;
            this.graphics = graphics;
            this.missiles = missiles;
            collider = new Rectangle(position.X,position.Y,8,8);
        }

        public Point Position { get => position; set => position = value; }
        public Rectangle Collider { get => collider; set => collider = value; }

        public void Draw()
        {
            graphics.FillEllipse(Brushes.White, position.X, position.Y, 8, 8);
        }
        public void Move()
        {
            if (position.Y < 0)
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

        public void Destroy()
        {
           missiles.Remove(this);
        }


    }
}
