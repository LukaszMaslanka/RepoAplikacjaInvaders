using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Invaders
{
    public partial class BattleField1 : Form
    {
        Form1 form1;

        GraDla1 gra;
        Gwiazdy gwiazdy;
        Rectangle obszarRysowania;
        Random losuj;

        Point lokalizacjaStatku = new Point(344, 590);
        StatekGracza statekGracza;
        
        List<Keys> keysPressed = new List<Keys>();

        private bool koniecGry = false;
        private bool graczWygral = false;

        public BattleField1(Form1 form1)
        {
            InitializeComponent();

            obszarRysowania = new Rectangle(0, 0, this.Width - 5, this.Height - 60);
            losuj = new Random();

            gwiazdy = new Gwiazdy(obszarRysowania, losuj);

            statekGracza = new StatekGracza(lokalizacjaStatku,Gracze.Player1,form1.Gracz1Nazwa);

            gra = new GraDla1(gwiazdy, obszarRysowania, losuj, statekGracza);

            gra.GameOver += new EventHandler(gra_GameOver);
            gra.GameOverGracz1 += new EventHandler(gra_GameOverGracz1);


            this.form1 = form1;
            if (form1.wycisz)
            {
                form1.odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.GameSound);
                form1.odtDzwiek.PlayLooping();
            }           
        }

        void gra_GameOverGracz1(object sender, EventArgs e)
        {
            gameTimer.Stop();
            form1.odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.SoundGameOver);
            form1.odtDzwiek.Play();
            koniecGry = true;
            //Gdy gracz przegra dane zostają zapisane do pliku
            ObslugaPlikow.OdczytajDane();
            ObslugaPlikow.ZapiszDane(statekGracza, gra.Punkty);

        }

        void gra_GameOver(object sender, EventArgs e)
        {
            if (gra.GraczWygral)
            {
                graczWygral = true;
                koniecGry = false;
            }
            gra_GameOverGracz1(this,e);
            
        }
        
        /// <summary>
        /// Zdarzenie formClosing formularza BattleField1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                form1.odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.GameTheme);
                form1.Show();
                if (form1.wycisz)
                    form1.odtDzwiek.PlayLooping();
                else
                    form1.odtDzwiek.Stop();
            }
        }

        private void BattleField1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            gra.RysujGre(g);

            if (koniecGry == true && graczWygral == false)
            {
                g.DrawString(statekGracza.NazwaStatku + " Przegrał!", new Font("Arial", 10, FontStyle.Regular), Brushes.Red, 400, 640);
                animationTimer.Stop();
                gameOverBanner1.Visible = true;
            }

            if (graczWygral)
            {
                g.DrawString(statekGracza.NazwaStatku + " Wygrał!", new Font("Arial", 10, FontStyle.Regular), Brushes.Green, 400, 640);
                animationTimer.Stop();
                gameOverBanner1.Visible = true;
            }
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            gra.MrugajGwiazdami();
            this.Refresh();
        }

        /// <summary>
        /// Zegar obsługi gry. Wywołuje metodę Go z klasy Game oraz odpowiadę za obsługę przycisków dla gracza.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            gra.nastepnaFala(5);
            gra.Go();

            foreach (Keys key in keysPressed)
            {
                if (key == Keys.Right)
                {
                    gra.PrzesunGracza(Kierunek.Prawo);
                    lokalizacjaStatku = statekGracza.Lokalizacja;
                    return;
                }
                if (key == Keys.Left)
                {
                    gra.PrzesunGracza(Kierunek.Lewo);
                    lokalizacjaStatku = statekGracza.Lokalizacja;
                    return;
                }
            }
        }
        
        private void BattleField1_KeyDown(object sender, KeyEventArgs e)
        {
            if (keysPressed.Contains(e.KeyCode))
            {
                keysPressed.Remove(e.KeyCode);
            }
            keysPressed.Add(e.KeyCode);

            if (e.KeyCode == Keys.Q)
            {
                this.Close();
            }
        }

        private void BattleField1_KeyUp(object sender, KeyEventArgs e)
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
        }
    }
}
