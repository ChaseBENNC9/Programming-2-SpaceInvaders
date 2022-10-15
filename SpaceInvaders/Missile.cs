﻿using System;
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
        private int life;
        private Random rand;


        public Missile(Point position, int velocity,Graphics graphics, List<Missile> missiles,Random rand)
        {
            this.position = position;
            this.velocity = velocity;
            this.graphics = graphics;
            this.missiles = missiles;
            collider = new Rectangle(position.X,position.Y,8,8);
            this.rand = rand;
            life = rand.Next(1, 70);
        }

        public Point Position { get => position; set => position = value; }
        public Rectangle Collider { get => collider; set => collider = value; }
        public int Life { get => life; set => life = value; }

        public void Draw()
        {
            graphics.FillEllipse(Brushes.White, position.X, position.Y, 8, 8);
        }
        public void Move()
        {
            if (life > 0)
            {
                life--;
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
            else if (life == 0)
            {
                Destroy();
            }
            
        }

        public void Destroy()
        {
           missiles.Remove(this);
        }


    }
}
