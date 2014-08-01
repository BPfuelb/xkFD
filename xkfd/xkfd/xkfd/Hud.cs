using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;

namespace xkfd
{
    class Hud
    {
        public Vector2 positionGleitenAnzeige;
        public Vector2 positionTastaturbelegung;
        public Vector2 positionCheckbox;
        public Vector2 positionPunkte;

        Spieler spieler;

        Texture2D hudTextur;
        String counter;

        public Texture2D tastaturTextur;
        // public Animation tastaturAnimation;


        public Texture2D checkBox_check;
        public Texture2D checkBox_uncheck;
        public Texture2D teleport;

        public Texture2D skin_frau;
        public Texture2D skin_hut;
        public Texture2D skin_einstein;

        public Texture2D aktuellerUnlock;

        public Vector2 unlockPosition;

        public Boolean gewonnen = false;

        public Boolean updateAchievement;
        public Boolean achievementOben;
        private int achievementDauer;

        public Hud(Spieler spieler, Texture2D hudTextur)
        {
            this.spieler = spieler;
            this.hudTextur = hudTextur;

            positionGleitenAnzeige = new Vector2(600, 15);
            positionTastaturbelegung = new Vector2(100, -250);
            positionCheckbox = new Vector2(1150, -12);
            positionPunkte = new Vector2(200, 15);

            unlockPosition = new Vector2(1000, 720);
            updateAchievement = false;
            achievementOben = false;
            achievementDauer = 0;
        }


        public void Update()
        {
            counter = "";
            for (int i = 0; i < spieler.gleitenResource / 2; i++)
            {
                counter += "|";
            }

        }

        public void UpdateHelp()
        {
            // tastaturAnimation.Update();
        }

        public void Draw(SpriteBatch sb, SpriteFont schrift, int maxPunkte)
        {
            sb.DrawString(schrift, "Gleiten: " + counter, positionGleitenAnzeige, Color.Black);
            sb.DrawString(schrift, "Punkte: " + spieler.punkte + "/" + maxPunkte, positionPunkte, Color.Black);

            sb.Draw(teleport, positionCheckbox + new Vector2(-10, 25), Color.White);
            if (spieler.teleport)
                sb.Draw(checkBox_check, positionCheckbox, Color.White);
            else
                sb.Draw(checkBox_uncheck, positionCheckbox, Color.White);
        }

        public void UpdateAchievment()
        {
            if (unlockPosition.Y > 720 - 123 && !updateAchievement && !achievementOben)
            {
                unlockPosition.Y--;
            }
            else if (unlockPosition.Y < 720 && !updateAchievement)
            {
                achievementOben = true;
                if (achievementDauer > 100)
                    unlockPosition.Y++;
                else
                    achievementDauer++;
            }
            else
                updateAchievement = true;
        }


        public void DrawAchivment(SpriteBatch sb)
        {
            sb.Draw(aktuellerUnlock, unlockPosition, Color.White);
        }


        public void DrawHelp(SpriteBatch sb, SpriteFont schrift)
        {
            // Tastenbelegung
            sb.Draw(tastaturTextur, positionTastaturbelegung, Color.White);

            // Strich auf dem Gelaufen wird (TODO vllt etwas anderes?)
            sb.Draw(hudTextur, new Rectangle(0, 530, 1280, 2), Color.Black);

            // Text zum starten
            sb.DrawString(schrift, "Zum Starten Enter drücken", new Vector2(300, 600), Color.Gray);
        }

    }
}
