using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{

    public class Mothership
    {
        private PictureBox picturebox;
        private Point position;
        private Rectangle boundary;
        private Rectangle border;
        public Mothership(PictureBox picturebox, Point position,Rectangle boundary)
        {
            this.picturebox = picturebox;
            this.position = position;
            this.boundary = boundary;
            border = new Rectangle(position.X, position.Y, 64, 64);
        }


        public void Move(int dir)
        {
       
                picturebox.Left += dir*10;

            if(picturebox.Left < boundary.Left)
            {
                picturebox.Left = boundary.Left;
            }
            else if(picturebox.Right > boundary.Right)
            {
                picturebox.Left = boundary.Right - 64;
            }



        }
        
    }
}
