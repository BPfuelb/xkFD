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

        public Fallen(Spieler spieler)
            : base(spieler)
        {
            beschleunigung = 0;
        }


        public override void update()
        {
            spieler.aktuellerSkin.fallenAnimation.Update();
            // ALT animation.Update();
            beschleunigung++;
            Console.WriteLine(beschleunigung);
            spieler.movePlayerDown(beschleunigung);
        }

        public override void Draw(SpriteBatch sb)
        {
            spieler.aktuellerSkin.fallenAnimation.Draw(sb, spieler.position);
            // ALT animation.Draw(sb, this.spieler.position);
        }

        // Zustandsänderungen bei Aktionen
        public override void ducken()
        {
            // Tue nichts
        }

        public override void springen()
        {
            if (spieler.gleitenResource > 0)
                spieler.setZustand(spieler.gleiten);
        }

        public override void gleiten()
        {
            spieler.setZustand(spieler.gleiten);
        }

        public override void laufen()
        {
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
            // Falle weiter...
        }
    }
}
