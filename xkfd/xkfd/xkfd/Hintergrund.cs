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
    public class Hintergrund
    {

        // Textur und Position für Hintergrund
        public Texture2D hintergrundTextur;
        public Texture2D hintergrundTexturCheat;
        public Texture2D aktuelleTextur;

        private int xPos = 0;
        private int yPos = 0;


        public Hintergrund()
        {
            aktuelleTextur = hintergrundTextur;
        }

        public void Update()
        {
            xPos = (xPos - 4 + 1024) % 1024 - 1024;
        }

        public void Update(GameTime gt, int geschwindigkeit)
        {
            Update(geschwindigkeit);

            // yPos = (int) (10f * Math.Sin(((gt.TotalGameTime.Milliseconds / 1000f) * (2 * Math.PI)) ) - 170);
        }

        public void Update(int geschwindigkeit)
        {
            xPos = (xPos - geschwindigkeit + 1024) % 1024 - 1024;
        }

        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(hintergrundTextur, hintegrundPosition, Color.White);
            sb.Draw(aktuelleTextur, new Vector2(xPos, yPos), Color.White);
            sb.Draw(aktuelleTextur, new Vector2(xPos + 1024, yPos), Color.White);
            sb.Draw(aktuelleTextur, new Vector2(xPos + 2048, yPos), Color.White);
            sb.Draw(aktuelleTextur, new Vector2(xPos + 3072, yPos), Color.White);
        }
    }
}
