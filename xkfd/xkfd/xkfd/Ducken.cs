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
    class Ducken : Zustand
    {
        public Ducken(Spieler spieler)
            : base(spieler)
        {
            // Position der Hitbox vom Player null punkt aus(42,63)
            // Größe der Hitbox 53 x 104 
            // hitbox = new Rectangle((int)spieler.position.X + 42, (int)spieler.position.Y + 63, 53, 104);
        }

        public override void update()
        {
            if (spieler.gleitenResource < 30)
                spieler.gleitenResource += 1;

            spieler.aktuellerSkin.duckenAnimation.Update(4);
        }

        public override void Draw(SpriteBatch sb)
        {
            spieler.aktuellerSkin.duckenAnimation.Draw(sb, this.spieler.position);
            // ALT animation.Draw(sb, this.spieler.position);
        }

        // Zustandsänderungen bei Aktionen
        public override void ducken()
        {

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
            spieler.aktuellerSkin.duckenAnimation.index = 0;
            spieler.hitboxKopf = new Rectangle((int)spieler.position.X + 50, (int)spieler.position.Y, 10, 40);
            spieler.setZustand(spieler.laufen);
        }

        public override void gewinnen()
        {
            spieler.setZustand(spieler.gewinnen);
        }

        public override void sterben()
        {

            ((Sterben)spieler.sterben).aktuell.soundTod.Play();
            spieler.setZustand(spieler.sterben);
        }

        public override void fallen()
        {
            spieler.aktuellerSkin.duckenAnimation.index = 0;
            spieler.hitboxKopf = new Rectangle((int)spieler.position.X + 50, (int)spieler.position.Y, 10, 90);
            spieler.setZustand(spieler.fallen);
        }
    }
}
