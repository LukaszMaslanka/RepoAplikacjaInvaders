using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invaders
{
    class Najezdzca
    {
        private const int PionowyInterval = 40;
        private const int PoziomyInterval = 5;

        private Bitmap obraz;

        public Point Lokalizacja;

        public TypNajezdzcy TypNajezdzcy { get; private set; }

        public Rectangle wielkoscNajezdzcy
        {
            get
            {
                return new Rectangle(Lokalizacja, obraz.Size);
            }
        }

        public bool koniecAnimacji = false;
        private bool zestrzelony = false;
        public bool Zestrzelony 
        {
            get { return zestrzelony; } 
            set
            {
                zestrzelony = value;
            }
        }

        //publiczna właściwość zachowujące się jak pole tylko do odczytu
        public int IloscPunktow { get; private set; }

        public Najezdzca(TypNajezdzcy typNajezdzcy, Point lokalizacja, int iloscPunktow, Bitmap obraz)
        {
            this.TypNajezdzcy = typNajezdzcy;
            this.Lokalizacja = lokalizacja;
            this.IloscPunktow = iloscPunktow;
            this.obraz = obraz;
            InicjalizacjaObrazow();
        }

        /// <summary>
        /// Przesuwanie najeźdzców po ekranie o określoną wartośc zdefiniowaną w stałych
        /// </summary>
        /// <param name="kierunekNajezdzcy"></param>
        public void Przesun(Direction kierunekNajezdzcy)
        {
            switch (kierunekNajezdzcy)
            {
                case Direction.Lewo: Lokalizacja.X -= PoziomyInterval;
                    break;
                case Direction.Prawo: Lokalizacja.X += PoziomyInterval;
                    break;
                case Direction.Gora: Lokalizacja.Y -= PionowyInterval;
                    break;
                case Direction.Dol: Lokalizacja.Y += PionowyInterval;
                    break;
                default: Lokalizacja.X -= PoziomyInterval;
                    break;
            }
        }

        public void RysujStatek(Graphics g)
        {
            if (zestrzelony)
            {
                StatekDestroy();
                g.DrawImage(obraz, Lokalizacja);  
            }
            else
            {
                g.DrawImage(obraz, Lokalizacja);  
            }
        }

       Bitmap[] eksplozja;

        private void InicjalizacjaObrazow()
        {
            eksplozja = new Bitmap[6];
            eksplozja[0] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_1, 51, 51);
            eksplozja[1] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_2, 51, 51);
            eksplozja[2] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_3, 51, 51);
            eksplozja[3] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_4, 51, 51);
            eksplozja[4] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_5, 51, 51);
            eksplozja[5] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_6, 51, 51);
        }

        /// <summary>
        /// Animacja wybuchu statku
        /// </summary>
        int klatka = 0;
        public Bitmap StatekDestroy()
        {
            klatka++;
            switch (klatka)
	        {
                case 1: obraz = eksplozja[0]; return obraz;
                case 2: obraz = eksplozja[1]; return obraz;
                case 3: obraz = eksplozja[2]; return obraz;
                case 4: obraz = eksplozja[3]; return obraz;
                case 5: obraz = eksplozja[4]; return obraz;
                case 6: obraz = eksplozja[5]; return obraz;
                default: klatka = 0; obraz = eksplozja[0]; koniecAnimacji = true; return obraz;
	        }
        }
    }
}
