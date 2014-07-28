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
    class HindernisC : Hindernis
    {

        Hitbox bodenHitbox;
        Hitbox stufeHitbox;

        public HindernisC(Texture2D textur, Vector2 position)
            : base(textur, position)
        {

            bodenHitbox = new Hitbox((int)position.X, (int)position.Y + 488, 320, 30);
            stufeHitbox = new Hitbox((int)position.X + 90, (int)position.Y + 458, 150, 30);

            hitboxListe.Add(stufeHitbox);
            hitboxListe.Add(bodenHitbox);

        }


    }
}
