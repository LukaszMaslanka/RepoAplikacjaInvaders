using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Invaders
{
    public partial class InvadersBanner : UserControl
    {
        
        public InvadersBanner()
        {
            InitializeComponent();
            InicjalizacjaObrazow();
            DoubleBuffered = true;
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        Bitmap[] bannerBitmap;
        private void InicjalizacjaObrazow()
        {
            bannerBitmap = new Bitmap[8];
            bannerBitmap[0] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner1, 416, 65);
            bannerBitmap[1] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner2, 416, 65);
            bannerBitmap[2] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner3, 416, 65);
            bannerBitmap[3] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner4, 416, 65);
            bannerBitmap[4] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner5, 416, 65);
            bannerBitmap[5] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner6, 416, 65);
            bannerBitmap[6] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner7, 416, 65);
            bannerBitmap[7] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner8, 416, 65);
        }

        int klatka = 0;
        public void banerAnimation(Timer banerAnimationTimer)
        {
            klatka++;
            switch (klatka)
            {
                case 1: BackgroundImage = bannerBitmap[0]; banerAnimationTimer.Interval = 125; break;
                case 2: BackgroundImage = bannerBitmap[1]; break;
                case 3: BackgroundImage = bannerBitmap[2]; break;
                case 4: BackgroundImage = bannerBitmap[3]; break;
                case 5: BackgroundImage = bannerBitmap[4]; break;
                case 6: BackgroundImage = bannerBitmap[5]; break;
                case 7: BackgroundImage = bannerBitmap[6]; break;
                case 8: BackgroundImage = bannerBitmap[7]; break;
                default: BackgroundImage = bannerBitmap[0]; klatka = 0;
                    break;
            }
        }
    }
}
