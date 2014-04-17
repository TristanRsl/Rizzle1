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
        Rectangle hitBox, newHitbox;
        public Rectangle select;
        int Speed, Timer, TimerSelect, AnimationSpeed = 7, FrameColumn, FrameLine, FrameColumnSelect;
        Direction direction;
        SpriteEffects Effect;

        public Personnage()
        {
            FrameColumn = 1;
            FrameLine = 3;
            FrameColumnSelect = 1;
            Timer = 0;
            TimerSelect = 0;
            hitBox = new Rectangle(600, 300, 40, 40);
            select = new Rectangle(hitBox.X, hitBox.Y + 30, 30, 30);
            Speed = 5;
            direction = Direction.Down;
        }

        public void Animate(int nbFrame)
        {
            Timer++;
            if (Timer == AnimationSpeed)
            {
                Timer = 0;
                    FrameColumn++;
                    if (FrameColumn > nbFrame)
                    {
                        FrameColumn = 1;
                    }
            }
        }

        public void AnimateSelect(int nbFrame)
        {
            TimerSelect++;
            if (TimerSelect == AnimationSpeed)
            {
                TimerSelect = 0;
                FrameColumnSelect++;
                if (FrameColumnSelect > nbFrame)
                {
                    FrameColumnSelect = 1;
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
            AnimateSelect(4);
            if (keyboard.IsKeyDown(Keys.Up))
            {
                newHitbox = new Rectangle(hitBox.X, hitBox.Y - Speed, hitBox.Width, hitBox.Height);
                if (hitBox.Y + Speed - 40 > 0)
                {
                    hitBox.Y -= Speed;
                }
                int cell = getCell(newHitbox.X + (30 / 2));
                int cell2 = getCell(newHitbox.Y);
                select = new Rectangle(cell, cell2 - 30, 30, 30);
                direction = Direction.Up;
                Effect = SpriteEffects.None;
                Animate(5);
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                newHitbox = new Rectangle(hitBox.X, hitBox.Y + Speed, hitBox.Width, hitBox.Height);
                if (hitBox.Y + Speed + 70 < 600)
                {
                    hitBox.Y += Speed;
                }
                int cell = getCell(newHitbox.X + (30 / 2));
                int cell2 = getCell(newHitbox.Y);
                select = new Rectangle(cell, cell2 + 60, 30, 30);
                direction = Direction.Down;
                Animate(5);
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                newHitbox = new Rectangle(hitBox.X + Speed, hitBox.Y, hitBox.Width, hitBox.Height);
                if (hitBox.X + Speed + 65 < 1200)
                {
                    hitBox.X += Speed;
                }
                int cell = getCell(newHitbox.X);
                int cell2 = getCell(newHitbox.Y + (30 / 2));
                select = new Rectangle(cell + 60, cell2, 30, 30);
                direction = Direction.Right;
                Animate(5);
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                newHitbox = new Rectangle(hitBox.X - Speed, hitBox.Y, hitBox.Width, hitBox.Height);
                if (hitBox.X + Speed - 40 > 0)
                {
                    hitBox.X -= Speed;
                }
                int cell = getCell(newHitbox.X);
                int cell2 = getCell(newHitbox.Y + (30 / 2));
                select = new Rectangle(cell - 30, cell2, 30, 30);
                direction = Direction.Left;
                Animate(5);
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
                case Direction.Down: FrameLine = 3;
                    Effect = SpriteEffects.None;
                    break;
                case Direction.Right: FrameLine = 1;
                    Effect = SpriteEffects.None;
                    break;
                case Direction.Left: FrameLine = 1;
                    Effect = SpriteEffects.FlipHorizontally;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.personnage, hitBox, new Rectangle((FrameColumn - 1) * 30, (FrameLine - 1) * 30, 30, 30), Color.White, 0f, new Vector2(0), Effect, 0f);
            spriteBatch.Draw(Ressources.select, select, new Rectangle((FrameColumnSelect - 1) * 76, 0, 76, 49), Color.White);
        }
    }
}
