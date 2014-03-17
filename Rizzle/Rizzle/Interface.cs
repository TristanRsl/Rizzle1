using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rizzle
{
    class Interface
    {
        public Rectangle basicT = new Rectangle(600,500,50,50), powerT = new Rectangle(0,0,0,0);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.buttonTowerBasic, basicT, Color.White);
        }
    }
}
