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
    public class HindernisE : Hindernis
    {
        public HindernisE(Texture2D textur, Texture2D texturCheat, Vector2 position)
            : base(textur, position, texturCheat)
        {
            hitboxListe.Add(new Hitbox((int)position.X, (int)position.Y + 488, 100, 200));
            hitboxListe.Add(new Hitbox((int)position.X + 220, (int)position.Y + 488, 100, 200));

            // Stacheln Hitbox
            hitboxListeStacheln.Add(new Hitbox((int)position.X + 100, (int)position.Y + 488, 100, 200));
        }



        public override void noteHinzufuegen(NotenHitbox note)
        {
            if (note.hitboxPosition.X > 320 || note.hitboxPosition.X < 0)
                Console.WriteLine("Fehlerhafte Position: " + note.hitboxPosition);
            else if (note.hitboxPosition.X <= 136)
                note.setPositionY(365);
            else if (note.hitboxPosition.X >= 184)
                note.setPositionY(365);
            else
                note.setPositionY(437);

            note.hitboxPosition.X += 1280;
            note.hitboxRect.X += 1280;

            note.hindernis = this;
            notenListe.Add(note);
        }

    }
}
