using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invaders
{
    class Gra
    {
        private int punkty = 0;
        private int iloscZyc = 2;
        private int fala = 0;
        private int pominieteKlatki = 0;

        private Direction kierunekNajezdzcow = Direction.Prawo;

        private Rectangle granice;
        private Random losuj;

        private List<Najezdzca> Najezdzcy = new List<Najezdzca>();

        private StatekGracza statekGracza;
        private List<Strzal> pociskiGracza = new List<Strzal>();
        private List<Strzal> pociskiNajezdzcow = new List<Strzal>();

        private Gwiazdy gwiazdy;

        public Gra(Gwiazdy gwiazdy, StatekGracza statekGracza, Rectangle obszarGry, Random losuj)
        {
            this.gwiazdy = gwiazdy;
            this.statekGracza = statekGracza;
            this.granice = obszarGry;
            this.losuj = losuj;
            InicjalizacjaNajezdzcow();
        }

        public void RysujGre(Graphics g)
        {
            
            g.DrawRectangle(new Pen(Brushes.Yellow, 1), granice);

            gwiazdy.RysujGwiazdy(g);

            for (int i = 0; i < Najezdzcy.Count; i++)
            {
                Najezdzcy[i].RysujStatek(g);
                Najezdzcy[i].Przesun(kierunekNajezdzcow);
            }

            statekGracza.RysujStatek(g);
            
            for (int i = 0; i < pociskiGracza.Count; i++)
            {
                pociskiGracza[i].RysujPocisk(g);
                pociskiGracza[i].PrzesunPocisk(Direction.Gora);
            }

            g.DrawString("Pilot: " + statekGracza.GraczName,new Font("Arial",10,FontStyle.Regular),Brushes.Yellow,0,640);
        }

        private void InicjalizacjaNajezdzcow()
        {
            int j = 1;
            for (int i = 0; i < 10; i++)
            {
                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.Niszczyciel, new Point(j, 0), 200,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.Niszczyciel, 51, 51)));

                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.MysliwiecStealth, new Point(j, 60), 150,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.MysliwiecStealth, 51, 51)));

                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.Mysliwiec, new Point(j, 120), 120,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.Mysliwiec, 51, 51)));

                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.Dron,new Point(j, 180), 50,
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
                statekGracza.PrzesunStatek(kierunek);
            }
        }

        public void WystrzelPociskGracza(Point Lokalizacja)
        {
            if (pociskiGracza.Count < 2)
            {
                pociskiGracza.Add(new Strzal(Lokalizacja, Direction.Gora, granice, Brushes.DeepSkyBlue));
            }
            
        }


        public void WystzelPociskNajezdzcy(Point Lokalizacja)
        {

            /*var najezdzcyOgnia = from _najezdzcy in Najezdzcy
                                 group _najezdzcy by _najezdzcy.Lokalizacja.X
                                 into _najezdzcyGroup
                                 orderby _najezdzcyGroup.Key descending
                                 select _najezdzcyGroup;
            
            foreach (var najezdzca in najezdzcyOgnia)
            {
                pociskiNajezdzcow.Add(new Strzal(Lokalizacja, Direction.Gora, granice, Brushes.Red));
            }*/
        }

        
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
            //Koniec gry
            /*var najezdzcyDol = from _najezdzcy in Najezdzcy
                               where _najezdzcy.wielkoscNajezdzcy.Bottom >= granice.Bottom - 51
                               group _najezdzcy by _najezdzcy.TypNajezdzcy
                                   into _najezdzcyGroup
                                   select _najezdzcyGroup;*/

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
            //koniec gry
            /*foreach (var najezdzca in najezdzcyDol)
            {
                System.Windows.Forms.MessageBox.Show("Test");
            }*/
        }
        

        public void Go()
        {
            PrzesunNajezdzcow();

            for (int i = 0; i < pociskiGracza.Count; i++)
            {
                if (!pociskiGracza[i].PrzesunPocisk(Direction.Gora))
                {
                    pociskiGracza.RemoveAt(i);
                }
            }
        }
    }
}
