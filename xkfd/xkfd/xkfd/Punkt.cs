using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;

namespace xkfd
{
    class Punkt
    {
        public Texture2D punktTextur;
        public Animation punktAnimation;

        public int wertigkeit;

        public Punkt(int wertigkeit)
        {
            this.wertigkeit = wertigkeit;
        }
    }
}
