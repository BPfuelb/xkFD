using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace xkfd
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Spiel Status
        enum Gamestate { running, menue, options };
        Gamestate gamestate = Gamestate.menue;

        // Spieler
        Spieler spieler;

        // Schriftart
        SpriteFont schrift;

        // Menü 
        Menue menue;
        KeyboardState OldKeyState;

        // Hintergrund
        Hintergrund hintergrund;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Spieler Initialisierung
            spieler = new Spieler();

            // Menü Initialisierung
            menue = new Menue();

            // Hintergrund Initialisierung
            hintergrund = new Hintergrund();

        }

        protected override void Initialize()
        {
            // Auflösung
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialisierung der Test Textur für den Spieler
            spieler.spielerTextur = Content.Load<Texture2D>("spieler_textur");

            // Lade Schirftart
            schrift = Content.Load<SpriteFont>("SpriteFont1");

            // Initialisierung der Animationen 
            /* 
            spieler.laufen.animationTexture = Content.Load<Texture2D>("animation_laufen");
            spieler.springen.animationTexture = Content.Load<Texture2D>("animation_springen");
            spieler.ducken.animationTexture = Content.Load<Texture2D>("animation_ducken");
            spieler.gleiten.animationTexture = Content.Load<Texture2D>("animation_gleiten");
            spieler.sterben.animationTexture = Content.Load<Texture2D>("animation_sterben");
            spieler.gewinnen.animationTexture = Content.Load<Texture2D>("animation_gewinnen");
            spieler.fallen.animationTexture = Content.Load<Texture2D>("animation_gewinnen");
             */

            // Initialisiere Menü Animationen
            menue.startTextur = Content.Load<Texture2D>("m_start");
            menue.neuTexture = Content.Load<Texture2D>("m_new");
            menue.fortsetzenTexture = Content.Load<Texture2D>("m_continue");
            menue.optionenTexture = Content.Load<Texture2D>("m_optionen");
            menue.exitTexture = Content.Load<Texture2D>("m_exit");


            // Menü Hintergrund
            hintergrund.hintergrundTextur = Content.Load<Texture2D>("hintergrund");
        }


        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();




            #region GamestateRunning

            if (gamestate == Gamestate.running)
            {
                // Leertaste zum Springen
                if (Keyboard.GetState().IsKeyDown(Keys.Space)) spieler.doSpringen();


                // Update spieler
                //  if (gameTime.TotalGameTime.Milliseconds % 1 == 0)
                spieler.Update();


                // Escape ins Menü zurück kehren
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) && OldKeyState.IsKeyUp(Keys.Escape))
                    gamestate = Gamestate.menue;

            }
            #endregion

            #region GamestateMenue

            if (gamestate == Gamestate.menue)
            {

                if (menue.start_m_ani == null) menue.start_m_ani = new Animation(menue.startTextur, 1, 4, 6);
                if (menue.option_m_ani == null) menue.option_m_ani = new Animation(menue.optionenTexture, 1, 4, 6);
                if (menue.exit_m_ani == null) menue.exit_m_ani = new Animation(menue.exitTexture, 1, 4, 6);
                if (menue.neu_m_ani == null) menue.neu_m_ani = new Animation(menue.neuTexture, 1, 4, 6);
                if (menue.fortsetzen_m_ani == null) menue.fortsetzen_m_ani = new Animation(menue.fortsetzenTexture, 1, 4, 6);


                // Auswahl im Menü ber Tastatur (Pfeiltasten)
                KeyboardState NewKeyState = Keyboard.GetState();

                if (Keyboard.GetState().IsKeyDown(Keys.Up) && OldKeyState.IsKeyUp(Keys.Up))
                    menue.prevMenue();

                if (Keyboard.GetState().IsKeyDown(Keys.Down) && OldKeyState.IsKeyUp(Keys.Down))
                    menue.nextMenue();

                if (Keyboard.GetState().IsKeyDown(Keys.Right) && OldKeyState.IsKeyUp(Keys.Right))
                    menue.rightMenue();

                if (Keyboard.GetState().IsKeyDown(Keys.Left) && OldKeyState.IsKeyUp(Keys.Left))
                    menue.leftMenue();


                OldKeyState = NewKeyState;

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    if (menue.auswahl == 0)
                    {
                        gamestate = Gamestate.running;
                        menue.spielAktiv = true;
                    }

                    if (menue.auswahl == 1)
                    {
                        if (!menue.spielAktiv)
                            gamestate = Gamestate.options;
                        else
                        {
                            gamestate = Gamestate.running;
                            spieler.doLaufen();
                            spieler.position.X = 1280 / 2 - 128;
                            spieler.position.Y = 720 / 2;
                        }
                    }

                    if (menue.auswahl == 2)
                    {
                        if(!menue.spielAktiv)
                            this.Exit();
                        else
                            gamestate = Gamestate.options;

                    }
                      if (menue.auswahl == 3)
                    {
                        if(!menue.spielAktiv)
                            this.Exit();
                    }
                }

                menue.Update();
            }
            #endregion


            #region GamestateOptions

            if (gamestate == Gamestate.options)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    gamestate = Gamestate.menue;
            }

            #endregion

            // Weiterschieben des Hintergrunds
            hintergrund.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); // Begin

            // Hintergunrd zeichnen
            spriteBatch.Draw(hintergrund.hintergrundTextur, hintergrund.hintegrundPosition, Color.White);

            #region GamestateRunning

            if (gamestate == Gamestate.running)
            {
                // Zeichne Spieler
                spriteBatch.Draw(spieler.spielerTextur, spieler.position, Color.White);

            }
            #endregion

            #region GamestateMenue
            if (gamestate == Gamestate.menue)
            {
                // Malt alle Animationen des Menüs
                menue.Draw(spriteBatch);

            }
            #endregion

            #region GamestateOptions

            #endregion

            spriteBatch.End(); // End

            base.Draw(gameTime);
        }
    }
}
