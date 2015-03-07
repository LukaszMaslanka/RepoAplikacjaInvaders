using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invaders
{
    class Rysuj
    {
        public Rysuj()
        {
            InicjalizacjaObrazow();
        }

        public static Bitmap KonwertujNaBitmap(Image Obraz, int Szerokosc, int Wysokosc)
        {
            Bitmap bitmap = new Bitmap(Szerokosc, Wysokosc);
            using (Graphics grafika = Graphics.FromImage(bitmap))
            {
                grafika.DrawImage(Obraz, 0, 0, Szerokosc, Wysokosc);
            }
            return bitmap;
        }

        Bitmap[] tloBitmap;
        private void InicjalizacjaObrazow()
        {
            tloBitmap = new Bitmap[11];
            tloBitmap[0] = KonwertujNaBitmap(Properties.Resources.background1, 800, 700);
            tloBitmap[1] = KonwertujNaBitmap(Properties.Resources.background2, 800, 700);
            tloBitmap[2] = KonwertujNaBitmap(Properties.Resources.background3, 800, 700);
            tloBitmap[3] = KonwertujNaBitmap(Properties.Resources.background4, 800, 700);
            tloBitmap[4] = KonwertujNaBitmap(Properties.Resources.background5, 800, 700);
            tloBitmap[5] = KonwertujNaBitmap(Properties.Resources.background6, 800, 700);
            tloBitmap[6] = KonwertujNaBitmap(Properties.Resources.background7, 800, 700);
            tloBitmap[7] = KonwertujNaBitmap(Properties.Resources.background8, 800, 700);
            tloBitmap[8] = KonwertujNaBitmap(Properties.Resources.background9, 800, 700);
            tloBitmap[9] = KonwertujNaBitmap(Properties.Resources.background10, 800, 700);
            tloBitmap[10] = KonwertujNaBitmap(Properties.Resources.background11, 800, 700);
        }

        int cell = 0;
        int klatka = 0;
        public void AnimujTlo(Graphics g)
        {
            klatka++;
            if (klatka >= 11)
            {
                klatka = 0;
            }
            switch (klatka)
            {
                case 0: cell = 0; break;
                case 1: cell = 1; break;
                case 2: cell = 2; break;
                case 3: cell = 3; break;
                case 4: cell = 4; break;
                case 5: cell = 5; break;
                case 6: cell = 6; break;
                case 7: cell = 7; break;
                case 8: cell = 8; break;
                case 9: cell = 9; break;
                case 10: cell = 10; break;
                case 11: cell = 11; break;
                default: cell = 0;
                    break;
            }
            g.DrawImage(tloBitmap[cell],0,0);
        }
    }
}
