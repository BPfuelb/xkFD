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
    public class Laufen : Zustand
    {

        public Laufen(Spieler spieler)
            : base(spieler)
        { }

        public override void update()
        {
            // Gleiten Ressource hochzählen solange der Charakter läuft TODO auch bei Ducken!
            if (spieler.gleitenResource < 30)
                spieler.gleitenResource += 1;

            // Update der Laufen Animation
            spieler.aktuellerSkin.laufenAnimation.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            // Malen des aktuellen Zustands der Animation
            spieler.aktuellerSkin.laufenAnimation.Draw(sb, this.spieler.position);
        }


        // Zustandsänderungen bei Aktionen
        public override void ducken()
        {
            spieler.hitboxKopf = new Rectangle((int)spieler.position.X +50 , (int)spieler.position.Y + 20  ,10, 40);
            spieler.setZustand(spieler.ducken);
        }

        public override void springen()
        {
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
            ((Sterben)spieler.sterben).aktuell.soundTod.Play();
            spieler.setZustand(spieler.sterben);
        }

        public override void fallen()
        {
            ((Fallen)spieler.fallen).beschleunigung = 0;
            spieler.setZustand(spieler.fallen);
        }
    }
}
