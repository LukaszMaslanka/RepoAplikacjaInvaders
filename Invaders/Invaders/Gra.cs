using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using WMPLib;
using System.IO;

namespace Invaders
{
    public class Gra
    {
        public int PoziomTrudnosci = 1;
        public int IloscNajezdzcowWLinii = 4;
        int iloscStrzalowNajezdzcy = 1;
        
        Kierunek kierunekNajezdzcow = Kierunek.Prawo;
        
        public Rectangle Granice;

        Random losuj;

        public bool GraczWygral = false;

        public List<Najezdzca> Najezdzcy = new List<Najezdzca>();
        public List<Strzal> pociskiNajezdzcow = new List<Strzal>();

        public Gwiazdy Gwiazdy;

        EventArgs e = null;
        public event EventHandler GameOver;

        public WindowsMediaPlayer LaserShot = new WindowsMediaPlayer();
        public WindowsMediaPlayer Boom = new WindowsMediaPlayer();

        public Gra(Gwiazdy gwiazdy, Rectangle granice, Random losuj)
        {
            this.Gwiazdy = gwiazdy;
            this.Granice = granice;
            this.losuj = losuj;
        }

        //Wirtualna metoda do przesłonienia w klasach potomnych
        virtual public void RysujGre(Graphics g)
        {

        }
        /// <summary>
        /// Wywołanie metody z klasy Gwiazdy
        /// </summary>
        public void MrugajGwiazdami()
        {
            Gwiazdy.Migotanie(losuj);
        }

        /// <summary>
        /// Inicjalizacja najeźdzców.
        /// </summary>
        public void InicjalizacjaNajezdzcow()
        {
            int j = 1;
            for (int i = 0; i < IloscNajezdzcowWLinii; i++)
            {
                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.Niszczyciel, new Point(j, 0), 30,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.Niszczyciel, 51, 51)));

                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.MysliwiecStealth, new Point(j, 60), 20,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.MysliwiecStealth, 51, 51)));

                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.Mysliwiec, new Point(j, 120), 15,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.Mysliwiec, 51, 51)));

                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.Dron, new Point(j, 180), 10,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.Dron, 51, 51)));
                j = j + 55;
            }
        }

        /// <summary>
        /// Przesunięcie najeźdzców do granicy pola walki. Nastepnie następuje procedura zmiany kierunku.
        /// </summary>
        public void PrzesunNajezdzcow()
        {
            var najezdzcyPrawo = from _najezdzcy in Najezdzcy
                                 where _najezdzcy.WielkoscNajezdzcy.Right >= Granice.Right
                                 group _najezdzcy by _najezdzcy.TypNajezdzcy
                                     into _najezdzcyGroup
                                     select _najezdzcyGroup;

            var najezdzcyLewo = from _najezdzcy in Najezdzcy
                                where _najezdzcy.WielkoscNajezdzcy.Left <= Granice.Left
                                group _najezdzcy by _najezdzcy.TypNajezdzcy
                                    into _najezdzcyGroup
                                    select _najezdzcyGroup;

            var najezdzcyDol = from _najezdzcy in Najezdzcy
                               where _najezdzcy.WielkoscNajezdzcy.Bottom >= Granice.Bottom - 51
                               group _najezdzcy by _najezdzcy.TypNajezdzcy
                                   into _najezdzcyGroup
                                   select _najezdzcyGroup;

            foreach (var najezdzca in najezdzcyPrawo)
            {
                kierunekNajezdzcow = Kierunek.Dol;
                for (int i = 0; i < Najezdzcy.Count; i++)
                {
                    Najezdzcy[i].Przesun(kierunekNajezdzcow);
                }
                kierunekNajezdzcow = Kierunek.Lewo;
                break;
            }

            foreach (var najezdzca in najezdzcyLewo)
            {
                kierunekNajezdzcow = Kierunek.Dol;
                for (int i = 0; i < Najezdzcy.Count; i++)
                {
                    Najezdzcy[i].Przesun(kierunekNajezdzcow);
                }
                kierunekNajezdzcow = Kierunek.Prawo;
                break;
            }

            foreach (var najezdzca in najezdzcyDol)
            {
                pociskiNajezdzcow.Clear();
                GraczWygral = true;
                GameOver(this,e);
            }

            //Przesunięcie całej falii najeźców
            for (int i = 0; i < Najezdzcy.Count; i++)
            {
                Najezdzcy[i].Przesun(kierunekNajezdzcow);
            }
        }

        /// <summary>
        /// Pogrupowanie najeźdzców według lokalizacji X. Wylosowanie strzelca i dodanie pocisku o lokalizacji strzelca.
        /// </summary>
        public void WystzelPociskNajezdzcy()
        {
            if (Najezdzcy.Count == 0) return;

            var strzelcy = from _strzelcy in Najezdzcy
                           group _strzelcy by _strzelcy.Lokalizacja.X into _strzelcyGroup
                           orderby _strzelcyGroup.Key descending
                           select _strzelcyGroup;

            var losujStrzelca = strzelcy.ElementAt(losuj.Next(strzelcy.ToList().Count));
            var dolnyStrzelec = losujStrzelca.Last();

            Point lokalizacjaPocisku = new Point(dolnyStrzelec.Lokalizacja.X, dolnyStrzelec.Lokalizacja.Y + 26);
            Strzal pociskNajezdzcy = new Strzal(lokalizacjaPocisku, Kierunek.Dol, Granice, Brushes.Red);

            if (pociskiNajezdzcow.Count < iloscStrzalowNajezdzcy)
            {
                pociskiNajezdzcow.Add(pociskNajezdzcy);
            }
        }
        /// <summary>
        /// Generowanie następnej fali najeźdzcow
        /// </summary>
        /// <param name="iloscFal"></param>
        public void NastepnaFala(int iloscFal)
        {
            if (Najezdzcy.Count == 0)
            {
                if (PoziomTrudnosci == iloscFal)
                {
                    pociskiNajezdzcow.Clear();
                    GraczWygral = true;
                    GameOver(this, e);
                }
                else
                {
                    IloscNajezdzcowWLinii++;
                    PoziomTrudnosci++;

                    kierunekNajezdzcow = Kierunek.Prawo;

                    if (PoziomTrudnosci > 4)
                        iloscStrzalowNajezdzcy++;

                    Najezdzcy.Clear();
                    InicjalizacjaNajezdzcow();
                }
            }
        }
    }
}
