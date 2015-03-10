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

        /*private Direction kierunekNajezdzcy = Direction.Prawo;
        public Direction KierunekNajezdzcy 
        {
            get
            {
                return kierunekNajezdzcy;
            }
            set
            {
                kierunekNajezdzcy = value;
                Przesun(kierunekNajezdzcy);
            }
        }*/

        public Rectangle wielkoscNajezdzcy
        {
            get
            {
                return new Rectangle(Lokalizacja, obraz.Size);
            }
        }

        public int IloscPunktow { get; private set; }

        public Najezdzca(TypNajezdzcy typNajezdzcy, Point lokalizacja, int iloscPunktow, Bitmap obraz)
        {
            this.TypNajezdzcy = typNajezdzcy;
            this.Lokalizacja = lokalizacja;
            this.IloscPunktow = iloscPunktow;
            this.obraz = obraz;
            InicjalizacjaObrazow();
        }

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
            g.DrawImage(obraz, Lokalizacja);
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

        int klatka = 0;
        public void StatekDestroy(Graphics g)
        {
            klatka++;
            switch (klatka)
	        {
                case 1: obraz = eksplozja[0]; g.DrawImage(obraz,Lokalizacja); break;
                case 2: obraz = eksplozja[1]; g.DrawImage(obraz,Lokalizacja); break;
                case 3: obraz = eksplozja[2]; g.DrawImage(obraz,Lokalizacja); break;
                case 4: obraz = eksplozja[3]; g.DrawImage(obraz,Lokalizacja); break;
                case 5: obraz = eksplozja[4]; g.DrawImage(obraz,Lokalizacja); break;
                case 6: obraz = eksplozja[5]; g.DrawImage(obraz,Lokalizacja); break;
                default: obraz = eksplozja[5]; g.DrawImage(obraz,Lokalizacja); klatka = 0;
                    break;
	        }
        }
    }
}
