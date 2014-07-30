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
    abstract class Zustand
    {
        public Spieler spieler;

        public SoundEffect sound;
        public SoundEffectInstance soundSoundInstance;


        public Rectangle hitbox;
            

        public Zustand(Spieler spieler)
        {
            this.spieler = spieler;
        }


        // Subklassen müssen alle Methoden Implementieren
        abstract public void springen();
        abstract public void ducken();
        abstract public void gleiten();
        abstract public void gewinnen();
        abstract public void sterben();
        abstract public void laufen();
        abstract public void fallen();

        abstract public void update();
        abstract public void Draw(SpriteBatch sb);
    }

    

}
