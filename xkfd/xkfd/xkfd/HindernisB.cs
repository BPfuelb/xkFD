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

        public HindernisB(Texture2D textur, Vector2 position)
            : base(textur, position)
        {

            hitboxListe.Add(new Hitbox((int)position.X, (int)position.Y + 488, 320, 200));
            hitboxListe.Add(new Hitbox((int)position.X+84, (int)position.Y + 376, 150, 30));

            punkteListe.Add(new Hitbox(this,1,(int)position.X + 140, (int)position.Y + 430, 32, 32));
            punkteListe.Add( new Hitbox(this,10,(int)position.X + 140, (int)position.Y + 280, 32, 32));
            punkteListe.Add(new Hitbox(this,5,(int)position.X + 290, (int)position.Y + 200, 32, 32));
        }

    }
}
