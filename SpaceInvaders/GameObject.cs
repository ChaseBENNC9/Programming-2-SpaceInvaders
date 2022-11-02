using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//The class which each object in the game inherits from. 
namespace SpaceInvaders
{
    public abstract class GameObject
    {
        protected Point position;
        protected Image image;
        protected Graphics graphics;
        protected Rectangle boundary;

        public GameObject(Point position, Image image, Graphics graphics, Rectangle boundary)
        {
            this.position = position;
            this.image = image;
            this.graphics = graphics;
            this.boundary = boundary;
        }

        public abstract void Draw();
        public abstract void Move();
    }
}
