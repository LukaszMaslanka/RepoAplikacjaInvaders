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
    public partial class GameOverBanner : UserControl
    {
        public GameOverBanner()
        {
            InitializeComponent();
            InicjalizacjaObrazow();
            DoubleBuffered = true;
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private Bitmap[] gameOverBanner;
        private void InicjalizacjaObrazow()
        {
            gameOverBanner = new Bitmap[8];
            gameOverBanner[0] = Rysuj.KonwertujNaBitmap(Properties.Resources.gameover_1, 535, 65);
            gameOverBanner[1] = Rysuj.KonwertujNaBitmap(Properties.Resources.gameover_2, 535, 65);
            gameOverBanner[2] = Rysuj.KonwertujNaBitmap(Properties.Resources.gameover_3, 535, 65);
            gameOverBanner[3] = Rysuj.KonwertujNaBitmap(Properties.Resources.gameover_4, 535, 65);
            gameOverBanner[4] = Rysuj.KonwertujNaBitmap(Properties.Resources.gameover_5, 535, 65);
            gameOverBanner[5] = Rysuj.KonwertujNaBitmap(Properties.Resources.gameover_6, 535, 65);
            gameOverBanner[6] = Rysuj.KonwertujNaBitmap(Properties.Resources.gameover_7, 535, 65);
            gameOverBanner[7] = Rysuj.KonwertujNaBitmap(Properties.Resources.gameover_8, 535, 65);
        }

        int klatka = 0;
        public void GameOverBanerAnimation()
        {
            klatka++;
            switch (klatka)
            {
                case 1: BackgroundImage = gameOverBanner[0]; animationTimer.Interval = 2000; break;
                case 2: BackgroundImage = gameOverBanner[1]; animationTimer.Interval = 125; break;
                case 3: BackgroundImage = gameOverBanner[2]; break;
                case 4: BackgroundImage = gameOverBanner[3]; break;
                case 5: BackgroundImage = gameOverBanner[4]; break;
                case 6: BackgroundImage = gameOverBanner[5]; break;
                case 7: BackgroundImage = gameOverBanner[6]; break;
                case 8: BackgroundImage = gameOverBanner[7]; break;
                default: BackgroundImage = gameOverBanner[0]; klatka = 0;
                    break;
            }
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            GameOverBanerAnimation();
        }
    }
}
