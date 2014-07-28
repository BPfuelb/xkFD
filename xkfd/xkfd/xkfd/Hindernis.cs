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


        public List<Hitbox> hitboxListe;

        // Generiert eine beliebige lange Liste von Hindernissen
        public static List<Hindernis> generieHindernisse(int anzahl, Texture2D hindernisSTextur, Texture2D hindernisATextur, Texture2D hindernisBTextur, Texture2D hindernisCTextur, Texture2D hindernisDTextur, Texture2D hindernisZTextur)
        {
            // Init zufallsgenerator
            Random random = new Random();

            // Init Liste
            List<Hindernis> liste = new List<Hindernis>();

            // Erstes Hindernis Links vom Bildschirm
            liste.Add(new HindernisS(hindernisSTextur, new Vector2(-320, 40)));

            // Vier Startelemente nebeneinander (füllung des Bildschirms)
            liste.Add(new HindernisS(hindernisSTextur, new Vector2(0, 40)));
            liste.Add(new HindernisS(hindernisSTextur, new Vector2(320, 40)));
            liste.Add(new HindernisS(hindernisSTextur, new Vector2(2 * 320, 40)));
            liste.Add(new HindernisS(hindernisSTextur, new Vector2(3 * 320, 40)));

            // Erzeuge mit Schleife Anzahl von zufälligen Hindernissen
            for (int i = 0; i < anzahl; i++)
            {
                switch ((int)random.Next(3))
                {
                    case 0:
                        liste.Add(new HindernisA(hindernisATextur, new Vector2(1280, 40)));
                        break;
                    case 1:
                        liste.Add(new HindernisB(hindernisBTextur, new Vector2(1280, 40)));
                        break;
                    case 2:
                        liste.Add(new HindernisC(hindernisCTextur, new Vector2(1280, 40)));
                        break;
                    case 3:
                        liste.Add(new HindernisD(hindernisDTextur, new Vector2(1280, 40)));
                        break;
                }
            }

            // Füge zum Schluss das Ziel hinzu
            liste.Add(new HindernisS(hindernisZTextur, new Vector2(1280, 40)));
            liste.Add(new HindernisS(hindernisSTextur, new Vector2(1280, 40)));
            liste.Add(new HindernisS(hindernisSTextur, new Vector2(1280, 40)));
            liste.Add(new HindernisS(hindernisSTextur, new Vector2(1280, 40)));

            // null Elemente damit die Schliefe zum weiterschieben nicht auf null pointer Exeception läuft

            return liste;
        }

        public Hindernis(Texture2D textur, Vector2 position)
        {

            this.hitboxListe = new List<Hitbox>();
            this.hindernisTextur = textur;
            this.position = position;
        }


        // Schiebt Hindernisse nach Linkts
        public void Update()
        {
            position.X -= 4;

            foreach (Hitbox hitbox in hitboxListe)
            {
                hitbox.move(4);
            }

        }

        public virtual List<Hitbox> gibHitboxen()
        {
            return hitboxListe;
        }
    }
}
