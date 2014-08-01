using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace xkfd
{
    class NotenHitbox : Hitbox
    {
        // Zugehöriges Hindernis (evlt. Object?)
        public Hindernis hindernis;
        public Punkt punkt;
        public Boolean faellt;
        public Animation haufenAnimation;

        public NotenHitbox(Punkt punkt, Hindernis hindernis, int posX, int posY, int width, int height)
            : base(posX, posY, width, height)
        {
            this.hindernis = hindernis;
            this.punkt = punkt;
            faellt = true;
        }

        public void Draw(SpriteBatch sb)
        {
            if (faellt)
                punkt.punktAnimation.Draw(sb, hitboxPosition);
            else
            {
                if (haufenAnimation == null)
                    haufenAnimation = new Animation(punkt.punktAnimationHaufen.gibTextur(), 2, 2, 4);
                haufenAnimation.Draw(sb, hitboxPosition);
            }
        }

        public void Update()
        {
            if (!faellt)
            {
                if (haufenAnimation == null)
                    haufenAnimation = new Animation(punkt.punktAnimationHaufen.gibTextur(), 2, 2, 4);
                haufenAnimation.UpdateNoLoop();
            }
        }
    }
}
