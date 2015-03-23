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

        //Flagi do obsługi zdarzeń
        bool koniecGry = false;
        bool remis = false;
        bool gracz1Wygral = false;
        bool gracz2Wygral = false;

        //Flagi do obsługi klawiszy/sterowanie statkami
        bool keyLeft = false;
        bool keyRight = false;
        bool keyD = false;
        bool keyA = false;

        EventArgs e;

        /// <summary>
        /// Konstruktor formy. Parametrem konstruktora jest Form1 aby móc uzyskać refernecje do form1 i móc zmieniać publiczne obiekty formy.
        /// </summary>
        /// <param name="form1"></param>
        public BattleField2(Form1 form1)
        {
            InitializeComponent();

            obszarRysowania = new Rectangle(0, 0, this.Width-7, this.Height-60);
            losuj = new Random();
            gwiazdy = new Gwiazdy(obszarRysowania,losuj);

            statekGracza1 = new StatekGracza(lokalizacjaStatek1,Gracze.Player1,form1.Gracz1Nazwa);
            statekGracza2 = new StatekGracza(lokalizacjaStatek2, Gracze.Player2, form1.Gracz2Nazwa);

            gra = new GraDla2(gwiazdy,obszarRysowania,losuj,statekGracza1,statekGracza2);

            gra.GameOver += new EventHandler(gra_GameOver);
            gra.Remis += new EventHandler(gra_Remis);
            gra.GameOverGracz1 += new EventHandler(gra_GameOverGracz1);
            gra.GameOverGracz2 += new EventHandler(gra_GameOverGracz2);

            this.form1 = form1;
            if (form1.wycisz)
            {
                form1.odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.GameSound);
                form1.odtDzwiek.PlayLooping();
            }
        }

        /// <summary>
        /// Zdarzenie GameOver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gra_GameOver(object sender, EventArgs e)
        {
            if (gra.GraczWygral)
            {
                if (gra.PunktyGracz1 > gra.PunktyGracz2)
                {
                    gra_GameOverGracz2(this,e);
                }
                else if (gra.PunktyGracz1 < gra.PunktyGracz2)
                {
                    gra_GameOverGracz1(this, e);
                }
                else if (gra.PunktyGracz1 == gra.PunktyGracz2)
                {
                    gra_Remis(this ,e);
                }
            }
         }
        /// <summary>
        /// Procedura obsługi zdarzenia Remis. Ustawienie flag koniecGry, remis, gracz1Wygral, gracz2Wygral
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gra_Remis(object sender, EventArgs e)
        {
            koniecGry = true;
            remis = true;
            gracz1Wygral = false;
            gracz2Wygral = false;

            gameTimer.Stop();
            form1.odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.SoundGameOver);
            form1.odtDzwiek.Play();
            //Gdy gracz przegra dane zostają zapisane do pliku
            ObslugaPlikow.OdczytajDane();
            ObslugaPlikow.ZapiszDane(statekGracza1, statekGracza2, gra.PunktyGracz1, gra.PunktyGracz2);
        }
        /// <summary>
        /// Procedura obsługi zdarzenia GameOVerGracz2. Ustawienie flag koniecGry, gracz1Wygral, gracz2Wygral
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            ObslugaPlikow.ZapiszDane(statekGracza1, statekGracza2, gra.PunktyGracz1, gra.PunktyGracz2);
        }
        /// <summary>
        /// Procedura obsługi zdarzenia GameOVerGracz1. Ustawienie flag koniecGry, gracz1Wygral, gracz2Wygral
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
            ObslugaPlikow.ZapiszDane(statekGracza1, statekGracza2, gra.PunktyGracz1, gra.PunktyGracz2);
        }
        /// <summary>
        /// Obsługa zdarzenia zamknięcia formy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                form1.textBox1.Visible = false;
                form1.Show();
                if (form1.wycisz)
                    form1.odtDzwiek.PlayLooping();
                else
                    form1.odtDzwiek.Stop();
            }
        }
        /// <summary>
        /// Zdarzenie Paint formy. 
        /// W zależności od flag ustawionych przez evemtu po zakończeniu gry na ekranie są wyświetlane odpowiednie informacje.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BattleField2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            gra.RysujGre(g);

            if (koniecGry && remis == true)
            {
                g.DrawString(" Remis!", new Font("Arial", 10, FontStyle.Regular), Brushes.White, 1060, 630);
                g.DrawString(" Remis!", new Font("Arial", 10, FontStyle.Regular), Brushes.White, 465, 630);
                animationTimer.Stop();
                gameOverBanner1.Visible = true;
            }

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
        /// <summary>
        /// Animacja tła.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            gra.MrugajGwiazdami();
            this.Refresh();
        }
        /// <summary>
        /// Zegar animacji. 
        /// Wywołuje odpowiednie metody.
        /// Przesuwa statek gracza po formatce.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            gra.NastepnaFala(5);
            gra.Go();

            if (keyLeft && keyD)
            {
                statekGracza1.PrzesunStatek(Kierunek.Lewo,obszarRysowania);
                lokalizacjaStatek1 = statekGracza1.Lokalizacja;

                statekGracza2.PrzesunStatek(Kierunek.Prawo,obszarRysowania);
                lokalizacjaStatek2 = statekGracza2.Lokalizacja;
            }

            if (keyRight && keyA)
            {
                statekGracza1.PrzesunStatek(Kierunek.Prawo, obszarRysowania);
                lokalizacjaStatek1 = statekGracza1.Lokalizacja;

                statekGracza2.PrzesunStatek(Kierunek.Lewo, obszarRysowania);
                lokalizacjaStatek2 = statekGracza2.Lokalizacja;
            }

            if (keyRight && keyD)
            {
                statekGracza1.PrzesunStatek(Kierunek.Prawo, obszarRysowania);
                lokalizacjaStatek1 = statekGracza1.Lokalizacja;

                statekGracza2.PrzesunStatek(Kierunek.Prawo, obszarRysowania);
                lokalizacjaStatek2 = statekGracza2.Lokalizacja;
            }

            if (keyLeft && keyA)
            {
                statekGracza1.PrzesunStatek(Kierunek.Lewo,obszarRysowania);
                lokalizacjaStatek1 = statekGracza1.Lokalizacja;

                statekGracza2.PrzesunStatek(Kierunek.Lewo, obszarRysowania);
                lokalizacjaStatek2 = statekGracza2.Lokalizacja;
            }

            if (keyA == false && keyD == false && keyRight == true)
            {
                statekGracza1.PrzesunStatek(Kierunek.Prawo, obszarRysowania);
                lokalizacjaStatek1 = statekGracza1.Lokalizacja;
            }

            if (keyA == false && keyD == false && keyLeft == true)
            {
                statekGracza1.PrzesunStatek(Kierunek.Lewo, obszarRysowania);
                lokalizacjaStatek1 = statekGracza1.Lokalizacja;
            }

            if (keyRight == false && keyLeft == false && keyD == true)
            {
                statekGracza2.PrzesunStatek(Kierunek.Prawo, obszarRysowania);
                lokalizacjaStatek2 = statekGracza2.Lokalizacja;
            }

            if (keyRight == false && keyLeft == false && keyA == true)
            {
                statekGracza2.PrzesunStatek(Kierunek.Lewo, obszarRysowania);
                lokalizacjaStatek2 = statekGracza2.Lokalizacja;
            }
        }
        /// <summary>
        /// Zdarzenie KeyDown. Po wciśnięciu odpowiedniego klawisza ustawia odpowiednie flagi na TRUE.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Zdarzenie KeyUp. Po zwolnieniu odpowiedniego klawisza ustawia odpowiednie flagi na FALSE.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
