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

        // Skin Texturen
        public Skin standardSkin, frauSkin, hutSkin, einsteinSkin;

        // Skin Liste
        public List<Skin> skinListe;

        public Optionen()
        {
            z_knopf_position = new Vector2(50,550);

            skinListe = new List<Skin>();
            skinListe.Add(standardSkin);
            skinListe.Add(frauSkin);
            skinListe.Add(hutSkin);
            skinListe.Add(einsteinSkin);
        }


        public void Update()
        { 
            
        }


        public void Draw(SpriteBatch sb)
        {
            // Zurück Knopf
            sb.Draw(z_knopf_Textur, z_knopf_position, Color.White);



        }
    }


}
