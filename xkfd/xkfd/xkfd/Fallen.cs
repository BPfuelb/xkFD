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
    unsafe class Fallen : Zustand
    {
        public int beschleunigung;

        float* hindernisPositionX;
        float* hindernisPositionY;

        public Fallen(Spieler spieler):base(spieler)
        {
            beschleunigung = 0;

            // Position der Hitbox vom Player null punkt aus(42,63)
            // Größe der Hitbox 53 x 104 

            
            /*
            hindernisPositionX = &spieler.position.X;
            hindernisPositionY = &spieler.position.Y;
            */
              
            hitbox = new Rectangle((int)spieler.position.X + 42, (int)spieler.position.Y + 63, 53, 104);
        }

        
        public override void update()
        {

            /*if (hitbox.Intersects(aktuelleUmgebung.hitbox))
            {
                spieler.doLaufen();
            }*/
            beschleunigung++;
            spieler.position.Y += beschleunigung*2;
            // spieler.position.Y += 1;
            // Aktualisierung der Hitbox
            
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
