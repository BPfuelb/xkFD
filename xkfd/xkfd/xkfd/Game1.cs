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

        // Optionenmenü
        Optionen optionen;

        // Sound
        Song titel;
        // SoundEffectInstance titelSoundInstance;

        // Hindernis Liste
        List<Hindernis> hindernisListe;



        // Hindernis Texturen
        Texture2D hindernisTexturS;
        Texture2D hindernisTexturA;
        Texture2D hindernisTexturB;
        Texture2D hindernisTexturC;
        Texture2D hindernisTexturD;
        Texture2D hindernisTexturZ;

        Texture2D dummyTexture;
        Texture2D dummyTexture2;


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

            // Optionen Initialisieren
            optionen = new Optionen();



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
            // spieler.spielerTextur = Content.Load<Texture2D>("spieler_textur");

            // Lade Schirftart
            schrift = Content.Load<SpriteFont>("SpriteFont1");

            // Initialisierung der Animationen 
            spieler.ducken.animationTexture = Content.Load<Texture2D>("ani_ducken_std");
            spieler.fallen.animationTexture = Content.Load<Texture2D>("ani_fallen_std");
            spieler.gewinnen.animationTexture = Content.Load<Texture2D>("ani_gewinnen_std");
            spieler.gleiten.animationTexture = Content.Load<Texture2D>("ani_gleiten_std");
            spieler.laufen.animationTexture = Content.Load<Texture2D>("ani_laufen_std");
            spieler.springen.animationTexture = Content.Load<Texture2D>("ani_springen_std");
            spieler.sterben.animationTexture = Content.Load<Texture2D>("ani_sterben_std");


            // Initialisiere Menü Animationen
            menue.startTextur = Content.Load<Texture2D>("m_start");
            menue.neuTexture = Content.Load<Texture2D>("m_new");
            menue.fortsetzenTexture = Content.Load<Texture2D>("m_continue");
            menue.optionenTexture = Content.Load<Texture2D>("m_optionen");
            menue.exitTexture = Content.Load<Texture2D>("m_exit");

            // Sound
            titel = Content.Load<Song>("titel");
            // titelSoundInstance = titel.CreateInstance();


            // Hintergrund
            hintergrund.hintergrundTextur = Content.Load<Texture2D>("hintergrund");


            // Optionen Texturen Laden

            // Optionen Zurück Knopf
            optionen.z_knopf_Textur = Content.Load<Texture2D>("o_zurueck");

            // Spring Sounds
            spieler.springen.sound = Content.Load<SoundEffect>("jump");
            spieler.springen.soundSoundInstance = spieler.springen.sound.CreateInstance();

            // Textur für Hindernisse
            hindernisTexturS = Content.Load<Texture2D>("hindernisS");
            hindernisTexturA = Content.Load<Texture2D>("hindernisA");
            hindernisTexturB = Content.Load<Texture2D>("hindernisB");
            hindernisTexturC = Content.Load<Texture2D>("hindernisC");
            hindernisTexturD = Content.Load<Texture2D>("hindernisD");
            hindernisTexturZ = Content.Load<Texture2D>("hindernisZ");

            // Liste mit Hindernisse die generiert werden
            hindernisListe = Hindernis.generieHindernisse(10, hindernisTexturS, hindernisTexturA, hindernisTexturB, hindernisTexturC, hindernisTexturD, hindernisTexturZ);


            // Test textur
            dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.Red });

            dummyTexture2 = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture2.SetData(new Color[] { Color.Green });
        }


        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            // Animation zum Laufen laden
            if (spieler.ducken.animation == null) spieler.ducken.animation = new Animation(spieler.laufen.animationTexture, 4, 3, 6);
            if (spieler.fallen.animation == null) spieler.fallen.animation = new Animation(spieler.fallen.animationTexture, 4, 3, 6);
            if (spieler.gewinnen.animation == null) spieler.gewinnen.animation = new Animation(spieler.gewinnen.animationTexture, 4, 3, 6);
            if (spieler.gleiten.animation == null) spieler.gleiten.animation = new Animation(spieler.gleiten.animationTexture, 4, 3, 6);
            if (spieler.laufen.animation == null) spieler.laufen.animation = new Animation(spieler.laufen.animationTexture, 4, 3, 3);
            if (spieler.springen.animation == null) spieler.springen.animation = new Animation(spieler.springen.animationTexture, 4, 2, 6);
            if (spieler.sterben.animation == null) spieler.sterben.animation = new Animation(spieler.sterben.animationTexture, 4, 3, 6);

            #region GamestateRunning

            if (gamestate == Gamestate.running)
            {
                
                // Der Spieler kann nur Gleiten wenn er am Fallen ist.
                if (spieler.aktuellerZustand == spieler.fallen && spieler.gleitenResource > 0)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) 
                        spieler.doGleiten();
                }

                // Der Spieler kann nur Ducken wenn er Läuft.
                
                /*
                if (spieler.aktuellerZustand == spieler.laufen)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Down)) 
                        spieler.doDucken();
                }
                else if (spieler.aktuellerZustand == spieler.ducken) // Wenn der Spieler geduckt ist kann er durch loslassen wieder Laufen.
                {
                    if (Keyboard.GetState().IsKeyUp(Keys.Down))
                        spieler.doLaufen();
                    }
                */

                // Leertaste zum Springen
                if (Keyboard.GetState().IsKeyDown(Keys.Space)) spieler.doSpringen();

                // Update spieler
                spieler.Update();

                // Aktualisiere Hindernisse (schiebe Hindernisse Weiter)
                if (hindernisListe.Count > 6 && spieler.aktuellerZustand != spieler.sterben)
                {
                    hindernisListe[0].Update();
                    hindernisListe[1].Update();
                    hindernisListe[2].Update();
                    hindernisListe[3].Update();
                    hindernisListe[4].Update();
                    hindernisListe[5].Update();
                    // Wenn das Hindernis an Position 1 Außerhalb des Bildes ist lösche das Hindernis an Position 0
                    if (hindernisListe[1].position.X <= -320)
                        hindernisListe.RemoveAt(0);
                    hintergrund.Update();
                }
                else
                    spieler.doGewinnen(); // Wenn weniger als 6 Hindernisse vorhanden sind gehe in den Gewinnenzustand über

                // Escape ins Menü zurück kehren
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) && OldKeyState.IsKeyUp(Keys.Escape))
                {
                    menue.auswahl = 2; // Auswahl auf Forsetzen bzw. Starten setzten
                    gamestate = Gamestate.menue;
                }


                // Kollisionserkennung


                // Erstellen einer Liste mit Hitboxen aus Bereich 2 & 3
                List<Hitbox> kollisionsListe = new List<Hitbox>();
                foreach (Hitbox hitbox in hindernisListe[2].gibHitboxen())
                { kollisionsListe.Add(hitbox); }

                foreach (Hitbox hitbox in hindernisListe[3].gibHitboxen())
                { kollisionsListe.Add(hitbox); }


                // Boolean Werte für Kollisionserkennung (für Füße)
                Boolean kollidiert = false;

                // Boden Kollisionserkennung mit allen Hitboxen
                // Prüfe erst alle hitboxen ab ob eine Kollision entstanden ist
                // siehe boolean kollidiert
                foreach (Hitbox hitbox in kollisionsListe)
                {
                    if (spieler.hitboxFussLinks.Intersects(hitbox.hitbox) )
                    {
                        kollidiert = true;
                        if (Keyboard.GetState().IsKeyDown(Keys.Down))
                            spieler.doDucken();
                        else 
                            spieler.doLaufen();
                        spieler.setPlayerPosition(hitbox.hitbox.Y - 160);
                    }
                }

                // wenn keine Kollision ist und er spieler nicht gerade am springen ist dann falle.
                if (!kollidiert && spieler.aktuellerZustand != spieler.springen)
                    {
                        spieler.doFallen();
                    }


                // Kopf Kollisionserkennung
                foreach (Hitbox hitbox in kollisionsListe)
                {
                    if (spieler.hitboxKopf.Intersects(hitbox.hitbox))
                    {
                        spieler.doSterben();
                    }
                }
            }



            if (spieler.position.Y >= 720)
                spieler.doSterben();


            #endregion

            #region GamestateMenue

            if (gamestate == Gamestate.menue)
            {
                // Animationen erstellen
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

                // Auswahl ausführen
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    switch (menue.auswahl)
                    {
                        case 0: // Beenden
                            this.Exit();
                            break;
                        case 1: // Optionen
                            gamestate = Gamestate.options;
                            break;
                        case 2: // Starten/Fortsetzen
                            gamestate = Gamestate.running;
                            menue.spielAktiv = true;
                            break;
                        case 3: // Spieler zurücksetzen (TODO)
                            gamestate = Gamestate.running;
                            spieler.doLaufen();
                            spieler.setZustand(spieler.laufen);
                            spieler.position.X = 1280 / 2 - 128;
                            spieler.position.Y = 720 / 2;
                            break;
                    }

                }

                menue.Update();
            }
            #endregion

            #region GamestateOptions

            // Wieder ins Haupmenü zurück
            if (gamestate == Gamestate.options)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    gamestate = Gamestate.menue;
            }

            #endregion

            // Weiterschieben des Hintergrunds


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
                spieler.Draw(spriteBatch);
                // spriteBatch.Draw(spieler.spielerTextur, spieler.position, Color.White);


                // Zeichne Hitboxen der Hindernisse
                foreach (Hindernis hindernis in hindernisListe)
                {
                    foreach (Hitbox hitbox in hindernis.gibHitboxen())
                    {
                        spriteBatch.Draw(dummyTexture, hitbox.hitbox, Color.Red);
                    }
                }


                // if (hindernisListe[1] != null)
                spriteBatch.Draw(hindernisListe[1].hindernisTextur, hindernisListe[1].position, Color.White);
                // if (hindernisListe[2] != null)
                spriteBatch.Draw(hindernisListe[2].hindernisTextur, hindernisListe[2].position, Color.White);
                // if (hindernisListe[3] != null)
                spriteBatch.Draw(hindernisListe[3].hindernisTextur, hindernisListe[3].position, Color.White);
                // if (hindernisListe[4] != null)
                spriteBatch.Draw(hindernisListe[4].hindernisTextur, hindernisListe[4].position, Color.White);
                // if (hindernisListe[5] != null)
                spriteBatch.Draw(hindernisListe[5].hindernisTextur, hindernisListe[5].position, Color.White);



                // Spieler Hitbox malen zum Testen
               //  spriteBatch.Draw(dummyTexture2, spieler.hitboxFussRechts, Color.Green);
                spriteBatch.Draw(dummyTexture2, spieler.linksOben, Color.Green);
                spriteBatch.Draw(dummyTexture2, spieler.hitboxFussLinks, Color.Green);
                /* 
                if (spieler.aktuellerZustand == spieler.ducken)
                {
                    spieler.hitboxKopf = new Rectangle((int)spieler.position.X + 100, (int)spieler.position.Y +100, 10, 40);
                    spriteBatch.Draw(dummyTexture2, spieler.hitboxKopf, Color.Green);
                }
                 * */

                // spriteBatch.Draw(dummyTexture2, spieler.hitboxKopf, Color.Blue);
                spriteBatch.Draw(dummyTexture2, spieler.hitboxKopf, Color.Blue);


                // Gleiten Ressource anzeigen
                spriteBatch.DrawString(schrift, "Gleiten: " + spieler.gleitenResource, new Vector2(128 + 50, 20), Color.Gray);
                

                // Titel sound aus
                MediaPlayer.Pause();
            }
            #endregion

            #region GamestateMenue
            if (gamestate == Gamestate.menue)
            {
                // Malt alle Animationen des Menüs
                menue.Draw(spriteBatch);

                // Titel Musik spielen
                if (MediaPlayer.State != MediaState.Playing)
                    MediaPlayer.Play(titel);

            }
            #endregion

            #region GamestateOptions

            if (gamestate == Gamestate.options)
            {
                // Malt alle Animationen des Menüs
                optionen.Draw(spriteBatch);
                spriteBatch.DrawString(schrift, "Zurück", new Vector2(128 + 50, 590), Color.Gray);
            }

            #endregion

            spriteBatch.End(); // End

            base.Draw(gameTime);
        }
    }
}
