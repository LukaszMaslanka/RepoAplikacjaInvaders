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
    public partial class Player1Ship : PictureBox
    {
        public Player1Ship()
        {
            InitializeComponent();
            InicjalizacjaObrazow();
            DoubleBuffered = true;
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = ImageLayout.Stretch;
            BackgroundImage = statekBitmap[0];
        }

        public bool Zywy = true;

        Bitmap[] statekBitmap;
        Bitmap[] eksplozja;
        private void InicjalizacjaObrazow()
        {
            statekBitmap = new Bitmap[4];
            statekBitmap[0] = Rysuj.KonwertujNaBitmap(Properties.Resources.Player1, 51, 51);
            statekBitmap[1] = Rysuj.KonwertujNaBitmap(Properties.Resources.player1_2, 51, 51);
            statekBitmap[2] = Rysuj.KonwertujNaBitmap(Properties.Resources.player1_3, 51, 51);
            statekBitmap[3] = Rysuj.KonwertujNaBitmap(Properties.Resources.player1_4, 51, 51);

            eksplozja = new Bitmap[6];
            eksplozja[0] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_1, 51, 51);
            eksplozja[0] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_2, 51, 51);
            eksplozja[2] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_3, 51, 51);
            eksplozja[3] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_4, 51, 51);
            eksplozja[4] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_5, 51, 51);
            eksplozja[5] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_6, 51, 51);
        }

        int klatka = 0;
        public void StatekAnimation()
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

        public void StatekDestroy()
        {
            klatka++;
            switch (klatka)
            {
                case 1: BackgroundImage = eksplozja[0]; break;
                case 2: BackgroundImage = eksplozja[1]; break;
                case 3: BackgroundImage = eksplozja[2]; break;
                case 4: BackgroundImage = eksplozja[3]; break;
                case 5: BackgroundImage = eksplozja[4]; break;
                case 6: BackgroundImage = eksplozja[5]; break;
                default: BackgroundImage = eksplozja[0]; klatka = 0; Zywy = true;
                    break;
            }
 
        }
    }
}
