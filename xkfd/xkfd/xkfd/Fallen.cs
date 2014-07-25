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
    class Fallen : Zustand
    {
        public int beschleunigung;
        public Rectangle hitbox;

        public Fallen(Spieler spieler):base(spieler)
        {
            beschleunigung = 0;
            hitbox = new Rectangle(0, 0, animationTexture.Width, animationTexture.Height);
        }

        
        public override void update()
        {
            /*if (hitbox.Intersects(aktuelleUmgebung.hitbox))
            {
                spieler.doLaufen();
            }*/
            beschleunigung++;
            spieler.position.Y += beschleunigung*2;
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
            beschleunigung = 0;
            spieler.setZustand(spieler.laufen);
        }

        public override void gewinnen()
        {
            spieler.setZustand(spieler.gewinnen);
        }

        public override void sterben()
        {
            spieler.setZustand(spieler.sterben);
        }

        public override void fallen()
        {
            // Setzte Zustand wieder auf Laufen
            spieler.setZustand(spieler.laufen);
        }
    }
}
