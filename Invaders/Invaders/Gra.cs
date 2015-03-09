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

        private Rectangle granice;
        private Random losuj;

        private Direction kierunekNajezdzcy;
        private List<Najezdzca> Najezdzcy = new List<Najezdzca>();

        private StatekGracza statekGracza;
        private List<Strzal> pociskiGracza = new List<Strzal>();
        private List<Strzal> pociskiNajezdzcow;

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
            int j = 0;
            for (int i = 0; i < 14; i++)
            {
                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.Niszczyciel, new Point(j, 0), 200,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.Niszczyciel, 51, 51), granice));

                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.MysliwiecStealth, new Point(j, 75), 150,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.MysliwiecStealth, 51, 51), granice));

                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.Mysliwiec, new Point(j, 150), 120,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.Mysliwiec, 51, 51), granice));

                Najezdzcy.Add(new Najezdzca(TypNajezdzcy.Dron, new Point(j, 225), 50,
                    Rysuj.KonwertujNaBitmap(Properties.Resources.Dron, 51, 51), granice));
                j = j + 51; 
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

        public void WystrzelPocisk(Point Lokalizacja)
        {
            if (pociskiGracza.Count < 2)
            {
                pociskiGracza.Add(new Strzal(Lokalizacja, Direction.Gora, granice));
            }
            
        }

        public void Go()
        {
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
