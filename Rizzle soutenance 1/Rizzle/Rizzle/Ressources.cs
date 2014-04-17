using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Rizzle
{
    class Ressources
    {
        public static Texture2D path, grass, towerPower, towerBasic, towerRange, basicEnnemi, fastEnnemi, armorEnnemi, buttonTowerBasic,
            buttonTowerPower, buttonTowerRange, MainMenu, bullet, gold, select, personnage, vieChateau, chateau, Map;
        public static SpriteFont write, vie, gameOver;
        public static Song ambiance;

        public static void LoadContent(ContentManager content)
        {
            //Picture
            path = content.Load<Texture2D>("path");
            grass = content.Load<Texture2D>("grass");
            basicEnnemi = content.Load<Texture2D>("basicEnnemi");
            fastEnnemi = content.Load<Texture2D>("grass");
            armorEnnemi = content.Load<Texture2D>("grass");
            buttonTowerBasic = content.Load<Texture2D>("bouton basic tourelle");
            buttonTowerPower = content.Load<Texture2D>("bouton power tourelle");
            buttonTowerRange = content.Load<Texture2D>("bouton range tourelle");
            towerBasic = content.Load<Texture2D>("basicTower");
            towerPower = content.Load<Texture2D>("powerTower");
            towerRange = content.Load<Texture2D>("rangeTower");
            MainMenu = content.Load<Texture2D>("MainMenu");
            bullet = content.Load<Texture2D>("bullet");
            gold = content.Load<Texture2D>("gold");
            select = content.Load<Texture2D>("select");
            personnage = content.Load<Texture2D>("player");
            vieChateau = content.Load<Texture2D>("vie");
            chateau = content.Load<Texture2D>("chateau");
            Map = content.Load<Texture2D>("Map");

            //Font
            write = content.Load<SpriteFont>("Write");
            vie = content.Load<SpriteFont>("Write");
            gameOver = content.Load<SpriteFont>("GameOver");

            //Song 
            ambiance = content.Load<Song>("01");
        }

        public static void UnloadContent()
        {
            //Picture
            path.Dispose();
            grass.Dispose();
            basicEnnemi.Dispose();
            fastEnnemi.Dispose();
            armorEnnemi.Dispose();
            buttonTowerBasic.Dispose();
            buttonTowerPower.Dispose();
            buttonTowerRange.Dispose();
            towerBasic.Dispose();
            towerPower.Dispose();
            towerRange.Dispose();
            MainMenu.Dispose();
            bullet.Dispose();
            gold.Dispose();
            select.Dispose();
            personnage.Dispose();
            vieChateau.Dispose();
            chateau.Dispose();
            Map.Dispose();

            //Song
            ambiance.Dispose();

            //Picture
            path = null;
            grass = null;
            basicEnnemi = null;
            fastEnnemi = null;
            armorEnnemi = null;
            buttonTowerBasic = null;
            buttonTowerPower = null;
            buttonTowerRange = null;
            towerBasic = null;
            towerPower = null;
            towerRange = null;
            MainMenu = null;
            bullet = null;
            gold = null;
            select = null;
            personnage = null;
            vieChateau = null;
            chateau = null;
            Map = null;

            //Song 
            ambiance = null;
        }
    }
}
