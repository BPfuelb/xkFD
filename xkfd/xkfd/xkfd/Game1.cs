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
        enum Gamestate { running, menue };
        Gamestate gamestate = Gamestate.running;

        // Spieler
        Spieler spieler;

        // Schriftart
        SpriteFont schrift;

        // Menü 
        Menue menue;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Spieler Initialisierung
            spieler = new Spieler(this);

            // Menü Initialisierung
            menue = new Menue();

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
            menue.optionenTexture = Content.Load<Texture2D>("m_optionen");
            menue.exitTexture = Content.Load<Texture2D>("m_exit");

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
            }
            #endregion

            #region GamestateMenue
            if (gamestate == Gamestate.menue)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up)) ;

                if (Keyboard.GetState().IsKeyDown(Keys.Down)) ;

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    this.Exit();
            }
            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); // Begin

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
            
            }
            #endregion

            spriteBatch.End(); // End

            base.Draw(gameTime);
        }
    }
}
