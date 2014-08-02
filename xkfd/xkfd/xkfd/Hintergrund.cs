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
    public class Hintergrund
    {

        // Textur und Position für Hintergrund
        public Texture2D hintergrundTextur;
        public Texture2D hintergrundTexturCheat;
        public Texture2D aktuelleTextur;

        /*
        public Vector2 hintegrundPosition1;
        public Vector2 hintegrundPosition2;
        public Vector2 hintegrundPosition3;*/

        private int xPos = 0;
        

        public Hintergrund()
        {
            aktuelleTextur = hintergrundTextur;
        }

        public void Update()
        {
            xPos = (xPos - 4 + 1024) % 1024 - 1024;
        }

        public void Update(int geschwindigkeit)
        {
            xPos = (xPos - geschwindigkeit + 1024) % 1024 - 1024;

            /*
            hintegrundPosition.X -= geschwindigkeit;
            if (hintegrundPosition.X <= -1024)
                hintegrundPosition.X = 0;*/
        }

        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(hintergrundTextur, hintegrundPosition, Color.White);
            sb.Draw(aktuelleTextur, new Vector2(xPos, 0), Color.White);
            sb.Draw(aktuelleTextur, new Vector2(xPos + 1024, 0), Color.White);
            sb.Draw(aktuelleTextur, new Vector2(xPos + 2048, 0), Color.White);
            sb.Draw(aktuelleTextur, new Vector2(xPos + 3072, 0), Color.White);
        }
    }
}
