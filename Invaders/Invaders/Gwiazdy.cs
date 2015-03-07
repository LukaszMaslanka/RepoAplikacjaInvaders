using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invaders
{
    class Gwiazdy
    {
        private struct Gwiazda
        {
            public Point punkt;
            public Pen pen;

            public Gwiazda(Point punkt, Pen pen)
            {
                this.punkt = punkt;
                this.pen = pen;
            }
        }

        public Gwiazdy(Rectangle obszar, Random losuj)
        {
            InicjalizujGwiazdy(obszar, losuj);
        }

        List<Gwiazda> gwiazdki = new List<Gwiazda>();
        Point punkty = new Point();
        Pen olowek = new Pen(Brushes.White,2);

        private void InicjalizujGwiazdy(Rectangle obszar, Random losuj)
        {         
            for (int i = 0; i < 300; i++)
            {
                punkty.X = losuj.Next(obszar.Width);
                punkty.Y = losuj.Next(obszar.Height);
                gwiazdki.Add(new Gwiazda(punkty,olowek));
            }
        }

        public void RysujGwiazdy(Graphics g)
        {
            foreach(Gwiazda gwiazda in gwiazdki)
            {
                g.DrawLine(gwiazda.pen, gwiazda.punkt.X, gwiazda.punkt.Y, gwiazda.punkt.X+2, gwiazda.punkt.Y+2);
            }
        }

        public void Migotanie(Random losuj)
        {
            for (int i = gwiazdki.Count -1; i > 295; i--)
            {
                gwiazdki.RemoveAt(i);
            }

            for (int i = 0; i < 4; i++)
            {
                punkty.X = losuj.Next(790);
                punkty.Y = losuj.Next(690);
                gwiazdki.Add(new Gwiazda(punkty, olowek));
            }
        }
    }
}
