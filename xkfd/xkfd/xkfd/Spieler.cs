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
    class Spieler
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

        // Skin

        public Skin skin; 

        // Punktestand
        private int punkte;

        // Spieler Position
        public Vector2 position;

        // Gleitenresource
        public int gleitenResource;


        public Rectangle hitboxFussRechts;
        public Rectangle hitboxFuss;
        public Rectangle linksOben;
        public Rectangle hitboxKopf;
        public Rectangle hitboxBeine;

        public Boolean teleport;

        
        // Konstruktor
        public Spieler()
        {
            /*                     x-Pos           y-Pos = Mitte + Hoehe der Laufen-Textur */
            position = new Vector2(1280 / 2 - 128, 720 / 2 + 60);

            laufen = new Laufen(this);
            springen = new Springen(this);
            ducken = new Ducken(this);
            gleiten = new Gleiten(this);
            sterben = new Sterben(this);
            gewinnen = new Gewinnen(this);
            fallen = new Fallen(this);

            aktuellerZustand = laufen; // Startzustand

            punkte = 0; // Punktestand initialisieren

            teleport = true;
            
            hitboxKopf = new Rectangle((int)position.X + 50, (int)position.Y, 10, 40);
            linksOben = new Rectangle((int)position.X, (int)position.Y, 10, 10);
            hitboxBeine = new Rectangle((int)position.X + 50, (int)position.Y +40 , 10, 40);

            // hitboxFussRechts = new Rectangle((int)position.X, (int)position.Y + 110 , 10, 10);
            hitboxFuss = new Rectangle((int)position.X, (int)position.Y + 110, 50, 10);
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

            if (zustand == gleiten)
                Console.WriteLine("Zustand: Gleiten");

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
            hitboxFuss.Y -= y * faktor;
            hitboxBeine.Y -= y * faktor;
        }

        public void movePlayerDown(int y)
        {
            const int faktor = 1;
            position.Y += y * faktor;
            linksOben.Y += y * faktor;

            hitboxKopf.Y += y * faktor;
            hitboxFussRechts.Y += y * faktor;
            hitboxFuss.Y += y * faktor;
            hitboxBeine.Y += y * faktor;
        }

        public void setPlayerPosition(int y)
        {
            position.Y = y;
            linksOben.Y = y;


            if (aktuellerZustand != ducken)
                hitboxKopf.Y = (int)position.Y ;
            else
                hitboxKopf.Y = (int)position.Y + 40;

            hitboxFuss.Y = (int)position.Y + 110;
            hitboxBeine.Y = (int)position.Y + 60;
        }
    }
}
