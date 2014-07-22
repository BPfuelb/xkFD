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
        Vector2 optionenPosition;
        Vector2 exitPosition;

        // Texturen für Knöpfe
        public Texture2D startTextur;
        public Texture2D optionenTexture;
        public Texture2D exitTexture;

        // Animationen
        public Animation start_m_ani;
        public Animation option_m_ani;
        public Animation exit_m_ani;

        // Auswahl Cursor
        public int auswahl;

        public Menue()
        {
            auswahl = 0; 

            startPosition = new Vector2(128, 80);
            optionenPosition = new Vector2(128, 80 + 160);
            exitPosition = new Vector2(128, 80 + 160 + 160);

            /*
            start_m_ani = new Animation(startTextur, 1, 4, 4);
            option_m_ani = new Animation(optionenTexture, 1, 4, 4);
            exit_m_ani = new Animation(exitTexture, 1, 4, 4);
             */
        }

        public void Update()
        {
            if(auswahl == 0)
            start_m_ani.Update();
            if (auswahl == 1)
            option_m_ani.Update();
            if (auswahl == 2)
            exit_m_ani.Update();
        }

        public void Draw(SpriteBatch sb)
        {
            start_m_ani.Draw(sb, startPosition);
            option_m_ani.Draw(sb, optionenPosition);
            exit_m_ani.Draw(sb, exitPosition);
        }

        public void nextMenue()
        {
            auswahl = (auswahl + 1) % 3;
        }

        public void prevMenue()
        {
            auswahl = ((auswahl - 1 + 3) % 3);
        }

    }
}
