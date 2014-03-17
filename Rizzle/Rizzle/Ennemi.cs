using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Rizzle
{
    public enum typeEnnemi { basicEnnemi, fastEnnemi, armorEnnemi }
    class EnnemiList : List<Ennemi>
    {
        public void AddEnnemi(typeEnnemi typeEnnemi, int x, int y)
        {
            Add(new Ennemi(typeEnnemi, x, y));
        }
    }

    class Ennemi
    {
        int armor;
        float fast;
        Texture2D currentText;
        Rectangle ennemiPos;
        bool[,] marque = new bool[20, 40];

        public Ennemi(typeEnnemi typeEnnemi, int x, int y)
        {
            if (typeEnnemi == typeEnnemi.basicEnnemi)
            {
                armor = 50;
                fast = 0.10f;
                currentText = Ressources.basicEnnemi;
            }
            else if (typeEnnemi == typeEnnemi.armorEnnemi)
            {
                armor = 150;
                fast = 0.5f;
                currentText = Ressources.armorEnnemi;
            }
            else
            {
                armor = 30;
                fast = 1f;
                currentText = Ressources.fastEnnemi;
            }
            ennemiPos = new Rectangle(x, y, currentText.Width, currentText.Height);
        }

        public void pathEnnemi(int[,] mapPath, Ennemi enn)
        {
            if (enn.ennemiPos.X + 30 < 1199)
            {
                if (mapPath[(enn.ennemiPos.Y / 30) - 1, enn.ennemiPos.X / 30] == 1 & !marque[(enn.ennemiPos.Y / 30) - 1, enn.ennemiPos.X / 30]) //chemin en haut
                {
                    Rectangle neew = new Rectangle(enn.ennemiPos.X, enn.ennemiPos.Y - 30, enn.currentText.Width, enn.currentText.Height);
                    enn.marque[(enn.ennemiPos.Y / 30) - 1, enn.ennemiPos.X / 30] = true;
                    enn.ennemiPos.Y -= 30;
                }
                else if (mapPath[(enn.ennemiPos.Y / 30) + 1, enn.ennemiPos.X / 30] == 1 & !marque[(enn.ennemiPos.Y / 30) + 1, enn.ennemiPos.X / 30]) //chemin en bas
                {
                    Rectangle neew = new Rectangle(enn.ennemiPos.X, enn.ennemiPos.Y + 30, enn.currentText.Width, enn.currentText.Height);
                    enn.marque[(enn.ennemiPos.Y / 30) + 1, enn.ennemiPos.X / 30] = true;
                    enn.ennemiPos.Y += 30;
                }
                else if (mapPath[enn.ennemiPos.Y / 30, (enn.ennemiPos.X / 30) + 1] == 1 & !marque[enn.ennemiPos.Y / 30, (enn.ennemiPos.X / 30) + 1]) // chemin à droite
                {
                    Rectangle neew = new Rectangle(enn.ennemiPos.X + 30, enn.ennemiPos.Y, enn.currentText.Width, enn.currentText.Height);
                    enn.marque[enn.ennemiPos.Y / 30, (enn.ennemiPos.X / 30) + 1] = true;
                    enn.ennemiPos.X += 30;
                }
            }
            else
            {
                Rectangle neew = new Rectangle(enn.ennemiPos.X + 30, enn.ennemiPos.Y, enn.currentText.Width, enn.currentText.Height);
                enn.ennemiPos.X += 30;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Ennemi enn)
        {
            spriteBatch.Draw(enn.currentText, enn.ennemiPos, Color.White);
        }
    }
}
