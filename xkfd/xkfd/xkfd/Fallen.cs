using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xkfd
{
    class Fallen : Zustand
    {
        public int beschleunigung;
        public Fallen(Spieler spieler):base(spieler)
        {
            beschleunigung = 0;
        }

        
        public override void update()
        {
            beschleunigung++;
            spieler.position.Y += beschleunigung*2;
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
