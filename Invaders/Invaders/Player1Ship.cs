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
    public partial class Player1Ship : UserControl
    {
        public Player1Ship()
        {
            InitializeComponent();
            InicjalizacjaObrazow();
            DoubleBuffered = true;
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        Bitmap[] statekBitmap;
        private void InicjalizacjaObrazow()
        {
            statekBitmap = new Bitmap[4];
            statekBitmap[0] = Rysuj.KonwertujNaBitmap(Properties.Resources.Player1, 51, 51);
            statekBitmap[1] = Rysuj.KonwertujNaBitmap(Properties.Resources.player1_2, 51, 51);
            statekBitmap[2] = Rysuj.KonwertujNaBitmap(Properties.Resources.player1_3, 51, 51);
            statekBitmap[3] = Rysuj.KonwertujNaBitmap(Properties.Resources.player1_4, 51, 51);
        }

        int klatka = 0;
        public void statekAnimation()
        {
            klatka++;
            switch (klatka)
            {
                case 1: BackgroundImage = statekBitmap[0]; break;
                case 2: BackgroundImage = statekBitmap[1]; break;
                case 3: BackgroundImage = statekBitmap[2]; break;
                case 4: BackgroundImage = statekBitmap[3]; break;
                default: BackgroundImage = statekBitmap[0]; klatka = 0;
                    break;
            }
        }
    }
}
