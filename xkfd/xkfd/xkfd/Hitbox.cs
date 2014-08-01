using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace xkfd
{
    class Hitbox
    {

        public Rectangle hitboxRect;
        public Vector2 hitboxPosition;

        public Hitbox(int posX, int posY, int width, int height)
        { 
            hitboxRect = new Rectangle(posX, posY, width, height);
            hitboxPosition = new Vector2(posX, posY);
        }


        public void setPositionY(int y)
        {
            hitboxPosition.Y = y;
            hitboxRect.Y = y;
        }

        public void moveX(int x)
        {
            hitboxPosition.X -= x;
            hitboxRect.X -= x;
        }
        public void moveY(int y)
        {
            hitboxPosition.Y -= y;
            hitboxRect.Y -= y;
        }
    }
}
