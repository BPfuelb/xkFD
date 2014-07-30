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

        public void Draw(SpriteBatch sb)
        {
            // Zurück Knopf
            sb.Draw(z_knopf_Textur, z_knopf_position, Color.White);

            skinListe[0].laufenAnimation.Draw(sb, new Vector2(100, 50));
            skinListe[1].laufenAnimation.Draw(sb, new Vector2(100, 50 + 120 ));
            skinListe[2].laufenAnimation.Draw(sb, new Vector2(100, 50 + 120 + 120 ));
            skinListe[3].laufenAnimation.Draw(sb, new Vector2(100, 50 + 120  + 120 + 120));
        }
    }


}
