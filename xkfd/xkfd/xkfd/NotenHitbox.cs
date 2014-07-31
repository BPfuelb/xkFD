using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace xkfd
{
    class NotenHitbox : Hitbox
    {
        // Zugehöriges Hindernis (evlt. Object?)
        public Hindernis hindernis;
        public Punkt punkt;
        public Boolean faellt;

        public NotenHitbox(Punkt punkt, Hindernis hindernis, int posX, int posY, int width, int height):base(posX, posY,width,height)
        {
            this.hindernis = hindernis;
            this.punkt = punkt;
            faellt = true;
        }
    }
}
