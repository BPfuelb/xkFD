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
    public class HindernisF : Hindernis
    {
        Vector2 taschenRechernPos = new Vector2(80, 153);

        public HindernisF(Texture2D textur, Texture2D texturCheat, Vector2 position)
            : base(textur, position, texturCheat)
        {
            hitboxListe.Add(new Hitbox((int)position.X, (int)position.Y + 488, 320, 200));
            hitboxListe.Add(new Hitbox((int)position.X + 65, (int)position.Y + 100, 180, 305));
        }



        public override void DrawAni(SpriteBatch sb)
        {
            if (game1.schrift_rechner.MeasureString(game1.hud.zeit).X < 143)
                sb.DrawString(game1.schrift_rechner, game1.hud.zeit, hindernisPosition + taschenRechernPos, Color.Black);
            else
                sb.DrawString(game1.schrift_rechner, "ERROR", hindernisPosition + taschenRechernPos, Color.Black);


            if (special == game1.zielEinlauf)
            {
                if (specialAnimation == null)
                    specialAnimation = new Animation(special, 2, 2, 8);
                specialAnimation.Draw(sb, hindernisPosition + new Vector2(300, 400));
            }
        }


        public override void noteHinzufuegen(NotenHitbox note)
        {
            if (note.hitboxPosition.X > 320 || note.hitboxPosition.X < 0)
                Console.WriteLine("Fehlerhafte Position: " + note.hitboxPosition);
            else if (note.hitboxPosition.X <= 10)
                note.setPositionY(320);
            else if (note.hitboxPosition.X >= 265)
                note.setPositionY(320);
            else if (note.hitboxPosition.X > 120 && note.hitboxPosition.X < 190)
                note.setPositionY(50);
            else
                note.setPositionY(447);

            note.hitboxPosition.X += 1280;
            note.hitboxRect.X += 1280;

            note.hindernis = this;
            notenListe.Add(note);
        }
    }
}
