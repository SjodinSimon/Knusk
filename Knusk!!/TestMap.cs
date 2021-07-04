using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knusk__
{
    class TestMap : Map
    {
        public TestMap(Texture2D mapSprites, List<Rectangle> backgroundSceneRectangle, List<Rectangle> backgroundObjectsRectangle, List<Rectangle> groundRectangle, List<Rectangle> backgroundObjectRectangle, List<Rectangle> foregroundObjectRectangle, List<Rectangle> foregroundRectangle, List<Vector2> backgroundScenePos, List<Vector2> backgroundObjectsPos, List<Vector2> groundPos, List<Vector2> backgroundObjectPos, List<Vector2> foregroundObjectPos, List<Vector2> foregroundPos, List<Rectangle> hitBox, List<bool> fullyPermeable)
            : base(mapSprites, backgroundSceneRectangle, backgroundObjectsRectangle, groundRectangle, backgroundObjectRectangle, foregroundObjectRectangle, foregroundRectangle, backgroundScenePos, backgroundObjectsPos, groundPos, backgroundObjectPos, foregroundObjectPos, foregroundPos, hitBox, fullyPermeable)
        {
            this.mapSprites = mapSprites;
            this.backgroundSceneRectangle = backgroundSceneRectangle;
            this.backgroundObjectsRectangle = backgroundObjectsRectangle;
            this.groundRectangle = groundRectangle;
            this.backgroundObjectRectangle = backgroundObjectRectangle;
            this.foregroundObjectRectangle = foregroundObjectRectangle;
            this.foregroundRectangle = foregroundRectangle;
            this.backgroundScenePos = backgroundScenePos;
            this.backgroundObjectsPos = backgroundObjectsPos;
            this.groundPos = groundPos;
            this.backgroundObjectPos = backgroundObjectPos;
            this.foregroundObjectPos = foregroundObjectPos;
            this.foregroundPos = foregroundPos;
            this.mapSprites = mapSprites;
            this.hitBox = hitBox;
        }
    }
}
