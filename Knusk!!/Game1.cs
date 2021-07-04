using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Knusk__
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Rectangle> mapHitBox;

        Texture2D testMapSpriteSheet;
        TestMap testMap;

        Texture2D dudeSprite;
        Simon dude;

        Random r = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 720;
            graphics.PreferredBackBufferHeight = 405;
            graphics.ApplyChanges();
            // graphics.ToggleFullScreen();
        }
        

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            dudeSprite = this.Content.Load<Texture2D>("simonSpriteSheet");
            dude = new Simon(dudeSprite, 0, 0, 0, 0, new Rectangle(), new Vector2());

            testMapSpriteSheet = this.Content.Load<Texture2D>("testMapSpriteSheet");

            // mapSprites, backgroundSceneRectangle, backgroundObjectsRectangle, groundRectangle, backgroundObjectRectangle, foregroundObjectRectangle, foregroundRectangle, backgroundScenePos, backgroundObjectsPos, groundPos, backgroundObjectPos, foregroundObjectPos, foregroundPos, hitBofalse, false, false, true, truex}
            testMap = new TestMap(testMapSpriteSheet, new List<Rectangle> {new Rectangle(0, 0, 720, 405) }, new List<Rectangle>(), new List<Rectangle> { new Rectangle(0, 534, 240, 100), new Rectangle(0, 634, 240, 100), new Rectangle(0, 734, 240, 100), new Rectangle(0, 834, 240, 50), new Rectangle(0, 884, 240, 50) }, new List<Rectangle>(), new List<Rectangle>(), new List<Rectangle>(), new List<Vector2> { new Vector2(0, 0) }, new List<Vector2>(), new List<Vector2> {new Vector2(0, 355), new Vector2(240, 305), new Vector2(480, 355), new Vector2(50, 155), new Vector2(470, 155), }, new List<Vector2>(), new List<Vector2>(), new List<Vector2>(), new List<Rectangle> { new Rectangle(0, 355, 240, 100), new Rectangle(240, 305, 240, 100), new Rectangle(480, 355, 240, 100), new Rectangle(50, 155, 240, 50), new Rectangle(470, 155, 240, 50) }, new List<bool> {false, false, false, true, true });

            mapHitBox = testMap.hitBox;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            dude.Update(gameTime, mapHitBox, testMap.fullyPermeable);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            spriteBatch.Begin();
            // TODO: Add your drawing code here

            testMap.DrawBackground(spriteBatch);

            dude.Draw(spriteBatch);

            testMap.DrawForeground(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
