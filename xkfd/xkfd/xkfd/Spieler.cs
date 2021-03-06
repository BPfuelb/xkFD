﻿using System;
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
    public class Spieler
    {
        // Zustände
        public Zustand laufen;
        public Zustand springen;
        public Zustand ducken;
        public Zustand gleiten;
        public Zustand sterben;
        public Zustand gewinnen;
        public Zustand fallen;
        public Zustand cheaten;

        public Zustand aktuellerZustand;

        // Skin

        public Skin aktuellerSkin; 

        // Punktestand
        public int punkte;

        // Spieler Position
        public Vector2 position;
        
        // Spieler Position Visualisierung
        public Rectangle spielerPosition;

        // Gleitenresource
        public int gleitenResource;

        // Spieler Hitboxen
        public Rectangle hitboxFuss;
        public Rectangle hitboxKopf;
        public Rectangle hitboxBeine;
        public Rectangle hitboxKoerper;

        // Teleport Resource
        public Boolean teleport;

        public List<NotenHitbox> gesammelteNoten;

        // float für hitboxen
        float count;

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
            cheaten = new Cheaten(this);

            aktuellerZustand = laufen; // Startzustand

            punkte = 0; // Punktestand initialisieren

            teleport = true;
            
            hitboxKopf = new Rectangle((int)position.X + 50, (int)position.Y, 10, 40);
            spielerPosition = new Rectangle((int)position.X, (int)position.Y, 10, 10);
            hitboxBeine = new Rectangle((int)position.X + 50, (int)position.Y +40 , 10, 40);
            hitboxFuss = new Rectangle((int)position.X+20, (int)position.Y + 110, 30, 10);
            hitboxKoerper = new Rectangle((int)position.X +10, (int)position.Y, 40, 100);

            gesammelteNoten = new List<NotenHitbox>();
        }


        public void setZustand(Zustand zustand)
        {
            /*
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
             */

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
            spielerPosition.Y -= y * faktor;

            hitboxKopf.Y -= y * faktor;
            hitboxFuss.Y -= y * faktor;
            hitboxBeine.Y -= y * faktor;
            hitboxKoerper.Y -= y * faktor;
        }

        public void movePlayerUp(float y)
        {
            count += y;

            position.Y -= y ;

            if (count >= 1)
            {
                spielerPosition.Y -= 1;

                hitboxKopf.Y -= 1;
                hitboxFuss.Y -= 1;
                hitboxBeine.Y -= 1;
                hitboxKoerper.Y -= 1;
                count--;
            }
        }

        public void movePlayerDown(int y)
        {
            const int faktor = 1;
            position.Y += y * faktor;
            spielerPosition.Y += y * faktor;

            hitboxKopf.Y += y * faktor;
            hitboxFuss.Y += y * faktor;
            hitboxBeine.Y += y * faktor;
            hitboxKoerper.Y += y * faktor;
        }

        public void movePlayerDown(float y)
        {
            position.Y += y;

            count += y;
            if (count >= 1)
            {
                spielerPosition.Y += 1;

                hitboxKopf.Y += 1;
                hitboxFuss.Y += 1;
                hitboxBeine.Y += 1;
                hitboxKoerper.Y += 1;
                count--;
            }
        }

        public void setPlayerPosition(int y)
        {
            position.Y = y;
            spielerPosition.Y = y;


            if (aktuellerZustand != ducken)
                hitboxKopf.Y = (int)position.Y ;
            else
                hitboxKopf.Y = (int)position.Y + 40;

            hitboxFuss.Y = (int)position.Y + 110;
            hitboxBeine.Y = (int)position.Y + 60;
            hitboxKoerper.Y = (int)position.Y +5;
        }

        public void setPlayerPositionRelativ(int y)
        {
            spielerPosition.Y += y;
            position.Y = spielerPosition.Y;
            


            if (aktuellerZustand != ducken)
                hitboxKopf.Y = (int)position.Y;
            else
                hitboxKopf.Y = (int)position.Y + 40 ;

            hitboxFuss.Y = (int)position.Y + 110;
            hitboxBeine.Y = (int)position.Y + 60 ;
            hitboxKoerper.Y = (int)position.Y + 5 ;
        }
    }
}
