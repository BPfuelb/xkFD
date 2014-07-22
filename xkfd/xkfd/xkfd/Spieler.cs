using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        private Zustand aktuellerZustand;

        // Punktestand
        private int punkte;

        // Spieler Position
        public Vector2 position;

        // Hitbox
        private Rectangle hitbox;

        // Spieler Textur zum Testen
        public Texture2D spielerTextur;

        // Konstruktor
        public Spieler(Game1 game1)
        {
            laufen = new Laufen(this);
            springen = new Springen(this);
            ducken = new Ducken(this);
            gleiten = new Gleiten(this);
            sterben = new Sterben(this);
            gewinnen = new Gewinnen(this);
            fallen = new Fallen(this);

            aktuellerZustand = laufen;

            punkte = 0; // Punktestand initialisieren

            position = new Vector2(1280 / 2 - 128, 720 / 2);
        }


        public void setZustand(Zustand zustand)
        {
            this.aktuellerZustand = zustand;
        }


        // Rufe an Aktuellem Zustand Aktion auf
        #region AktionsAufrufe

        public void doLaufen()
        {
            aktuellerZustand.laufen();
        }

        public void doSpringen()
        {
            aktuellerZustand.springen();
        }

        public void doDucken()
        {
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
    }
}
