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

        /// <summary>
        /// Statyczna metda kowertująca grafikę na bitmapy
        /// </summary>
        /// <param name="obraz"></param>
        /// <param name="szerokoscObrazu"></param>
        /// <param name="wysokoscObrazu"></param>
        /// <returns></returns>
        public static Bitmap KonwertujNaBitmap(Image obraz, int szerokoscObrazu, int wysokoscObrazu)
        {
            Bitmap bitmap = new Bitmap(szerokoscObrazu, wysokoscObrazu);
            using (Graphics grafika = Graphics.FromImage(bitmap))
            {
                grafika.DrawImage(obraz, 0, 0, szerokoscObrazu, wysokoscObrazu);
            }
            return bitmap;
        }

        //Animacja tła formularza1
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

        int komorkaAnimacji = 0;
        int klatka = 0;
        public void AnimujTlo(Graphics g)
        {
            klatka++;
            if (klatka >= 10)
            {
                klatka = 0;
            }
            switch (klatka)
            {
                case 0: komorkaAnimacji = 0; break;
                case 1: komorkaAnimacji = 1; break;
                case 2: komorkaAnimacji = 2; break;
                case 3: komorkaAnimacji = 3; break;
                case 4: komorkaAnimacji = 4; break;
                case 5: komorkaAnimacji = 5; break;
                case 6: komorkaAnimacji = 6; break;
                case 7: komorkaAnimacji = 7; break;
                case 8: komorkaAnimacji = 8; break;
                case 9: komorkaAnimacji = 9; break;
                case 10: komorkaAnimacji = 10; break;
                default: komorkaAnimacji = 0;
                    break;
            }
            g.DrawImage(tloBitmap[komorkaAnimacji],0,0);
        }
    }
}
