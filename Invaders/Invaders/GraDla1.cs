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
    class GraDla1
    {
        public int punkty = 0;
        private int iloscZyc = 3;
        private int iloscStrzalowNajezdzcy = 1;
        //poziomTrudnosc inicjalizuje także ilosc najeźdzców w linni. 
        //Podczas wyświetalania poziomTrudnsci -3. Ilosc możliwych poziomów wynosi 7
        private int poziomTrudnosci = 4;
        EventArgs e = null;

        private Direction kierunekNajezdzcow = Direction.Prawo;

        private Rectangle granice;
        private Random losuj;

        private List<Najezdzca> Najezdzcy = new List<Najezdzca>();
        private List<Strzal> pociskiNajezdzcow = new List<Strzal>();

        private StatekGracza statekGracza;
        private List<Strzal> pociskiGracza = new List<Strzal>();
        

        private Gwiazdy gwiazdy;

        public event EventHandler GameOver;
        public event EventHandler PlayerWins;

        /***Dodanie dźwięków z wykorzystaniem bibliotek WMPLib. Takie rozwiązanie gwarantuje
        odtworzenie kilku dźwięków na raz***/
        WMPLib.WindowsMediaPlayer laserShot = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer boom = new WMPLib.WindowsMediaPlayer();

        public static bool wavSkopiowane = true;

        /// <summary>
        /// Kopiowanie plików *.wav do folderu Temp
        /// </summary>
        public static void kopiujWav()
        {
            if (File.Exists(Path.GetTempPath() + "SoundLaserShot.wav"))
            {
                //Plik istnieje;
                wavSkopiowane = true;
            }
            else
            {
                try
                {
                    File.Copy(@"Resources\SoundLaserShot.wav", Path.GetTempPath() + "SoundLaserShot.wav");
                    wavSkopiowane = true;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Błąd: " + e.Message);
                    wavSkopiowane = false;
                }

            }

            if (File.Exists(Path.GetTempPath() + "SoundBoom.wav"))
            {
                //Plik istnieje
                wavSkopiowane = true;
            }
            else
            {
                try
                {
                    File.Copy(@"Resources\SoundBoom.wav", Path.GetTempPath() + "SoundBoom.wav");
                    wavSkopiowane = true;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Błąd: " + e.Message);
                    wavSkopiowane = false;
                }
            }
        }

        /// <summary>
        /// Konstruktor klasy Gra
        /// </summary>
        /// <param name="gwiazdy"></param>
        /// <param name="statekGracza"></param>
        /// <param name="obszarGry"></param>
        /// <param name="losuj"></param>
        public GraDla1(Gwiazdy gwiazdy, StatekGracza statekGracza, Rectangle obszarGry, Random losuj)
        {           
            this.gwiazdy = gwiazdy;
            this.statekGracza = statekGracza;
            this.granice = obszarGry;
            this.losuj = losuj;
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
        public void RysujGre(Graphics g)
        {
            g.DrawRectangle(new Pen(Brushes.Black, 0), granice);

            gwiazdy.RysujGwiazdy(g);

            for (int i = 0; i < Najezdzcy.Count; i++)
            {
                Najezdzcy[i].RysujStatek(g);
            }
            for (int i = 0; i < pociskiNajezdzcow.Count; i++)
            {
                pociskiNajezdzcow[i].RysujPocisk(g);
            }
           
            statekGracza.RysujStatek(g);
            for (int i = 0; i < pociskiGracza.Count; i++)
            {
                pociskiGracza[i].RysujPocisk(g);
            }

            g.DrawString("Pilot: " + statekGracza.NazwaStatku + " Ilość żyć: " + iloscZyc + " punkty: " + punkty + 
                " Poziom trudności: " + (poziomTrudnosci - 3), new Font("Arial",10,FontStyle.Regular),Brushes.Green,0,640);
        }

        /// <summary>
        /// Inicjalizacja najeźdzców. poziomTrudnośc odpowiada za ilość najeźdzców w fali.
        /// </summary>
        private void InicjalizacjaNajezdzcow()
        {
            int j = 1;
            for (int i = 0; i < poziomTrudnosci; i++)
            {
                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.Niszczyciel, new Point(j, 0), 30,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.Niszczyciel, 51, 51)));

                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.MysliwiecStealth, new Point(j, 60), 20,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.MysliwiecStealth, 51, 51)));

                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.Mysliwiec, new Point(j, 120), 15,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.Mysliwiec, 51, 51)));

                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.Dron,new Point(j, 180), 10,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.Dron, 51, 51)));
                j = j +55; 
            }
        }

        public void MrugajGwiazdami()
        {
            gwiazdy.Migotanie(losuj);
        }

        public void PrzesunGracza(Direction kierunek)
        {
            if (statekGracza.Zywy == true)
            {
                statekGracza.PrzesunStatek(kierunek,granice);
            }
        }

        /// <summary>
        /// Dodanie pocisku gracza do listy pociskiGracza. Lokalizacja.Y jest zmieniona ze względu na lokacje pocisku
        /// Po wywłoaniu metoda zostaje odtworzony dźwięk laserShot.
        /// Na ekranie mogą przebywać maks dwa pociki gracza
        /// </summary>
        /// <param name="Lokalizacja"></param>
        public void WystrzelPociskGracza(Point Lokalizacja)
        {
            Point lokalizacjaPocisku = new Point(Lokalizacja.X, Lokalizacja.Y - 25);
            if (pociskiGracza.Count < 2)
            {
                pociskiGracza.Add(new Strzal(lokalizacjaPocisku, Direction.Gora, granice, Brushes.DeepSkyBlue));
                if (wavSkopiowane)
                {
                    laserShot.URL = Path.GetTempPath() + "SoundLaserShot.wav";
                }
            }
        }

        /// <summary>
        /// Metoda grupuje strzelców po lokalizacji X. Następnie strzelec jest losowany i wybierany
        /// Do strzelca jest przypiswany pocik z zmienioną lokalizacją Y. Generowany jest nowy obiekt strzał
        /// i jeżeli liczba pocisków najeźdzców jest mniejsza od dozwolonych strzałów pociski są dodawane do listy
        /// </summary>
        private void WystzelPociskNajezdzcy()
        {
            if (Najezdzcy.Count == 0) return;

            var strzelcy = from _strzelcy in Najezdzcy
                           group _strzelcy by _strzelcy.Lokalizacja.X into _strzelcyGroup
                           orderby _strzelcyGroup.Key descending
                           select _strzelcyGroup;

            var losujStrzelca = strzelcy.ElementAt(losuj.Next(strzelcy.ToList().Count));
            var dolnyStrzelec = losujStrzelca.Last();

            Point lokalizacjaPocisku = new Point(dolnyStrzelec.Lokalizacja.X, dolnyStrzelec.Lokalizacja.Y + 26);
            Strzal pociskNajezdzcy = new Strzal(lokalizacjaPocisku, Direction.Dol, granice, Brushes.Red);

            if (pociskiNajezdzcow.Count < iloscStrzalowNajezdzcy)
            {
                pociskiNajezdzcow.Add(pociskNajezdzcy);
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
                    where _najezdzca.wielkoscNajezdzcy.Contains(strzal.Lokalizacja) == true
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
                    boom.URL = Path.GetTempPath() + "SoundBoom.wav";
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
                                   where celnyPocisk.Kierunek == Direction.Dol && statekGracza.wielkoscStatku.Contains(celnyPocisk.Lokalizacja)
                                   select celnyPocisk;
            
            if (graczZestrzelony.Count() > 0)
            {
                iloscZyc--;
                if (wavSkopiowane)
                {
                    boom.URL = Path.GetTempPath() + "SoundBoom.wav";
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
                    GameOver(this, e);
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

        /// <summary>
        /// Generowanie kolejne Falii najeźdzców. w zależności od poziomu trudności zmieniania jest zmienna
        /// iloscStrzalowNajezdzcy
        /// </summary>
        private void nastepnaFala()
        {
            if (Najezdzcy.Count == 0)
            {
                // Poziom trudnosci -1 dla poprawnego czyszczenia najezdzcow po zakonczonej fali 9
                if (poziomTrudnosci > 9)
                {
                    pociskiGracza.Clear();
                    pociskiNajezdzcow.Clear();
                    PlayerWins(this, e);
                }
                else
                {
                    poziomTrudnosci++;

                    kierunekNajezdzcow = Direction.Prawo;

                    if (iloscStrzalowNajezdzcy <= 2 && poziomTrudnosci > 9)
                        iloscStrzalowNajezdzcy++;

                    Najezdzcy.Clear();
                    InicjalizacjaNajezdzcow();
                }
            }
        }

        /// <summary>
        /// Linq sprawdzę czy falaNajeźdzców dotarła do którejś z krawędzie. 
        /// Jeżeli tak to wykonywana jest procedura przesunięcia najeźdzców
        /// </summary>
        private void PrzesunNajezdzcow()
        {
            var najezdzcyPrawo = from _najezdzcy in Najezdzcy
                                 where _najezdzcy.wielkoscNajezdzcy.Right >= granice.Right
                                 group _najezdzcy by _najezdzcy.TypNajezdzcy
                                     into _najezdzcyGroup
                                     select _najezdzcyGroup;

            var najezdzcyLewo = from _najezdzcy in Najezdzcy
                                where _najezdzcy.wielkoscNajezdzcy.Left <= granice.Left
                                group _najezdzcy by _najezdzcy.TypNajezdzcy
                                    into _najezdzcyGroup
                                    select _najezdzcyGroup;

            var najezdzcyDol = from _najezdzcy in Najezdzcy
                               where _najezdzcy.wielkoscNajezdzcy.Bottom >= granice.Bottom - 51
                               group _najezdzcy by _najezdzcy.TypNajezdzcy
                                   into _najezdzcyGroup
                                   select _najezdzcyGroup;

            foreach (var najezdzca in najezdzcyPrawo)
            {
                kierunekNajezdzcow = Direction.Dol;
                for (int i = 0; i < Najezdzcy.Count; i++)
                {
                    Najezdzcy[i].Przesun(kierunekNajezdzcow);
                }
                kierunekNajezdzcow = Direction.Lewo;
                break;
            }

            foreach (var najezdzca in najezdzcyLewo)
            {
                kierunekNajezdzcow = Direction.Dol;
                for (int i = 0; i < Najezdzcy.Count; i++)
                {
                    Najezdzcy[i].Przesun(kierunekNajezdzcow);
                }
                kierunekNajezdzcow = Direction.Prawo;
                break;
            }

            foreach (var najezdzca in najezdzcyDol)
            {
                pociskiGracza.Clear();
                pociskiNajezdzcow.Clear();
                GameOver(this, e);
            }

            //Przesunięcie całej falii najeźców
            for (int i = 0; i < Najezdzcy.Count; i++)
            {
                Najezdzcy[i].Przesun(kierunekNajezdzcow);
            }
        }
        /// <summary>
        /// Metoda steruje całą rozgrywką
        /// </summary>
        public void Go()
        {
            //Przesuwanie pocisków najeźdzcy. Jeżeli metoda zwróci false to znaczy że pocisk znalazł się poza 
            //obszarem rysowania i pocisk jest usuwany z listy
            for (int i = 0; i < pociskiNajezdzcow.Count; i++)
            {
                if (!pociskiNajezdzcow[i].PrzesunPocisk())
                {
                    pociskiNajezdzcow.RemoveAt(i);
                }
            }

            //Przesuwanie pocisków gracza. eżeli metoda zwróci false to znaczy że pocisk znalazł się poza 
            //obszarem rysowania i pocisk jest usuwany z listy
            for (int i = 0; i < pociskiGracza.Count; i++)
            {
                if (!pociskiGracza[i].PrzesunPocisk())
                {
                    pociskiGracza.RemoveAt(i);
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
                if (Najezdzcy[i].Zestrzelony == true && Najezdzcy[i].koniecAnimacji == true)
                {
                    punkty += Najezdzcy[i].IloscPunktow;
                    Najezdzcy.RemoveAt(i); 
                }
            }

            nastepnaFala();
        }
    }
}