using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
namespace SpaceInvaders
{
    //This is the Mothership that the player will control.
    //It follows the cursor on the X-axis and will fire a missile when the mouse is clicked.
   
    public class Mothership
    {
        //private Image image;
        private Rectangle boundary;

        private const int SIZE = 64;
        private Point position;
        private Graphics graphics;
        private PictureBox picturebox;
        private List<Missile> missiles;
        private SoundPlayer sound;
        private Random rand;

        public Mothership(PictureBox picturebox,Rectangle boundary, Point position,Graphics graphics,List<Missile> missiles,Random rand,SoundPlayer sound)
        {
            this.picturebox = picturebox;
            this.boundary = boundary;
            this.position = position;
            this.graphics = graphics;
            this.missiles = missiles;
            this.rand = rand;
            this.sound = sound;
        }

        public PictureBox Picturebox { get => picturebox; set => picturebox = value; }
        public Point Position { get => position; set => position = value; }
        public PictureBox Picturebox1 { get => picturebox; set => picturebox = value; }

        public void Move(int mouse)
        {
            if(mouse-SIZE/2 > boundary.Left && mouse+SIZE/2 < boundary.Right)
            {
                picturebox.Left = mouse - SIZE/2;
            }


        }


        public void Shoot()
        {
            
            missiles.Add(new Missile(new Point(picturebox.Left + SIZE /2 - (8), picturebox.Top), 32, graphics,missiles,rand,Properties.Resources.missile,boundary));
            sound.Play();
        }

        
    }
}
