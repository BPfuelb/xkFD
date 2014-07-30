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
    class Springen:Zustand
    {
        int sprungHoehe = 10;

        public Springen(Spieler spieler):base(spieler)
        {

        }

        public override void update()
        {

            spieler.movePlayerUp(sprungHoehe);
             sprungHoehe -= 1;
            if (sprungHoehe == 0)
            {
                sprungHoehe = 10;
                ((Fallen)spieler.fallen).beschleunigung = 0;
                spieler.doFallen();
            }
            spieler.aktuellerSkin.sprignenAnimation.Update(4);
            // ALT animation.Update(4);
        }

        
        public override void Draw(SpriteBatch sb)
        {
            spieler.aktuellerSkin.sprignenAnimation.Draw(sb, this.spieler.position);
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
            spieler.setZustand(spieler.sterben);
        }

        public override void fallen()
        {
            spieler.setZustand(spieler.fallen);
        }
    }
}
