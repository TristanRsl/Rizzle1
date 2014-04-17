using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Rizzle
{
    enum Langue
    {
        Fr,
        An,
    }
    class Menu
    {
        public enum GameState
        {
            MainMenu,
            Options,
        }

        public enum BoolButton
        {
            Jouer,
            Options,
            Quitter,
            Rien,
        }

        //FIELD

        static GameState CurrentGameState;
        static BoolButton currentBoolButton;

        static Langue CurrentLangue = Langue.Fr;

        int screenWidth = 1200, screenHeight = 600;

        Bouton btnQuit, btnJouer, btnOption;
        Texture2D Quit, Jouer, Option;

        //CONSTRUCTORS
        public Menu(GraphicsDeviceManager graphics, ContentManager Content)
        {
            CurrentGameState = GameState.MainMenu;
            currentBoolButton = BoolButton.Rien;
            Quit = Content.Load<Texture2D>("BoutonQuitter");
            Jouer = Content.Load<Texture2D>("BoutonJouer");
            Option = Content.Load<Texture2D>("BoutonOption");

            #region load btn
            btnQuit = new Bouton(Quit, graphics.GraphicsDevice);
            btnQuit.setPosition(new Vector2(600 - (Quit.Width / 2) + 2, screenHeight / 2 + 150));

            btnOption = new Bouton(Option, graphics.GraphicsDevice);
            btnOption.setPosition(new Vector2(600 - (Option.Width / 2) + 2, screenHeight / 2 + 50));

            btnJouer = new Bouton(Jouer, graphics.GraphicsDevice);
            btnJouer.setPosition(new Vector2(600 - (Jouer.Width / 2) - 10, screenHeight / 2 - 50));
            #endregion
        }

        //METHODS 

        #region GetSet
        public static Langue currentLangue
        {
            get { return CurrentLangue; }
            set { CurrentLangue = value; }
        }

        public static BoolButton CurrentBoolButton
        {
            get { return currentBoolButton; }
            set { currentBoolButton = value; }
        }

        #endregion

        //UPDATE & DRAW
        public void Update(MouseState mouse)
        {
            if (currentBoolButton == BoolButton.Quitter)
                Game1.exit = true;
            if (currentBoolButton == BoolButton.Jouer)
                Game1.jouer = true;
            if (currentBoolButton == BoolButton.Options)
                Game1.option = true;
            switch (CurrentLangue)
            {
                case Langue.Fr:
                    {
                        switch (CurrentGameState)
                        {
                            case GameState.MainMenu:
                                if (btnJouer.isClicked == true)
                                {
                                    CurrentBoolButton = BoolButton.Jouer;
                                }
                                if (btnQuit.isClicked == true)
                                {
                                    CurrentBoolButton = BoolButton.Quitter;
                                }
                                if (btnOption.isClicked == true)
                                {
                                    CurrentBoolButton = BoolButton.Options;
                                }
                                btnJouer.Update(mouse);
                                btnOption.Update(mouse);
                                btnQuit.Update(mouse);
                                break;
                        }
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ContentManager Content)
        {
            spriteBatch.Draw(Ressources.MainMenu, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            btnQuit.Draw(spriteBatch);
            btnOption.Draw(spriteBatch);
            btnJouer.Draw(spriteBatch);
        }
    }
}
