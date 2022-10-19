using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    //This is the Mothership that the player will control. It follows the cursor on the X-axis and will fire a missile when the mouse is clicked
    public class Mothership
    {
        //private Image image;
        private Rectangle boundary;
        private Point position;
        private Graphics graphics;
        private PictureBox picturebox;
        private List<Missile> missiles;
        private Random rand;

        public Mothership(PictureBox picturebox,Rectangle boundary, Point position,Graphics graphics,List<Missile> missiles,Random rand)
        {
            this.picturebox = picturebox;
            this.boundary = boundary;
            this.position = position;
            this.graphics = graphics;
            this.missiles = missiles;
            this.rand = rand;
        }

        public PictureBox Picturebox { get => picturebox; set => picturebox = value; }
        public Point Position { get => position; set => position = value; }


        //public void Move(int dir)
        //{

        //        picturebox.Left += dir*10;

        //    if(picturebox.Left < boundary.Left)
        //    {
        //        picturebox.Left = boundary.Left;
        //    }
        //    else if(picturebox.Right > boundary.Right)
        //    {
        //        picturebox.Left = boundary.Right - 64;
        //    }



        //}
        public void Move(int mouse)
        {
            if(mouse-32 > boundary.Left && mouse+32 < boundary.Right)
            {
                picturebox.Left = mouse - 32;
            }


        }

        //public void Draw()
        //{

        //    graphics.DrawImage(image, position.X, position.Y, 64, 64);
        //    //graphics.DrawRectangle(Pens.Red, position.X, position.Y, 25, 25);


        //}
        public void Shoot()
        {
            missiles.Add(new Missile(new Point(picturebox.Left + 32, picturebox.Top), 32, graphics,missiles,rand));
        }

        
    }
}
