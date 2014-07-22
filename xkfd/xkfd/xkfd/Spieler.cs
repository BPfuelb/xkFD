using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
        private Vector2 position;

        // Hitbox
        private Rectangle hitbox;

        // Konstruktor
        public Spieler()
        {
            laufen = new Laufen(this);
            springen = new Springen(this);
            ducken = new Ducken(this);
            gleiten = new Gleiten(this);
            sterben = new Sterben(this);
            gewinnen = new Gewinnen(this);
            fallen = new Gewinnen(this);

            punkte = 0; // Punktestand initialisieren
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
    }
}
