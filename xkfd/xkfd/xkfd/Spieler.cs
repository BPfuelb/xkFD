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

        // Punktestand
        private int punkte;

        // Spieler Position
        public Vector2 position;



        // Spieler Textur zum Testen
        // public Texture2D spielerTextur;

        // Konstruktor
        public Spieler()
        {
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
            springen.hitbox.X = (int)position.X + 42;
            springen.hitbox.Y = (int)position.Y + 63;
            aktuellerZustand.springen();
        }

        public void doDucken()
        {
            ducken.hitbox.X = (int)position.X + 42;
            ducken.hitbox.Y = (int)position.Y + 63;
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
            fallen.hitbox.X = (int)position.X + 42;
            fallen.hitbox.Y = (int)position.Y + 63;
            aktuellerZustand.fallen();
        }

        public void doGleiten()
        {
            gleiten.hitbox.X = (int)position.X + 42;
            gleiten.hitbox.Y = (int)position.Y + 63;

            aktuellerZustand.gleiten();
        }
        #endregion


        public void Update()
        {

            aktuellerZustand.hitbox.X = (int)position.X + 42;
            aktuellerZustand.hitbox.Y = (int)position.Y + 63;
            aktuellerZustand.update();
        }

        public void Draw(SpriteBatch sb)
        {
            aktuellerZustand.Draw(sb);
        }
    }
}
