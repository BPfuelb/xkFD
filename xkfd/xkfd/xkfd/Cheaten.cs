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
    class Cheaten : Zustand
    {
        public Cheaten(Spieler spieler)
            : base(spieler)
        {
            // Position der Hitbox vom Player null punkt aus(42,63)
            // Größe der Hitbox 53 x 104 
            hitbox = new Rectangle((int)spieler.position.X + 42, (int)spieler.position.Y + 63, 53, 104);
        }

        public override void update()
        {
            spieler.aktuellerSkin.cheatenAnimation.Update(4);
        }

        public override void Draw(SpriteBatch sb)
        {
            spieler.aktuellerSkin.cheatenAnimation.Draw(sb, this.spieler.position);
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
            spieler.setZustand(spieler.gewinnen);
        }

        public override void sterben()
        {
            // tue nichts
        }
        public override void fallen()
        {
            // tue nichts
        }
    }
}
