using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace xkfd
{
    class Menue
    {
        // Positionen für Knöpfe
        Vector2 startPosition;
        Vector2 neuPosition;
        Vector2 fortsetzenPosition;
        Vector2 optionenPosition;
        Vector2 exitPosition;

        // Texturen für Knöpfe
        public Texture2D startTextur;
        public Texture2D optionenTexture;
        public Texture2D exitTexture;
        public Texture2D neuTexture;
        public Texture2D fortsetzenTexture;


        // Animationen
        public Animation start_m_ani;
        public Animation option_m_ani;
        public Animation exit_m_ani;
        public Animation neu_m_ani;
        public Animation fortsetzen_m_ani;

        // Läuft bereits ein spiel?
        public Boolean spielAktiv;

        // Auswahl Cursor
        public int auswahl;

        public Menue()
        {
            auswahl = 2; // Initialisierung Auswahl
            spielAktiv = false;

            // Positionen für Start/neuesSpiel/Forsetzen gleich
            startPosition = new Vector2(128, 80);
            neuPosition = new Vector2(128, 80);
            fortsetzenPosition = new Vector2(128, 80);

            optionenPosition = new Vector2(128, 80 + 160);
            exitPosition = new Vector2(128, 80 + 160 + 160);
        }

        public void Update()
        {
            // Aktives Feld Updaten
            if (auswahl == 0)
                exit_m_ani.Update();
            if (auswahl == 1)
                option_m_ani.Update();
            if (auswahl == 2 && !spielAktiv)
                start_m_ani.Update();
            if (auswahl == 2 && spielAktiv)
                fortsetzen_m_ani.Update();
            if (auswahl == 3 && spielAktiv)
                neu_m_ani.Update();

        }

        public void Draw(SpriteBatch sb)
        {
            // Wenn noch kein Spiel läuft nur Start anbieten
            if (!spielAktiv)
                start_m_ani.Draw(sb, startPosition);
            else        // Wenn ein Spiel läuft Neu und Fortsetzten anbieten
            {
                if (auswahl == 3)
                    neu_m_ani.Draw(sb, neuPosition);
                else
                    fortsetzen_m_ani.Draw(sb, fortsetzenPosition);
            }
            option_m_ani.Draw(sb, optionenPosition);
            exit_m_ani.Draw(sb, exitPosition);
        }

        // Im Menü nach Oben 
        public void prevMenue()
        {
            if (auswahl == 3)
                auswahl--;
            auswahl = (auswahl + 1) % 3;
        }
        // Im Menü nach Unten
        public void nextMenue()
        {
            if (auswahl == 3)
                auswahl--;
            auswahl = ((auswahl - 1 + 3) % 3);
        }
        // Im Menü wo möglich nach Links
        public void leftMenue()
        {
            if (auswahl == 3)
                auswahl--;
        }
        // Im Menü wo möglich nach Rechts
        public void rightMenue()
        {
            if (auswahl == 2)
                auswahl++;
        }


    }
}
