using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rizzle
{
    class Ressources
    {
        public static Texture2D path, grass, towerPower, towerBasic, basicEnnemi, fastEnnemi, armorEnnemi, buttonTowerBasic, arbre;

        public static void LoadContent(ContentManager content)
        {
            path = content.Load<Texture2D>("path");
            grass = content.Load<Texture2D>("grass");
            basicEnnemi = content.Load<Texture2D>("ennemi basic");
            fastEnnemi = content.Load<Texture2D>("grass");
            armorEnnemi = content.Load<Texture2D>("grass");
            buttonTowerBasic = content.Load<Texture2D>("path");
            towerBasic = content.Load<Texture2D>("basicTower");
            towerPower = content.Load<Texture2D>("grass");
            arbre = content.Load<Texture2D>("arbre");
        }

        public static void UnloadContent()
        {
            path.Dispose();
            grass.Dispose();
            basicEnnemi.Dispose();
            fastEnnemi.Dispose();
            armorEnnemi.Dispose();
            buttonTowerBasic.Dispose();
            arbre.Dispose();

            path = null;
            grass = null;
            basicEnnemi = null;
            fastEnnemi = null;
            armorEnnemi = null;
            buttonTowerBasic = null;
            arbre = null;
        }
    }
}
