using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace xkfd
{
    class Hitbox
    {

        public Rectangle hitbox;

        public Hitbox(int posX, int posY, int width, int height)
        { hitbox = new Rectangle(posX, posY, width, height); }

        public void move(int x)
        {
            hitbox.X -= x;
        }
    }
}
