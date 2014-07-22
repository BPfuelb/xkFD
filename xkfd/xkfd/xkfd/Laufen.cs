using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xkfd
{
    class Laufen:Zustand
    {

        public Laufen(Spieler spieler):base(spieler)
        {
        }

        public override void update()
        {

        }


        // Zustandsänderungen bei Aktionen
        public override void ducken()
        {
            spieler.setZustand(spieler.ducken);
        }

        public override void springen()
        {
            // Gehe in Springen Zustand über
            
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
