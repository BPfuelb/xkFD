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

        Spieler spieler;

        Texture2D hudTextur;
        String counter;

        public Texture2D tastaturTextur;
        public Animation tastaturAnimation;


        public Texture2D checkBox_check;
        public Texture2D checkBox_uncheck;
        public Texture2D teleport;

        Rectangle balken;

        public Hud(Spieler spieler, Texture2D hudTextur)
        {
            this.spieler = spieler;
            positionGleitenAnzeige = new Vector2(600,15);
            positionTastaturbelegung = new Vector2(384,-20);
            positionCheckbox = new Vector2(1150, -12);

            this.hudTextur = hudTextur;

            balken = new Rectangle(800, 30, 20, 30);
        }


        public void Update()
        {
            counter = "";
            for (int i = 0; i < spieler.gleitenResource /2; i++)
            {
                counter += "|";
            }

        }

        public void UpdateHelp()
        {
            tastaturAnimation.Update();
        }

        public void Draw(SpriteBatch sb, SpriteFont schrift)
        {
            sb.DrawString(schrift, "Gleiten: " + counter, positionGleitenAnzeige, Color.Black);
            // sb.Draw(hudTextur, balken, Color.Gray);
            sb.Draw(teleport, positionCheckbox+ new Vector2(-10,25), Color.White);
            if (spieler.teleport)
                sb.Draw(checkBox_check, positionCheckbox, Color.White);
            else
                sb.Draw(checkBox_uncheck, positionCheckbox, Color.White);
        }

        public void DrawHelp(SpriteBatch sb, SpriteFont schrift)
        {
            tastaturAnimation.Draw(sb, positionTastaturbelegung);
            sb.Draw(hudTextur, new Rectangle(0,530,1280,2), Color.Black);
            
            sb.DrawString(schrift, "Zum Starten Enter drücken", new Vector2(200, 650), Color.Gray);
        }

    }
}
