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

        public HindernisF(Texture2D textur, Texture2D texturCheat, Vector2 position, Punkt p1, Punkt p2, Punkt p5, Punkt p10, PowerUp powerUp)
            : base(textur, position, texturCheat)
        {
            Vector2 notePos1 = new Vector2((int)position.X + 6, (int)position.Y + 345);
            Vector2 notePos2 = new Vector2((int)position.X + 275, (int)position.Y + 280);

            hitboxListe.Add(new Hitbox((int)position.X, (int)position.Y + 488, 320, 200));
            hitboxListe.Add(new Hitbox((int)position.X + 65, (int)position.Y + 100, 180, 305));

            double zufallPowerUp = .8;

            // Noten zufällig zuweisen
            for (int i = 0; i < 2; i++)
            {
                switch ((int)game1.rand.Next(5))
                {
                    case 0:
                        if (i == 0)
                            notenListe.Add(new NotenHitbox(p1, this, (int)notePos1.X, (int)notePos1.Y, 32, 32));
                        else
                            notenListe.Add(new NotenHitbox(p1, this, (int)notePos2.X, (int)notePos2.Y, 32, 32));
                        break;
                    case 1:
                        if (i == 0)
                            notenListe.Add(new NotenHitbox(p2, this, (int)notePos1.X, (int)notePos1.Y, 32, 32));
                        else
                            notenListe.Add(new NotenHitbox(p2, this, (int)notePos2.X, (int)notePos2.Y, 32, 32));
                        break;
                    case 2:
                        if (i == 0)
                            notenListe.Add(new NotenHitbox(p5, this, (int)notePos1.X, (int)notePos1.Y, 32, 32));
                        else
                            notenListe.Add(new NotenHitbox(p5, this, (int)notePos2.X, (int)notePos2.Y, 32, 32));

                        break;
                    case 3:
                        if (i == 0)
                            notenListe.Add(new NotenHitbox(p10, this, (int)notePos1.X, (int)notePos1.Y, 32, 32));
                        else
                            notenListe.Add(new NotenHitbox(p10, this, (int)notePos2.X, (int)notePos2.Y, 32, 32));
                        break;
                    case 4:
                        if (i == 0 && game1.rand.Next() > zufallPowerUp)
                            notenListe.Add(new NotenHitbox(powerUp, this, (int)notePos1.X, (int)notePos1.Y, 32, 32));
                        else if (game1.rand.Next() > zufallPowerUp)
                            notenListe.Add(new NotenHitbox(powerUp, this, (int)notePos2.X, (int)notePos2.Y, 32, 32));
                        break;
                }
            }



            foreach (NotenHitbox noteHitbox in notenListe)
            {
                punkteAnzahl += noteHitbox.punkt.wertigkeit;
            }
        }


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
            else if (note.hitboxPosition.X <= 30)
                note.setPositionY(320);
            else if (note.hitboxPosition.X >= 275)
                note.setPositionY(320);
            else if (note.hitboxPosition.X > 130 && note.hitboxPosition.Y < 180)
                note.setPositionY(72);
            else
                note.setPositionY(447);

            note.hitboxPosition.X += 1280;
            note.hitboxRect.X += 1280;

            note.hindernis = this;
            notenListe.Add(note);
        }
    }
}
