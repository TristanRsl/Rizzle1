using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rizzle
{
    public enum typeTower { basicTower, powerfullTower }           //énumération définissant les types de tourelles
    public class TowerList : List<Tower>
    {
        public void AddTower(typeTower TypeTow, int x, int y)
        {
            Add(new Tower(TypeTow, x, y));
        }
    }
    public class Tower
    {
        int slow, power, wTower, hTower;
        float range;
        Rectangle towerPos;
        Texture2D currentText;

        public Tower(typeTower Type, int x, int y)
        {
            if (Type == typeTower.basicTower)               //différents attributs pour chaques tourelles
            {
                currentText = Ressources.towerBasic;
                range = 100 * Convert.ToInt32(Math.PI);
                slow = 10;
                power = 100;
                wTower = 30;
                hTower = 30;
            }
            else
            {
                currentText = Ressources.towerPower;
                range = 75 * Convert.ToInt32(Math.PI);
                slow = 5;
                power = 250;
                wTower = 30;
                hTower = 30;
            }

            towerPos = new Rectangle(x, y, wTower, hTower);        //place la tourelle à la position du curseur de la souris
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentText, towerPos, new Rectangle(0,0,60,50), Color.White);       //dessine la tourelle
        }
    }
}
