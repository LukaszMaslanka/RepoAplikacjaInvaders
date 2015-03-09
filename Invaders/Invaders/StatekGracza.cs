﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invaders
{
    class StatekGracza
    {
        public String GraczName;

        private Gracze TypGracza;

        public Point Lokalizacja;

        private Bitmap obraz;

        private Rectangle obszarGry;

        public Rectangle wielkoscStatku
        {
            get
            {
                return new Rectangle(Lokalizacja, obraz.Size);
            }
        }

        private bool zywy = true;
        public bool Zywy 
        {
            get
            {
                return zywy;
            }
            set
            {
                zywy = value;
            }
        }

        Bitmap[] statekGracz1Bitmap;
        Bitmap[] statekGracz2Bitmap;
        Bitmap[] eksplozja;

        public StatekGracza(Point lokalizacja, Gracze TypGracza, Rectangle obszarGry,String GraczName)
        {
            this.GraczName = GraczName;
            this.TypGracza = TypGracza;
            this.Lokalizacja = lokalizacja;
            this.obszarGry = obszarGry;
            InicjalizacjaObrazow();
        }

        private void InicjalizacjaObrazow()
        {
            statekGracz1Bitmap = new Bitmap[4];
            statekGracz1Bitmap[0] = Rysuj.KonwertujNaBitmap(Properties.Resources.Player1, 51, 51);
            statekGracz1Bitmap[1] = Rysuj.KonwertujNaBitmap(Properties.Resources.player1_2, 51, 51);
            statekGracz1Bitmap[2] = Rysuj.KonwertujNaBitmap(Properties.Resources.player1_3, 51, 51);
            statekGracz1Bitmap[3] = Rysuj.KonwertujNaBitmap(Properties.Resources.player1_4, 51, 51);

            statekGracz2Bitmap = new Bitmap[4];
            statekGracz2Bitmap[0] = Rysuj.KonwertujNaBitmap(Properties.Resources.Player2, 51, 51);
            statekGracz2Bitmap[1] = Rysuj.KonwertujNaBitmap(Properties.Resources.player2_2, 51, 51);
            statekGracz2Bitmap[2] = Rysuj.KonwertujNaBitmap(Properties.Resources.player2_3, 51, 51);
            statekGracz2Bitmap[3] = Rysuj.KonwertujNaBitmap(Properties.Resources.player2_4, 51, 51);

            eksplozja = new Bitmap[6];
            eksplozja[0] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_1, 51, 51);
            eksplozja[1] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_2, 51, 51);
            eksplozja[2] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_3, 51, 51);
            eksplozja[3] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_4, 51, 51);
            eksplozja[4] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_5, 51, 51);
            eksplozja[5] = Rysuj.KonwertujNaBitmap(Properties.Resources.boom_6, 51, 51);
        }

        public void RysujStatek(Graphics g)
        {
            if (Zywy == true)
            {
                ObrazStatku();
                g.DrawImage(obraz, Lokalizacja);
            }
            else
            {
                statekDestroy();
                g.DrawImage(obraz, Lokalizacja);
            }
        }

        public void PrzesunStatek(Direction kierunek)
        {
            if (kierunek == Direction.Prawo)
            {
                if (Lokalizacja.X + 51 >= obszarGry.Right)
                {
                    Lokalizacja.X = obszarGry.Right - 51;
                }
                else
                {
                    Lokalizacja.X += 10;
                }
            }
            else if (kierunek == Direction.Lewo)
            {
                if (Lokalizacja.X <= obszarGry.Left)
                {
                    Lokalizacja.X = obszarGry.Left;
                }
                else
                {
                    Lokalizacja.X -= 10;
                }
            }
        }

        int klatka = 0;
        private Bitmap ObrazStatku()
        {
            klatka++;
            if (TypGracza == Gracze.Player1)
	        {
                switch (klatka)
                {
                    case 1: obraz = statekGracz1Bitmap[0]; return obraz; 
                    case 2: obraz = statekGracz1Bitmap[1]; return obraz; 
                    case 3: obraz = statekGracz1Bitmap[2]; return obraz; 
                    case 4: obraz = statekGracz1Bitmap[3]; return obraz;
                    default: klatka = 0; obraz = statekGracz1Bitmap[0]; return obraz;
                }
	        }
            else
            {
                switch (klatka)
                {
                    case 1: obraz = statekGracz2Bitmap[0]; return obraz;
                    case 2: obraz = statekGracz2Bitmap[1]; return obraz;
                    case 3: obraz = statekGracz2Bitmap[2]; return obraz;
                    case 4: obraz = statekGracz2Bitmap[3]; return obraz;
                    default: klatka = 0; obraz = statekGracz2Bitmap[0]; return obraz;
                }
            }  
        }

        int klatka2 = 0;
        private Bitmap statekDestroy()
        {
            klatka2++;
            switch (klatka2)
            {
                case 1: obraz = eksplozja[0]; return obraz;
                case 2: obraz = eksplozja[1]; return obraz;
                case 3: obraz = eksplozja[2]; return obraz;
                case 4: obraz = eksplozja[3]; return obraz;
                case 5: obraz = eksplozja[4]; return obraz;
                case 6: obraz = eksplozja[5]; return obraz;
                default: klatka2 = 0; obraz = eksplozja[0]; Zywy = true; return obraz;
            }
        }
    }
}
