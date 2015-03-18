using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invaders
{
    public class Strzal
    {
        private const int przesunInterval = 20;
        private const int szerokoscPocisku = 5;
        private const int dlugoscPocisku = 15;

        public Point Lokalizacja;

        private Direction kierunek;
        public Direction Kierunek 
        {
            get
            {
                return kierunek;
            }
            private set { }
        }
        private Rectangle granice;

        private Brush kolorPocisku;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="lokalizacja"></param>
        /// <param name="kierunek"></param>
        /// <param name="granice"></param>
        /// <param name="kolorPocisku"></param>
        public Strzal(Point lokalizacja, Direction kierunek, Rectangle granice, Brush kolorPocisku)
        {
            this.Lokalizacja = lokalizacja;
            this.kierunek = kierunek;
            this.granice = granice;
            this.kolorPocisku = kolorPocisku;
        }

        public void RysujPocisk(Graphics g)
        {
            g.FillRectangle(kolorPocisku, new Rectangle(Lokalizacja.X + 25, Lokalizacja.Y, szerokoscPocisku, dlugoscPocisku));
        }

        /// <summary>
        /// Przesuwanie pocisku po erkanie. Jeżelo pocisk przesunie się poza obszar rysowania metoda zwraca false;
        /// </summary>
        /// <returns></returns>
        public bool PrzesunPocisk()
        {
            if (kierunek == Direction.Gora)
            {
                if (Lokalizacja.Y >= granice.Top)
                {
                    Lokalizacja.Y -= przesunInterval;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (Lokalizacja.Y <= granice.Bottom)
                {
                    Lokalizacja.Y += przesunInterval;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
