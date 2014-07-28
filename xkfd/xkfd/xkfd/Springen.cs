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

            // Position der Hitbox vom Player null punkt aus(42,63)
            // Größe der Hitbox 53 x 104 
            hitbox = new Rectangle((int)spieler.position.X + 42, (int)spieler.position.Y + 63, 53, 104);
        }

        public override void update()
        {
            //spieler.position.Y -= sprungHoehe*2;
            //sprungHoehe -= 1;
            if (sprungHoehe == 0)
            {
                sprungHoehe = 10;
                spieler.doFallen();
            }

            hitbox.X = (int)spieler.position.X + 42;
            hitbox.Y = (int)spieler.position.Y + 63;

            animation.Update(4);
        }

        
        public override void Draw(SpriteBatch sb)
        {
            animation.Draw(sb, this.spieler.position);
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
            spieler.setZustand(spieler.gleiten);
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
