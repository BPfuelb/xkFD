using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace xkfd
{
    abstract class Zustand
    {
        public Spieler spieler;
        public Texture2D animationTexture;

        // Subklassen müssen alle Methoden Implementieren
        abstract public void springen();
        abstract public void ducken();
        abstract public void gleiten();
        abstract public void gewinnen();
        abstract public void sterben();
        abstract public void laufen();
        abstract public void fallen();
    }

    

}
