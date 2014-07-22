using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xkfd
{
    class Animation
    {

        Texture2D textur;
        int spalte, zeile;
        int index;
        int slowMoFactor;
        int slowMoTimer;

        Vector2 position;

        public Animation(Texture2D textur, int spalte, int zeile, int slowMoFactor)
        {
            this.textur = textur;
            this.spalte = spalte;
            this.zeile = zeile;
            this.slowMoFactor = slowMoFactor;
            this.slowMoTimer = slowMoFactor;
        }

        public void Update()
        {
            slowMoTimer--;
            if (slowMoTimer == 0)
            {
                slowMoTimer = slowMoFactor;
                index++;
            }
            if (index >= spalte * zeile) index = 0;
        }

        public void Draw(SpriteBatch sb, Vector2 pos)
        {
            int tileWidth = textur.Width / spalte;
            int tileHeight = textur.Height / zeile;

            Rectangle rect = new Rectangle(0, 0, tileWidth, tileHeight);

            rect.X = (index % spalte) * tileWidth;
            rect.Y = (index % zeile) * tileHeight;

            sb.Draw(textur, pos, rect, Color.White);
        }
    }
}
