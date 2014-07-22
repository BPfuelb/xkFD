using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xkfd
{
    class Laufen:Zustand
    {

        public Laufen(Spieler spieler)
        {
            this.spieler = spieler;
        }


        // Zustandsänderungen bei Aktionen
        public override void ducken()
        {
            spieler.setZustand(spieler.ducken);
        }

        public override void springen()
        {
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

    }
}
