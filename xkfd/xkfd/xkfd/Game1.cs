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

        // Debug
        Boolean debug = false;

        // Spiel Status
        enum Gamestate { running, menue, options, ladebildschirm };
        Gamestate gamestate = Gamestate.menue;

        // Spieler
        Spieler spieler;

        // Schriftart
        SpriteFont schrift_40;
        SpriteFont schrift_20;

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

        // Hud
        Hud hud;

        // Tutorial starten
        Boolean start = true;


        KeyboardState NewKeyState;

        // Hindernis Texturen
        Texture2D hindernisTexturS;
        Texture2D hindernisTexturA;
        Texture2D hindernisTexturB;
        Texture2D hindernisTexturC;
        Texture2D hindernisTexturD;
        Texture2D hindernisTexturZ;

        // Hud Texturen
        Texture2D hudTextur;
        Texture2D dummyTexture;
        Texture2D dummyTexture2;

        // Skins
        Skin standardSkin, frauenSkin, hutSkin, einsteinSkin;

        // Logo
        Texture2D logo;

        // Konfig Datei

        KonfigDatei konfig;

        int gewonnen;

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

            // Konfig Datei;
            konfig = new KonfigDatei();
            gewonnen = int.Parse(konfig.ReadFile());
        }

        protected override void Initialize()
        {
            // Auflösung
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            base.Initialize();

            konfig.ReadFile();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Lade Schirftart
            schrift_40 = Content.Load<SpriteFont>("SpriteFont1");
            schrift_20 = Content.Load<SpriteFont>("SpriteFont2");

            #region SkinsInitialisieren
            // Skin initialisieren

            // Standard Skin
            standardSkin = new Skin();
            standardSkin.duckenTextur = Content.Load<Texture2D>("ani_ducken_std");
            standardSkin.fallenTextur = Content.Load<Texture2D>("ani_fallen_std");
            standardSkin.gewinnenTextur = Content.Load<Texture2D>("ani_gewinnen_std");
            standardSkin.gleitenTextur = Content.Load<Texture2D>("ani_gleiten_std");
            standardSkin.laufenTextur = Content.Load<Texture2D>("ani_laufen_std");
            standardSkin.sprignenTextur = Content.Load<Texture2D>("ani_springen_std");

            standardSkin.sterbenTexturKoepfen = Content.Load<Texture2D>("ani_sterben1_koepfen_std");
            standardSkin.sterbenTexturStolpern = Content.Load<Texture2D>("ani_sterben2_stolpern_std");
            standardSkin.sterbenTexturKlatscher = Content.Load<Texture2D>("ani_sterben3_klatscher_std");



            // Frauen Skin
            frauenSkin = new Skin();

            frauenSkin.duckenTextur = Content.Load<Texture2D>("ani_ducken_frau");
            frauenSkin.fallenTextur = Content.Load<Texture2D>("ani_fallen_frau");
            frauenSkin.gewinnenTextur = Content.Load<Texture2D>("ani_gewinnen_frau");
            frauenSkin.gleitenTextur = Content.Load<Texture2D>("ani_gleiten_frau");
            frauenSkin.laufenTextur = Content.Load<Texture2D>("ani_laufen_frau");
            frauenSkin.sprignenTextur = Content.Load<Texture2D>("ani_springen_frau");

            frauenSkin.sterbenTexturKoepfen = Content.Load<Texture2D>("ani_sterben1_koepfen_frau");
            frauenSkin.sterbenTexturStolpern = Content.Load<Texture2D>("ani_sterben2_stolpern_frau");
            frauenSkin.sterbenTexturKlatscher = Content.Load<Texture2D>("ani_sterben3_klatscher_frau");

            // Hut Skin
            hutSkin = new Skin();

            hutSkin.duckenTextur = Content.Load<Texture2D>("ani_ducken_std");
            hutSkin.fallenTextur = Content.Load<Texture2D>("ani_fallen_std");
            hutSkin.gewinnenTextur = Content.Load<Texture2D>("ani_gewinnen_std");
            hutSkin.gleitenTextur = Content.Load<Texture2D>("ani_gleiten_std");
            hutSkin.laufenTextur = Content.Load<Texture2D>("ani_laufen_std");
            hutSkin.sprignenTextur = Content.Load<Texture2D>("ani_springen_std");

            hutSkin.sterbenTexturKoepfen = Content.Load<Texture2D>("ani_sterben1_koepfen_std");
            hutSkin.sterbenTexturStolpern = Content.Load<Texture2D>("ani_sterben2_stolpern_std");
            hutSkin.sterbenTexturKlatscher = Content.Load<Texture2D>("ani_sterben2_stolpern_std");

            // Einstein Skin
            einsteinSkin = new Skin();

            einsteinSkin.duckenTextur = Content.Load<Texture2D>("ani_ducken_std");
            einsteinSkin.fallenTextur = Content.Load<Texture2D>("ani_fallen_std");
            einsteinSkin.gewinnenTextur = Content.Load<Texture2D>("ani_gewinnen_std");
            einsteinSkin.gleitenTextur = Content.Load<Texture2D>("ani_gleiten_std");
            einsteinSkin.laufenTextur = Content.Load<Texture2D>("ani_laufen_std");
            einsteinSkin.sprignenTextur = Content.Load<Texture2D>("ani_springen_std");

            einsteinSkin.sterbenTexturKoepfen = Content.Load<Texture2D>("ani_sterben1_koepfen_std");
            einsteinSkin.sterbenTexturStolpern = Content.Load<Texture2D>("ani_sterben2_stolpern_std");
            einsteinSkin.sterbenTexturKlatscher = Content.Load<Texture2D>("ani_sterben2_stolpern_std");

            #endregion

            // Skin Auswahl in Optionen hinzufügen
            optionen.skinHinzufuegen(standardSkin);
            optionen.skinHinzufuegen(frauenSkin);
            optionen.skinHinzufuegen(hutSkin);
            optionen.skinHinzufuegen(einsteinSkin);


            // Setzte standard Skin (evtl Datei auslesen)
            spieler.aktuellerSkin = optionen.skinListe[optionen.auswahl];

            // Sterben animationen
            // ((Sterben)spieler.sterben).koepfen.textur = Content.Load<Texture2D>("ani_sterben1_koepfen_std");
            // ((Sterben)spieler.sterben).dagegen.textur = Content.Load<Texture2D>("ani_sterben2_stolpern_std");

            // Initialisiere Menü Animationen
            menue.startTextur = Content.Load<Texture2D>("m_start");
            menue.neuTexture = Content.Load<Texture2D>("m_new");
            menue.fortsetzenTexture = Content.Load<Texture2D>("m_continue");
            menue.optionenTexture = Content.Load<Texture2D>("m_optionen");
            menue.exitTexture = Content.Load<Texture2D>("m_exit");

            menue.radioTexture = Content.Load<Texture2D>("radio");

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

            // Sterben Sounds

            // Sterbe Sound beim Köpfen
            ((Sterben)spieler.sterben).koepfen.soundTod = Content.Load<SoundEffect>("dsslop");
            ((Sterben)spieler.sterben).koepfen.soundSoundInstance = ((Sterben)spieler.sterben).koepfen.soundTod.CreateInstance();

            // Sterbe Sound beim dagegen laufen
            ((Sterben)spieler.sterben).stolpern.soundTod = Content.Load<SoundEffect>("dsskedth");
            ((Sterben)spieler.sterben).stolpern.soundSoundInstance = ((Sterben)spieler.sterben).stolpern.soundTod.CreateInstance();

            // Sterben Sound beim klatschen
            ((Sterben)spieler.sterben).klatscher.soundTod = Content.Load<SoundEffect>("dsskedth");
            ((Sterben)spieler.sterben).klatscher.soundSoundInstance = ((Sterben)spieler.sterben).klatscher.soundTod.CreateInstance();

            // Textur für Hindernisse
            hindernisTexturS = Content.Load<Texture2D>("hindernisS");
            hindernisTexturA = Content.Load<Texture2D>("hindernisA");
            hindernisTexturB = Content.Load<Texture2D>("hindernisB");
            hindernisTexturC = Content.Load<Texture2D>("hindernisC");
            hindernisTexturD = Content.Load<Texture2D>("hindernisD");
            hindernisTexturZ = Content.Load<Texture2D>("hindernisZ");

            // Liste mit Hindernisse die generiert werden
            hindernisListe = Hindernis.generieHindernisse(10, hindernisTexturS, hindernisTexturA, hindernisTexturB, hindernisTexturC, hindernisTexturD, hindernisTexturZ);


            // HudTextur
            hudTextur = new Texture2D(GraphicsDevice, 1, 1);
            hudTextur.SetData(new Color[] { Color.Gray });


            // Logo
            logo = Content.Load<Texture2D>("logo");

            // Hud Initialisieren
            hud = new Hud(spieler, hudTextur);

            hud.teleport = Content.Load<Texture2D>("teleport");
            hud.checkBox_check = Content.Load<Texture2D>("checkbox_check");
            hud.checkBox_uncheck = Content.Load<Texture2D>("checkbox_uncheck");
            hud.tastaturTextur = Content.Load<Texture2D>("ani_tastatur");

            hud.skin_frau = Content.Load<Texture2D>("unlock_skin_frau");
            hud.skin_hut = Content.Load<Texture2D>("unlock_skin_hut");
            hud.skin_einstein = Content.Load<Texture2D>("unlock_skin_einstein");


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
            loadAnimation();

            #region GamestateLoading
            if (gamestate == Gamestate.ladebildschirm)
            {


                if (start)
                {
                    spieler.position.X = -20;
                    start = false;
                }

                if (spieler.position.X <= 512)
                {
                    spieler.position.X++;
                    hintergrund.Update(2);
                }
                else
                {
                    hintergrund.Update();
                }
                spieler.Update();

                // nur die Tastenbelegung im Hud Updaten
                hud.UpdateHelp();

                NewKeyState = Keyboard.GetState();

                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && OldKeyState.IsKeyUp(Keys.Enter))
                {
                    spieler.position.X = 512;
                    start = true;
                    gamestate = Gamestate.running;
                }
                OldKeyState = NewKeyState;
            }

            #endregion

            #region GamestateRunning

            if (gamestate == Gamestate.running)
            {
                // Hud Update
                hud.Update();

                // Teleport bei Curser Taste nach Oben
                if (spieler.teleport == true && Keyboard.GetState().IsKeyDown(Keys.Up) && spieler.aktuellerZustand != spieler.sterben && spieler.aktuellerZustand != spieler.gewinnen)
                {
                    spieler.teleport = false; // Teleportresource Verbrauchen
                    ((Fallen)spieler.fallen).beschleunigung = 0;
                    spieler.setPlayerPosition(10);
                }

                // Leertaste zum Springen
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    spieler.doSpringen();

                // Wenn Gleiten und Leertaste loslassen dann Fallen
                if (spieler.aktuellerZustand == spieler.gleiten && Keyboard.GetState().IsKeyUp(Keys.Space))
                    spieler.doFallen();


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
                {
                    spieler.doGewinnen(); // Wenn weniger als 6 Hindernisse vorhanden sind gehe in den Gewinnenzustand über
                    if (menue.spielAktiv)
                    {
                        menue.spielAktiv = false;
                        gewonnen++;
                        hud.gewonnen = true;
                        konfig.WriteFile(gewonnen.ToString());
                    }

                }

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
                    if (spieler.hitboxFuss.Intersects(hitbox.hitbox))
                    {
                        kollidiert = true;
                        // Spieler kann sich nur ducken wenn er Läuft
                        if (Keyboard.GetState().IsKeyDown(Keys.Down) && spieler.aktuellerZustand != spieler.fallen)
                            spieler.doDucken();
                        else
                            spieler.doLaufen();
                        spieler.setPlayerPosition(hitbox.hitbox.Y - 110);
                    }
                }

                // wenn keine Kollision ist und er spieler nicht gerade am springen ist dann falle.
                if (!kollidiert && spieler.aktuellerZustand != spieler.springen && spieler.aktuellerZustand != spieler.gleiten)
                {
                    spieler.doFallen();
                }


                // Kopf Kollisionserkennung
                if (menue.spielAktiv && spieler.aktuellerZustand == spieler.laufen)
                {
                    foreach (Hitbox hitbox in kollisionsListe)
                    {
                        if (spieler.hitboxKopf.Intersects(hitbox.hitbox)) // Wenn Kopf kollidiert
                        {
                            ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).koepfen;// Passt den Todeszustand an
                            menue.spielAktiv = false; // setzt Menü nach Tod wieder auf anfang
                            spieler.doSterben();
                            Console.WriteLine("Im laufen gestorben Kopf");
                        }
                        if (spieler.hitboxBeine.Intersects(hitbox.hitbox)) // Wenn Beine kollidiert
                        {
                            ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).stolpern; // Passt den Todeszustand an
                            menue.spielAktiv = false; // setzt Menü nach Tod wieder auf anfang
                            spieler.doSterben();
                            Console.WriteLine("Im laufen gestorben Beine");
                        }

                    }
                }
                else if (menue.spielAktiv) // Kollisionerkennung im Flug/Fallen/Gleiten
                {
                    foreach (Hitbox hitbox in kollisionsListe) // für Sonstige Kollisionen in Zuständen die oben noch nicht abgefangen werden
                    {
                        if (spieler.position.Y > 500 && spieler.hitboxKopf.Intersects(hitbox.hitbox) && spieler.hitboxBeine.Intersects(hitbox.hitbox))
                        {
                            menue.spielAktiv = false;
                            ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).klatscher; // Passt den Todeszustand an
                            spieler.doSterben();
                        }
                        else if (spieler.hitboxKopf.Intersects(hitbox.hitbox) && !spieler.hitboxBeine.Intersects(hitbox.hitbox))
                        {
                            menue.spielAktiv = false;
                            Boolean kollidiertBoden = false;
                            while (!kollidiertBoden)
                            {
                                foreach (Hitbox hb in kollisionsListe)
                                {
                                    if (spieler.hitboxFuss.Intersects(hb.hitbox) || spieler.position.Y > 720)
                                    { kollidiertBoden = true; }
                                }
                                spieler.movePlayerDown(1);
                            }
                            ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).koepfen; // Passt den Todeszustand an
                            spieler.doSterben();
                        }
                        else if (spieler.hitboxBeine.Intersects(hitbox.hitbox) && !spieler.hitboxKopf.Intersects(hitbox.hitbox))
                        {
                            spieler.setPlayerPosition(hitbox.hitbox.Top - 80);
                            menue.spielAktiv = false;
                            ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).stolpern; // Passt den Todeszustand an
                            spieler.doSterben();
                        }

                    }
                }
            }



            if (spieler.position.Y >= 1720)
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
                NewKeyState = Keyboard.GetState();

                if (Keyboard.GetState().IsKeyDown(Keys.Up) && OldKeyState.IsKeyUp(Keys.Up))
                    menue.prevMenue();

                if (Keyboard.GetState().IsKeyDown(Keys.Down) && OldKeyState.IsKeyUp(Keys.Down))
                    menue.nextMenue();

                if (Keyboard.GetState().IsKeyDown(Keys.Right) && OldKeyState.IsKeyUp(Keys.Right))
                    menue.rightMenue();

                if (Keyboard.GetState().IsKeyDown(Keys.Left) && OldKeyState.IsKeyUp(Keys.Left))
                    menue.leftMenue();



                // Auswahl ausführen
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && OldKeyState.IsKeyUp(Keys.Enter))
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
                            neustart();
                            gamestate = Gamestate.ladebildschirm;
                            menue.spielAktiv = true;
                            break;
                        case 3: // Spieler zurücksetzen (TODO)
                            neustart();
                            gamestate = Gamestate.ladebildschirm;
                            break;
                    }


                }

                OldKeyState = NewKeyState;

                menue.Update();
            }
            #endregion

            #region GamestateOptions

            // Wieder ins Haupmenü zurück
            if (gamestate == Gamestate.options)
            {
                optionen.Update();

                NewKeyState = Keyboard.GetState();
                int freischalten = 1;
                if (gewonnen >= 1)
                    freischalten++;
                if (gewonnen >= 5)
                    freischalten++;
                if (gewonnen >= 10)
                    freischalten++;

                if (Keyboard.GetState().IsKeyDown(Keys.Down) && OldKeyState.IsKeyUp(Keys.Down))
                    optionen.auswahl = (optionen.auswahl + 1) % freischalten;

                if (Keyboard.GetState().IsKeyDown(Keys.Up) && OldKeyState.IsKeyUp(Keys.Up))
                    optionen.auswahl = (optionen.auswahl + freischalten - 1) % freischalten;

                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && OldKeyState.IsKeyUp(Keys.Enter))
                {
                    spieler.aktuellerSkin = optionen.skinListe[optionen.auswahl + 1];
                    gamestate = Gamestate.menue;
                }

                OldKeyState = NewKeyState;

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    gamestate = Gamestate.menue;
            }

            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); // Begin

            // Hintergunrd zeichnen
            spriteBatch.Draw(hintergrund.hintergrundTextur, hintergrund.hintegrundPosition, Color.White);

            #region GamestateLoading

            if (gamestate == Gamestate.ladebildschirm)
            {
                hud.DrawHelp(spriteBatch, schrift_40);
                spieler.Draw(spriteBatch);
            }

            #endregion

            #region GamestateRunning

            if (gamestate == Gamestate.running)
            {

                // Zeichne Spieler
                spieler.Draw(spriteBatch);
                // spriteBatch.Draw(spieler.spielerTextur, spieler.position, Color.White);

                hud.Draw(spriteBatch, schrift_40);


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



                // Zeichne Hitboxen der Hindernisse
                if (debug)
                {
                    foreach (Hindernis hindernis in hindernisListe)
                    {
                        foreach (Hitbox hitbox in hindernis.gibHitboxen())
                        {
                            spriteBatch.Draw(dummyTexture, hitbox.hitbox, Color.Red);
                        }

                    }
                    // Spieler Hitbox malen zum Testen

                    //  spriteBatch.Draw(dummyTexture2, spieler.hitboxFussRechts, Color.Green);
                    spriteBatch.DrawString(schrift_40, "X: " + spieler.position.X + " Y: " + spieler.position.Y, new Vector2(0, 0), Color.Black);

                    spriteBatch.Draw(dummyTexture2, spieler.linksOben, Color.Green);
                    spriteBatch.Draw(dummyTexture2, spieler.hitboxFuss, Color.Green);

                    // spriteBatch.Draw(dummyTexture2, spieler.hitboxKopf, Color.Blue);
                    spriteBatch.Draw(dummyTexture2, spieler.hitboxKopf, Color.Green);
                    spriteBatch.Draw(dummyTexture2, spieler.hitboxBeine, Color.Green);

                }


                if (hud.gewonnen)
                    hud.DrawAchivment(spriteBatch,schrift_20,gewonnen);




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
                optionen.Draw(spriteBatch, schrift_40, gewonnen);
                spriteBatch.DrawString(schrift_40, "Zurück", new Vector2(128 + 50, 590), Color.Gray);

                spriteBatch.DrawString(schrift_40, "Gewonnen: " + gewonnen, new Vector2(600, 590), Color.Gray);
            }

            #endregion

            spriteBatch.Draw(logo, new Vector2(0, 720 - 83), Color.White);
            spriteBatch.End(); // End

            base.Draw(gameTime);
        }


        public void loadAnimation()
        {


            // Skin Animationen laden

            // StandardSkin
            if (standardSkin.duckenAnimation == null) standardSkin.duckenAnimation = new Animation(standardSkin.duckenTextur, 4, 4, 4);
            if (standardSkin.fallenAnimation == null) standardSkin.fallenAnimation = new Animation(standardSkin.fallenTextur, 2, 2, 6);
            if (standardSkin.gewinnenAnimation == null) standardSkin.gewinnenAnimation = new Animation(standardSkin.gewinnenTextur, 5, 2, 6);
            if (standardSkin.gleitenAnimation == null) standardSkin.gleitenAnimation = new Animation(standardSkin.gleitenTextur, 2, 2, 6);
            if (standardSkin.laufenAnimation == null) standardSkin.laufenAnimation = new Animation(standardSkin.laufenTextur, 4, 3, 3);
            if (standardSkin.sprignenAnimation == null) standardSkin.sprignenAnimation = new Animation(standardSkin.sprignenTextur, 4, 2, 6);

            if (standardSkin.sterbenAnimationKoepfen == null) standardSkin.sterbenAnimationKoepfen = new Animation(standardSkin.sterbenTexturKoepfen, 2, 5, 6);
            if (standardSkin.sterbenAnimationStolpern == null) standardSkin.sterbenAnimationStolpern = new Animation(standardSkin.sterbenTexturStolpern, 2, 5, 6);
            if (standardSkin.sterbenAnimationKlatscher == null) standardSkin.sterbenAnimationKlatscher = new Animation(standardSkin.sterbenTexturKlatscher, 12, 1, 7);

            // Fraud Skin
            if (frauenSkin.duckenAnimation == null) frauenSkin.duckenAnimation = new Animation(frauenSkin.duckenTextur, 4, 4, 4);
            if (frauenSkin.fallenAnimation == null) frauenSkin.fallenAnimation = new Animation(frauenSkin.fallenTextur, 2, 2, 6);
            if (frauenSkin.gewinnenAnimation == null) frauenSkin.gewinnenAnimation = new Animation(frauenSkin.gewinnenTextur, 5, 2, 6);
            if (frauenSkin.gleitenAnimation == null) frauenSkin.gleitenAnimation = new Animation(frauenSkin.gleitenTextur, 2, 2, 6);
            if (frauenSkin.laufenAnimation == null) frauenSkin.laufenAnimation = new Animation(frauenSkin.laufenTextur, 4, 3, 3);
            if (frauenSkin.sprignenAnimation == null) frauenSkin.sprignenAnimation = new Animation(frauenSkin.sprignenTextur, 4, 2, 6);

            if (frauenSkin.sterbenAnimationKoepfen == null) frauenSkin.sterbenAnimationKoepfen = new Animation(frauenSkin.sterbenTexturKoepfen, 2, 5, 6);
            if (frauenSkin.sterbenAnimationStolpern == null) frauenSkin.sterbenAnimationStolpern = new Animation(frauenSkin.sterbenTexturStolpern, 2, 5, 6);
            if (frauenSkin.sterbenAnimationKlatscher == null) frauenSkin.sterbenAnimationKlatscher = new Animation(frauenSkin.sterbenTexturKlatscher, 12, 1, 7);

            // Hut Skin
            if (hutSkin.duckenAnimation == null) hutSkin.duckenAnimation = new Animation(hutSkin.duckenTextur, 4, 4, 4);
            if (hutSkin.fallenAnimation == null) hutSkin.fallenAnimation = new Animation(hutSkin.fallenTextur, 2, 2, 6);
            if (hutSkin.gewinnenAnimation == null) hutSkin.gewinnenAnimation = new Animation(hutSkin.gewinnenTextur, 5, 2, 6);
            if (hutSkin.gleitenAnimation == null) hutSkin.gleitenAnimation = new Animation(hutSkin.gleitenTextur, 2, 2, 6);
            if (hutSkin.laufenAnimation == null) hutSkin.laufenAnimation = new Animation(hutSkin.laufenTextur, 4, 3, 3);
            if (hutSkin.sprignenAnimation == null) hutSkin.sprignenAnimation = new Animation(hutSkin.sprignenTextur, 4, 2, 6);

            if (hutSkin.sterbenAnimationKoepfen == null) hutSkin.sterbenAnimationKoepfen = new Animation(hutSkin.sterbenTexturKoepfen, 2, 5, 6);
            if (hutSkin.sterbenAnimationStolpern == null) hutSkin.sterbenAnimationStolpern = new Animation(hutSkin.sterbenTexturStolpern, 2, 5, 6);
            if (hutSkin.sterbenAnimationKlatscher == null) hutSkin.sterbenAnimationKlatscher = new Animation(hutSkin.sterbenTexturKlatscher, 2, 5, 6);


            // Einstein Skin
            if (einsteinSkin.duckenAnimation == null) einsteinSkin.duckenAnimation = new Animation(einsteinSkin.duckenTextur, 4, 4, 4);
            if (einsteinSkin.fallenAnimation == null) einsteinSkin.fallenAnimation = new Animation(einsteinSkin.fallenTextur, 2, 2, 6);
            if (einsteinSkin.gewinnenAnimation == null) einsteinSkin.gewinnenAnimation = new Animation(einsteinSkin.gewinnenTextur, 5, 2, 6);
            if (einsteinSkin.gleitenAnimation == null) einsteinSkin.gleitenAnimation = new Animation(einsteinSkin.gleitenTextur, 2, 2, 6);
            if (einsteinSkin.laufenAnimation == null) einsteinSkin.laufenAnimation = new Animation(einsteinSkin.laufenTextur, 4, 3, 3);
            if (einsteinSkin.sprignenAnimation == null) einsteinSkin.sprignenAnimation = new Animation(einsteinSkin.sprignenTextur, 4, 2, 6);

            if (einsteinSkin.sterbenAnimationKoepfen == null) einsteinSkin.sterbenAnimationKoepfen = new Animation(einsteinSkin.sterbenTexturKoepfen, 2, 5, 6);
            if (einsteinSkin.sterbenAnimationStolpern == null) einsteinSkin.sterbenAnimationStolpern = new Animation(einsteinSkin.sterbenTexturStolpern, 2, 5, 6);
            if (einsteinSkin.sterbenAnimationKlatscher == null) einsteinSkin.sterbenAnimationKlatscher = new Animation(einsteinSkin.sterbenTexturKlatscher, 2, 5, 6);

            // Menü Radio Animation
            if (menue.radio_m_ani == null) menue.radio_m_ani = new Animation(menue.radioTexture, 2, 2, 6);
        }

        public void neustart()
        {
            spieler = new Spieler();
            hud = new Hud(spieler, hudTextur);
            LoadContent();
            loadAnimation();
            hud.gewonnen = false;

            ((Fallen)spieler.fallen).beschleunigung = 0;

            spieler.aktuellerSkin = optionen.skinListe[optionen.auswahl];

            spieler.aktuellerSkin.sterbenAnimationKlatscher.index = 0;
            spieler.aktuellerSkin.sterbenAnimationKoepfen.index = 0;
            spieler.aktuellerSkin.sterbenAnimationStolpern.index = 0;

            hindernisListe = Hindernis.generieHindernisse(10, hindernisTexturS, hindernisTexturA, hindernisTexturB, hindernisTexturC, hindernisTexturD, hindernisTexturZ);
        }
    }
}
