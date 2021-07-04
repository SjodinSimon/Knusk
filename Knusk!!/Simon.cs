using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knusk__
{
    class Simon : MovingObject
    {
        public Simon(Texture2D texture, float X, float Y, float speedX, float speedY, Rectangle sourceRectangle, Vector2 previousPos)
            : base(texture, X, Y, speedX, speedY, sourceRectangle, previousPos)
        {
            // source rectangles run right
            animateRectangle[0] = new Rectangle(0, 0, 49, 56);
            animateRectangle[1] = new Rectangle(49, 0, 49, 56);
            animateRectangle[2] = new Rectangle(98, 0, 49, 56);
            animateRectangle[3] = new Rectangle(147, 0, 49, 56);
            animateRectangle[4] = new Rectangle(196, 0, 49, 56);
            animateRectangle[5] = new Rectangle(245, 0, 49, 56);
            animateRectangle[6] = new Rectangle(294, 0, 49, 56);
            animateRectangle[7] = new Rectangle(343, 0, 49, 56);

            // source rectangles run left
            animateRectangle[8] = new Rectangle(0, 56, 49, 56);
            animateRectangle[9] = new Rectangle(49, 56, 49, 56);
            animateRectangle[10] = new Rectangle(98, 56, 49, 56);
            animateRectangle[11] = new Rectangle(147, 56, 49, 56);
            animateRectangle[12] = new Rectangle(196, 56, 49, 56);
            animateRectangle[13] = new Rectangle(245, 56, 49, 56);
            animateRectangle[14] = new Rectangle(294, 56, 49, 56);
            animateRectangle[15] = new Rectangle(343, 56, 49, 56);

            // source rectangles for jumping and falling to the right
            animateRectangle[16] = new Rectangle(0, 112, 38, 67); // jump
            animateRectangle[17] = new Rectangle(38, 112, 38, 67);
            animateRectangle[18] = new Rectangle(76, 112, 38, 67);
            animateRectangle[19] = new Rectangle(114, 112, 38, 67); // Fall
            animateRectangle[20] = new Rectangle(152, 112, 38, 67);
            animateRectangle[21] = new Rectangle(190, 112, 38, 67);
            animateRectangle[22] = new Rectangle(228, 112, 38, 67); // Land
            animateRectangle[23] = new Rectangle(266, 112, 38, 67);

            // source rectangle Jump and fall left
            animateRectangle[24] = new Rectangle(0, 179, 38, 67); // land
            animateRectangle[25] = new Rectangle(38, 179, 38, 67);
            animateRectangle[26] = new Rectangle(76, 179, 38, 67); // fall
            animateRectangle[27] = new Rectangle(114, 179, 38, 67);
            animateRectangle[28] = new Rectangle(152, 179, 38, 67);
            animateRectangle[29] = new Rectangle(190, 179, 38, 67); // jump
            animateRectangle[30] = new Rectangle(228, 179, 38, 67);
            animateRectangle[31] = new Rectangle(266, 179, 38, 67);

            // source rectangle TEMPORARY idle pose
            animateRectangle[32] = new Rectangle(356, 182, 40, 64); // right
            animateRectangle[33] = new Rectangle(316, 182, 40, 64); // left

            anticipatoryHitBoxUp.Height = 1;
            anticipatoryHitBoxDown.Height = 1;
            anticipatoryHitBoxLeft.Width = 1;
            anticipatoryHitBoxRight.Width = 1;
        }

        private Rectangle[] animateRectangle = new Rectangle[34];

        private int runTimerThreshold = 50;
        private float runTimerRight = 0;
        private float runTimerLeft = 0;
        private int fallTimerThreshold = 50;
        private float fallTimer = 0;
        private int jumpTimerThreshold = 50;
        private float jumpTimer = 0;
        private int landTimerThreshold = 50;
        private float landTimer = 0;

        private int jumpHeight = 15;
        private int runningSpeed = 6;

        private int currentFrame = 0;

        private bool canMoveRight = true;
        private bool canMoveLeft = true;
        private bool runRightBegin = true;
        private bool runLeftBegin = true;
        private bool runRight = false;
        private bool runLeft = false;
        private bool foundWall = false;

        private bool hasFoothold;
        private bool foundFoothold;

        private float footPosY;

        private bool isJump = false;
        private bool isJumpBegin = true;
        private bool canJump = false;
        private bool isLand = false;
        private bool hasLanded = true;

        private Rectangle hitBox;
        private Rectangle anticipatoryHitBoxUp;
        private Rectangle anticipatoryHitBoxDown;
        private Rectangle anticipatoryHitBoxLeft;
        private Rectangle anticipatoryHitBoxRight;

        private bool fallRightBegin = true;
        private bool fallLeftBegin = true;

        private bool rightDir = true;
        private bool leftDir = false;
        
        public void Update(GameTime gameTime, List<Rectangle> mapHitBox, List<bool> fullyPermeable)
        {
            // Passive logic
            CheckIdle(gameTime); // toggles idle animation when appropriate

            CheckMovement(gameTime); // corrects position and such according to speed and direction

            CheckPermeability(gameTime, mapHitBox, fullyPermeable); // Checks if player is in contact with ground

            CheckFall(gameTime); // falling logiclogic for falling

            // Activate right movement
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftThumbstickRight) && canMoveRight || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight) && canMoveRight)
            {
                if (!isJump && hasFoothold)
                {
                    runRight = true;
                }
                rightDir = true;
                leftDir = false;
                speed.X = runningSpeed;
            }

            // Activate left movement
            else if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftThumbstickLeft) && canMoveLeft || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft) && canMoveLeft)
            {
                if (!isJump && hasFoothold)
                {
                    runLeft = true;
                }
                leftDir = true;
                rightDir = false;
                speed.X = -runningSpeed;
            }
            else
            {
                speed.X = 0;
                runRight = false;
                runLeft = false;
            }

            // Handles all jump-related logic in specialized method
            Run(gameTime);

            // Jump activation
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftThumbstickUp) && canJump || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp) && canJump || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.X) && canJump)
            {
                isJump = true;
            }

            // Calls method for jump-related logic
            Jump(gameTime);

            // Drop or crouch
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftThumbstickDown) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
            {

            }
        }

        private void CheckMovement(GameTime gameTime)
        {
            // Creates log of where player was previously (might be removed later)
            previousPos = new Vector2(X, Y);

            // logic general movement
            X += speed.X;
            footPosY += speed.Y;
            Y = footPosY - animateRectangle[currentFrame].Height;

            // moves and reshapes hitbox to match player's position and shape
            hitBox.X = Convert.ToInt32(X);
            hitBox.Y = Convert.ToInt32(Y);
            hitBox.Height = animateRectangle[currentFrame].Height;
            hitBox.Width = animateRectangle[currentFrame].Width;

            anticipatoryHitBoxUp.X = Convert.ToInt32(X);
            anticipatoryHitBoxUp.Y = Convert.ToInt32(Y) + anticipatoryHitBoxUp.Height;
            anticipatoryHitBoxUp.Width = animateRectangle[currentFrame].Width;

            anticipatoryHitBoxDown.X = Convert.ToInt32(X);
            anticipatoryHitBoxDown.Y = Convert.ToInt32(Y) - animateRectangle[currentFrame].Height - anticipatoryHitBoxDown.Height;
            anticipatoryHitBoxDown.Width = animateRectangle[currentFrame].Width;

            anticipatoryHitBoxLeft.X = Convert.ToInt32(X) - anticipatoryHitBoxLeft.Width;
            anticipatoryHitBoxLeft.Y = Convert.ToInt32(Y);
            anticipatoryHitBoxLeft.Height = animateRectangle[currentFrame].Height;

            anticipatoryHitBoxRight.X = Convert.ToInt32(X) + animateRectangle[currentFrame].Width;
            anticipatoryHitBoxRight.Y = Convert.ToInt32(Y);
            anticipatoryHitBoxLeft.Height = animateRectangle[currentFrame].Height;
        }

        private void CheckIdle(GameTime gameTime)
        {
            // Checks if character is still and activates idle
            if (speed.X == 0 && speed.Y == 0 && !runRight && !runLeft && !isJump)
            {
                if (rightDir)
                {
                    currentFrame = 32;
                    sourceRectangle = animateRectangle[currentFrame];
                }
                else if (leftDir)
                {
                    currentFrame = 33;
                    sourceRectangle = animateRectangle[currentFrame];
                }
            }
        }

        private void CheckPermeability(GameTime gameTime, List<Rectangle> mapHitBox, List<bool> fullyPermeable)
        {
            for (int i = 0; i < mapHitBox.Count; i++)
            {
                if (hitBox.Intersects(mapHitBox[i]) && speed.Y > 0 && !hasFoothold)
                {
                    hasFoothold = true;
                    speed.Y = 0;
                    footPosY = mapHitBox[i].Y;
                }

                if (anticipatoryHitBoxDown.Intersects(mapHitBox[i]) && hasFoothold)
                {
                    foundFoothold = true;
                }
            }
            if (!foundFoothold)
            {
                hasFoothold = false;
            }
            foundFoothold = false;

            for (int i = 0; i < mapHitBox.Count; i++)
            {
                if (!fullyPermeable[i])
                {
                    if (anticipatoryHitBoxRight.Intersects(mapHitBox[i]) && speed.X > 0)
                    {
                        speed.X = 0;
                        X = mapHitBox[i].X - animateRectangle[currentFrame].Width;
                        canMoveRight = false;
                    }
                    else if (anticipatoryHitBoxLeft.Intersects(mapHitBox[i]) && speed.X < 0)
                    {
                        speed.X = 0;
                        X = mapHitBox[i].X + mapHitBox[i].Width;
                        canMoveLeft = false;
                    }

                    if (anticipatoryHitBoxLeft.Intersects(mapHitBox[i]) && !canMoveLeft)
                    {
                        foundWall = true;
                    }
                    else if (anticipatoryHitBoxRight.Intersects(mapHitBox[i]) && !canMoveRight)
                    {
                        foundWall = true;
                    }
                }
                if (!foundWall)
                {
                    canMoveLeft = true;
                    canMoveRight = true;
                }
                foundWall = false;
            }
        }

        private void CheckFall(GameTime gameTime)
        {
            // Fall
            if (!hasFoothold)
            {
                speed.Y++;
            }
            else if (hasFoothold) // Don't fall
            {
                speed.Y = 0;

                if (!canJump) // checks if contact with ground is a landing
                {
                    runRightBegin = true;
                    runLeftBegin = true;
                    canJump = true;
                    isJump = false;
                    isJumpBegin = true;
                    isLand = true;
                }
            }

            // Landing animation stuffs
            if (isLand)
            {
                canMoveRight = false;
                canMoveLeft = false;

                if (rightDir)
                {
                    if (landTimer > landTimerThreshold)
                    {
                        currentFrame = 23;
                    }
                    else
                    {
                        landTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        currentFrame = 22;
                    }

                    if (currentFrame == 23)
                    {
                        if (landTimer > landTimerThreshold * 2)
                        {
                            hasLanded = true;
                        }
                        else
                        {
                            landTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        }
                    }
                    sourceRectangle = animateRectangle[currentFrame];
                }
                else if (leftDir)
                {
                    if (landTimer > landTimerThreshold)
                    {
                        currentFrame = 25;
                    }
                    else
                    {
                        landTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        currentFrame = 24;
                    }

                    if (currentFrame == 25)
                    {
                        if (landTimer > landTimerThreshold * 2)
                        {
                            hasLanded = true;
                        }
                        else
                        {
                            landTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        }
                    }
                    sourceRectangle = animateRectangle[currentFrame];
                }

                if (hasLanded)
                {
                    landTimer = 0;
                    isLand = false;
                    canMoveRight = true;
                    canMoveLeft = true;
                }
            }

            // Fall animation stuff
            if (speed.Y > 0)
            {
                hasLanded = false;

                if (rightDir) // Fall right
                {
                    if (fallRightBegin)
                    {
                        fallTimer = 0;
                        currentFrame = 19;
                        fallRightBegin = false;
                    }

                    if (fallTimer > fallTimerThreshold)
                    {
                        sourceRectangle = animateRectangle[currentFrame];

                        if (currentFrame == 21)
                        {
                            currentFrame = 19;
                        }
                        else
                        {
                            currentFrame++;
                        }

                        fallTimer = 0;
                    }
                    else
                    {
                        fallTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                }
                else if (leftDir) // Fall left
                {
                    if (fallLeftBegin)
                    {
                        fallTimer = 0;
                        currentFrame = 28;
                        fallLeftBegin = false;
                    }

                    if (fallTimer > fallTimerThreshold)
                    {
                        sourceRectangle = animateRectangle[currentFrame];

                        if (currentFrame == 26)
                        {
                            currentFrame = 28;
                        }
                        else
                        {
                            currentFrame--;
                        }

                        fallTimer = 0;
                    }
                    else
                    {
                        fallTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                }
            }
            else if (speed.Y <= 0)
            {
                fallLeftBegin = true;
                fallRightBegin = true;
            }
        }

        private void Run(GameTime gameTime)
        {
            // Run right
            if (runRight)
            {
                if (runRightBegin)
                {
                    currentFrame = 6;
                    runRightBegin = false;
                }

                if (1 == 1)
                {
                    if (runTimerRight > runTimerThreshold)
                    {
                        sourceRectangle = animateRectangle[currentFrame];

                        if (currentFrame == 7)
                        {
                            currentFrame = 0;
                        }
                        else
                        {
                            currentFrame++;
                        }

                        runTimerRight = 0;
                    }
                    else
                    {
                        runTimerRight += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                }
            }
            else if (!runRight)
            {
                runRightBegin = true;
            }

            // Run left
            if (runLeft)
            {
                if (runLeftBegin)
                {
                    currentFrame = 9;
                    runLeftBegin = false;
                }

                if (1 == 1)
                {
                    if (runTimerLeft > runTimerThreshold)
                    {
                        sourceRectangle = animateRectangle[currentFrame];

                        if (currentFrame == 8)
                        {
                            currentFrame = 15;
                        }
                        else
                        {
                            currentFrame--;
                        }

                        runTimerLeft = 0;
                    }
                    else
                    {
                        runTimerLeft += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                }
            }
            else if (!runLeft)
            {
                runLeftBegin = true;
            }
        }

        private void Jump(GameTime gameTime)
        {
            // Jump logic
            if (isJump && speed.Y <= 0)
            {
                hasLanded = false;

                if (rightDir)
                {
                    if (isJumpBegin)
                    {
                        jumpTimer = 0;
                        currentFrame = 16;
                        canMoveRight = false;
                        canMoveLeft = false;
                        sourceRectangle = animateRectangle[currentFrame];
                        isJumpBegin = false;
                    }

                    if (jumpTimer > jumpTimerThreshold)
                    {
                        canMoveRight = true;
                        canMoveLeft = true;

                        if (canJump)
                        {
                            canJump = false;
                            speed.Y = -jumpHeight;
                        }

                        if (speed.Y <= -jumpHeight / 2)
                        {
                            currentFrame = 17;
                            sourceRectangle = animateRectangle[currentFrame];
                        }
                        else if (speed.Y > -jumpHeight / 2)
                        {
                            currentFrame = 18;
                            sourceRectangle = animateRectangle[currentFrame];
                        }
                    }
                    else
                    {
                        jumpTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                }
                else if (leftDir)
                {
                    if (isJumpBegin)
                    {
                        jumpTimer = 0;
                        currentFrame = 31;
                        canMoveRight = false;
                        canMoveLeft = false;
                        sourceRectangle = animateRectangle[currentFrame];
                        isJumpBegin = false;
                    }

                    if (jumpTimer > fallTimerThreshold)
                    {
                        canMoveRight = true;
                        canMoveLeft = true;

                        if (canJump)
                        {
                            canJump = false;
                            speed.Y = -jumpHeight;
                        }

                        if (speed.Y <= -jumpHeight / 2)
                        {
                            currentFrame = 30;
                            sourceRectangle = animateRectangle[currentFrame];
                        }
                        else if (speed.Y > -jumpHeight / 2)
                        {
                            currentFrame = 29;
                            sourceRectangle = animateRectangle[currentFrame];
                        }
                    }
                    else
                    {
                        jumpTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                }
            }
        }
    }
}
