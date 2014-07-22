using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xkfd
{
    class Sterben : Zustand
    {
        public Sterben(Spieler spieler)
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
            // Tue nichts
        }

        public override void gewinnen()
        {
            // Tue nichts
        }

        public override void sterben()
        {
            // Tue nichts
        }
    }
}
