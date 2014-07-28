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
    class Laufen:Zustand
    {

        public Laufen(Spieler spieler):base(spieler)
        {
            // Position der Hitbox vom Player null punkt aus(42,63)
            // Größe der Hitbox 53 x 104 
            hitbox = new Rectangle((int)spieler.position.X + 42, (int)spieler.position.Y + 63, 53, 104);
        }

        public override void update()
        {
            animation.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            animation.Draw(sb, this.spieler.position);
        }


        // Zustandsänderungen bei Aktionen
        public override void ducken()
        {
            spieler.setZustand(spieler.ducken);
        }

        public override void springen()
        {
            /* 
            // Optimierung richtiger Absprung Animation
            switch(animation.index)
            {
                case 0:
                case 4:
                case 5:
                case 6:
                case 10:
                case 11:

                    // Gehe in Springen Zustand über
                    spieler.springen.sound.Play();
                    spieler.setZustand(spieler.springen);
                    break;
            }
             */


                 // Gehe in Springen Zustand über
                    spieler.springen.sound.Play();
                    spieler.setZustand(spieler.springen);
        }

        public override void gleiten()
        {
            spieler.setZustand(spieler.gleiten);
        }

        public override void laufen()
        {
            // Lauf weiter
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
            spieler.setZustand(spieler.fallen);
        }
    }
}
