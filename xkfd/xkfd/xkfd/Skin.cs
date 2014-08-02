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
    public class Skin
    {
        // Texturen Skin
        public Texture2D laufenTextur, duckenTextur, fallenTextur, gleitenTextur, sprignenTextur,
            gewinnenTextur, cheatenTextur, sterbenTexturKoepfen, sterbenTexturStolpern, sterbenTexturKlatscher, sterbenTexturPieksen;

        // Animationen
        public Animation laufenAnimation, duckenAnimation, fallenAnimation, gleitenAnimation, sprignenAnimation,
            gewinnenAnimation, cheatenAnimation, sterbenAnimationKoepfen, sterbenAnimationStolpern, sterbenAnimationKlatscher, sterbenAnimationPieksen;

        public Skin()
        {
        }

    }
}
