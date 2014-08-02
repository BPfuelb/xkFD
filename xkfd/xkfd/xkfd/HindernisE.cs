using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace xkfd
{
    public class HindernisE : Hindernis
    {
        public HindernisE(Texture2D textur, Vector2 position, Punkt p1, Punkt p2, Punkt p5, Punkt p10, PowerUp powerUp)
            : base(textur, position)
        {
            hitboxListe.Add(new Hitbox((int)position.X, (int)position.Y + 488, 100, 200));
            hitboxListe.Add(new Hitbox((int)position.X + 220, (int)position.Y + 488, 100, 200));

            // Stacheln Hitbox
            hitboxListeStacheln.Add(new Hitbox((int)position.X + 100, (int)position.Y + 488, 100, 200));

            // Noten
            notenListe.Add(new NotenHitbox(p1, this, (int)position.X + 40, (int)position.Y + 300, 32, 32));


            foreach (NotenHitbox noteHitbox in notenListe)
            {
                punkteAnzahl += noteHitbox.punkt.wertigkeit;
            }

        }

    }
}
