using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Rizzle
{
    class Map
    {
        public static int screenWidth = 1200, screenHeight = 600;
        static int[,] carte = new int[screenHeight / 30, screenWidth / 30];
        private List<Texture2D> tileTextures = new List<Texture2D>();
        static Random rand = new Random();
        TowerList tower = new TowerList();
        Personnage personnage = new Personnage();
        EnnemiList ennemi = new EnnemiList();
        Interface inter = new Interface();
        MouseState oldmouse, mouse;
        public List<Tuple<int, int>> Nature = new List<Tuple<int, int>>();


        


        


        public void Update(GameTime gameTime)
        {
            KeyboardState key = Keyboard.GetState();
            mouse = Mouse.GetState();
            Rectangle mouseBox = new Rectangle(mouse.X, mouse.Y, 10, 10);
            personnage.Update(key);
            if (key.IsKeyDown(Keys.Space))
            {
                ennemi.AddEnnemi(typeEnnemi.basicEnnemi, 0, 300);
            }
            foreach (Ennemi enn in ennemi)
                enn.pathEnnemi(carte, enn);
            if (mouseBox.Intersects(inter.basicT) & mouse.LeftButton == ButtonState.Pressed & oldmouse.LeftButton == ButtonState.Released)
            {
                if (carte[personnage.select.Y / 30,personnage.select.X / 30] == 0)
                tower.AddTower(typeTower.basicTower, personnage.select.X, personnage.select.Y);
            }
            oldmouse = mouse;
        }

        public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }

        public void Draw(SpriteBatch spriteBatch)
        {


            AddTexture(Ressources.grass);
            AddTexture(Ressources.path);
            AddTexture(Ressources.arbre);
            Texture2D arbre = tileTextures[2];

            for (int x = 0; x < carte.GetLength(1); x++)
            {
                for (int y = 0; y < carte.GetLength(0); y++)
                {
                    int textureIndex = carte[y, x];
                    if (textureIndex == -1)
                        continue;
                    Texture2D texture = tileTextures[textureIndex];
                    spriteBatch.Draw(texture, new Rectangle(x * 30, y*30, 30, 30), Color.White);
                }
            }

            spriteBatch.Draw(arbre, new Rectangle(10, 15, 92, 98), Color.White);
            personnage.Draw(spriteBatch);
            foreach (Ennemi enn in ennemi)
                enn.Draw(spriteBatch, enn);
            inter.Draw(spriteBatch);
            foreach (Tower tour in tower)
                tour.Draw(spriteBatch);
        }
        
    }
   
}
