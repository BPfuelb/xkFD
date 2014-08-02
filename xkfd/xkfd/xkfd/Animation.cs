using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace xkfd
{
  public   class Animation
    {

        Texture2D textur;
        int spalte, zeile;
        public int index;
        int slowMoFactor;
        int slowMoTimer;
        public int tileWidth;
        public int tileHeight;
        float transparence;

        public Animation(Texture2D textur, int spalte, int zeile, int slowMoFactor)
        {
            index = 0;
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

        public void UpdateNoLoop()
        {
            slowMoTimer--;
            if (slowMoTimer == 0 && index < spalte * zeile -1)
            {
                slowMoTimer = slowMoFactor;
                index++;
            }
            if (slowMoFactor != slowMoTimer && index >= spalte * zeile -1) slowMoTimer = slowMoFactor;
        }


        public void Update(int loopFromToEnd)
        {
            slowMoTimer--;
            if (slowMoTimer == 0)
            {
                slowMoTimer = slowMoFactor;
                index++;
            }
            if (index >= spalte * zeile) 
                index = loopFromToEnd;
        }

        public void Draw(SpriteBatch sb, Vector2 pos)
        {
            tileWidth = textur.Width / spalte;
            tileHeight = textur.Height / zeile;

            Rectangle rect = new Rectangle(0, 0, tileWidth, tileHeight);

            
            rect.X = (index % spalte) * tileWidth;
            rect.Y = (index / spalte) * tileHeight;

            sb.Draw(textur, pos, rect, Color.White);
        }


        public void DrawTransparent(SpriteBatch sb, Vector2 pos)
        {
            tileWidth = textur.Width / spalte;
            tileHeight = textur.Height / zeile;

            Rectangle rect = new Rectangle(0, 0, tileWidth, tileHeight);


            rect.X = (index % spalte) * tileWidth;
            rect.Y = (index / spalte) * tileHeight;

            transparence = transparence + (float)0.15 %  (float)(Math.PI *2);

            float trans = ((float)Math.Sin(transparence)) / 2 + 0.5f;
            sb.Draw(textur, pos, rect, Color.White * trans);
        }

        public Texture2D gibTextur()
        {
            return textur;
        }

    }
}
