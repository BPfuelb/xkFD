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
    class Sterben : Zustand
    {
        public  SterbenAnimation koepfen;
        public SterbenAnimation dagegen;
        public SterbenAnimation aktuell;


        public Sterben(Spieler spieler):base(spieler)
        {
            // Position der Hitbox vom Player null punkt aus(42,63)
            // Größe der Hitbox 53 x 104 

            koepfen = new SterbenAnimationKopf(this);
            dagegen = new SterbenAnimationBeine(this);
            aktuell = koepfen;


            hitbox = new Rectangle((int)spieler.position.X + 42, (int)spieler.position.Y + 63, 53, 104);
        }

        public override void update()
        {
            aktuell.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            aktuell.Draw(sb);
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
            
            
        }
        public override void fallen()
        {
            // Tue nichts
        }
    }
}
