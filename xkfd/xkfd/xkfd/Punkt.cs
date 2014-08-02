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
    class Punkt
    {
        public Texture2D punktTextur;
        public Animation punktAnimation;

        public Texture2D punktTexturHaufen;
        public Animation punktAnimationHaufen;

        public Animation aktuelleAnimation;

        public int wertigkeit;

        public SoundEffect sound;
        public SoundEffectInstance soundSoundInstance;



        public Punkt(int wertigkeit)
        {
            aktuelleAnimation = punktAnimation;
            this.wertigkeit = wertigkeit;
        }

        public void initSound(SoundEffect sound)
        {
            this.sound = sound;
            soundSoundInstance = this.sound.CreateInstance();
        }

        public void playSound()
        {
            sound.Play();
        }
    }
}
