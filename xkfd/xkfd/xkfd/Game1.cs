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
using System.Diagnostics;


namespace xkfd
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {

        // Debug
        Boolean debug = false;

        #region Vor Sound Analyse
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Effect invert;



        // Spiel Status
        enum Gamestate { running, menue, options, ladebildschirm, cheat };
        Gamestate gamestate = Gamestate.menue;

        // Spieler
        Spieler spieler;

        // Schriftarten
        public SpriteFont schrift_40, schrift_20, schrift_rechner;

        // Menü 
        Menue menue;
        KeyboardState OldKeyState;

        // Hintergrund
        Hintergrund hintergrund;

        // Optionenmenü
        Optionen optionen;

        // Sound
        Song titel;

        // Hindernis Liste
        List<Hindernis> hindernisListe;

        // Hud
        public Hud hud;

        // Tutorial starten
        Boolean start = true;

        // Entprellen der Tastatur
        KeyboardState NewKeyState;

        // Hindernis Texturen
        public Texture2D hindernisTexturS,
                            hindernisTexturA,
                            hindernisTexturB,
                            hindernisTexturC,
                            hindernisTexturD,
                            hindernisTexturE,
                            hindernisTexturF,
                            hindernisTexturZ;
        public Texture2D hindernisTexturS_cheat,
                            hindernisTexturA_cheat,
                            hindernisTexturB_cheat,
                            hindernisTexturC_cheat,
                            hindernisTexturD_cheat,
                            hindernisTexturE_cheat,
                            hindernisTexturF_cheat,
                            hindernisTexturZ_cheat;
        public Texture2D zielEinlauf, cheat_qr;

        // Hud Texturen
        Texture2D hudTextur;

        //DebugTextur
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
        public Punkt punkt1, punkt2, punkt5, punkt10;

        Texture2D notenPlatzerTextur;
        Animation notenPlatzerAnimation;
        Vector2 notenPlatzerPosition;

        public PowerUp powerUp;

        int notenFallSchrittweite = 0;
        Boolean cheat = false;
        Boolean zielInSicht = false;

        // Punkte Liste
        public List<NotenHitbox> punkteListeDraw = new List<NotenHitbox>();
        public List<NotenHitbox> punkteListeKollisionen = new List<NotenHitbox>();

        List<NotenHitbox> notenFreilassen = new List<NotenHitbox>();

        public Random rand = new Random();

        #endregion

        // Sound Analyse

        Song musik;

        // Lied Länge in MS
        public int liedlaenge = 0;
        public List<int> liedWerte = new List<int>();


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
            gewonnen = int.Parse(konfig.ReadFile("config.txt"));

            // Punkte Initialisierung
            punkt1 = new Punkt(1);
            punkt2 = new Punkt(2);
            punkt5 = new Punkt(5);
            punkt10 = new Punkt(10);

            // Powerup
            powerUp = new PowerUp();
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

            // Process.Start(@"calc");

            konfig.ReadFile("config.txt");
            MediaPlayer.Stop();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            invert = Content.Load<Effect>("InvertShader");

            // Lade Schirftart
            schrift_40 = Content.Load<SpriteFont>("SpriteFont1");
            schrift_20 = Content.Load<SpriteFont>("SpriteFont2");
            schrift_rechner = Content.Load<SpriteFont>("SpriteFont3");

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

            standardSkin.cheatenTextur = Content.Load<Texture2D>("ani_cheaten_std");

            standardSkin.sterbenTexturKoepfen = Content.Load<Texture2D>("ani_sterben1_koepfen_std");
            standardSkin.sterbenTexturStolpern = Content.Load<Texture2D>("ani_sterben2_stolpern_std");
            standardSkin.sterbenTexturKlatscher = Content.Load<Texture2D>("ani_sterben3_klatscher_std");
            standardSkin.sterbenTexturKlatscherOben = Content.Load<Texture2D>("ani_sterben3_klatscher_oben_std");
            standardSkin.sterbenTexturPieksen = Content.Load<Texture2D>("ani_sterben4_pieksen_std");



            // Frauen Skin
            frauenSkin = new Skin();

            frauenSkin.duckenTextur = Content.Load<Texture2D>("ani_ducken_frau");
            frauenSkin.fallenTextur = Content.Load<Texture2D>("ani_fallen_frau");
            frauenSkin.gewinnenTextur = Content.Load<Texture2D>("ani_gewinnen_frau");
            frauenSkin.gleitenTextur = Content.Load<Texture2D>("ani_gleiten_frau");
            frauenSkin.laufenTextur = Content.Load<Texture2D>("ani_laufen_frau");
            frauenSkin.sprignenTextur = Content.Load<Texture2D>("ani_springen_frau");

            frauenSkin.cheatenTextur = Content.Load<Texture2D>("ani_cheaten_frau");

            frauenSkin.sterbenTexturKoepfen = Content.Load<Texture2D>("ani_sterben1_koepfen_frau");
            frauenSkin.sterbenTexturStolpern = Content.Load<Texture2D>("ani_sterben2_stolpern_frau");
            frauenSkin.sterbenTexturKlatscher = Content.Load<Texture2D>("ani_sterben3_klatscher_frau");
            frauenSkin.sterbenTexturKlatscherOben = Content.Load<Texture2D>("ani_sterben3_klatscher_oben_frau");
            frauenSkin.sterbenTexturPieksen = Content.Load<Texture2D>("ani_sterben4_pieksen_frau");

            // Hut Skin
            hutSkin = new Skin();

            hutSkin.duckenTextur = Content.Load<Texture2D>("ani_ducken_hut");
            hutSkin.fallenTextur = Content.Load<Texture2D>("ani_fallen_hut");
            hutSkin.gewinnenTextur = Content.Load<Texture2D>("ani_gewinnen_hut");
            hutSkin.gleitenTextur = Content.Load<Texture2D>("ani_gleiten_hut");
            hutSkin.laufenTextur = Content.Load<Texture2D>("ani_laufen_hut");
            hutSkin.sprignenTextur = Content.Load<Texture2D>("ani_springen_hut");

            hutSkin.cheatenTextur = Content.Load<Texture2D>("ani_cheaten_hut");

            hutSkin.sterbenTexturKoepfen = Content.Load<Texture2D>("ani_sterben1_koepfen_hut");
            hutSkin.sterbenTexturStolpern = Content.Load<Texture2D>("ani_sterben2_stolpern_hut");
            hutSkin.sterbenTexturKlatscher = Content.Load<Texture2D>("ani_sterben3_klatscher_hut");
            hutSkin.sterbenTexturKlatscherOben = Content.Load<Texture2D>("ani_sterben3_klatscher_oben_hut");
            hutSkin.sterbenTexturPieksen = Content.Load<Texture2D>("ani_sterben4_pieksen_hut");

            // Einstein Skin
            einsteinSkin = new Skin();

            einsteinSkin.duckenTextur = Content.Load<Texture2D>("ani_ducken_einstein");
            einsteinSkin.fallenTextur = Content.Load<Texture2D>("ani_fallen_einstein");
            einsteinSkin.gewinnenTextur = Content.Load<Texture2D>("ani_gewinnen_einstein");
            einsteinSkin.gleitenTextur = Content.Load<Texture2D>("ani_gleiten_einstein");
            einsteinSkin.laufenTextur = Content.Load<Texture2D>("ani_laufen_einstein");
            einsteinSkin.sprignenTextur = Content.Load<Texture2D>("ani_springen_einstein");

            einsteinSkin.cheatenTextur = Content.Load<Texture2D>("ani_cheaten_einstein");

            einsteinSkin.sterbenTexturKoepfen = Content.Load<Texture2D>("ani_sterben1_koepfen_einstein");
            einsteinSkin.sterbenTexturStolpern = Content.Load<Texture2D>("ani_sterben2_stolpern_einstein");
            einsteinSkin.sterbenTexturKlatscher = Content.Load<Texture2D>("ani_sterben3_klatscher_einstein");
            einsteinSkin.sterbenTexturKlatscherOben = Content.Load<Texture2D>("ani_sterben3_klatscher_oben_einstein");
            einsteinSkin.sterbenTexturPieksen = Content.Load<Texture2D>("ani_sterben4_pieksen_einstein");
            #endregion


            #region Optionen

            // Skin Auswahl in Optionen hinzufügen
            optionen.skinHinzufuegen(standardSkin);
            optionen.skinHinzufuegen(frauenSkin);
            optionen.skinHinzufuegen(hutSkin);
            optionen.skinHinzufuegen(einsteinSkin);

            // Optionen Zurück Knopf
            optionen.z_knopf_Textur = Content.Load<Texture2D>("o_zurueck");

            // Setzte standard Skin (evtl Datei auslesen)
            spieler.aktuellerSkin = optionen.skinListe[optionen.auswahl];

            #endregion

            #region Menü

            // Initialisiere Menü Animationen
            menue.startTextur = Content.Load<Texture2D>("m_start");
            menue.neuTexture = Content.Load<Texture2D>("m_new");
            menue.fortsetzenTexture = Content.Load<Texture2D>("m_continue");
            menue.optionenTexture = Content.Load<Texture2D>("m_optionen");
            menue.exitTexture = Content.Load<Texture2D>("m_exit");

            menue.radioTexture = Content.Load<Texture2D>("radio");

            #endregion

            #region Hintergrund
            hintergrund.aktuelleTextur = Content.Load<Texture2D>("hintergrund");
            hintergrund.hintergrundTextur = Content.Load<Texture2D>("hintergrund");
            hintergrund.hintergrundTexturCheat = Content.Load<Texture2D>("hintergrund_cheat_inv");

            #endregion

            #region Sounds
            // Sound
            titel = Content.Load<Song>("titel");
            musik = Content.Load<Song>("skysand");

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

            // Sterben Sound beim klatschen Oben
            ((Sterben)spieler.sterben).klatscher_oben.soundTod = Content.Load<SoundEffect>("dsskedth");
            ((Sterben)spieler.sterben).klatscher_oben.soundSoundInstance = ((Sterben)spieler.sterben).klatscher_oben.soundTod.CreateInstance();

            // Sterben Sound beim pieksen
            ((Sterben)spieler.sterben).pieksen.soundTod = Content.Load<SoundEffect>("dsskedth");
            ((Sterben)spieler.sterben).pieksen.soundSoundInstance = ((Sterben)spieler.sterben).pieksen.soundTod.CreateInstance();

            #endregion

            #region Hindernis Texturen

            // Textur für Hindernisse
            hindernisTexturS = Content.Load<Texture2D>("linie");
            hindernisTexturA = Content.Load<Texture2D>("hindernisA");
            hindernisTexturB = Content.Load<Texture2D>("hindernisB");
            hindernisTexturC = Content.Load<Texture2D>("hindernisC");
            hindernisTexturD = Content.Load<Texture2D>("hindernisD");
            hindernisTexturE = Content.Load<Texture2D>("hindernisE");
            hindernisTexturF = Content.Load<Texture2D>("hindernisF");
            hindernisTexturF = Content.Load<Texture2D>("hindernisF");
            hindernisTexturZ = Content.Load<Texture2D>("linie");

            // Textur für Hindernisse
            hindernisTexturS_cheat = Content.Load<Texture2D>("linie");
            hindernisTexturA_cheat = Content.Load<Texture2D>("hindernisA_cheat");
            hindernisTexturB_cheat = Content.Load<Texture2D>("hindernisB_cheat");
            hindernisTexturC_cheat = Content.Load<Texture2D>("hindernisC_cheat");
            hindernisTexturD_cheat = Content.Load<Texture2D>("hindernisD_cheat");
            hindernisTexturE_cheat = Content.Load<Texture2D>("hindernisE_cheat");
            hindernisTexturF_cheat = Content.Load<Texture2D>("hindernisF_cheat");
            hindernisTexturZ_cheat = Content.Load<Texture2D>("hindernisZ_cheat");

            //
            zielEinlauf = Content.Load<Texture2D>("ani_jubelmenge");
            cheat_qr = Content.Load<Texture2D>("cheat_qr");

            #endregion

            #region Hud
            // HudTextur
            hudTextur = new Texture2D(GraphicsDevice, 1, 1);
            hudTextur.SetData(new Color[] { Color.Gray });

            // Hud Initialisieren
            hud = new Hud(spieler, hudTextur);

            hud.teleport = Content.Load<Texture2D>("teleport");
            hud.checkBox_check = Content.Load<Texture2D>("checkbox_check");
            hud.checkBox_uncheck = Content.Load<Texture2D>("checkbox_uncheck");
            hud.tastaturTextur = Content.Load<Texture2D>("ani_tastatur");

            // Achievement Texturen
            hud.skin_frau = Content.Load<Texture2D>("unlock_skin_frau");
            hud.skin_hut = Content.Load<Texture2D>("unlock_skin_hut");
            hud.skin_einstein = Content.Load<Texture2D>("unlock_skin_einstein");


            hud.gameOverTextur = Content.Load<Texture2D>("GameOver");

            // Game Over Sound Ohh
            hud.soundGameOver = Content.Load<SoundEffect>("ooh");
            hud.soundGameOverSoundInstance = hud.soundGameOver.CreateInstance();


            // Hud Linie
            hud.linieListe.Add(new HindernisS(hindernisTexturS, hindernisTexturS_cheat, new Vector2(0, 0)));
            hud.linieListe.Add(new HindernisS(hindernisTexturS, hindernisTexturS_cheat, new Vector2(320, 0)));
            hud.linieListe.Add(new HindernisS(hindernisTexturS, hindernisTexturS_cheat, new Vector2(2 * 320, 0)));
            hud.linieListe.Add(new HindernisS(hindernisTexturS, hindernisTexturS_cheat, new Vector2(3 * 320, 0)));
            hud.linieListe.Add(new HindernisS(hindernisTexturS, hindernisTexturS_cheat, new Vector2(1280, 0)));


            #endregion

            #region Noten/Punkte und PowerUp
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
            #endregion

            // Logo
            logo = Content.Load<Texture2D>("logo");


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
                    MediaPlayer.Play(musik);
                    spieler.position.X = 1280 / 2 - 128;
                    start = true;
                    gamestate = Gamestate.running;
                }


                #region Tastaturabfrage
                // Teleport bei Curser Taste nach Oben
                if (spieler.teleport && Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    spieler.teleport = false;
                    ((Fallen)spieler.fallen).beschleunigung = 0;
                    spieler.setPlayerPosition(10);
                }

                // Leertaste zum Springen
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    spieler.doSpringen();
                }

                // Wenn Gleiten und Leertaste loslassen dann Fallen
                if (spieler.aktuellerZustand == spieler.gleiten && Keyboard.GetState().IsKeyUp(Keys.Space))
                    spieler.doFallen();



                #endregion


                OldKeyState = NewKeyState;




                #region Kollisionserkennung

                // Erstellen einer Liste mit Hitboxen aus Bereich 2 & 3
                List<Hitbox> kollisionsListe = new List<Hitbox>();
                List<Hitbox> kollisionsListeStacheln = new List<Hitbox>();

                // Hitbox Listen aufbauen
                foreach (Hindernis hindernis in hindernisListe.GetRange(2, 3))
                {
                    // Stacheln Liste erstellen
                    foreach (Hitbox hitbox in hindernis.gibSterben())
                    {
                        kollisionsListeStacheln.Add(hitbox);
                    }

                    //Boden Liste erstellen
                    foreach (Hitbox hitbox in hindernis.gibHitboxen())
                    {
                        kollisionsListe.Add(hitbox);
                    }
                }





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
                        {
                            spieler.teleport = true;
                            spieler.doLaufen();
                        }
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

                #endregion



            }

            #endregion

            #region GamestateRunning

            if (gamestate == Gamestate.running)
            {


                // Hud Update
                hud.UpdateMitTimer(gameTime);

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
                    MediaPlayer.Pause();
                    menue.auswahl = 2; // Auswahl auf Forsetzen bzw. Starten setzten
                    gamestate = Gamestate.menue;
                }

                if (spieler.aktuellerZustand == spieler.laufen && Keyboard.GetState().IsKeyDown(Keys.LeftControl) && Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.Back))
                {
                    hintergrund.aktuelleTextur = hintergrund.hintergrundTexturCheat;
                    spieler.aktuellerZustand = spieler.cheaten;
                    hud.schriftFarbe = Color.White;
                    gamestate = Gamestate.cheat;
                    cheat = true;
                }

                if ((spieler.aktuellerZustand == spieler.sterben || spieler.aktuellerZustand == spieler.gewinnen) && Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    // Song Musik spielen
                    MediaPlayer.Play(titel);

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
                else
                    hindernisListe[hindernisListe.Count - 4].UpdateAnimation();

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
                    }
                    if (hud.gewonnen)
                        hud.UpdateAchievment();
                }

                #endregion

                #region PunkteKollisionen

                if (spieler.aktuellerZustand != spieler.sterben)
                {
                    foreach (NotenHitbox hitbox in punkteListeKollisionen)
                    {
                        if (spieler.hitboxKoerper.Intersects(hitbox.hitboxRect))
                        {
                            notenPlatzerAnimation.index = 0;
                            notenPlatzerPosition = hitbox.hitboxPosition - new Vector2(16, 16);
                            if (hitbox.punkt.wertigkeit != 0)
                            {
                                spieler.punkte += hitbox.punkt.wertigkeit;
                                hitbox.zielPosition += new Vector2(new Random().Next(640), new Random().Next(100));
                                spieler.gesammelteNoten.Add(hitbox);
                                hitbox.platzerAnimation = new Animation(notenPlatzerTextur, 2, 2, 5);
                            }
                            else
                                spieler.teleport = true;
                            hitbox.hindernis.loescheHitboxPunkt(hitbox);
                            hitbox.punkt.playSound();
                        }
                    }
                }
                #endregion

                #region Kollisionserkennung

                // Erstellen einer Liste mit Hitboxen aus Bereich 2 & 3
                List<Hitbox> kollisionsListe = new List<Hitbox>();
                List<Hitbox> kollisionsListeStacheln = new List<Hitbox>();

                // Hitbox Listen aufbauen
                foreach (Hindernis hindernis in hindernisListe.GetRange(2, 3))
                {
                    // Stacheln Liste erstellen
                    foreach (Hitbox hitbox in hindernis.gibSterben())
                    {
                        kollisionsListeStacheln.Add(hitbox);
                    }

                    //Boden Liste erstellen
                    foreach (Hitbox hitbox in hindernis.gibHitboxen())
                    {
                        kollisionsListe.Add(hitbox);
                    }
                }





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
                        if (spieler.position.Y > 378 && spieler.hitboxKopf.Intersects(hitbox.hitboxRect) && spieler.hitboxBeine.Intersects(hitbox.hitboxRect))
                        {
                            menue.spielAktiv = false;
                            ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).klatscher; // Passt den Todeszustand an
                            hud.soundGameOverSoundInstance.Play();
                            spieler.doSterben();
                        }
                        else if (spieler.position.Y <= 378 && spieler.hitboxKopf.Intersects(hitbox.hitboxRect) && spieler.hitboxBeine.Intersects(hitbox.hitboxRect)) // Spieler gegen Taschenrechner
                        {
                            Console.WriteLine("Taschenrechner");
                            menue.spielAktiv = false;
                            Boolean kollidiertBoden = false;
                            ((Sterben)spieler.sterben).aktuell.soundTod.Play();
                            while (!kollidiertBoden && spieler.position.Y < 900)
                            {
                                foreach (Hitbox hb in kollisionsListe)
                                {
                                    if (spieler.hitboxFuss.Intersects(hb.hitboxRect))
                                    {
                                        kollidiertBoden = true;
                                    }
                                }
                                spieler.movePlayerDown(1);
                            }
                            spieler.setPlayerPositionRelativ(-256);
                            ((Sterben)spieler.sterben).aktuell = ((Sterben)spieler.sterben).klatscher_oben; // Passt den Todeszustand an
                            hud.soundGameOverSoundInstance.Play();
                            menue.spielAktiv = false;
                            spieler.doSterben();


                        }
                        else if (spieler.hitboxKopf.Intersects(hitbox.hitboxRect))//  && !spieler.hitboxBeine.Intersects(hitbox.hitboxRect))
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
                        else if (spieler.hitboxBeine.Intersects(hitbox.hitboxRect)) //&& !spieler.hitboxKopf.Intersects(hitbox.hitboxRect))
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

            #region Notenfreilassen nach Tod

            if (spieler.aktuellerZustand == spieler.sterben)
            {
                if (spieler.gesammelteNoten.Count > 0)
                {
                    int zufall = new Random().Next(spieler.gesammelteNoten.Count % 5) + 1;

                    if (gameTime.TotalGameTime.Milliseconds % 500 == 0)
                    {
                        spieler.gesammelteNoten.GetRange(0, zufall).ForEach(delegate(NotenHitbox note)
                        {
                            if (spieler.position.Y > 120)
                            {
                                note.setRichtung(spieler);
                            }
                            else
                                note.setRichtung(spieler, new Vector2(0, 256));
                        });
                        notenFreilassen.AddRange(spieler.gesammelteNoten.GetRange(0, zufall));
                        spieler.gesammelteNoten.RemoveRange(0, zufall);
                    }
                }
                foreach (NotenHitbox note in notenFreilassen)
                {
                    if (note.hitboxPosition.Y <= note.zielPosition.Y)
                        note.faellt = false;
                    note.UpdateFreilassen(spieler);
                }
            }

            #endregion



            // Sterben wenn der Spieler auserhalb des Bildschirms ist
            if (spieler.position.Y >= 800 && menue.spielAktiv)
            {
                menue.spielAktiv = false; // setzt Menü nach Tod wieder auf anfang
                hud.soundGameOverSoundInstance.Play();
                spieler.doSterben();
            }


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
                                    notenhitbox.setPositionY((int)bodenHitbox.hitboxPosition.Y - 33);
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

            #region GamestateCheat

            if (gamestate == Gamestate.cheat)
            {
                // Hud Update
                hud.Update();

                // Update spieler
                spieler.Update();

                #region Spieler Position anpassen

                // Fallen einleiten
                if (hindernisListe.Count == 11)
                {
                    zielInSicht = true;
                }

                // Spieler hochschieben
                if (spieler.position.Y >= 80 && !zielInSicht)
                {
                    spieler.movePlayerUp(1.5f * (float)Math.Sin((spieler.position.Y) / 360));
                }
                else if (spieler.position.Y <= hindernisListe[hindernisListe.Count - 1].hitboxListe[0].hitboxPosition.Y - 110) // Spieler runterschieben
                {
                    spieler.movePlayerDown((float)Math.Sin((1200 - spieler.position.Y) / 1000));
                }
                else // Wenn Spieler im Ziel angekommen ist
                {
                    hintergrund.aktuelleTextur = hintergrund.hintergrundTextur;
                    cheat = false;
                    menue.spielAktiv = false;
                    hud.schriftFarbe = Color.Black;
                    gamestate = Gamestate.running;
                    spieler.doGewinnen(); // Wenn weniger als 6 Hindernisse vorhanden sind gehe in den Gewinnenzustand über
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

                    hintergrund.Update(gameTime, 1);
                }
                else // Wenn Spieler im Ziel, nur noch Zielanimation updaten
                    hindernisListe[hindernisListe.Count - 4].UpdateAnimation();



                #endregion

                #region Noten/Powerup Animation  aktualisierung
                punkt1.punktAnimation.Update();
                punkt2.punktAnimation.Update();
                punkt5.punktAnimation.Update();
                punkt10.punktAnimation.Update();
                powerUp.punktAnimation.Update();
                #endregion



                // Berechne Vektor für Zielbestimmung der Noten
                foreach (NotenHitbox note in punkteListeKollisionen)
                {
                    note.zielPosition = Vector2.Normalize(spieler.position + new Vector2(20, 50) - note.hitboxPosition);
                    note.UpdateSammeln();
                }

                #region Kollisionen von Noten mit Körper

                foreach (NotenHitbox hitbox in punkteListeKollisionen)
                {
                    if (spieler.hitboxKoerper.Intersects(hitbox.hitboxRect))
                    {
                        notenPlatzerAnimation.index = 0;
                        notenPlatzerPosition = hitbox.hitboxPosition - new Vector2(16, 16);
                        if (hitbox.punkt.wertigkeit != 0) // Gesammelte Noten speichern
                        {
                            spieler.punkte += hitbox.punkt.wertigkeit;
                            hitbox.zielPosition += new Vector2(new Random().Next(640), 0);
                            spieler.gesammelteNoten.Add(hitbox);
                        }
                        else
                            spieler.teleport = true;
                        hitbox.hindernis.loescheHitboxPunkt(hitbox);
                        hitbox.punkt.playSound();
                    }
                }

                #endregion
            }
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


                if (MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(titel);
                }

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
                            {
                                MediaPlayer.Resume();
                                gamestate = Gamestate.running;
                            }
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
            GraphicsDevice.Clear(Color.White);


            if (!cheat)
            {
                #region normal
                spriteBatch.Begin(); // Begin

                // Hintergrund zeichnen
                hintergrund.Draw(spriteBatch);

                #region GamestateLoading

                if (gamestate == Gamestate.ladebildschirm)
                {
                    hud.DrawHelp(spriteBatch, schrift_40);
                    spieler.Draw(spriteBatch);
                    hud.Draw(spriteBatch);

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



                }



                #endregion

                #region GamestateRunning

                if (gamestate == Gamestate.running)
                {


                    // Hindernisse zeichnen
                    foreach (Hindernis hindernis in hindernisListe.GetRange(1, 5))
                    {
                        // Male Hindernis Textur
                        spriteBatch.Draw(hindernis.hindernisTextur, hindernis.hindernisPosition, Color.White);
                        // Male Spezial Animationen/Texturen
                        hindernis.DrawAni(spriteBatch);

                        // Male alle Noten
                        foreach (NotenHitbox notenHitbox in hindernis.gibPunkte())
                        {
                            notenHitbox.Draw(spriteBatch);
                        }
                    }


                    notenPlatzerAnimation.Draw(spriteBatch, notenPlatzerPosition);

                    if (notenFreilassen.Count != 0)
                    {
                        foreach (NotenHitbox note in notenFreilassen)
                        {
                            note.Draw(spriteBatch);
                        }
                    }

                    // Zeichne Spieler
                    spieler.Draw(spriteBatch);

                    hud.Draw(spriteBatch, schrift_40, Hindernis.punkteAnzahl, gameTime, cheat, invert);

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
                    // MediaPlayer.Pause();
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

                if (gamestate == Gamestate.options)
                {
                    // Malt alle Animationen des Menüs
                    optionen.Draw(spriteBatch, schrift_40, gewonnen);
                }

                #endregion

                // Logo Zeichnen
                spriteBatch.Draw(logo, new Vector2(0, 720 - 83), Color.White);

                spriteBatch.End(); // End
                #endregion
            }
            else
            {
                #region Invertiert
                spriteBatch.Begin(0, BlendState.NonPremultiplied, null, null, null, invert);
                invert.CurrentTechnique = invert.Techniques[0];

                // Hintergrund zeichnen
                hintergrund.Draw(spriteBatch);

                #region GamestateCheaten

                if (gamestate == Gamestate.cheat)
                {


                    // Hindernisse zeichnen
                    foreach (Hindernis hindernis in hindernisListe.GetRange(1, 5))
                    {
                        spriteBatch.Draw(hindernis.hindernisTexturCheat, hindernis.hindernisPosition, Color.White);
                        //hindernis.DrawAni(spriteBatch);

                        foreach (NotenHitbox notenHitbox in hindernis.gibPunkte())
                        {
                            notenHitbox.Draw(spriteBatch);
                        }
                    }



                    notenPlatzerAnimation.Draw(spriteBatch, notenPlatzerPosition);

                    if (notenFreilassen.Count != 0)
                    {
                        foreach (NotenHitbox note in notenFreilassen)
                        {
                            note.punkt.punktAnimation.Draw(spriteBatch, note.hitboxPosition);
                        }
                    }

                    // Zeichne Spieler
                    spieler.Draw(spriteBatch);
                }
                #endregion

                spriteBatch.End();





                spriteBatch.Begin(); // Begin

                hud.Draw(spriteBatch, schrift_40, Hindernis.punkteAnzahl, gameTime, cheat, invert);

                spriteBatch.End(); // End



                #endregion
            }
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

            if (standardSkin.cheatenAnimation == null) standardSkin.cheatenAnimation = new Animation(standardSkin.cheatenTextur, 4, 2, 6);

            if (standardSkin.sterbenAnimationKoepfen == null) standardSkin.sterbenAnimationKoepfen = new Animation(standardSkin.sterbenTexturKoepfen, 2, 5, 6);
            if (standardSkin.sterbenAnimationStolpern == null) standardSkin.sterbenAnimationStolpern = new Animation(standardSkin.sterbenTexturStolpern, 2, 5, 6);
            if (standardSkin.sterbenAnimationKlatscher == null) standardSkin.sterbenAnimationKlatscher = new Animation(standardSkin.sterbenTexturKlatscher, 12, 1, 7);
            if (standardSkin.sterbenAnimationKlatscherOben == null) standardSkin.sterbenAnimationKlatscherOben = new Animation(standardSkin.sterbenTexturKlatscherOben, 6, 2, 7);
            if (standardSkin.sterbenAnimationPieksen == null) standardSkin.sterbenAnimationPieksen = new Animation(standardSkin.sterbenTexturPieksen, 2, 5, 6);

            // Fraud Skin
            if (frauenSkin.duckenAnimation == null) frauenSkin.duckenAnimation = new Animation(frauenSkin.duckenTextur, 4, 4, 4);
            if (frauenSkin.fallenAnimation == null) frauenSkin.fallenAnimation = new Animation(frauenSkin.fallenTextur, 2, 2, 6);
            if (frauenSkin.gewinnenAnimation == null) frauenSkin.gewinnenAnimation = new Animation(frauenSkin.gewinnenTextur, 5, 2, 6);
            if (frauenSkin.gleitenAnimation == null) frauenSkin.gleitenAnimation = new Animation(frauenSkin.gleitenTextur, 2, 2, 6);
            if (frauenSkin.laufenAnimation == null) frauenSkin.laufenAnimation = new Animation(frauenSkin.laufenTextur, 4, 3, 3);
            if (frauenSkin.sprignenAnimation == null) frauenSkin.sprignenAnimation = new Animation(frauenSkin.sprignenTextur, 4, 2, 6);

            if (frauenSkin.cheatenAnimation == null) frauenSkin.cheatenAnimation = new Animation(frauenSkin.cheatenTextur, 4, 2, 6);

            if (frauenSkin.sterbenAnimationKoepfen == null) frauenSkin.sterbenAnimationKoepfen = new Animation(frauenSkin.sterbenTexturKoepfen, 2, 5, 6);
            if (frauenSkin.sterbenAnimationStolpern == null) frauenSkin.sterbenAnimationStolpern = new Animation(frauenSkin.sterbenTexturStolpern, 2, 5, 6);
            if (frauenSkin.sterbenAnimationKlatscher == null) frauenSkin.sterbenAnimationKlatscher = new Animation(frauenSkin.sterbenTexturKlatscher, 12, 1, 7);
            if (frauenSkin.sterbenAnimationKlatscherOben == null) frauenSkin.sterbenAnimationKlatscherOben = new Animation(frauenSkin.sterbenTexturKlatscherOben, 6, 2, 7);
            if (frauenSkin.sterbenAnimationPieksen == null) frauenSkin.sterbenAnimationPieksen = new Animation(frauenSkin.sterbenTexturPieksen, 2, 5, 6);

            // Hut Skin
            if (hutSkin.duckenAnimation == null) hutSkin.duckenAnimation = new Animation(hutSkin.duckenTextur, 4, 4, 4);
            if (hutSkin.fallenAnimation == null) hutSkin.fallenAnimation = new Animation(hutSkin.fallenTextur, 2, 2, 6);
            if (hutSkin.gewinnenAnimation == null) hutSkin.gewinnenAnimation = new Animation(hutSkin.gewinnenTextur, 5, 2, 6);
            if (hutSkin.gleitenAnimation == null) hutSkin.gleitenAnimation = new Animation(hutSkin.gleitenTextur, 2, 2, 6);
            if (hutSkin.laufenAnimation == null) hutSkin.laufenAnimation = new Animation(hutSkin.laufenTextur, 4, 3, 3);
            if (hutSkin.sprignenAnimation == null) hutSkin.sprignenAnimation = new Animation(hutSkin.sprignenTextur, 4, 2, 6);

            if (hutSkin.cheatenAnimation == null) hutSkin.cheatenAnimation = new Animation(hutSkin.cheatenTextur, 4, 2, 6);

            if (hutSkin.sterbenAnimationKoepfen == null) hutSkin.sterbenAnimationKoepfen = new Animation(hutSkin.sterbenTexturKoepfen, 2, 5, 6);
            if (hutSkin.sterbenAnimationStolpern == null) hutSkin.sterbenAnimationStolpern = new Animation(hutSkin.sterbenTexturStolpern, 2, 5, 6);
            if (hutSkin.sterbenAnimationKlatscher == null) hutSkin.sterbenAnimationKlatscher = new Animation(hutSkin.sterbenTexturKlatscher, 12, 1, 7);
            if (hutSkin.sterbenAnimationKlatscherOben == null) hutSkin.sterbenAnimationKlatscherOben = new Animation(hutSkin.sterbenTexturKlatscherOben, 6, 2, 7);
            if (hutSkin.sterbenAnimationPieksen == null) hutSkin.sterbenAnimationPieksen = new Animation(hutSkin.sterbenTexturPieksen, 2, 5, 6);

            // Einstein Skin
            if (einsteinSkin.duckenAnimation == null) einsteinSkin.duckenAnimation = new Animation(einsteinSkin.duckenTextur, 4, 4, 4);
            if (einsteinSkin.fallenAnimation == null) einsteinSkin.fallenAnimation = new Animation(einsteinSkin.fallenTextur, 2, 2, 6);
            if (einsteinSkin.gewinnenAnimation == null) einsteinSkin.gewinnenAnimation = new Animation(einsteinSkin.gewinnenTextur, 5, 2, 6);
            if (einsteinSkin.gleitenAnimation == null) einsteinSkin.gleitenAnimation = new Animation(einsteinSkin.gleitenTextur, 2, 2, 6);
            if (einsteinSkin.laufenAnimation == null) einsteinSkin.laufenAnimation = new Animation(einsteinSkin.laufenTextur, 4, 3, 3);
            if (einsteinSkin.sprignenAnimation == null) einsteinSkin.sprignenAnimation = new Animation(einsteinSkin.sprignenTextur, 4, 2, 6);

            if (einsteinSkin.cheatenAnimation == null) einsteinSkin.cheatenAnimation = new Animation(einsteinSkin.cheatenTextur, 4, 2, 6);

            if (einsteinSkin.sterbenAnimationKoepfen == null) einsteinSkin.sterbenAnimationKoepfen = new Animation(einsteinSkin.sterbenTexturKoepfen, 2, 5, 6);
            if (einsteinSkin.sterbenAnimationStolpern == null) einsteinSkin.sterbenAnimationStolpern = new Animation(einsteinSkin.sterbenTexturStolpern, 2, 5, 6);
            if (einsteinSkin.sterbenAnimationKlatscher == null) einsteinSkin.sterbenAnimationKlatscher = new Animation(einsteinSkin.sterbenTexturKlatscher, 12, 1, 7);
            if (einsteinSkin.sterbenAnimationKlatscherOben == null) einsteinSkin.sterbenAnimationKlatscherOben = new Animation(einsteinSkin.sterbenTexturKlatscherOben, 6, 2, 7);
            if (einsteinSkin.sterbenAnimationPieksen == null) einsteinSkin.sterbenAnimationPieksen = new Animation(einsteinSkin.sterbenTexturPieksen, 2, 5, 6);

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

            // Hud Game Over

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

            cheat = false;
            zielInSicht = false;
            notenFreilassen.Clear();

            spieler.aktuellerSkin = optionen.skinListe[optionen.auswahl];

            spieler.aktuellerSkin.sterbenAnimationKlatscher.index = 0;
            spieler.aktuellerSkin.sterbenAnimationKlatscherOben.index = 0;
            spieler.aktuellerSkin.sterbenAnimationKoepfen.index = 0;
            spieler.aktuellerSkin.sterbenAnimationStolpern.index = 0;
            spieler.aktuellerSkin.sterbenAnimationPieksen.index = 0;

            konfig.ReadFileAnalyse("analyse.txt", this);
            // HindernisListe generieren


            // Anzahl der Pixel pro Hindernis / Pixel Verschiebungen Pro update / 60 Updates Pro Sekunde = Zeit für Hindernis um in den Bildschirm geschoben zu werden in Sekunden
            // 320                              /       4                       /       60              = 1,3333 sekunden = 1333,3333

            // Millisekunden pro Lied   /  Millisekunden pro Hindernis  = Notwendige Hindernisse
            // 239621                   /            1333 = 179

            // 

            hindernisListe = Hindernis.generieHindernisse(liedlaenge / 1334 - 10, this);


            // Pixelverschiebungen  * 60 Updates Pro sekunde     / 1000 = Pixel pro Millisekunde
            //    4                * 60                         / 1000 = 0,24            


            foreach (int k in liedWerte)
            {
                int i = k + 512;
                int hindernisIndex = (int)(i * 0.24f) / 320;
                int notenPosition = (int)(i * 0.24f) - 320 * hindernisIndex;

                switch ((int)rand.Next(5))
                {
                    case 0:
                        hindernisListe[hindernisIndex].noteHinzufuegen(new NotenHitbox(punkt1, null, notenPosition, 300, 32, 32));
                        Hindernis.punkteAnzahl += 1;
                        break;
                    case 1:
                        hindernisListe[hindernisIndex].noteHinzufuegen(new NotenHitbox(punkt2, null, notenPosition, 300, 32, 32));
                        Hindernis.punkteAnzahl += 2;
                        break;
                    case 2:
                        hindernisListe[hindernisIndex].noteHinzufuegen(new NotenHitbox(punkt5, null, notenPosition, 300, 32, 32));
                        Hindernis.punkteAnzahl += 5;
                        break;
                    case 3:
                        hindernisListe[hindernisIndex].noteHinzufuegen(new NotenHitbox(punkt10, null, notenPosition, 300, 32, 32));
                        Hindernis.punkteAnzahl += 10;
                        break;
                    case 4:
                        if (rand.Next() > 0.9)
                            hindernisListe[hindernisIndex].noteHinzufuegen(new NotenHitbox(powerUp, null, notenPosition, 300, 32, 32));
                        else
                        {
                            hindernisListe[hindernisIndex].noteHinzufuegen(new NotenHitbox(punkt5, null, notenPosition, 300, 32, 32));
                            Hindernis.punkteAnzahl += 5;
                        }
                        break;
                }

            }

            liedWerte.Clear();
        }

    }
}
