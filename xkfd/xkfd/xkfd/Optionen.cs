﻿using System;
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
    class Optionen
    {
        // Zurück Knopf position
        public Vector2 z_knopf_position;

        // Zurück Knopf textur
        public Texture2D z_knopf_Textur;

        // Skin Liste
        public List<Skin> skinListe;

        public int auswahl;

        public Optionen()
        {
            z_knopf_position = new Vector2(50, 550);
            skinListe = new List<Skin>();

            auswahl = 0; 
        }

        public void skinHinzufuegen(Skin skin)
        {
            skinListe.Add(skin);
        }

        public void Update()
        {
            skinListe[auswahl].laufenAnimation.Update();
        }

        public void Draw(SpriteBatch sb, SpriteFont schrift, int gewonnen)
        {
            // Zurück Knopf
            sb.Draw(z_knopf_Textur, z_knopf_position, Color.White);

            skinListe[0].laufenAnimation.Draw(sb, new Vector2(100, 50));
            sb.DrawString(schrift, "Standard Skin", new Vector2(300, 55), Color.Gray);
            
            skinListe[1].laufenAnimation.Draw(sb, new Vector2(100, 170 ));
            if(gewonnen >= 1)
                sb.DrawString(schrift, "Weiblicher Skin", new Vector2(300, 175), Color.Gray);
            else
                sb.DrawString(schrift, "noch 1 mal Gewinnen", new Vector2(300, 175), Color.Gray);

            skinListe[2].laufenAnimation.Draw(sb, new Vector2(100, 290 ));
            if (gewonnen >= 5)
                sb.DrawString(schrift, "Hut Skin", new Vector2(300, 295), Color.Gray);
            else
                sb.DrawString(schrift, "noch " + (5 - gewonnen) + " mal Gewinnen", new Vector2(300, 295), Color.Gray);

            skinListe[3].laufenAnimation.Draw(sb, new Vector2(100, 410));
            
            if (gewonnen >= 10)
                sb.DrawString(schrift, "Einstein Skin", new Vector2(300, 415), Color.Gray);
            else
                sb.DrawString(schrift, "noch " + (10 - gewonnen) + " mal Gewinnen", new Vector2(300, 415), Color.Gray);
        }
    }


}
