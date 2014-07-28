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

        Hitbox hitboxBodenVorne;
        Hitbox hitboxBodenHinten;

        public HindernisD(Texture2D textur, Vector2 position)
            : base(textur, position)
        {

            hitboxBodenVorne = new Hitbox((int)position.X, (int)position.Y + 488, 50, 30);
            hitboxBodenHinten = new Hitbox((int)position.X + 270, (int)position.Y + 488, 50, 30);


            hitboxListe.Add(hitboxBodenVorne);
            hitboxListe.Add(hitboxBodenHinten);

        }


    }
}
