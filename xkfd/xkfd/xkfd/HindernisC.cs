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
    public class HindernisC : Hindernis
    {

        public HindernisC(Texture2D textur, Texture2D texturCheat, Vector2 position, Punkt p1, Punkt p2, Punkt p5, Punkt p10, PowerUp powerUp)
            : base(textur, position, texturCheat)
        {
            hitboxListe.Add(new Hitbox((int)position.X, (int)position.Y + 488, 320, 200));
            hitboxListe.Add(new Hitbox((int)position.X + 90, (int)position.Y + 458, 150, 200));

            Vector2 notePos1 = new Vector2((int)position.X + 50,(int)position.Y + 450);
            Vector2 notePos2 = new Vector2((int)position.X + 270, (int)position.Y + 280);

            

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
                        else if(game1.rand.Next() > zufallPowerUp)
                            notenListe.Add(new NotenHitbox(powerUp, this, (int)notePos2.X, (int)notePos2.Y, 32, 32));
                        break;
                }
            }


          

            foreach (NotenHitbox noteHitbox in notenListe)
            {
                punkteAnzahl += noteHitbox.punkt.wertigkeit;
            }
        }

                public HindernisC(Texture2D textur, Texture2D texturCheat, Vector2 position)
            : base(textur, position, texturCheat)
        {
            hitboxListe.Add(new Hitbox((int)position.X, (int)position.Y + 488, 320, 200));
            hitboxListe.Add(new Hitbox((int)position.X + 90, (int)position.Y + 458, 150, 200));
        }



        public override void noteHinzufuegen(NotenHitbox note)
        {
            if (note.hitboxPosition.X > 320 || note.hitboxPosition.X < 0)
                Console.WriteLine("Fehlerhafte Position: " + note.hitboxPosition);
            else if (note.hitboxPosition.X <= 84)
                note.setPositionY(340);
            else if (note.hitboxPosition.X >= 235)
                note.setPositionY(340);
            else
                note.setPositionY(280);

            note.hitboxPosition.X += 1280;
            note.hitboxRect.X += 1280;

            note.hindernis = this;
            notenListe.Add(note);
        }
    }
}
