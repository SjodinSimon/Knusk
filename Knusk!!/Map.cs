using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knusk__
{
    abstract class Map
    {
        public Map(Texture2D mapSprites, List<Rectangle> backgroundSceneRectangle, List<Rectangle> backgroundObjectsRectangle, List<Rectangle> groundRectangle, List<Rectangle> backgroundObjectRectangle, List<Rectangle> foregroundObjectRectangle, List<Rectangle> foregroundRectangle, List<Vector2> backgroundScenePos, List<Vector2> backgroundObjectsPos, List<Vector2> groundPos, List<Vector2> backgroundObjectPos, List<Vector2> foregroundObjectPos, List<Vector2> foregroundPos, List<Rectangle> hitBox, List<bool> fullyPermeable)
        {
            this.mapSprites = mapSprites;
            this.fullyPermeable = fullyPermeable;
        }

        protected Texture2D mapSprites;

        protected List<Rectangle> backgroundSceneRectangle;
        protected List<Rectangle> backgroundObjectsRectangle;
        protected List<Rectangle> groundRectangle;
        protected List<Rectangle> backgroundObjectRectangle;
        protected List<Rectangle> foregroundObjectRectangle;
        protected List<Rectangle> foregroundRectangle;

        protected List<Vector2> backgroundScenePos;
        protected List<Vector2> backgroundObjectsPos;
        protected List<Vector2> groundPos;
        protected List<Vector2> backgroundObjectPos;
        protected List<Vector2> foregroundObjectPos;
        protected List<Vector2> foregroundPos;

        protected List<Rectangle> hBox;
        public List<Rectangle> hitBox
        {
            get
            {
                return hBox;
            }
            set
            {
                hBox = value;
            }
        }

        protected List<bool> fullPermeable;
        public List<bool> fullyPermeable
        {
            get
            {
                return fullPermeable;
            }
            set
            {
                fullPermeable = value;
            }
        }

        public virtual void DrawBackground(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < backgroundScenePos.Count; i++)
            {
                spriteBatch.Draw(mapSprites, backgroundScenePos[i], backgroundSceneRectangle[i], Color.White);
            }

            for (int i = 0; i < backgroundObjectsPos.Count; i++)
            {
                spriteBatch.Draw(mapSprites, backgroundObjectsPos[i], backgroundObjectsRectangle[i], Color.White);
            }

            for (int i = 0; i < groundPos.Count; i++)
            {
                spriteBatch.Draw(mapSprites, groundPos[i], groundRectangle[i], Color.White);
            }

            for (int i = 0; i < backgroundObjectPos.Count; i++)
            {
                spriteBatch.Draw(mapSprites, backgroundObjectPos[i], backgroundObjectRectangle[i], Color.White);
            }
        }

        public virtual void DrawForeground(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < foregroundObjectPos.Count; i++)
            {
                spriteBatch.Draw(mapSprites, foregroundObjectPos[i], foregroundObjectRectangle[i], Color.White);
            }

            for (int i = 0; i < foregroundPos.Count; i++)
            {
                spriteBatch.Draw(mapSprites, foregroundPos[i], foregroundRectangle[i], Color.White);
            }
        }
    }
}
