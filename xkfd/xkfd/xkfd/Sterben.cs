using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xkfd
{
    class Sterben : Zustand
    {
        public Sterben(Spieler spieler):base(spieler)
        {
        }

        public override void update()
        {
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
        public override void fallen()
        {
            // Tue nichts
        }
    }
}
