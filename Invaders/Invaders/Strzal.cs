﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invaders
{
    class Strzal
    {
        private const int przesunInterval = 20;
        private const int szerokoscPocisku = 5;
        private const int dlugoscPocisku = 15;

        public Point Lokalizacja;

        private Direction kierunek;
        private Rectangle granice;

        private Brush kolorPocisku;

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

        public bool PrzesunPocisk(Direction kierunek)
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