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
    public class HindernisC : Hindernis
    {

        public HindernisC(Texture2D textur, Texture2D texturCheat, Vector2 position, Punkt p1, Punkt p2, Punkt p5, Punkt p10, PowerUp powerUp)
            : base(textur, position, texturCheat)
        {
            hitboxListe.Add(new Hitbox((int)position.X, (int)position.Y + 488, 320, 200));
            hitboxListe.Add(new Hitbox((int)position.X + 90, (int)position.Y + 458, 150, 200));

            notenListe.Add(new NotenHitbox(p1, this, (int)position.X + 50, (int)position.Y + 450, 32, 32));
            notenListe.Add(new NotenHitbox(p5, this, (int)position.X + 270, (int)position.Y + 280, 32, 32));


            foreach (NotenHitbox noteHitbox in notenListe)
            {
                punkteAnzahl += noteHitbox.punkt.wertigkeit;
            }
        }


    }
}
