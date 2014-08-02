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
   public class Gewinnen : Zustand
    {
        public Gewinnen(Spieler spieler):base(spieler)
        {
        }

        public override void update()
        {
            spieler.aktuellerSkin.gewinnenAnimation.Update();
            // ALT animation.Update();
        }


        public override void Draw(SpriteBatch sb)
        {
            spieler.aktuellerSkin.gewinnenAnimation.Draw(sb, spieler.position);
            // ALT animation.Draw(sb, this.spieler.position);
        }

        // Zustandsänderungen bei Aktionen
        public override void ducken()
        {
            // Tue nichts
        }

        public override void springen()
        {
            // Tue nichts
        }

        public override void gleiten()
        {
            // Tue nichts
        }

        public override void laufen()
        {
            // Tue nichts
        }

        public override void gewinnen()
        {
            // Tue nichts
        }

        public override void sterben()
        {
            // Tue nichts
        }

        public override void fallen()
        {
            // Tue nichts
        }
    }
}
