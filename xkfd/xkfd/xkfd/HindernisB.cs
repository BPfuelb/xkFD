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
    public class HindernisB : Hindernis
    {
        public HindernisB(Texture2D textur, Texture2D texturCheat, Vector2 position)
            : base(textur, position, texturCheat)
        {
            hitboxListe.Add(new Hitbox((int)position.X, (int)position.Y + 488, 320, 200));
            hitboxListe.Add(new Hitbox((int)position.X + 84, (int)position.Y + 376, 150, 30));
        }



        public override void noteHinzufuegen(NotenHitbox note)
        {
            if (note.hitboxPosition.X > 320 || note.hitboxPosition.X < 0)
                Console.WriteLine("Fehlerhafte Position: " + note.hitboxPosition);
            else if (note.hitboxPosition.X <= 127)
                note.setPositionY(420);
            else if (note.hitboxPosition.X >= 247)
                note.setPositionY(210);
            else
                note.setPositionY(289);

            note.hitboxPosition.X += 1280;
            note.hitboxRect.X += 1280;

            note.hindernis = this;
            notenListe.Add(note);
        }
    }
}
