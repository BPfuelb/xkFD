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

        Hitbox hitboxBodenVorne;
        Hitbox hitboxBodenHinten;

        public HindernisA(Texture2D textur, Vector2 position)
            : base(textur, position)
        {

            hitboxBodenVorne = new Hitbox((int)position.X, (int)position.Y + 488, 100, 30);
            hitboxBodenHinten = new Hitbox((int)position.X +220, (int)position.Y + 488, 100, 30);


            hitboxListe.Add(hitboxBodenVorne);
            hitboxListe.Add(hitboxBodenHinten);
        }

    }
}
