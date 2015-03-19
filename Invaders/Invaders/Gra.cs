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
        int iloscStrzalowNajezdzcy = 1;
        public int iloscNajezdzcowWLinii = 4;

        public String wyniki;
        
        Direction kierunekNajezdzcow = Direction.Prawo;
        
        public Rectangle granice;

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
            this.granice = granice;
            this.losuj = losuj;
        }

        virtual public void RysujGre(Graphics g)
        {

        }

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

        public void MrugajGwiazdami()
        {
            Gwiazdy.Migotanie(losuj);
        }

        /// <summary>
        /// Inicjalizacja najeźdzców. poziomTrudnośc odpowiada za ilość najeźdzców w fali.
        /// </summary>
        public void InicjalizacjaNajezdzcow()
        {
            int j = 1;
            for (int i = 0; i < iloscNajezdzcowWLinii; i++)
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

        public void PrzesunNajezdzcow()
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
                pociskiNajezdzcow.Clear();
                GameOver(this,e);
            }

            //Przesunięcie całej falii najeźców
            for (int i = 0; i < Najezdzcy.Count; i++)
            {
                Najezdzcy[i].Przesun(kierunekNajezdzcow);
            }
        }

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
            Strzal pociskNajezdzcy = new Strzal(lokalizacjaPocisku, Direction.Dol, granice, Brushes.Red);

            if (pociskiNajezdzcow.Count < iloscStrzalowNajezdzcy)
            {
                pociskiNajezdzcow.Add(pociskNajezdzcy);
            }
        }

        /// <summary>
        /// Generowanie kolejne Falii najeźdzców. w zależności od poziomu trudności zmieniania jest zmienna
        /// iloscStrzalowNajezdzcy
        /// </summary>
        public void nastepnaFala(int iloscFal)
        {
            if (Najezdzcy.Count == 0)
            {
                // Poziom trudnosci -1 dla poprawnego czyszczenia najezdzcow po zakonczonej fali 9
                if (PoziomTrudnosci == iloscFal)
                {
                    pociskiNajezdzcow.Clear();
                    GraczWygral = true;
                    GameOver(this, e);
                }
                else
                {
                    iloscNajezdzcowWLinii++;
                    PoziomTrudnosci++;

                    kierunekNajezdzcow = Direction.Prawo;

                    if (PoziomTrudnosci > 4)
                        iloscStrzalowNajezdzcy++;

                    Najezdzcy.Clear();
                    InicjalizacjaNajezdzcow();
                }
            }
        }
    }
}
