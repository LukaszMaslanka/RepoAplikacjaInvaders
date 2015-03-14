using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Invaders
{
    public partial class BattleField1 : Form
    {
        Form1 form1;
        Gra gra;
        Rectangle obszarRysowania;
        Random losuj;
        Gwiazdy gwiazdy;
        StatekGracza statekGracza;
        Point lokalizacjaStatku = new Point(344, 590);
        List<Keys> keysPressed = new List<Keys>();
        private bool koniecGry = false;
        private bool graczWygral = false;
        
        public BattleField1(Form1 form1)
        {
            this.form1 = form1;
            form1.odtDzwiek.Stop();
            InitializeComponent();

            obszarRysowania = new Rectangle(0, 0, this.Width-5, this.Height-60);

            losuj = new Random();

            gwiazdy = new Gwiazdy(obszarRysowania, losuj);
            statekGracza = new StatekGracza(lokalizacjaStatku,Gracze.Player1,obszarRysowania,form1.GraczName1);
            
            gra = new Gra(gwiazdy,statekGracza,obszarRysowania,losuj);
            gra.GameOVer += new EventHandler(gra_GameOVer);
            gra.PlayerWins += new EventHandler(gra_PlayerWins);
        }

        void gra_PlayerWins(object sender, EventArgs e)
        {
            gameTimer.Stop();
            graczWygral = true;
        }

        void gra_GameOVer(object sender, EventArgs e)
        {
            gameTimer.Stop();
            koniecGry = true;
        }
 
        private void BattleField1_FormClosing(object sender, FormClosingEventArgs e)
        {
            animationTimer.Stop();
            gameTimer.Stop();
            if (e.Cancel = MessageBox.Show("Czy przerwać grę?", "Koniec gry", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                if (koniecGry == false && graczWygral == false)
                {
                    gameTimer.Start();
                    animationTimer.Start();
                }
            }
            else
            {
                form1.Show();
                if (form1.wycisz)
                    form1.odtDzwiek.PlayLooping();
                else
                    form1.odtDzwiek.Stop();
            }
            
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            gra.MrugajGwiazdami();
            this.Refresh();
        }

        private void BattleField1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            gra.RysujGre(g);

            if (koniecGry)
            {
                g.DrawString("Koniec Gry", new Font("Arial", 25, FontStyle.Regular), Brushes.Yellow,
                    new Point(this.Width / 2, this.Height / 2));
                animationTimer.Stop();
            }
                
            if (graczWygral)
            {
                g.DrawString("Gracz " + statekGracza.GraczName + " uzyskał " + gra.punkty, new Font("Arial", 25, FontStyle.Regular), Brushes.Green,
                    new Point(this.Width / 2, this.Height / 2));
                animationTimer.Stop();
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            gra.Go();

            foreach (Keys key in keysPressed)
            {
                if (key == Keys.Right)
                {
                    gra.PrzesunGracza(Direction.Prawo);
                    lokalizacjaStatku = statekGracza.Lokalizacja;
                    return;
                }
                if (key == Keys.Left)
                {
                    gra.PrzesunGracza(Direction.Lewo);
                    lokalizacjaStatku = statekGracza.Lokalizacja;
                    return;
                }
            }
        }
        
        private void BattleField1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gra.WystrzelPociskGracza(lokalizacjaStatku);
                return;
            }

            if (keysPressed.Contains(e.KeyCode))
            {
                keysPressed.Remove(e.KeyCode);
            }
            keysPressed.Add(e.KeyCode);

            //Przy zakonczeniu gry nalezy zapisac wynik do bazy. trzeba bedzie utworzyc metoda zakonczgre!!
            if (e.KeyCode == Keys.Q)
            {
                this.Close();
            }
        }

        private void BattleField1_KeyUp(object sender, KeyEventArgs e)
        {
            if (keysPressed.Contains(e.KeyCode))
            {
                keysPressed.Remove(e.KeyCode);
            }
        }
    }
}
