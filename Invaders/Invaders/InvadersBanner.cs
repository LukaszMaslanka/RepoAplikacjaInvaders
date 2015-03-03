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
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        Bitmap[] animacjaBaneru;
        public void InicjalizacjaObrazow()
        {
            animacjaBaneru = new Bitmap[8];
            animacjaBaneru[0] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner1, 416, 65);
            animacjaBaneru[1] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner2, 416, 65);
            animacjaBaneru[2] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner3, 416, 65);
            animacjaBaneru[3] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner4, 416, 65);
            animacjaBaneru[4] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner5, 416, 65);
            animacjaBaneru[5] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner6, 416, 65);
            animacjaBaneru[6] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner7, 416, 65);
            animacjaBaneru[7] = Rysuj.KonwertujNaBitmap(Properties.Resources.InvadersBanner8, 416, 65);
        }

        int klatka = 0;
        private void banerAnimationTimer_Tick(object sender, EventArgs e)
        {
            klatka++;
            switch (klatka)
            {
                case 1: BackgroundImage = animacjaBaneru[0]; banerAnimationTimer.Interval = 2000; break;
                case 2: BackgroundImage = animacjaBaneru[1]; banerAnimationTimer.Interval = 100; break;
                case 3: BackgroundImage = animacjaBaneru[2]; break;
                case 4: BackgroundImage = animacjaBaneru[3]; break;
                case 5: BackgroundImage = animacjaBaneru[4]; break;
                case 6: BackgroundImage = animacjaBaneru[5]; break;
                case 7: BackgroundImage = animacjaBaneru[6]; break;
                case 8: BackgroundImage = animacjaBaneru[7]; break;
                default: BackgroundImage = animacjaBaneru[0]; klatka = 0;
                    break;
            }
        }
    }
}
