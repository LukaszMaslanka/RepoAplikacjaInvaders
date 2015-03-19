using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Invaders
{
    public partial class BattleField2 : Form
    {
        Form1 form1;

        GraDla2 gra;
        Gwiazdy gwiazdy;
        Rectangle obszarRysowania;
        Random losuj;

        Point lokalizacjaStatek1 = new Point(600, 580);
        Point lokalizacjaStatek2 = new Point(100, 580);
        StatekGracza statekGracza1;
        StatekGracza statekGracza2;
        bool koniecGry = false;
        bool gracz1Wygral = false;
        bool gracz2Wygral = false;
        EventArgs e;

        public BattleField2(Form1 form1)
        {
            InitializeComponent();

            obszarRysowania = new Rectangle(0, 0, this.Width-7, this.Height-60);
            losuj = new Random();
            gwiazdy = new Gwiazdy(obszarRysowania,losuj);

            statekGracza1 = new StatekGracza(lokalizacjaStatek1,Gracze.Player1,form1.GraczName1);
            statekGracza2 = new StatekGracza(lokalizacjaStatek2, Gracze.Player2, form1.GraczName2);

            gra = new GraDla2(gwiazdy,obszarRysowania,losuj,statekGracza1,statekGracza2);

            gra.GameOver += new EventHandler(gra_GameOver);
            gra.GameOverGracz1 += new EventHandler(gra_GameOverGracz1);
            gra.GameOverGracz2 += new EventHandler(gra_GameOverGracz2);

            this.form1 = form1;
            if (form1.wycisz)
            {
                form1.odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.GameSound);
                form1.odtDzwiek.PlayLooping();
            }
            
        }

        void gra_GameOver(object sender, EventArgs e)
        {
            if (gra.GraczWygral)
            {
                if (gra.punktyGracz1 > gra.punktyGracz2)
                {
                    gra_GameOverGracz2(this,e);
                }
                else if (gra.punktyGracz1 < gra.punktyGracz2)
                {
                    gra_GameOverGracz1(this, e);
                }
            }
            gameTimer.Stop();
        }

        void gra_GameOverGracz2(object sender, EventArgs e)
        {
            koniecGry = true;
            gracz1Wygral = true;
            gracz2Wygral = false;

            gameTimer.Stop();
            form1.odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.SoundGameOver);
            form1.odtDzwiek.Play();
            //Gdy gracz przegra dane zostają zapisane do pliku
            ObslugaPlikow.OdczytajDane();
            ObslugaPlikow.ZapiszDane(statekGracza1, statekGracza2, gra.punktyGracz1, gra.punktyGracz2);
        }
        /// <summary>
        /// Procedura obsługi zdarzenia GameOver
        /// Wygrywa Gracz 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gra_GameOverGracz1(object sender, EventArgs e)
        {
            koniecGry = true;
            gracz1Wygral = false;
            gracz2Wygral = true;

            gameTimer.Stop();
            form1.odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.SoundGameOver);
            form1.odtDzwiek.Play();
            //Gdy gracz przegra dane zostają zapisane do pliku
            ObslugaPlikow.OdczytajDane();
            ObslugaPlikow.ZapiszDane(statekGracza1, statekGracza2, gra.punktyGracz1, gra.punktyGracz2);
        }

        private void BattleField2_FormClosing(object sender, FormClosingEventArgs e)
        {
            animationTimer.Stop();
            gameTimer.Stop();
            if (e.Cancel = MessageBox.Show("Czy przerwać grę?", "Koniec gry", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                if (koniecGry == false && gracz1Wygral == false && gracz2Wygral == false)
                {
                    gameTimer.Start();
                    animationTimer.Start();
                }
            }
            else
            {
                form1.odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.GameTheme);
                form1.Show();
                if (form1.wycisz)
                    form1.odtDzwiek.PlayLooping();
                else
                    form1.odtDzwiek.Stop();
            }
        }

        private void BattleField2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            gra.RysujGre(g);

            if (koniecGry && gracz1Wygral == true && gracz2Wygral == false)
            {
                g.DrawString(statekGracza1.NazwaStatku + " Wygrał!", new Font("Arial", 10, FontStyle.Regular), Brushes.Green, 960, 630);
                g.DrawString(statekGracza2.NazwaStatku + " Przegrał!", new Font("Arial", 10, FontStyle.Regular), Brushes.Red, 365, 630);
                animationTimer.Stop();
                gameOverBanner1.Visible = true;
            }

            if (koniecGry && gracz1Wygral == false && gracz2Wygral == true)
            {
                g.DrawString(statekGracza1.NazwaStatku + " Przegrał!", new Font("Arial", 10, FontStyle.Regular), Brushes.Red, 960, 630);
                g.DrawString(statekGracza2.NazwaStatku + " Wygrał!", new Font("Arial", 10, FontStyle.Regular), Brushes.Green, 365, 630);
                animationTimer.Stop();
                gameOverBanner1.Visible = true;
            }

            if (koniecGry && gracz1Wygral == false && gracz2Wygral == false)
            {
                g.DrawString(statekGracza1.NazwaStatku + " Przegrał!", new Font("Arial", 10, FontStyle.Regular), Brushes.Red, 960, 630);
                g.DrawString(statekGracza2.NazwaStatku + " Przegrał!", new Font("Arial", 10, FontStyle.Regular), Brushes.Red, 365, 630);
                animationTimer.Stop();
                gameOverBanner1.Visible = true;
            }
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            gra.MrugajGwiazdami();
            this.Refresh();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            gra.nastepnaFala(5);
            gra.Go();

            if (keyLeft && keyD)
            {
                statekGracza1.PrzesunStatek(Direction.Lewo,obszarRysowania);
                lokalizacjaStatek1 = statekGracza1.Lokalizacja;

                statekGracza2.PrzesunStatek(Direction.Prawo,obszarRysowania);
                lokalizacjaStatek2 = statekGracza2.Lokalizacja;
            }

            if (keyRight && keyA)
            {
                statekGracza1.PrzesunStatek(Direction.Prawo, obszarRysowania);
                lokalizacjaStatek1 = statekGracza1.Lokalizacja;

                statekGracza2.PrzesunStatek(Direction.Lewo, obszarRysowania);
                lokalizacjaStatek2 = statekGracza2.Lokalizacja;
            }

            if (keyRight && keyD)
            {
                statekGracza1.PrzesunStatek(Direction.Prawo, obszarRysowania);
                lokalizacjaStatek1 = statekGracza1.Lokalizacja;

                statekGracza2.PrzesunStatek(Direction.Prawo, obszarRysowania);
                lokalizacjaStatek2 = statekGracza2.Lokalizacja;
            }

            if (keyLeft && keyA)
            {
                statekGracza1.PrzesunStatek(Direction.Lewo,obszarRysowania);
                lokalizacjaStatek1 = statekGracza1.Lokalizacja;

                statekGracza2.PrzesunStatek(Direction.Lewo, obszarRysowania);
                lokalizacjaStatek2 = statekGracza2.Lokalizacja;
            }

            if (keyA == false && keyD == false && keyRight == true)
            {
                statekGracza1.PrzesunStatek(Direction.Prawo, obszarRysowania);
                lokalizacjaStatek1 = statekGracza1.Lokalizacja;
            }

            if (keyA == false && keyD == false && keyLeft == true)
            {
                statekGracza1.PrzesunStatek(Direction.Lewo, obszarRysowania);
                lokalizacjaStatek1 = statekGracza1.Lokalizacja;
            }

            if (keyRight == false && keyLeft == false && keyD == true)
            {
                statekGracza2.PrzesunStatek(Direction.Prawo, obszarRysowania);
                lokalizacjaStatek2 = statekGracza2.Lokalizacja;
            }

            if (keyRight == false && keyLeft == false && keyA == true)
            {
                statekGracza2.PrzesunStatek(Direction.Lewo, obszarRysowania);
                lokalizacjaStatek2 = statekGracza2.Lokalizacja;
            }
        }

        bool keyLeft = false;
        bool keyRight = false;
        bool keyD = false;
        bool keyA = false;
        private void BattleField2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                keyLeft = true;
            }
            else if (e.KeyCode == Keys.D)
            {
                keyD = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                keyRight = true;
            }
            else if (e.KeyCode == Keys.A)
            {
                keyA = true;
            }
        }

        private void BattleField2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gra.WystrzelPociskGracza1(lokalizacjaStatek1);
                return;
            }

            if (e.KeyCode == Keys.ShiftKey)
            {
                gra.WystrzelPociskGracza2(lokalizacjaStatek2);
                return;
            }

            if (e.KeyCode == Keys.Left)
            {
                keyLeft = false;
            }
            else if (e.KeyCode == Keys.D)
            {
                keyD = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                keyRight = false;
            }
            else if (e.KeyCode == Keys.A)
            {
                keyA = false;
            }

            if (e.KeyCode == Keys.Q)
            {
                this.Close();
            }
        }
    }
}
