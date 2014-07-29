using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace xkfd
{
    unsafe class Spieler
    {
        // Zustände
        public Zustand laufen;
        public Zustand springen;
        public Zustand ducken;
        public Zustand gleiten;
        public Zustand sterben;
        public Zustand gewinnen;
        public Zustand fallen;

        public Zustand aktuellerZustand;

        // Punktestand
        private int punkte;

        // Spieler Position
        public Vector2 position;

        // Gleitenresource
        public int gleitenResource;


        public Rectangle hitboxFussRechts;
        public Rectangle hitboxFussLinks;
        public Rectangle linksOben;
        public Rectangle hitboxKopf;

        // Spieler Textur zum Testen
        // public Texture2D spielerTextur;

        // Konstruktor
        public Spieler()
        {
            /*                     x-Pos           y-Pos = Mitte + Hoehe der Laufen-Textur */
            position = new Vector2(1280 / 2 - 128, 720 / 2);

            laufen = new Laufen(this);
            springen = new Springen(this);
            ducken = new Ducken(this);
            gleiten = new Gleiten(this);
            sterben = new Sterben(this);
            gewinnen = new Gewinnen(this);
            fallen = new Fallen(this);

            aktuellerZustand = laufen;

            punkte = 0; // Punktestand initialisieren

            hitboxKopf = new Rectangle((int)position.X + 100, (int)position.Y + 70, 10, 90);
            linksOben = new Rectangle((int)position.X, (int)position.Y, 10, 10);
            hitboxFussRechts = new Rectangle((int)position.X + 100, (int)position.Y + 170, 10, 10);
            hitboxFussLinks = new Rectangle((int)position.X + 20, (int)position.Y + 170, 80, 10);
        }


        public void setZustand(Zustand zustand)
        {
            if (zustand == laufen)
                Console.WriteLine("Zustand: Laufen");

            if (zustand == springen)
                Console.WriteLine("Zustand: Springen");

            if (zustand == fallen)
                Console.WriteLine("Zustand: Fallen");
            
            if (zustand == ducken)
                Console.WriteLine("Zustand: Ducken");
            
            if (zustand == sterben)
                Console.WriteLine("Zustand: Sterben");

            if (zustand == gewinnen)
                Console.WriteLine("Zustand: Gewinnen");

            this.aktuellerZustand = zustand;
        }


        // Rufe an Aktuellem Zustand Aktion auf
        #region AktionsAufrufe

        public void doLaufen()
        {
            // Console.WriteLine("doLaufen");
            aktuellerZustand.laufen();
        }

        public void doSpringen()
        {
            // Console.WriteLine("doSpringen");
            aktuellerZustand.springen();
        }

        public void doDucken()
        {
           //  Console.WriteLine("doDucken");
            aktuellerZustand.ducken();
        }

        public void doSterben()
        {
            aktuellerZustand.sterben();
        }

        public void doGewinnen()
        {
            aktuellerZustand.gewinnen();
        }

        public void doFallen()
        {
           //  Console.WriteLine("doFallen");
            aktuellerZustand.fallen();
        }

        public void doGleiten()
        {
            aktuellerZustand.gleiten();
        }
        #endregion


        public void Update()
        {
            aktuellerZustand.update();
        }

        public void Draw(SpriteBatch sb)
        {
            aktuellerZustand.Draw(sb);
        }

        public void movePlayerUp(int y)
        {
            const int faktor = 2;
            position.Y -= y * faktor;
            linksOben.Y -= y * faktor;

            hitboxKopf.Y -= y * faktor;
            hitboxFussRechts.Y -= y * faktor;
            hitboxFussLinks.Y -= y * faktor;
        }

        public void movePlayerDown(int y)
        {
            const int faktor = 1;
            position.Y += y * faktor;
            linksOben.Y += y * faktor;

            hitboxKopf.Y += y * faktor;
            hitboxFussRechts.Y += y * faktor;
            hitboxFussLinks.Y += y * faktor;
        }

        public void setPlayerPosition(int y)
        {
            position.Y = y;
            linksOben.Y = y;

            hitboxKopf.Y = (int)position.Y + 70;
            hitboxFussRechts.Y = (int)position.Y + 170;
            hitboxFussLinks.Y = (int)position.Y + 170;
        }
    }
}
