﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Invaders
{
    class GraDla2 : Gra
    {
        StatekGracza statekGracza1;
        List<Strzal> pociskiGracza1 = new List<Strzal>();
        StatekGracza statekGracza2;
        List<Strzal> pociskiGracza2 = new List<Strzal>();

        EventArgs e;
        public event EventHandler Remis;
        public event EventHandler GameOverGracz1;
        public event EventHandler GameOverGracz2;

        public int PunktyGracz1 = 0;
        public int PunktyGracz2 = 0;
        int iloscZycGracz1 = 3;
        int iloscZycGracz2 = 3;

        public GraDla2(Gwiazdy gwiazdy, Rectangle granice, Random losuj, 
            StatekGracza statekGracza1, StatekGracza statekGracza2)
            :base(gwiazdy, granice, losuj)
        {
            this.statekGracza1 = statekGracza1;
            this.statekGracza2 = statekGracza2;

            IloscNajezdzcowWLinii = 10;
            InicjalizacjaNajezdzcow();
        }
        /// <summary>
        /// Przesłoniona metoda RysujGre
        /// </summary>
        /// <param name="g"></param>
        override public void RysujGre(Graphics g)
        {
            g.DrawRectangle(new Pen(Brushes.Black, 1), Granice);
            Gwiazdy.RysujGwiazdy(g);

            statekGracza1.RysujStatek(g);
            for (int i = 0; i < pociskiGracza1.Count; i++)
            {
                pociskiGracza1[i].RysujPocisk(g);
            }

            statekGracza2.RysujStatek(g);
            for (int i = 0; i < pociskiGracza2.Count; i++)
            {
                pociskiGracza2[i].RysujPocisk(g);
            }

            for (int i = 0; i < Najezdzcy.Count; i++)
            {
                Najezdzcy[i].RysujStatek(g);
            }
            
            for (int i = 0; i < pociskiNajezdzcow.Count; i++)
            {
                pociskiNajezdzcow[i].RysujPocisk(g);
            }

            g.DrawString("Pilot: " + statekGracza1.NazwaStatku + " Ilość żyć: " + iloscZycGracz1 + " punkty: " + PunktyGracz1 +
                " Poziom trudności: " + (PoziomTrudnosci), new Font("Arial", 10, FontStyle.Regular), Brushes.DeepSkyBlue,595 , 630);

            g.DrawString("Pilot: " + statekGracza2.NazwaStatku + " Ilość żyć: " + iloscZycGracz2 + " punkty: " + PunktyGracz2 +
                " Poziom trudności: " + (PoziomTrudnosci), new Font("Arial", 10, FontStyle.Regular), Brushes.Violet, 0, 630);
        }
        /// <summary>
        /// Przesuwanie po formatce gracza1
        /// </summary>
        /// <param name="kierunek"></param>
        public void PrzesunGracza1(Kierunek kierunek)
        {
            if (statekGracza1.Zywy == true)
            {
                statekGracza1.PrzesunStatek(kierunek, Granice);
            }
        }
        /// <summary>
        /// Dodanie nowego pocisku do listy pociskiGracza1
        /// </summary>
        /// <param name="Lokalizacja"></param>
        public void WystrzelPociskGracza1(Point Lokalizacja)
        {
            Point lokalizacjaPocisku = new Point(Lokalizacja.X, Lokalizacja.Y - 25);
            if (pociskiGracza1.Count < 2)
            {
                pociskiGracza1.Add(new Strzal(lokalizacjaPocisku, Kierunek.Gora, Granice, Brushes.DeepSkyBlue));
                if (ObslugaPlikow.wavSkopiowane)
                {
                    LaserShot.URL = Path.GetTempPath() + "SoundLaserShot.wav";
                }
            }
        }
        /// <summary>
        /// Przesuwanie po formatce gracza2
        /// </summary>
        /// <param name="kierunek"></param>
        public void PrzesunGracza2(Kierunek kierunek)
        {
            if (statekGracza2.Zywy == true)
            {
                statekGracza2.PrzesunStatek(kierunek,Granice);
            }
        }
        /// <summary>
        /// Dodanie nowego pocisku do listy pociskiGracza2
        /// </summary>
        /// <param name="Lokalizacja"></param>
        public void WystrzelPociskGracza2(Point Lokalizacja)
        {
            Point lokalizacjaPocisku = new Point(Lokalizacja.X, Lokalizacja.Y - 25);
            if (pociskiGracza2.Count < 2)
            {
                pociskiGracza2.Add(new Strzal(lokalizacjaPocisku, Kierunek.Gora, Granice, Brushes.Violet));
                if (ObslugaPlikow.wavSkopiowane)
                {
                    LaserShot.URL = Path.GetTempPath() + "SoundLaserShot.wav";
                }
            }
        }
        /// <summary>
        /// Metoda sprawdza czy pocisk graczy pokrywają się z polem najeźdzcy.
        /// </summary>
        private void NajezdzcaTrafiony()
        {
            List<Strzal> strzalytrafione = new List<Strzal>();
            List<Najezdzca> zestrzeleniNajezdzcyGracz1 = new List<Najezdzca>();
            List<Najezdzca> zestrzeleniNajezdzcyGracz2 = new List<Najezdzca>();

            foreach (Strzal strzal in pociskiGracza1)
            {
                var zestrzeleni = from _najezdzca in Najezdzcy
                                  where _najezdzca.WielkoscNajezdzcy.Contains(strzal.Lokalizacja) == true
                                      && strzal.Kierunek == Kierunek.Gora
                                  select new { zestrzeleniNajezdzcyGracz1 = _najezdzca, StrzalTrafiony = strzal };

                if (zestrzeleni.Count() > 0)
                {
                    foreach (var zestrzelony in zestrzeleni)
                    {
                        strzalytrafione.Add(zestrzelony.StrzalTrafiony);
                        zestrzeleniNajezdzcyGracz1.Add(zestrzelony.zestrzeleniNajezdzcyGracz1);
                    }
                }
            }

            foreach (Najezdzca najezdzca in zestrzeleniNajezdzcyGracz1)
            {
                if (ObslugaPlikow.wavSkopiowane)
                {
                    Boom.URL = Path.GetTempPath() + "SoundBoom.wav";
                }
                najezdzca.Zestrzelony = true;
                PunktyGracz1 += najezdzca.IloscPunktow;
            }

            foreach (Strzal strzal in pociskiGracza2)
            {
                var zestrzeleni = from _najezdzca in Najezdzcy
                                  where _najezdzca.WielkoscNajezdzcy.Contains(strzal.Lokalizacja) == true
                                      && strzal.Kierunek == Kierunek.Gora
                                  select new { zestrzeleniNajezdzcy = _najezdzca, StrzalTrafiony = strzal };

                if (zestrzeleni.Count() > 0)
                {
                    foreach (var zestrzelony in zestrzeleni)
                    {
                        strzalytrafione.Add(zestrzelony.StrzalTrafiony);
                        zestrzeleniNajezdzcyGracz2.Add(zestrzelony.zestrzeleniNajezdzcy);
                    }
                }
            }

            foreach (Najezdzca najezdzca in zestrzeleniNajezdzcyGracz2)
            {
                if (ObslugaPlikow.wavSkopiowane)
                {
                    Boom.URL = Path.GetTempPath() + "SoundBoom.wav";
                }
                najezdzca.Zestrzelony = true;
                PunktyGracz2 += najezdzca.IloscPunktow;
            }

            foreach (Strzal shot in strzalytrafione)
            {
                pociskiGracza1.Remove(shot);
                pociskiGracza2.Remove(shot);
            }
        }

        /// <summary>
        /// Metoda sprawdza czy pocisk najeźdzcy pokrywa się z polem gracza.
        /// </summary>
        private void GraczTrafiony()
        {
            bool usunPocisk = false;
            var gracz1Zestrzelony = from celnyPocisk in pociskiNajezdzcow
                                    where celnyPocisk.Kierunek == Kierunek.Dol && statekGracza1.WielkoscStatku.Contains(celnyPocisk.Lokalizacja) == true
                                    select celnyPocisk;

            var gracz2Zestrzelony = from celnyPocisk in pociskiNajezdzcow
                                    where celnyPocisk.Kierunek == Kierunek.Dol && statekGracza2.WielkoscStatku.Contains(celnyPocisk.Lokalizacja) == true
                                    select celnyPocisk;

            if (gracz1Zestrzelony.Count() > 0)
            {
                iloscZycGracz1--;
                if (ObslugaPlikow.wavSkopiowane)
                {
                    Boom.URL = Path.GetTempPath() + "SoundBoom.wav";
                }

                statekGracza1.Zywy = false;

                if (iloscZycGracz1 > 0)
                {
                    usunPocisk = true;
                }
                else
                {
                    pociskiGracza1.Clear();
                    pociskiNajezdzcow.Clear();
                    GameOverGracz1(this,e);
                }
            }

            if (gracz2Zestrzelony.Count() > 0)
            {
                iloscZycGracz2--;
                if (ObslugaPlikow.wavSkopiowane)
                {
                    Boom.URL = Path.GetTempPath() + "SoundBoom.wav";
                }

                statekGracza2.Zywy = false;

                if (iloscZycGracz2 > 0)
                {
                    usunPocisk = true;
                }
                else
                {
                    pociskiGracza2.Clear();
                    pociskiNajezdzcow.Clear();
                    GameOverGracz2(this, e);
                }
            }

            if (usunPocisk)
            {
                foreach (Strzal strzal in pociskiNajezdzcow.ToList())
                {
                    pociskiNajezdzcow.Remove(strzal);
                }
            }
        }
        
        public void Go()
        {
            //Przesuwanie pocisków gracza. jeżeli metoda zwróci false to znaczy że pocisk znalazł się poza 
            //obszarem rysowania i pocisk jest usuwany z listy
            for (int i = 0; i < pociskiGracza1.Count; i++)
            {
                if (!pociskiGracza1[i].PrzesunPocisk())
                {
                    pociskiGracza1.RemoveAt(i);
                }
            }
            
            //Przesuwanie pocisków gracza. jeżeli metoda zwróci false to znaczy że pocisk znalazł się poza 
            //obszarem rysowania i pocisk jest usuwany z listy
            for (int i = 0; i < pociskiGracza2.Count; i++)
            {
                if (!pociskiGracza2[i].PrzesunPocisk())
                {
                    pociskiGracza2.RemoveAt(i);
                }
            }

            //Przesuwanie pocisków najeźdzcy. Jeżeli metoda zwróci false to znaczy że pocisk znalazł się poza 
            //obszarem rysowania i pocisk jest usuwany z listy
            for (int i = 0; i < pociskiNajezdzcow.Count; i++)
            {
                if (!pociskiNajezdzcow[i].PrzesunPocisk())
                {
                    pociskiNajezdzcow.RemoveAt(i);
                }
            }
            
            WystzelPociskNajezdzcy();
            PrzesunNajezdzcow();
            GraczTrafiony();
            NajezdzcaTrafiony();

            //Usuwanie najeźdzców odbywa się w metodzie Go() ze względu na anicmacje wybuchu.
            for (int i = 0; i < Najezdzcy.Count; i++)
            {
                if (Najezdzcy[i].Zestrzelony == true && Najezdzcy[i].KoniecAnimacji == true)
                {
                    Najezdzcy.RemoveAt(i);
                }
            }
        }
    }
}
