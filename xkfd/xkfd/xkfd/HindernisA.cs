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

        Rectangle hitboxBodenVorne;
        Rectangle hitboxBodenHinten;

        public HindernisA(Texture2D textur, Vector2 position)
            : base(textur, position)
        {

            hitboxBodenVorne = new Rectangle(100,30, (int)this.position.X + 0,488);
            hitboxBodenHinten = new Rectangle(100, 30, (int)this.position.X + 220, 488);

        }

        public override List<Hitbox> gibHitboxen()
        {
            List<Hitbox> liste = new List<Hitbox>();

            //liste.Add(hitboxBodenHinten);
            //liste.Add(hitboxBodenVorne);

            return liste;
        }

    }
}
