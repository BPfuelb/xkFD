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
    class Hindernis
    {
        public Texture2D hindernisTextur;
        public Vector2 position;

        public static List<Hindernis> generieHindernisse(int anzahl, Texture2D hindernisSTextur, Texture2D hindernisATextur, Texture2D hindernisBTextur, Texture2D hindernisCTextur, Texture2D hindernisZTextur)
        {
            Random random = new Random();

            List<Hindernis> liste = new List<Hindernis>();
            liste.Add(new HindernisS(hindernisSTextur, new Vector2(0, 720 / 2 + 128 + 35)));

            for (int i = 0; i < anzahl; i++)
            {
                switch ((int)random.Next(3))
                {
                    case 0:
                        liste.Add(new HindernisA(hindernisATextur, new Vector2(1280, 720 / 2 + 128 + 35)));
                        break;
                    case 1:
                        liste.Add(new HindernisB(hindernisBTextur, new Vector2(1280, 720 / 2 + 128 + 35)));
                        break;
                    case 2:
                        liste.Add(new HindernisC(hindernisCTextur, new Vector2(1280, 720 / 2 + 128 + 35)));
                        break;
                }
            }
            liste.Add(new HindernisS(hindernisZTextur, new Vector2(1280, 128 + 35)));
            liste.Add(null);
            liste.Add(null);
            liste.Add(null);
            return liste;
        }

        public Hindernis(Texture2D textur, Vector2 position)
        {
            this.hindernisTextur = textur;
            this.position = position;
        }

        public void Update()
        {
            position.X -= 2;
        }
    }
}
