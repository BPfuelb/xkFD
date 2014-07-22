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


        public Optionen()
        {
            z_knopf_position = new Vector2(50,550);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(z_knopf_Textur, z_knopf_position, Color.White);
        }
    }


}
