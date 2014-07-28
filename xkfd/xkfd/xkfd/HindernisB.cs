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
    class HindernisB : Hindernis
    {
        Hitbox bodenHitbox;
        Hitbox fliegendeHitbox;

        public HindernisB(Texture2D textur, Vector2 position)
            : base(textur, position)
        {

            bodenHitbox = new Hitbox((int)position.X, (int)position.Y + 488, 320, 30);
            fliegendeHitbox = new Hitbox((int)position.X+84, (int)position.Y + 376, 150, 30);

            hitboxListe.Add(fliegendeHitbox);
            hitboxListe.Add(bodenHitbox);
        }

    }
}
