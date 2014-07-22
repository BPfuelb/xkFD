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
    class Menue
    {
        // Positionen für Knöpfe
        Vector2 startPosition;
        Vector2 optionenPosition;
        Vector2 exitPosition;

        // Texturen für Knöpfe
        public Texture2D startTextur;
        public Texture2D optionenTexture;
        public Texture2D exitTexture;

        public Menue()
        { 
            startPosition = new Vector2();
            optionenPosition = new Vector2();
            exitPosition = new Vector2();
        }

        public void update()
        { 
        }

    }
}
