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
        public HindernisE(Texture2D textur, Texture2D texturCheat, Vector2 position, Punkt p1, Punkt p2, Punkt p5, Punkt p10, PowerUp powerUp)
            : base(textur, position, texturCheat)
        {
            hitboxListe.Add(new Hitbox((int)position.X, (int)position.Y + 488, 100, 200));
            hitboxListe.Add(new Hitbox((int)position.X + 220, (int)position.Y + 488, 100, 200));

            // Stacheln Hitbox
            hitboxListeStacheln.Add(new Hitbox((int)position.X + 100, (int)position.Y + 488, 100, 200));

            Vector2 notePos1 = new Vector2((int)position.X + 40, (int)position.Y + 300);

            // Noten zufällig zuweisen
            for (int i = 0; i < 0; i++)
            {
                switch ((int)game1.rand.Next(5))
                {
                    case 0:
                        notenListe.Add(new NotenHitbox(p1, this, (int)notePos1.X, (int)notePos1.Y, 32, 32));
                        break;
                    case 1:
                        notenListe.Add(new NotenHitbox(p2, this, (int)notePos1.X, (int)notePos1.Y, 32, 32));
                        break;
                    case 2:
                        notenListe.Add(new NotenHitbox(p5, this, (int)notePos1.X, (int)notePos1.Y, 32, 32));
                        break;
                    case 3:
                        notenListe.Add(new NotenHitbox(p10, this, (int)notePos1.X, (int)notePos1.Y, 32, 32));
                        break;
                    case 4:
                        if (game1.rand.Next() > zufallPowerUp)
                            notenListe.Add(new NotenHitbox(powerUp, this, (int)notePos1.X, (int)notePos1.Y, 32, 32));
                        break;
                }
            }


            foreach (NotenHitbox noteHitbox in notenListe)
            {
                punkteAnzahl += noteHitbox.punkt.wertigkeit;
            }

        }

    }
}
