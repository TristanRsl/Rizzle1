using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Rizzle
{
    class Bouton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color couleur = new Color(255, 255, 255, 255);

        public Vector2 taille;

        //CONSTRUCTOR
        public Bouton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;
            taille = new Vector2(230, 72);
        }

        bool down;
        public bool isClicked;

        //UPDATE
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)taille.X, (int)taille.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (mouseRectangle.Intersects(rectangle))
            {
                if (couleur.A == 255) down = false;
                if (couleur.A == 0) down = true;
                if (down) couleur.A += 5; else couleur.A -= 5;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else if (couleur.A < 255)
            {
                couleur.A += 5;
                isClicked = false;
            }
        }

        //METHOD
        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        //DRAW
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, couleur);
        }
    }
}
