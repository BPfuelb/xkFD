using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xkfd
{
    class Ducken : Zustand
    {
        public Ducken(Spieler spieler)
        { 
            this.spieler = spieler; 
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
    }
}
