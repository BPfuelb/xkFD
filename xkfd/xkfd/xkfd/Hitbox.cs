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
        public Vector2 position;

        public Hitbox(int posX, int posY, int width, int height)
        { 
            hitbox = new Rectangle(posX, posY, width, height);
            position = new Vector2(posX, posY);
        }

        public void move(int x)
        {
            hitbox.X -= x;
        }
    }
}
