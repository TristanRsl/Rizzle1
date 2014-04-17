using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rizzle
{
    public enum typeTower { basicTower, powerTower, rangeTower }           //énumération définissant les types de tourelles
    public class TowerList : List<Tower>
    {
        public void AddTower(typeTower TypeTow, int x, int y)
        {
            Add(new Tower(TypeTow, x, y));
        }
    }
    public class Tower
    {
        public int slow, power, wTower, hTower, range, cadence;
        public Rectangle towerPos;
        public Vector2 Position;
        Texture2D currentText;

        public Tower(typeTower Type, int x, int y)
        {
            if (Type == typeTower.basicTower)               //différents attributs pour chaques tourelles
            {
                currentText = Ressources.towerBasic;
                range = 60;
                slow = 10;
                power = 6;
                wTower = 24;
                hTower = 30;
                cadence = 5;
            }
            else if (Type == typeTower.powerTower)
            {
                currentText = Ressources.towerPower;
                range = 30;
                slow = 5;
                power = 10;
                wTower = 30;
                hTower = 30;
                cadence = 3;
            }
            else
            {
                currentText = Ressources.towerRange;
                range = 120;
                slow = 5;
                power = 4;
                wTower = 30;
                hTower = 30;
                cadence = 7;
            }

            towerPos = new Rectangle(x, y, 30, 30);        //place la tourelle à la position du curseur selecteur
            Position = new Vector2(x, y);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentText, towerPos, new Rectangle(0,0,wTower,hTower), Color.White);       //dessine la tourelle
        }
    }
}
