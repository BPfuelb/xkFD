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
    class Hintergrund
    {

        // Textur und Position für Hintergrund
        public Texture2D hintergrundTextur;
        public Texture2D hintergrundTexturCheat;
        public Texture2D aktuelleTextur;

        public Vector2 hintegrundPosition;

        public Hintergrund()
        {
            aktuelleTextur = hintergrundTextur;
            hintegrundPosition = new Vector2(0, 0);
        }

        public void Update()
        {
            hintegrundPosition.X -= 4 ;
            if (hintegrundPosition.X <= -1024)
                hintegrundPosition.X = 0;
        }

        public void Update(int geschwindigkeit)
        {
            hintegrundPosition.X -= geschwindigkeit;
            if (hintegrundPosition.X <= -1024)
                hintegrundPosition.X = 0;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(hintergrundTextur, hintegrundPosition, Color.White);
        }
    }
}
