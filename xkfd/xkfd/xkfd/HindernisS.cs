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
    public class HindernisS : Hindernis
    {
        Hitbox bodenHitbox;


        public  HindernisS(Texture2D textur, Vector2 position)
            : base(textur, position)
        {
            bodenHitbox = new Hitbox((int)position.X, (int)position.Y + 488, 320, 200);
            hitboxListe.Add(bodenHitbox);
        }

        public HindernisS(Texture2D textur, Vector2 position, Texture2D special)
            : base(textur, position)
        {
            this.special = special;
            bodenHitbox = new Hitbox((int)position.X, (int)position.Y + 488, 320, 200);
            hitboxListe.Add(bodenHitbox);
        }
    }
}
