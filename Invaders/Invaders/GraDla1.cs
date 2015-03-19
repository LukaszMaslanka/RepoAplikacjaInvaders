using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using WMPLib;
using System.IO;

namespace Invaders
{
    class GraDla1 : Gra
    {
        private StatekGracza statekGracza;
        private List<Strzal> pociskiGracza = new List<Strzal>();

        EventArgs e;
        public event EventHandler GameOverGracz1;

        public int Punkty = 0;
        private int iloscZyc = 3;

        
        /// <summary>
        /// Konstruktor klasy Gra
        /// </summary>
        /// <param name="gwiazdy"></param>
        /// <param name="statekGracza"></param>
        /// <param name="obszarGry"></param>
        /// <param name="losuj"></param>
        public GraDla1(Gwiazdy gwiazdy, Rectangle granice, Random losuj, StatekGracza statekGracza)
            :base(gwiazdy,granice,losuj)
        {           
            this.statekGracza = statekGracza;

            IloscNajezdzcowWLinii = 4;
            InicjalizacjaNajezdzcow();
        }

        /// <summary>
        /// Rysowanie gry na formularzu. 
        /// Najpierw rysowany jest kwadrat który wzynacza granicę gry.
        /// Następnie rysowane są:
        /// Gwiazdy
        /// Statki Najeźdzców
        /// Pociski Najeźedzców
        /// Statek Gracza
        /// Pociski Gracza
        /// Na końcu rysowane są napisy znajdujące się w stopcje formularza
        /// </summary>
        /// <param name="g"></param>
        override public void RysujGre(Graphics g)
        {
            g.DrawRectangle(new Pen(Brushes.Black, 0), Granice);
            Gwiazdy.RysujGwiazdy(g);
         
            statekGracza.RysujStatek(g);
            for (int i = 0; i < pociskiGracza.Count; i++)
            {
                pociskiGracza[i].RysujPocisk(g);
            }

            for (int i = 0; i < Najezdzcy.Count; i++)
            {
                Najezdzcy[i].RysujStatek(g);
            }
            for (int i = 0; i < pociskiNajezdzcow.Count; i++)
            {
                pociskiNajezdzcow[i].RysujPocisk(g);
            }

            g.DrawString("Pilot: " + statekGracza.NazwaStatku + " Ilość żyć: " + iloscZyc + " punkty: " + Punkty + 
                " Poziom trudności: " + (PoziomTrudnosci), new Font("Arial",10,FontStyle.Regular),Brushes.Green,0,640);
        }
        /// <summary>
        /// Przesuwanie gracza do granic pola walki.
        /// </summary>
        /// <param name="kierunek"></param>
        public void PrzesunGracza(Direction kierunek)
        {
            if (statekGracza.Zywy == true)
            {
                statekGracza.PrzesunStatek(kierunek,Granice);
            }
        }

        /// <summary>
        /// Dodanie pocisku gracza do listy pociskiGracza. Lokalizacja.Y jest zmieniona ze względu na lokacje pocisku
        /// Po wywłoaniu metody zostaje odtworzony dźwięk laserShot.
        /// Na ekranie mogą przebywać maks dwa pociki gracza
        /// </summary>
        /// <param name="Lokalizacja"></param>
        public void WystrzelPociskGracza(Point Lokalizacja)
        {
            Point lokalizacjaPocisku = new Point(Lokalizacja.X, Lokalizacja.Y - 25);
            if (pociskiGracza.Count < 2)
            {
                pociskiGracza.Add(new Strzal(lokalizacjaPocisku, Direction.Gora, Granice, Brushes.DeepSkyBlue));
                if (wavSkopiowane)
                {
                    LaserShot.URL = Path.GetTempPath() + "SoundLaserShot.wav";
                }
            }
        }

        /// <summary>
        /// Metoda sprawdza czy najeźdzca został trafiony pociskiem gracza.
        /// Tworzone są dwie listy następnie w zapytaniu sprawdzany jest warunek czy lokalizacja pocisku gracza
        /// pokrywa się z lokalizacją najeźdzcy. Jeżeli tak dane są dodwana do list.
        /// Dla każdego najeźdzyc w liście zestrzeleniNajezdzcy generowany jest dźwięk wybuchu a falga trafiony
        /// zsotaje zmieniona na true. Następnie pociki są usuwane z listy pociskiGracza
        /// </summary>
        private void NajezdzcaTrafiony()
        {
            List<Strzal> strzalytrafione = new List<Strzal>();
            List<Najezdzca> zestrzeleniNajezdzcy = new List<Najezdzca>();
            
            foreach (Strzal strzal in pociskiGracza)
            {
                var zestrzeleni = from _najezdzca in Najezdzcy
                    where _najezdzca.WielkoscNajezdzcy.Contains(strzal.Lokalizacja) == true
                        && strzal.Kierunek == Direction.Gora
                    select new {zestrzeleniNajezdzcy = _najezdzca, StrzalTrafiony = strzal};
                
                if (zestrzeleni.Count() > 0)
                {
                    foreach (var zestrzelony in zestrzeleni)
                    {
                        strzalytrafione.Add(zestrzelony.StrzalTrafiony);
                        zestrzeleniNajezdzcy.Add(zestrzelony.zestrzeleniNajezdzcy);
                    }   
                }
            }

            foreach (Najezdzca najezdzca in zestrzeleniNajezdzcy)
            {
                if (wavSkopiowane)
                {
                    Boom.URL = Path.GetTempPath() + "SoundBoom.wav";
                }
                najezdzca.Zestrzelony = true;
            }

            foreach (Strzal shot in strzalytrafione)
            {
                pociskiGracza.Remove(shot);
            }
        }
        
        /// <summary>
        /// Analogicznie do NajeźdzcaTrafiony. Gdy iloscZyc Gracza jest == 0 zostaje wygenerowany Event GameOver
        /// </summary>
        private void GraczTrafiony()
        {   
            bool usunPocisk = false;
            var graczZestrzelony = from celnyPocisk in pociskiNajezdzcow
                                   where celnyPocisk.Kierunek == Direction.Dol && statekGracza.WielkoscStatku.Contains(celnyPocisk.Lokalizacja)
                                   select celnyPocisk;
            
            if (graczZestrzelony.Count() > 0)
            {
                iloscZyc--;
                if (wavSkopiowane)
                {
                    Boom.URL = Path.GetTempPath() + "SoundBoom.wav";
                }
                
                statekGracza.Zywy = false;

                if (iloscZyc > 0)
                {
                    usunPocisk = true;
                }
                else
                {
                    pociskiGracza.Clear();
                    pociskiNajezdzcow.Clear();
                    GameOverGracz1(this, e);
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
            //Przesuwanie pocisków gracza. eżeli metoda zwróci false to znaczy że pocisk znalazł się poza 
            //obszarem rysowania i pocisk jest usuwany z listy
            for (int i = 0; i < pociskiGracza.Count; i++)
            {
                if (!pociskiGracza[i].PrzesunPocisk())
                {
                    pociskiGracza.RemoveAt(i);
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
            //Suma punktów odbywa się tutuaj aby nie dodawać punktów graczowi za trafienie w wybuch.
            for (int i = 0; i < Najezdzcy.Count; i++)
            {
                if (Najezdzcy[i].Zestrzelony == true && Najezdzcy[i].KoniecAnimacji == true)
                {
                    Punkty += Najezdzcy[i].IloscPunktow;
                    Najezdzcy.RemoveAt(i); 
                }
            }
        }
    }
}