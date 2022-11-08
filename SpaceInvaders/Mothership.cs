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
        private Rectangle boundary;

        private const int SIZE = 64;
        private Graphics graphics;
        private PictureBox picturebox;
        private List<Missile> missiles;
        private SoundPlayer sound;
        private Random rand;

        public Mothership(PictureBox picturebox, Rectangle boundary, Graphics graphics, List<Missile> missiles, Random rand, SoundPlayer sound)
        {
            this.picturebox = picturebox;
            this.boundary = boundary;
            this.graphics = graphics;
            this.missiles = missiles;
            this.rand = rand;
            this.sound = sound;
        }

        public PictureBox Picturebox { get => picturebox; set => picturebox = value; }

        public void Move(int mouse) //Moves the mothership to the mouse position , offset so the center of the mothership always lines up with the mouse.
        {
            if (mouse - SIZE / 2 > boundary.Left && mouse + SIZE / 2 < boundary.Right)
            {
                picturebox.Left = mouse - SIZE / 2;
            }


        }


        public void Shoot() //Create a new missile at the position of the mothership. The offset of -8 is because missile's have a set width of 16 and this forces it to the middle
        //Plays the missile sound
        {

            missiles.Add(new Missile(new Point(picturebox.Left + SIZE / 2 - 8, picturebox.Top), 32, graphics, missiles, rand, Properties.Resources.missile, boundary));
            sound.Play();
        }


    }
}
