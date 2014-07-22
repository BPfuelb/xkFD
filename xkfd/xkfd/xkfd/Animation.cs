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

        Texture2D texture;
        int col, row;
        int index;
        int slowMoFactor;
        int slowMoTimer;

        Vector2 position;

        public Animation(Texture2D tex, int col, int row, int slowMoFactor)
        {
            texture = tex;
            this.col = col;
            this.row = row;
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
            if (index >= col * row) index = 0;
        }

        public void Draw(SpriteBatch sb, Vector2 pos)
        {
            int tileWidth = texture.Width / col;
            int tileHeight = texture.Height / row;

            Rectangle rect = new Rectangle(0, 0, tileWidth, tileHeight);

            rect.X = (index % col) * tileWidth;
            rect.Y = (index / row) * tileHeight;
            // if (index > col * row) return;

            sb.Draw(texture, pos, rect, Color.White);
        }
    }
}
