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
    class Hintergrund
    {

        // Textur und Position für Hintergrund
        public Texture2D hintergrundTextur;
        public Vector2 hintegrundPosition;

        public Hintergrund()
        {
            hintegrundPosition = new Vector2(0, 0);
        }

        public void Update()
        {
            hintegrundPosition.X = hintegrundPosition.X - 2;
            if (hintegrundPosition.X == -282)
                hintegrundPosition.X = 0;
        }
    }
}
