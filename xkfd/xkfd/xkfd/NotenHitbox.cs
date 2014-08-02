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
    class NotenHitbox : Hitbox
    {
        // Zugehöriges Hindernis (evlt. Object?)
        public Hindernis hindernis;
        public Punkt punkt;
        public Boolean faellt;
        public Animation haufenAnimation;

        public Vector2 zielPosition;
        public Vector2 zielRichtung;

        public NotenHitbox(Punkt punkt, Hindernis hindernis, int posX, int posY, int width, int height)
            : base(posX, posY, width, height)
        {
            this.hindernis = hindernis;
            this.punkt = punkt;
            faellt = true;
            zielRichtung = new Vector2(0, 0);
            zielPosition = new Vector2(320, 10);
        }

        public void Draw(SpriteBatch sb)
        {
            if (faellt)
                punkt.punktAnimation.Draw(sb, hitboxPosition);
            else
            {
                if (haufenAnimation == null)
                    haufenAnimation = new Animation(punkt.punktAnimationHaufen.gibTextur(), 2, 2, 4);
                haufenAnimation.Draw(sb, hitboxPosition);
            }
        }

        public void Update()
        {
            if (!faellt)
            {
                if (haufenAnimation == null)
                    haufenAnimation = new Animation(punkt.punktAnimationHaufen.gibTextur(), 2, 2, 4);
                haufenAnimation.UpdateNoLoop();
            }
        }

        public void UpdateFreilassen()
        {
            hitboxPosition += zielRichtung + new Vector2((float)Math.Sin(hitboxPosition.Y / 720 * Math.PI * 2),0);
        }

        public void UpdateSammeln()
        {
            hitboxPosition += (zielPosition) * 7;

            hitboxRect.Y += (int)(zielPosition.Y * 7)-1;
            hitboxRect.X += (int)(zielPosition.X * 7);
        }

        public void setRichtung(Spieler spieler)
        {
            if (((Sterben)spieler.sterben).aktuell != ((Sterben)spieler.sterben).klatscher)
                hitboxPosition = spieler.position + new Vector2(80, 100);
            else
                hitboxPosition = new Vector2(540,800);

            zielRichtung = Vector2.Normalize(zielPosition - spieler.position);
        }


        public void DrawFreilassen(SpriteBatch sb)
        {
            sb.Draw(this.punkt.punktTextur, this.hitboxPosition, Color.White);
        }

    }
}
