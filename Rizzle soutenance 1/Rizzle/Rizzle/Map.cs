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
        #region field
        public static int screenWidth = 1200, screenHeight = 600;
        static int[,] carte = new int[screenHeight / 30, screenWidth / 30];
        private List<Texture2D> tileTextures = new List<Texture2D>();
        static Random rand = new Random();
        TowerList tower = new TowerList();
        Personnage personnage = new Personnage();
        EnnemiList ennemi = new EnnemiList();
        MouseState oldmouse, mouse;
        KeyboardState key, oldkey;
        bool[,] occupation = new bool[20, 40];
        int gold = 300, FrameColumn = 1, Timer = 0, AnimationSpeed = 6, chateauLife = 10;
        BulletQueue bulletQueue = new BulletQueue();
        Queue<Ennemi> quiAttaquer;
        Rectangle posGold = new Rectangle(600, 10, 26, 28);
        public Rectangle basicT = new Rectangle(400, 537, 70, 60), powerT = new Rectangle(800, 537, 70, 60),
            rangeT = new Rectangle(600, 537, 70, 60);
        #endregion

        public Map()
        {
            createMap();
        }

        public static int[,] createMap()
        {
            #region création de la map
            List<Tuple<int, int>> CoCarte = new List<Tuple<int, int>>(); // Créer la premiere liste de Tupple de points aléatoires
            List<Tuple<int, int>> CoCarte2 = new List<Tuple<int, int>>(); // Créer la liste de tupple avec les points intermédiaires 
            int y = 0;
            CoCarte.Add(new Tuple<int, int>(300, 0)); // Génére le premier points en j = O c'est à dire la première ligne toute à  droite
            for (int i = 0; y < screenWidth - 1; i++) //  Créer une liste de 6 tupples 
            {
                int rand_x = rand.Next(150, 450);
                y += rand.Next(61, 91);
                if (y < screenWidth - 90)
                    CoCarte.Add(new Tuple<int, int>(rand_x, y));
            }
            CoCarte.Add(new Tuple<int, int>(300, screenWidth - 1));
            for (int i = 0; i < CoCarte.Count - 1; i++) // génére la deuxieme liste de points intermédiaires .
                CoCarte2.Add(new Tuple<int, int>(CoCarte[i + 1].Item1, CoCarte[i].Item2));

            foreach (Tuple<int, int> item in CoCarte) // Mets à jours les points sur la carte
                carte[item.Item1 / 30, item.Item2 / 30] = 1;
            foreach (Tuple<int, int> item in CoCarte2) //  Mets à jour les points intermédiaires sur la carte
                carte[item.Item1 / 30, item.Item2 / 30] = 1;

            int l = 0;
            for (int j = 0; j < CoCarte.Count - 1; j++) // Differents cas lorsque le carré est en haut || ou en bas !
            {
                for (int i = 0; i < CoCarte2[l].Item1 - CoCarte[j].Item1; i++) //chemin en haut
                    carte[(CoCarte2[l].Item1 - i) / 30, CoCarte[j].Item2 / 30] = 1;
                for (int i = 0; i < CoCarte[j + 1].Item2 - CoCarte2[l].Item2; i++) //chemin à droite
                    carte[CoCarte2[l].Item1 / 30, (CoCarte2[l].Item2 + i) / 30] = 1;
                for (int i = 0; i < CoCarte[j].Item1 - CoCarte2[l].Item1; i++) //chemin en bas
                    carte[(CoCarte[j].Item1 - i) / 30, CoCarte[j].Item2 / 30] = 1;
                if (l < CoCarte2.Count)
                    l++;
            }
            return carte;
            #endregion
        }

        public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }

        public bool isInRange(Tower tour, Vector2 position)
        {
            return (Vector2.Distance(tour.Position, position) <= tour.range);
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

        public void Update(GameTime gameTime)
        {
            Animate(6);
            #region map update
            key = Keyboard.GetState();
            mouse = Mouse.GetState();
            if (chateauLife > 0)
            {
                Rectangle mouseBox = new Rectangle(mouse.X, mouse.Y, 10, 10);
                personnage.Update(key);

                if (key.IsKeyDown(Keys.Space) & oldkey.IsKeyUp(Keys.Space))
                    ennemi.AddEnnemi(typeEnnemi.basicEnnemi, 0, 300);

                foreach (Ennemi enn in ennemi)
                { 
                    if (enn.Life > 0)
                        enn.pathEnnemi(carte, enn);
                    foreach (Tower tour in tower)
                    {
                        quiAttaquer = new Queue<Ennemi>();
                        if (isInRange(tour, enn.EnnPosition) & enn.Life > 0)
                        {
                            quiAttaquer.Enqueue(enn);
                            if (bulletQueue.Count == 0)
                            {
                                bulletQueue.AddBullet(tour);
                                bulletQueue.Peek().target(quiAttaquer.Peek());
                                quiAttaquer.Peek().Life -= tour.power * 1;
                                bulletQueue.Peek().destroy(bulletQueue);
                            }
                            if (enn.Life <= 0 || !isInRange(tour, enn.EnnPosition))
                            {
                                if (enn.Life < 0)
                                    enn.Life = 0;
                                if (enn.Life == 0)
                                    gold += enn.piece;
                                quiAttaquer.Dequeue();
                            }
                        }
                    }
                    if (enn.ennemiPos.X > 1120 & enn.Life > 0)
                    {
                        chateauLife -= 1;
                        enn.Life = 0;
                    }
                }

                #region pose des tourelles
                if (mouseBox.Intersects(basicT) & mouse.LeftButton == ButtonState.Pressed & oldmouse.LeftButton == ButtonState.Released)
                {
                    if (gold >= 50 & carte[personnage.select.Y / 30, personnage.select.X / 30] == 0 & occupation[personnage.select.Y / 30,
                        personnage.select.X / 30] == false)
                    {
                        gold -= 50;
                        tower.AddTower(typeTower.basicTower, personnage.select.X, personnage.select.Y);
                        occupation[personnage.select.Y / 30, personnage.select.X / 30] = true;
                    }
                }
                if (mouseBox.Intersects(powerT) & mouse.LeftButton == ButtonState.Pressed & oldmouse.LeftButton == ButtonState.Released)
                {
                    if (gold >= 150 & carte[personnage.select.Y / 30, personnage.select.X / 30] == 0 & occupation[personnage.select.Y / 30,
                        personnage.select.X / 30] == false)
                    {
                        gold -= 150;
                        tower.AddTower(typeTower.powerTower, personnage.select.X, personnage.select.Y);
                        occupation[personnage.select.Y / 30, personnage.select.X / 30] = true;
                    }
                }
                if (mouseBox.Intersects(rangeT) & mouse.LeftButton == ButtonState.Pressed & oldmouse.LeftButton == ButtonState.Released)
                {
                    if (gold >= 100 & carte[personnage.select.Y / 30, personnage.select.X / 30] == 0 & occupation[personnage.select.Y / 30,
                        personnage.select.X / 30] == false)
                    {
                        gold -= 100;
                        tower.AddTower(typeTower.rangeTower, personnage.select.X, personnage.select.Y);
                        occupation[personnage.select.Y / 30, personnage.select.X / 30] = true;
                    }
                }
                #endregion
            }
            if (key.IsKeyDown(Keys.R) & oldkey.IsKeyUp(Keys.R))
            {
                chateauLife = 10;
                gold = 300;
                FrameColumn = 1;
                Timer = 0;
                AnimationSpeed = 6;
                bulletQueue = new BulletQueue();
                occupation = new bool[20, 40];
                tower = new TowerList();
                personnage = new Personnage();
                ennemi = new EnnemiList();
            }
            oldmouse = mouse;
            oldkey = key;
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            #region map draw
            AddTexture(Ressources.grass);
            AddTexture(Ressources.path);
            spriteBatch.Draw(Ressources.Map, new Vector2(0), Color.White);
            for (int x = 0; x < carte.GetLength(1); x++)
            {
                for (int y = 0; y < carte.GetLength(0); y++)
                {
                    int textureIndex = carte[y, x];
                    if (textureIndex == -1)
                        continue;
                    Texture2D texture = tileTextures[textureIndex];
                    spriteBatch.Draw(texture, new Rectangle(x * 30, y * 30, 30, 30), Color.White);
                }
            }
            foreach (Ennemi enn in ennemi)
            {
                Color touchEnn;
                if (enn.Life != 150)
                    touchEnn = Color.Red;
                else
                    touchEnn = Color.White;
                if (enn.Life > 0)
                    enn.Draw(spriteBatch, enn, touchEnn);
            }
            foreach (Tower tour in tower)
                tour.Draw(spriteBatch);
            personnage.Draw(spriteBatch);
            foreach (Bullet bullet in bulletQueue)
                bullet.Draw(spriteBatch);
            string vieActuelle = string.Format("{0}", chateauLife);
            spriteBatch.Draw(Ressources.vieChateau, new Rectangle(1100, 10, 50, 50), Color.White);
            spriteBatch.DrawString(Ressources.vie, vieActuelle, new Vector2(1115, 20), Color.White);
            #region gestion de la couleur
            Color colorBasic, colorRange, colorPower;
            if (gold >= 50)
                colorBasic = Color.White;
            else
                colorBasic = Color.Gray;
            if (gold >= 100)
                colorRange = Color.White;
            else
                colorRange = Color.Gray;
            if (gold >= 150)
                colorPower = Color.White;
            else
                colorPower = Color.Gray;
            #endregion
            spriteBatch.Draw(Ressources.buttonTowerBasic, basicT, colorBasic);
            spriteBatch.Draw(Ressources.buttonTowerPower, powerT, colorPower);
            spriteBatch.Draw(Ressources.buttonTowerRange, rangeT, colorRange);
            string goldDispo = string.Format("{0}", gold);
            Color color;
            if (gold != 0)
                color = Color.Yellow;
            else
                color = Color.DarkRed;
            spriteBatch.DrawString(Ressources.write, goldDispo, new Vector2(560, 10), color);
            spriteBatch.Draw(Ressources.gold, posGold, new Rectangle((FrameColumn - 1) * 26, 0, 26, 27), Color.White);
            spriteBatch.Draw(Ressources.chateau, new Vector2(1120, 250), Color.White);
            if (chateauLife == 0)
            {
                string gameOver = string.Format("Game Over");
                spriteBatch.DrawString(Ressources.gameOver, gameOver, new Vector2(300, 150), Color.White);
            }
            #endregion
        }
    }
}
