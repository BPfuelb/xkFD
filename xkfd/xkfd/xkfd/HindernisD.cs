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
    class HindernisD : Hindernis
    {

        public HindernisD(Texture2D textur, Vector2 position, Punkt p1, Punkt p2, Punkt p5, Punkt p10)
            : base(textur, position)
        {

            hitboxListe.Add(new Hitbox((int)position.X, (int)position.Y + 488, 50, 200));
            hitboxListe.Add(new Hitbox((int)position.X + 270, (int)position.Y + 488, 50, 200));

            punkteListe.Add(new NotenHitbox(p5,this, (int)position.X + 130, (int)position.Y + 450, 32, 32));
            punkteListe.Add(new NotenHitbox(p2,this, (int)position.X + 150, (int)position.Y + 580, 32, 32));
           
            foreach (NotenHitbox noteHitbox in punkteListe)
            {
                punkteAnzahl += noteHitbox.punkt.wertigkeit;
            }
        }


    }
}
