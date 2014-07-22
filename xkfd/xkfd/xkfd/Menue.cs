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
            auswahl = 0;
            spielAktiv = false;

            startPosition = new Vector2(128, 80);

            neuPosition = new Vector2(128, 80);
            fortsetzenPosition = new Vector2(128, 80);

            optionenPosition = new Vector2(128, 80 + 160);
            exitPosition = new Vector2(128, 80 + 160 + 160);
        }

        public void Update()
        {
            if (!spielAktiv)
            {
                if (auswahl == 0)
                    start_m_ani.Update();
                if (auswahl == 1)
                    option_m_ani.Update();
                if (auswahl == 2)
                    exit_m_ani.Update();
            }
            else
            {
                if (auswahl == 1)
                    neu_m_ani.Update();
                if (auswahl == 0)
                    fortsetzen_m_ani.Update();
                if (auswahl == 2)
                    option_m_ani.Update();
                if (auswahl == 3)
                    exit_m_ani.Update();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (!spielAktiv)
                start_m_ani.Draw(sb, startPosition);
            else
            {
                if (auswahl == 0)
                    fortsetzen_m_ani.Draw(sb, fortsetzenPosition);
                else
                    neu_m_ani.Draw(sb, neuPosition);
            }
            option_m_ani.Draw(sb, optionenPosition);
            exit_m_ani.Draw(sb, exitPosition);
        }

        public void nextMenue()
        {
            if (!spielAktiv)
                auswahl = (auswahl + 1) % 3;
            else
            {
                if (auswahl == 0)
                    auswahl++;
                auswahl = (auswahl + 1) % 4;
            }
        }

        public void prevMenue()
        {
            if (!spielAktiv)
                auswahl = ((auswahl - 1 + 3) % 3);
            else
            {
                if (auswahl == 1)
                    auswahl--;
                auswahl = ((auswahl - 1 + 4) % 4);
            }
        }

        public  void leftMenue()
        {
            if (auswahl == 0)
                auswahl++;
        }

         public  void rightMenue()
        {
            if (auswahl == 1)
                auswahl--;
        }


    }
}
