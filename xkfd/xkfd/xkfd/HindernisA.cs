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
    class HindernisA : Hindernis
    {
        public HindernisA(Texture2D textur, Vector2 position, Punkt p1, Punkt p2, Punkt p5, Punkt p10)
            : base(textur, position)
        {
            hitboxListe.Add(new Hitbox((int)position.X, (int)position.Y + 488, 100, 200));
            hitboxListe.Add(new Hitbox((int)position.X + 220, (int)position.Y + 488, 100, 200));

            punkteListe.Add(new NotenHitbox(p1, this,(int)position.X + 40, (int)position.Y + 300, 32, 32));

        }

    }
}
