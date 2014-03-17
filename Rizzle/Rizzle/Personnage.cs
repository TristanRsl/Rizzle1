using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rizzle
{
    public enum Direction
    {
        Up, Down, Left, Right
    }
    class Personnage
    {
        bool Animation;
        Rectangle hitBox, newHitbox;
        public Rectangle select;
        int Speed, FrameLine, Timer, AnimationSpeed = 6, FrameColumn;
        Direction direction;
        SpriteEffects Effect;

        public Personnage()
        {
            FrameColumn = 2;
            Animation = true;
            Timer = 0;
            hitBox = new Rectangle(600, 300, 30, 30);
            select = new Rectangle(hitBox.X, hitBox.Y+30, 30, 30);
            Speed = 5;
            direction = Direction.Down;
            FrameLine = 1;
        }

        public void Animate(int nbFrame)
        {
            Timer++;
            if (Timer == AnimationSpeed)
            {
                Timer = 0;
                if (Animation)
                {
                    FrameColumn++;
                    if (FrameColumn > nbFrame)
                    {
                        FrameColumn -= 1;
                        Animation = false;
                    }
                }
                else
                {
                    FrameColumn--;
                    if (FrameColumn < 1)
                    {
                        FrameColumn = 2;
                        Animation = true;
                    }
                }
            }
        }

        public int getCell(int x)
        {
            int k = x / 30;
            return k * 30;
        }

        public void Update(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Up))
            {
                if (hitBox.Y + Speed > 0)
                {
                    newHitbox = new Rectangle(hitBox.X, hitBox.Y - Speed, hitBox.Width, hitBox.Height);
                    hitBox.Y -= Speed;
                }
                int cell = getCell(newHitbox.X + 15);
                int cell2 = getCell(newHitbox.Y);
                select = new Rectangle(cell, cell2 - 30, 30, 30);
                direction = Direction.Up;
                Effect = SpriteEffects.None;
                Animate(3);
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                if (hitBox.Y + Speed + 30 < 600)
                {
                    newHitbox = new Rectangle(hitBox.X, hitBox.Y + Speed, hitBox.Width, hitBox.Height);
                    hitBox.Y += Speed;
                }
                int cell = getCell(newHitbox.X + 15);
                int cell2 = getCell(newHitbox.Y);
                select = new Rectangle(cell, cell2 + 30, 30, 30);
                direction = Direction.Down;
                Animate(3);
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                if (hitBox.X + Speed + 30 < 1200)
                {
                    newHitbox = new Rectangle(hitBox.X + Speed, hitBox.Y, hitBox.Width, hitBox.Height);
                    hitBox.X += Speed;
                }
                int cell = getCell(newHitbox.X );
                int cell2 = getCell(newHitbox.Y + 15);
                select = new Rectangle(cell + 30, cell2, 30, 30);
                direction = Direction.Right;
                Animate(3);
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                if (hitBox.X + Speed > 0)
                {
                    newHitbox = new Rectangle(hitBox.X - Speed, hitBox.Y, hitBox.Width, hitBox.Height);
                    hitBox.X -= Speed;
                }
                int cell = getCell(newHitbox.X);
                int cell2 = getCell(newHitbox.Y + 15);
                select = new Rectangle(cell - 30, cell2, 30, 30);
                direction = Direction.Left;
                Animate(3);
            }

            if (keyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyUp(Keys.Down) && keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right))
            {
                FrameColumn = 2;
                Timer = 0;
            }

            switch (direction)
            {
                case Direction.Up: FrameLine = 2;
                    Effect = SpriteEffects.None;
                    break;
                case Direction.Down: FrameLine = 1;
                    Effect = SpriteEffects.None;
                    break;
                case Direction.Right: FrameLine = 3;
                    Effect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.Left: FrameLine = 3;
                    Effect = SpriteEffects.None;
                    break;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.path, hitBox, new Rectangle(0, 0, 30, 30), Color.White, 0f, new Vector2(0), Effect, 0f);
            spriteBatch.Draw(Ressources.path, select, Color.White);
        }
    }
}
