using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knusk__
{
    abstract class SingleObject
    {
        protected Texture2D texture;
        protected Vector2 vector;
        protected Rectangle sourceRectangle;
        public SingleObject(Texture2D texture, float x, float y, Rectangle sourceRectangle)
        {
            this.texture = texture;
            this.vector.X = x;
            this.vector.Y = y;
            this.sourceRectangle = sourceRectangle;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, sourceRectangle, Color.White);
        }
        public float X
        {
            get { return vector.X; }
            set { vector.X = value; }
        }
        public float Y
        {
            get { return vector.Y; }
            set { vector.Y = value; }
        }
        public float Width
        {
            get { return texture.Width; }
        }
        public float Height
        {
            get { return texture.Height; }
        }
    }
}