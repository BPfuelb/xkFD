﻿using System;
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
    public class Hindernis
    {
        public Texture2D hindernisTextur;
        public Texture2D hindernisTexturCheat;

        public Vector2 hindernisPosition;

        public List<Hitbox> hitboxListe;
        public List<Hitbox> hitboxListeStacheln;
        public List<NotenHitbox> notenListe;

        public Texture2D special;
        public Animation specialAnimation;

        public static int punkteAnzahl = 0;

        private static Game1 game1;


        // Generiert eine beliebige lange Liste von Hindernissen
        public static List<Hindernis> generieHindernisse(int anzahl, Game1 game)
        {

            game1 = game;
            // Init zufallsgenerator
            Random random = new Random();

            // Init Liste
            List<Hindernis> liste = new List<Hindernis>();

            // Erstes Hindernis Links vom Bildschirm
            liste.Add(new HindernisS(game.hindernisTexturS, new Vector2(-320, 40), game.zielEinlauf));

           // Vier Startelemente nebeneinander (füllung des Bildschirms)
            liste.Add(new HindernisS(game.hindernisTexturS, new Vector2(0, 40)));
            liste.Add(new HindernisS(game.hindernisTexturS, new Vector2(320, 40)));
            liste.Add(new HindernisS(game.hindernisTexturS, new Vector2(2 * 320, 40), game.cheat_qr));
            liste.Add(new HindernisS(game.hindernisTexturS, new Vector2(3 * 320, 40)));

            int anzahlVerschiedenerHindernisse = 5;
            // Erzeuge mit Schleife Anzahl von zufälligen Hindernissen
            for (int i = 0; i < anzahl; i++)
            {
                switch ((int)random.Next(anzahlVerschiedenerHindernisse))
                {
                    case 0:
                        liste.Add(new HindernisA(game.hindernisTexturA, new Vector2(1280, 40), game.punkt1, game.punkt2, game.punkt5, game.punkt10, game.powerUp));
                        break;
                    case 1:
                        liste.Add(new HindernisB(game.hindernisTexturB, new Vector2(1280, 40), game.punkt1, game.punkt2, game.punkt5, game.punkt10, game.powerUp));
                        break;
                    case 2:
                        liste.Add(new HindernisC(game.hindernisTexturC, new Vector2(1280, 40), game.punkt1, game.punkt2, game.punkt5, game.punkt10, game.powerUp));
                        break;
                    case 3:
                        liste.Add(new HindernisD(game.hindernisTexturD, new Vector2(1280, 40), game.punkt1, game.punkt2, game.punkt5, game.punkt10, game.powerUp));
                        break;
                    case 4:
                        liste.Add(new HindernisE(game.hindernisTexturE, new Vector2(1280, 40), game.punkt1, game.punkt2, game.punkt5, game.punkt10, game.powerUp));
                        break;

                }
            }

            // Füge zum Schluss das Ziel hinzu
            liste.Add(new HindernisS(game.hindernisTexturZ, new Vector2(1280, 40), game.zielEinlauf));
            liste.Add(new HindernisS(game.hindernisTexturS, new Vector2(1280, 40)));
            liste.Add(new HindernisS(game.hindernisTexturS, new Vector2(1280, 40)));
            liste.Add(new HindernisS(game.hindernisTexturS, new Vector2(1280, 40)));

            return liste;
        }

        // Konstrukter für StartHindernis
        public Hindernis(Texture2D textur, Vector2 position)
        {
            this.hitboxListe = new List<Hitbox>();
            this.hindernisTextur = textur;
            this.hindernisPosition = position;

            this.notenListe = new List<NotenHitbox>();
            this.hitboxListeStacheln= new List<Hitbox>();
        }

        // Schiebt Hindernisse nach Links
        public void Update()
        {
            // Hindernis Position Updaten
            hindernisPosition.X -= 4;

            // Hitboxen zur Kollisionserkennung aktualisieren
            foreach (Hitbox hitbox in hitboxListe)
            {
                hitbox.moveX(4);
            }

            // Punkte Hitboxen aktualisieren
            foreach (NotenHitbox punkt in notenListe)
            {
                punkt.moveX(4);
            }

            // Stacheln Hitboxen aktualisieren
            foreach (Hitbox stachel in hitboxListeStacheln)
            {
                stachel.moveX(4);
            }

            if (special != null)
            {
                // Zieleinlauf Animation
                if (specialAnimation == null) specialAnimation= new Animation(special, 2, 2, 4);
                specialAnimation.Update();
            }

        }

        public void UpdateAnimation()
        {
            if (special != null)
            {
                // Zieleinlauf Animation
                if (specialAnimation == null) specialAnimation = new Animation(special, 2, 2, 4);
                specialAnimation.Update();
            }
        }

        public virtual List<Hitbox> gibHitboxen()
        {
            return hitboxListe;
        }

        public virtual List<NotenHitbox> gibPunkte()
        {
            return notenListe;
        }

        public virtual void loescheHitboxPunkt(NotenHitbox notenhitbox)
        {
            notenListe.Remove(notenhitbox);
        }

        public virtual List<Hitbox> gibSterben()
        {
            return hitboxListeStacheln;
        }

        public void DrawAni(SpriteBatch sb)
        {
            if (special == game1.zielEinlauf)
            {
                if (specialAnimation == null) 
                    specialAnimation = new Animation(special, 2, 2, 8);
                specialAnimation.Draw(sb, hindernisPosition + new Vector2(300,400));
            }

            if (special == game1.cheat_qr)
            {
                sb.Draw(special, hindernisPosition + new Vector2(300, 600), Color.White);
            }
        }
    }
}
