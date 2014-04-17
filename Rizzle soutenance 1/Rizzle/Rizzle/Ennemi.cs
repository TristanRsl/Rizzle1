using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Rizzle
{
    public enum typeEnnemi { basicEnnemi, fastEnnemi, armorEnnemi }
    public class EnnemiList : List<Ennemi>
    {
        public void AddEnnemi(typeEnnemi typeEnnemi, int x, int y)
        {
            Add(new Ennemi(typeEnnemi, x, y));
        }
    }

    public class Ennemi
    {
        private int life;

        public int Life
        {
            get { return life; }
            set { life = value; }
        }
        int Timer = 0, fast, FrameColumn = 1, AnimationSpeed = 2;
        public int piece;
        Texture2D currentText;
        public Rectangle ennemiPos;
        public Vector2 EnnPosition;
        bool[,] marque = new bool[20, 40];
        SpriteEffects Effect = SpriteEffects.FlipHorizontally;

        public Ennemi(typeEnnemi typeEnnemi, int x, int y)
        {
            if (typeEnnemi == typeEnnemi.basicEnnemi)
            {
                life = 150;
                fast = 2;
                piece = 5;
                currentText = Ressources.basicEnnemi;
            }
            else if (typeEnnemi == typeEnnemi.armorEnnemi)
            {
                life = 200;
                fast = 1;
                piece = 20;
                currentText = Ressources.armorEnnemi;
            }
            else
            {
                life = 100;
                fast = 3;
                piece = 10;
                currentText = Ressources.fastEnnemi;
            }
            ennemiPos = new Rectangle(x, y, 30, 30);
            EnnPosition = new Vector2(x, y);
            for (int i = 0; i < marque.GetLength(0); i++)
                for (int j = 0; j < marque.GetLength(1); j++)
                    marque[i, j] = false;
            marque[10, 0] = true;
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

        public void avance(int direction, Ennemi enn)
        {
            if (direction == 1)
                ennemiPos.X += enn.fast;
            else if (direction == 2)
                ennemiPos.Y += enn.fast;
            else
                ennemiPos.Y -= enn.fast;
        }

        public int pathVerify(int[,] mapPath)
        {
            if (mapPath[ennemiPos.Y / 30, (ennemiPos.X / 30) + 1] != 0 & !marque[ennemiPos.Y / 30,
                (ennemiPos.X / 30) + 1])
                return 1;
            else if (mapPath[(ennemiPos.Y / 30) + 1, ennemiPos.X / 30] != 0 & !marque[(ennemiPos.Y / 30) + 1,
                    ennemiPos.X / 30])
                return 2;
            else if (mapPath[(ennemiPos.Y / 30) - 1, ennemiPos.X / 30] != 0 & !marque[(ennemiPos.Y / 30) - 1,
                 ennemiPos.X / 30])
                return 3;
            return 0;
        }

        int avancement = 0;
        int direction;
        public void pathEnnemi(int[,] mapPath, Ennemi enn)
        {
            if (avancement == 0)
                direction = pathVerify(mapPath);
            if (avancement == 30 / enn.fast)
            {
                if (direction == 1)
                    marque[enn.ennemiPos.Y / 30, (enn.ennemiPos.X / 30) - 1] = true;
                else if (direction == 2)
                    marque[(enn.ennemiPos.Y / 30) - 1, enn.ennemiPos.X / 30] = true;
                else if (direction == 3)
                    marque[(enn.ennemiPos.Y / 30) + 1, enn.ennemiPos.X / 30] = true;
                direction = pathVerify(mapPath);
                avancement = 0;
            }
            avance(direction, enn);
            avancement += 1;
            EnnPosition = new Vector2(enn.ennemiPos.X, enn.ennemiPos.Y);
            Animate(5);
        }

        public void Draw(SpriteBatch spriteBatch, Ennemi enn, Color color)
        {
            spriteBatch.Draw(enn.currentText, enn.ennemiPos, new Rectangle((FrameColumn - 1) * 30, 0, 30, 30), color, 0f, new Vector2(0), Effect, 1f);
        }
    }
}
