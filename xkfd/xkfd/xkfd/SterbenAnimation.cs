using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace xkfd
{
    abstract class SterbenAnimation
    {
        public Sterben sterben;
        public Texture2D textur;
        public Animation animationTod;


        public SoundEffect soundTod;
        public SoundEffectInstance soundSoundInstance;

        public SterbenAnimation(Sterben sterben)
        {
            this.sterben = sterben;        
        }

        abstract public void Update();
        abstract public void Draw(SpriteBatch sb);
    }
}
