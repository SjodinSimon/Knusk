using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knusk__
{
    abstract class MovingObject : SingleObject
    {
        protected Vector2 speed;
        protected Vector2 previousPos;
        public MovingObject(Texture2D texture, float X, float Y, float speedX, float speedY, Rectangle sourceRectangle, Vector2 previousPos)
            : base(texture, X, Y, sourceRectangle)
        {
            this.speed.X = speedX;
            this.speed.Y = speedX;
        }
    }
}
