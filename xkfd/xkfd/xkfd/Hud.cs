using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace xkfd
{
    public class Hud
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

        // Game Over Anzeige
        public Texture2D gameOverTextur;
        public Animation gameOverAnimation;
        public Vector2 gameOverPosition;

        // Game Over Sound 

        public SoundEffect soundGameOver;
        public SoundEffectInstance soundGameOverSoundInstance;

        // Skins
        public Texture2D skin_frau;
        public Texture2D skin_hut;
        public Texture2D skin_einstein;

        // Akuteller Unlock Skin
        public Texture2D aktuellerUnlock;

        public Vector2 unlockPosition;
        public Vector2 positionTimer;

        public Boolean gewonnen = false;

        public Boolean updateAchievement;
        public Boolean achievementOben;
        private int achievementDauer;
        public Color schriftFarbe;

        // Zeit zähler
        int timer = 0;
        int gameTimeTmp = 0;


        float transparence = 0;

        public Hud(Spieler spieler, Texture2D hudTextur)
        {
            this.spieler = spieler;
            this.hudTextur = hudTextur;

            positionGleitenAnzeige = new Vector2(600, 15);
            positionTastaturbelegung = new Vector2(100, -250);
            positionCheckbox = new Vector2(1150, -12);

            positionPunkte = new Vector2(100, 15);
            positionTimer = new Vector2(1000, 640);


            unlockPosition = new Vector2(1000, 720);
            updateAchievement = false;
            achievementOben = false;
            achievementDauer = 0;

            gameOverPosition = new Vector2(100,100);
            schriftFarbe = Color.Black;
        }


        public void Update()
        {
            counter = "";
            for (int i = 0; i < spieler.gleitenResource / 2; i++)
            {
                counter += "|";
            }

            gameOverAnimation.Update();
        }

        public void UpdateMitTimer(GameTime gt)
        {
            if (gt.TotalGameTime.Seconds != gameTimeTmp)
            {
                timer++;
                gameTimeTmp = gt.TotalGameTime.Seconds;
            }

            Update();
        }

        public void UpdateHelp()
        {
            // tastaturAnimation.Update();
        }

        public void Draw(SpriteBatch sb, SpriteFont schrift, int maxPunkte, GameTime gt)
        {
            sb.DrawString(schrift, "Gleiten: " + counter, positionGleitenAnzeige, schriftFarbe);
            sb.DrawString(schrift, "Punkte: " + spieler.punkte + "/" + maxPunkte, positionPunkte, schriftFarbe);

            if (spieler.aktuellerZustand == spieler.sterben)
            {
                gameOverAnimation.DrawTransparent(sb, gameOverPosition);
            }

            sb.Draw(teleport, positionCheckbox + new Vector2(-10, 25), schriftFarbe);
            if (spieler.teleport)
                sb.Draw(checkBox_check, positionCheckbox, schriftFarbe);
            else
                sb.Draw(checkBox_uncheck, positionCheckbox, schriftFarbe);

			// Timer in Trialsystem anzeigen
            sb.DrawString(schrift, CalcTrial(timer), new Vector2(positionTimer.X - schrift.MeasureString(CalcTrial(timer)).Length(), positionTimer.Y), schriftFarbe, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
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
            transparence = transparence + (float)0.15 % (float)(Math.PI * 2);

            float trans = ((float)Math.Sin(transparence)) / 2 + 0.5f;

            sb.DrawString(schrift, "Zum Starten Enter drücken", new Vector2(300, 600), Color.Black * trans );

        }

		public string CalcTrial(int zahl)
		{
            if (zahl == 0)
                return "";
			return "" + CalcTrial(zahl/3) + zahl%3;
		}
    }
}
