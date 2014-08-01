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

        // Entprellen der Tastatur
        KeyboardState NewKeyState;

        // Hindernis Texturen
        Texture2D hindernisTexturS;
        Texture2D hindernisTexturA;
        Texture2D hindernisTexturB;
        Texture2D hindernisTexturC;
        Texture2D hindernisTexturD;
        Texture2D hindernisTexturE;
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

        // Anzal der Gewonnen spiele
        int gewonnen;

        // Punkte / Noten
        Punkt punkt1, punkt2, punkt5, punkt10;
        Texture2D notenPlatzerTextur;
        Animation notenPlatzerAnimation;
        Vector2 notenPlatzerPosition;

        PowerUp powerUp;

        int notenFallSchrittweite = 0;

        // Punkte Liste
        List<NotenHitbox> punkteListeDraw = new List<NotenHitbox>();
        List<NotenHitbox> punkteListeKollisionen = new List<NotenHitbox>();

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

            // Punkte Initialisierung
            punkt1 = new Punkt(1);
            punkt2 = new Punkt(2);
            punkt5 = new Punkt(5);
            punkt10 = new Punkt(10);
            
            // Powerup
            powerUp = new PowerUp();

            // Noten Platzer Größe init
        }

        int getRandomNumber()
        {
            // RFC 1149.5 specifies 4 as the standard IEEE-vetted random number.
            // www.xkcd.com/221

            return 4;   // chosen by fair dice roll.
                        // guaranteed to be random.
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
            standardSkin.sterbenTexturPieksen = Content.Load<Texture2D>("ani_sterben2_stolpern_std");



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
            frauenSkin.sterbenTexturPieksen = Content.Load<Texture2D>("ani_sterben4_pieksen_std");

            // Hut Skin
            hutSkin = new Skin();

            hutSkin.duckenTextur = Content.Load<Texture2D>("ani_ducken_hut");
            hutSkin.fallenTextur = Content.Load<Texture2D>("ani_fallen_hut");
            hutSkin.gewinnenTextur = Content.Load<Texture2D>("ani_gewinnen_hut");
            hutSkin.gleitenTextur = Content.Load<Texture2D>("ani_gleiten_hut");
            hutSkin.laufenTextur = Content.Load<Texture2D>("ani_laufen_hut");
            hutSkin.sprignenTextur = Content.Load<Texture2D>("ani_springen_hut");

            hutSkin.sterbenTexturKoepfen = Content.Load<Texture2D>("ani_sterben1_koepfen_hut");
            hutSkin.sterbenTexturStolpern = Content.Load<Texture2D>("ani_sterben2_stolpern_hut");
            hutSkin.sterbenTexturKlatscher = Content.Load<Texture2D>("ani_sterben3_klatscher_hut");
            hutSkin.sterbenTexturPieksen = Content.Load<Texture2D>("ani_sterben4_pieksen_std");

            // Einstein Skin
            einsteinSkin = new Skin();

            einsteinSkin.duckenTextur = Content.Load<Texture2D>("ani_ducken_std");
            einsteinSkin.fallenTextur = Content.Load<Texture2D>("ani_fallen_std");
            einsteinSkin.gewinnenTextur = Content.Load<Texture2D>("ani_gewinnen_std");
            einsteinSkin.gleitenTextur = Content.Load<Texture2D>("ani_gleiten_std");
            einsteinSkin.laufenTextur = Content.Load<Texture2D>("ani_laufen_einstein");
            einsteinSkin.sprignenTextur = Content.Load<Texture2D>("ani_springen_einstein");

            einsteinSkin.sterbenTexturKoepfen = Content.Load<Texture2D>("ani_sterben1_koepfen_std");
            einsteinSkin.sterbenTexturStolpern = Content.Load<Texture2D>("ani_sterben2_stolpern_std");
            einsteinSkin.sterbenTexturKlatscher = Content.Load<Texture2D>("ani_sterben2_stolpern_std");
            einsteinSkin.sterbenTexturPieksen = Content.Load<Texture2D>("ani_sterben4_pieksen_std");

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


            // Hintergrund
            hintergrund.hintergrundTextur = Content.Load<Texture2D>("hintergrund");

            // Optionen Zurück Knopf
            optionen.z_knopf_Textur = Content.Load<Texture2D>("o_zurueck");

            // Sound
            titel = Content.Load<Song>("titel");

            // Spring Sounds
            spieler.springen.sound = Content.Load<SoundEffect>("jump");
            spieler.springen.soundSoundInstance = spieler.springen.sound.CreateInstance();

            // Einsammel Sound
            punkt1.initSound(Content.Load<SoundEffect>("pop"));
            punkt2.initSound(Content.Load<SoundEffect>("pop"));
            punkt5.initSound(Content.Load<SoundEffect>("pop"));
            punkt10.initSound(Content.Load<SoundEffect>("pop"));

            powerUp.initSound(Content.Load<SoundEffect>("teleportAufladen"));

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


            // Sterben Sound beim pieksen
            ((Sterben)spieler.sterben).pieksen.soundTod = Content.Load<SoundEffect>("dsskedth");
            ((Sterben)spieler.sterben).pieksen.soundSoundInstance = ((Sterben)spieler.sterben).pieksen.soundTod.CreateInstance();




            // Textur für Hindernisse
            hindernisTexturS = Content.Load<Texture2D>("hindernisS");
            hindernisTexturA = Content.Load<Texture2D>("hindernisA");
            hindernisTexturB = Content.Load<Texture2D>("hindernisB");
            hindernisTexturC = Content.Load<Texture2D>("hindernisC");
            hindernisTexturD = Content.Load<Texture2D>("hindernisD");
            hindernisTexturE = Content.Load<Texture2D>("hindernisE");
            hindernisTexturZ = Content.Load<Texture2D>("hindernisZ");

            // Liste mit Hindernisse die generiert werden
            hindernisListe = Hindernis.generieHindernisse(10, hindernisTexturS, hindernisTexturA, hindernisTexturB, hindernisTexturC, hindernisTexturD, hindernisTexturE, hindernisTexturZ, punkt1, punkt2, punkt5, punkt10, powerUp);


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

            hud.gameOverTextur = Content.Load<Texture2D>("GameOver");
            // Game Over Sound Ohh

            hud.soundGameOver = Content.Load<SoundEffect>("ooh");
            hud.soundGameOverSoundInstance = hud.soundGameOver.CreateInstance();


            // Punkt Textur
            punkt1.punktTextur = Content.Load<Texture2D>("note1");
            punkt1.punktTexturHaufen = Content.Load<Texture2D>("note1_haufen");

            punkt2.punktTextur = Content.Load<Texture2D>("note2");
            punkt2.punktTexturHaufen = Content.Load<Texture2D>("note2_haufen");

            punkt5.punktTextur = Content.Load<Texture2D>("note5");
            punkt5.punktTexturHaufen = Content.Load<Texture2D>("note5_haufen");

            punkt10.punktTextur = Content.Load<Texture2D>("note10");
            punkt10.punktTexturHaufen = Content.Load<Texture2D>("note10_haufen");

            // Punkte Platzer Textur
            notenPlatzerTextur = Content.Load<Texture2D>("ani_notenpuff");

            // PowerUp Textur

            powerUp.punktTextur = Content.Load<Texture2D>("powerup");

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
                    hintergrund.Update(3);
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

                if (Keyboard.GetState().IsKeyDown(Keys.Escape) && OldKeyState.IsKeyUp(Keys.Escape))
                {
                    gamestate = Gamestate.menue;
                }

                OldKeyState = NewKeyState;
            }

            #endregion

            #region GamestateRunning

            if (gamestate == Gamestate.running)
            {


                // Hud Update
                hud.Update();

                // Update spieler
                spieler.Update();

                #region Tastaturabfrage
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

                // Escape ins Menü zurück kehren
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) && OldKeyState.IsKeyUp(Keys.Escape))
                {
                    menue.auswahl = 2; // Auswahl auf Forsetzen bzw. Starten setzten
                    gamestate = Gamestate.menue;
                }

                if (spieler.aktuellerZustand == spieler.sterben && Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gamestate = Gamestate.menue;
                }
                #endregion


                #region Aktualisierung_Hindernisse_Punkte

                // Aktualisiere Hindernisse (schiebe Hindernisse Weiter)
                if (hindernisListe.Count > 6 && spieler.aktuellerZustand != spieler.sterben)
                {
                    // Listen mit Punkten Clearen
                    punkteListeDraw.Clear();
                    punkteListeKollisionen.Clear();

                    hindernisListe[0].Update();
                    punkteListeDraw.AddRange(hindernisListe[0].gibPunkte());

                    hindernisListe[1].Update();
                    punkteListeDraw.AddRange(hindernisListe[1].gibPunkte());

                    hindernisListe[2].Update();
                    punkteListeKollisionen.AddRange(hindernisListe[2].gibPunkte());

                    hindernisListe[3].Update();
                    punkteListeKollisionen.AddRange(hindernisListe[3].gibPunkte());

                    hindernisListe[4].Update();
                    punkteListeDraw.AddRange(hindernisListe[4].gibPunkte());

                    hindernisListe[5].Update();
                    punkteListeDraw.AddRange(hindernisListe[5].gibPunkte());

                    // Wenn das Hindernis an Position 1 Außerhalb des Bildes ist lösche das Hindernis an Position 0
                    if (hindernisListe[1].hindernisPosition.X <= -320)
                        hindernisListe.RemoveAt(0);
                    hintergrund.Update();
                }
                #endregion

                #region Noten/Powerup Animation  aktualisierung
                punkt1.punktAnimation.Update();
                punkt2.punktAnimation.Update();
                punkt5.punktAnimation.Update();
                punkt10.punktAnimation.Update();
                powerUp.punktAnimation.Update();
                #endregion


                #region GewonnenAktionen
                if (hindernisListe.Count == 6)
                {
                    if (spieler.aktuellerZustand == spieler.laufen)
                    {
                        spieler.doGewinnen(); // Wenn weniger als 6 Hindernisse vorhanden sind gehe in den Gewinnenzustand über
                        if (menue.spielAktiv)
                        {
                            menue.spielAktiv = false;
                            gewonnen++;
                            hud.gewonnen = true;
                            konfig.WriteFile(gewonnen.ToString());
                        }
                        if (hud.gewonnen)
                            hud.UpdateAchievment();
                    }
                }

                #endregion


                #region PunkteKollisionen

                foreach (NotenHitbox hitbox in punkteListeKollisionen)
                {
                    if (spieler.hitboxKoerper.Intersects(hitbox.hitboxRect))
                    {
                        notenPlatzerAnimation.index = 0;
                        notenPlatzerPosition = hitbox.hitboxPosition - new Vector2(16, 16);
                        hitbox.hindernis.loescheHitboxPunkt(hitbox);
                        if (hitbox.punkt.wertigkeit != 0)
                            spieler.punkte += hitbox.punkt.wertigkeit;
                        else
                            spieler.teleport = true;
                        hitbox.punkt.playSound();
                    }
                }

                #endregion

                #region Kollisionserkennung

                // Erstellen einer Liste mit Hitboxen aus Bereich 2 & 3
                List<Hitbox> kollisionsListe = new List<Hitbox>();
                List<Hitbox> kollisionsListeStacheln = new List<Hitbox>();

                //Boden Liste erstellen
                foreach (Hitbox hitbox in hindernisListe[2].gibHitboxen())
                { kollisionsListe.Add(hitbox); }

                foreach (Hitbox hitbox in hindernisListe[3].gibHitboxen())
                { kollisionsListe.Add(hitbox); }

                // Stacheln Liste erstellen
                foreach (Hitbox hitbox in hindernisListe[2].gibSterben())
                { kollisionsListeStacheln.Add(hitbox); }

                foreach (Hitbox hitbox in hindernisListe[3].gibSterben())
                { kollisionsListeStacheln.Add(hitbox); }

                // Boolean Werte für Kollisionserkennung (für Füße)
                Boolean kollidiert = false;

                // Boden Kollisionserkennung mit allen Hitboxen
                // Prüfe erst alle hitboxen ab ob eine Kollision entstanden ist
                // siehe boolean kollidiert
                foreach (Hitbox hitbox in kollisionsListe)
                {
                    if (spieler.hitboxFuss.Intersects(hitbox.hitboxRect))
                    {
                        kollidiert = true;
                        // Spieler kann sich nur ducken wenn er Läuft
                        if (Keyboard.GetState().IsKeyDown(Keys.Down) && spieler.aktuellerZustand != spieler.fallen)
                            spieler.doDucken();
                        else
                            spieler.doLaufen();
                        spieler.setPlayerPosition(hitbox.hitboxRect.Y - 110);
                    }
                }


                // Prüfe auf Stacheln
                foreach (Hitbox hitbox in kollisionsListeStacheln)
                {
                    if (menue.spielAktiv && spieler.hitboxBeine.Intersects(hitbox.hitboxRect))
                    {
                        ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).pieksen;// Passt den Todeszustand an
                        menue.spielAktiv = false; // setzt Menü nach Tod wieder auf anfang
                        spieler.setPlayerPosition((int)hitbox.hitboxPosition.Y - 80);
                        hud.soundGameOverSoundInstance.Play();
                        spieler.doSterben();
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
                        if (spieler.hitboxKopf.Intersects(hitbox.hitboxRect)) // Wenn Kopf kollidiert
                        {
                            ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).koepfen;// Passt den Todeszustand an
                            menue.spielAktiv = false; // setzt Menü nach Tod wieder auf anfang
                            hud.soundGameOverSoundInstance.Play();
                            spieler.doSterben();
                        }
                        if (spieler.hitboxBeine.Intersects(hitbox.hitboxRect)) // Wenn Beine kollidiert
                        {
                            ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).stolpern; // Passt den Todeszustand an
                            menue.spielAktiv = false; // setzt Menü nach Tod wieder auf anfang
                            hud.soundGameOverSoundInstance.Play();
                            spieler.doSterben();
                        }

                    }
                }
                else if (menue.spielAktiv) // Kollisionerkennung im Flug/Fallen/Gleiten
                {
                    foreach (Hitbox hitbox in kollisionsListe) // für Sonstige Kollisionen in Zuständen die oben noch nicht abgefangen werden
                    {
                        if (spieler.position.Y > 500 && spieler.hitboxKopf.Intersects(hitbox.hitboxRect) && spieler.hitboxBeine.Intersects(hitbox.hitboxRect))
                        {
                            menue.spielAktiv = false;
                            ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).klatscher; // Passt den Todeszustand an
                            hud.soundGameOverSoundInstance.Play();
                            spieler.doSterben();
                        }
                        else if (spieler.hitboxKopf.Intersects(hitbox.hitboxRect) )//  && !spieler.hitboxBeine.Intersects(hitbox.hitboxRect))
                        {
                            menue.spielAktiv = false;
                            Boolean kollidiertBoden = false;
                            while (!kollidiertBoden)
                            {
                                foreach (Hitbox hb in kollisionsListe)
                                {
                                    if (spieler.hitboxFuss.Intersects(hb.hitboxRect) || spieler.position.Y > 720)
                                    { kollidiertBoden = true; }
                                }
                                spieler.movePlayerDown(1);
                            }
                            ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).koepfen; // Passt den Todeszustand an
                            hud.soundGameOverSoundInstance.Play();
                            menue.spielAktiv = false;
                            spieler.doSterben();
                        }
                        else if (spieler.hitboxBeine.Intersects(hitbox.hitboxRect) ) //&& !spieler.hitboxKopf.Intersects(hitbox.hitboxRect))
                        {
                            spieler.setPlayerPosition(hitbox.hitboxRect.Top - 80);
                            menue.spielAktiv = false;
                            ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).stolpern; // Passt den Todeszustand an
                            spieler.doSterben();
                        }

                    }
                }
            }
                #endregion



            // Sterben wenn der Spieler auserhalb des Bildschirms ist
            if (spieler.position.Y >= 1720)
                spieler.doSterben();


            #region Noten/Punkte_Animation
            if (spieler.aktuellerZustand == spieler.sterben)
            {
                if (gameTime.TotalGameTime.Milliseconds % 10 == 0)
                    notenFallSchrittweite++;

                foreach (NotenHitbox notenhitbox in punkteListeDraw)
                {
                    notenhitbox.Update();
                    if (notenhitbox.punkt.wertigkeit != 0)
                    {
                        if (notenhitbox.faellt)
                        {
                            foreach (Hitbox bodenHitbox in notenhitbox.hindernis.gibHitboxen())
                            {
                                if (notenhitbox.faellt && notenhitbox.hitboxRect.Y < 800 && !notenhitbox.hitboxRect.Intersects(bodenHitbox.hitboxRect))
                                {
                                    notenhitbox.moveY(-(1 + notenFallSchrittweite));
                                }
                                else if (notenhitbox.hitboxRect.Y >= 800)
                                {
                                    notenhitbox.faellt = false;
                                }
                                else if (notenhitbox.faellt)
                                {
                                    notenhitbox.faellt = false;
                                    notenhitbox.setPositionY((int)bodenHitbox.hitboxPosition.Y - 30);
                                }
                            }
                        }
                    }
                }

                foreach (NotenHitbox notenhitbox in punkteListeKollisionen)
                {
                    notenhitbox.Update();
                    if (notenhitbox.punkt.wertigkeit != 0)
                    {
                        if (notenhitbox.faellt)
                        {
                            foreach (Hitbox bodenHitbox in notenhitbox.hindernis.gibHitboxen())
                            {
                                if (notenhitbox.faellt && notenhitbox.hitboxRect.Y < 800 && !notenhitbox.hitboxRect.Intersects(bodenHitbox.hitboxRect))
                                {
                                    notenhitbox.moveY(-(1 + notenFallSchrittweite));
                                }
                                else if (notenhitbox.hitboxRect.Y >= 800)
                                {
                                    notenhitbox.faellt = false;
                                }
                                else if (notenhitbox.faellt)
                                {
                                    notenhitbox.faellt = false;
                                    notenhitbox.setPositionY((int)bodenHitbox.hitboxPosition.Y - 30);
                                }
                            }
                        }
                    }
                }
            }

            notenPlatzerPosition.X -= 4;
            notenPlatzerAnimation.UpdateNoLoop();


            #endregion

            #endregion

            #region GamestateMenue

            if (gamestate == Gamestate.menue)
            {
                // Menü Animationen laden
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
                            if (!menue.spielAktiv)
                            {
                                neustart();
                                gamestate = Gamestate.ladebildschirm;
                            }
                            else
                                gamestate = Gamestate.running;
                            menue.spielAktiv = true;
                            break;
                        case 3: // Spieler zurücksetzen (TODO)
                            neustart();
                            gamestate = Gamestate.ladebildschirm;
                            menue.spielAktiv = true;
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
                    spieler.aktuellerSkin = optionen.skinListe[optionen.auswahl];
                    menue.auswahl = 2;
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

            // Hintergrund zeichnen
            spriteBatch.Draw(hintergrund.hintergrundTextur, hintergrund.hintegrundPosition, Color.White);

            #region GamestateLoading

            if (gamestate == Gamestate.ladebildschirm)
            {
                hud.DrawHelp(spriteBatch, schrift_40, gameTime);
                spieler.Draw(spriteBatch);
            }

            #endregion

            #region GamestateRunning

            if (gamestate == Gamestate.running)
            {


                // spriteBatch.Draw(spieler.spielerTextur, spieler.position, Color.White);

                hud.Draw(spriteBatch, schrift_40,Hindernis.punkteAnzahl, gameTime);


                // Hindernisse zeichnen
                spriteBatch.Draw(hindernisListe[1].hindernisTextur, hindernisListe[1].hindernisPosition, Color.White);
                spriteBatch.Draw(hindernisListe[2].hindernisTextur, hindernisListe[2].hindernisPosition, Color.White);
                spriteBatch.Draw(hindernisListe[3].hindernisTextur, hindernisListe[3].hindernisPosition, Color.White);
                spriteBatch.Draw(hindernisListe[4].hindernisTextur, hindernisListe[4].hindernisPosition, Color.White);
                spriteBatch.Draw(hindernisListe[5].hindernisTextur, hindernisListe[5].hindernisPosition, Color.White);





                foreach (NotenHitbox notenHitbox in hindernisListe[1].gibPunkte())
                {
                    notenHitbox.Draw(spriteBatch);
                }

                foreach (NotenHitbox notenHitbox in hindernisListe[2].gibPunkte())
                {
                    notenHitbox.Draw(spriteBatch);
                }

                foreach (NotenHitbox notenHitbox in hindernisListe[3].gibPunkte())
                {
                    notenHitbox.Draw(spriteBatch);
                }

                foreach (NotenHitbox notenHitbox in hindernisListe[4].gibPunkte())
                {
                    notenHitbox.Draw(spriteBatch);
                }

                foreach (NotenHitbox notenHitbox in hindernisListe[5].gibPunkte())
                {
                    notenHitbox.Draw(spriteBatch);
                }

                notenPlatzerAnimation.Draw(spriteBatch, notenPlatzerPosition);


                #region Debugsection
                if (debug)
                {
                    foreach (Hindernis hindernis in hindernisListe)
                    {
                        foreach (Hitbox hitbox in hindernis.gibHitboxen())
                        {
                            spriteBatch.Draw(dummyTexture, hitbox.hitboxRect, Color.Red);
                        }

                        foreach (Hitbox hitbox in hindernis.gibSterben())
                        {
                            spriteBatch.Draw(dummyTexture2, hitbox.hitboxRect, Color.Green);
                        }
                    }

                    // Punkte/Noten Zeichnen

                    foreach (NotenHitbox note in punkteListeDraw)
                    {
                        spriteBatch.Draw(dummyTexture, note.hitboxRect, Color.Red);
                    }

                    foreach (NotenHitbox note in punkteListeKollisionen)
                    {
                        spriteBatch.Draw(dummyTexture, note.hitboxRect, Color.Red);
                    }


                    // Spieler Hitbox malen zum Testen
                    //  spriteBatch.Draw(dummyTexture2, spieler.hitboxFussRechts, Color.Green);
                    spriteBatch.DrawString(schrift_40, "X: " + spieler.position.X + " Y: " + spieler.position.Y, new Vector2(0, 0), Color.Black);

                    spriteBatch.Draw(dummyTexture2, spieler.spielerPosition, Color.Green);
                    spriteBatch.Draw(dummyTexture2, spieler.hitboxFuss, Color.Green);

                    // spriteBatch.Draw(dummyTexture2, spieler.hitboxKopf, Color.Blue);
                    spriteBatch.Draw(dummyTexture2, spieler.hitboxKopf, Color.Green);
                    spriteBatch.Draw(dummyTexture2, spieler.hitboxBeine, Color.Green);

                    spriteBatch.Draw(dummyTexture2, spieler.hitboxKoerper, Color.Blue);

                }
                #endregion


                // Zeichne Spieler
                spieler.Draw(spriteBatch);

                #region AchievmentAnzeigen
                if (gewonnen == 1)
                {
                    hud.aktuellerUnlock = hud.skin_frau;
                    hud.DrawAchivment(spriteBatch);
                }
                else if (gewonnen == 5)
                {
                    hud.aktuellerUnlock = hud.skin_hut;
                    hud.DrawAchivment(spriteBatch);
                }
                else if (gewonnen == 10)
                {
                    hud.aktuellerUnlock = hud.skin_einstein;
                    hud.DrawAchivment(spriteBatch);
                }
                #endregion


                

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
            }

            #endregion

            // Logo Zeichnen
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
            if (standardSkin.sterbenAnimationPieksen == null) standardSkin.sterbenAnimationPieksen= new Animation(standardSkin.sterbenTexturPieksen, 2, 5, 7);

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
            if (hutSkin.sterbenAnimationKlatscher == null) hutSkin.sterbenAnimationKlatscher = new Animation(hutSkin.sterbenTexturKlatscher, 12, 1, 7);


            // Einstein Skin
            if (einsteinSkin.duckenAnimation == null) einsteinSkin.duckenAnimation = new Animation(einsteinSkin.duckenTextur, 4, 4, 4);
            if (einsteinSkin.fallenAnimation == null) einsteinSkin.fallenAnimation = new Animation(einsteinSkin.fallenTextur, 2, 2, 6);
            if (einsteinSkin.gewinnenAnimation == null) einsteinSkin.gewinnenAnimation = new Animation(einsteinSkin.gewinnenTextur, 5, 2, 6);
            if (einsteinSkin.gleitenAnimation == null) einsteinSkin.gleitenAnimation = new Animation(einsteinSkin.gleitenTextur, 2, 2, 6);
            if (einsteinSkin.laufenAnimation == null) einsteinSkin.laufenAnimation = new Animation(einsteinSkin.laufenTextur, 4, 3, 3);
            if (einsteinSkin.sprignenAnimation == null) einsteinSkin.sprignenAnimation = new Animation(einsteinSkin.sprignenTextur, 4, 2, 6);

            if (einsteinSkin.sterbenAnimationKoepfen == null) einsteinSkin.sterbenAnimationKoepfen = new Animation(einsteinSkin.sterbenTexturKoepfen, 2, 5, 6);
            if (einsteinSkin.sterbenAnimationStolpern == null) einsteinSkin.sterbenAnimationStolpern = new Animation(einsteinSkin.sterbenTexturStolpern, 2, 5, 6);
            if (einsteinSkin.sterbenAnimationKlatscher == null) einsteinSkin.sterbenAnimationKlatscher = new Animation(einsteinSkin.sterbenTexturKlatscher, 12, 1, 7);

            // Menü Radio Animation
            if (menue.radio_m_ani == null) menue.radio_m_ani = new Animation(menue.radioTexture, 2, 2, 6);

            // Punkte Animation
            if (punkt1.punktAnimation == null) punkt1.punktAnimation = new Animation(punkt1.punktTextur, 2, 2, 4);
            if (punkt1.punktAnimationHaufen == null) punkt1.punktAnimationHaufen = new Animation(punkt1.punktTexturHaufen, 2, 2, 4);

            if (punkt2.punktAnimation == null) punkt2.punktAnimation = new Animation(punkt2.punktTextur, 2, 2, 4);
            if (punkt2.punktAnimationHaufen == null) punkt2.punktAnimationHaufen = new Animation(punkt2.punktTexturHaufen, 2, 2, 4);

            if (punkt5.punktAnimation == null) punkt5.punktAnimation = new Animation(punkt5.punktTextur, 2, 2, 4);
            if (punkt5.punktAnimationHaufen == null) punkt5.punktAnimationHaufen = new Animation(punkt5.punktTexturHaufen, 2, 2, 4);

            if (punkt10.punktAnimation == null) punkt10.punktAnimation = new Animation(punkt10.punktTextur, 2, 2, 4);
            if (punkt10.punktAnimationHaufen == null) punkt10.punktAnimationHaufen = new Animation(punkt10.punktTexturHaufen, 2, 2, 4);

            // Notenplatzer Animation
            if (notenPlatzerAnimation == null) notenPlatzerAnimation = new Animation(notenPlatzerTextur, 2, 2, 5);

            // Powerup/Atom Animation

            if (powerUp.punktAnimation == null) powerUp.punktAnimation = new Animation(powerUp.punktTextur, 4, 4, 4);


            // Game Over Animation
            if (hud.gameOverAnimation == null) hud.gameOverAnimation = new Animation(hud.gameOverTextur, 1, 4, 4);
        }

        public void neustart()
        {
            spieler = new Spieler();
            hud = new Hud(spieler, hudTextur);
            LoadContent();
            loadAnimation();
            hud.gewonnen = false;
            Hindernis.punkteAnzahl = 0;
            notenFallSchrittweite = 0;
            ((Fallen)spieler.fallen).beschleunigung = 0;

            spieler.aktuellerSkin = optionen.skinListe[optionen.auswahl];

            spieler.aktuellerSkin.sterbenAnimationKlatscher.index = 0;
            spieler.aktuellerSkin.sterbenAnimationKoepfen.index = 0;
            spieler.aktuellerSkin.sterbenAnimationStolpern.index = 0;
            spieler.aktuellerSkin.sterbenAnimationPieksen.index = 0;

            hindernisListe = Hindernis.generieHindernisse(10, hindernisTexturS, hindernisTexturA, hindernisTexturB, hindernisTexturC, hindernisTexturD, hindernisTexturE, hindernisTexturZ, punkt1, punkt2, punkt5, punkt10, powerUp);

        }
    }
}
