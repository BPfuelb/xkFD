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
    class SterbenAnimationKlatscher : SterbenAnimation
    {



        public SterbenAnimationKlatscher(Sterben sterben)
            : base(sterben)
        { }

        public override void Update()
        {
            sterben.spieler.aktuellerSkin.sterbenAnimationKlatscher.UpdateNoLoop();
            // ALT animationTod.UpdateNoLoop();
        }

        public override void Draw(SpriteBatch sb)
        {
            sterben.spieler.aktuellerSkin.sterbenAnimationKlatscher.Draw(sb, this.sterben.spieler.position + new Vector2(16, 8));
            // ALT animationTod.Draw(sb, this.sterben.spieler.position + new Vector2(16, 8));
        }
    }
}
