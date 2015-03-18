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
        Rectangle obszarRysowania;
        Random losuj;
        Gwiazdy gwiazdy;
        StatekGracza statekGracza;
        Point lokalizacjaStatku = new Point(344, 590);
        List<Keys> keysPressed = new List<Keys>();
        private bool koniecGry = false;
        private bool graczWygral = false;
        string wyniki = "";

        public BattleField1(Form1 form1)
        {
            this.form1 = form1;
            //wykorzystanie ukrytej pierwszej formy do odtworzenia dźwięku
            if (form1.wycisz)
            {
                form1.odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.GameSound);
                form1.odtDzwiek.PlayLooping();
            }
            
            InitializeComponent();

            obszarRysowania = new Rectangle(0, 0, this.Width-5, this.Height-60);

            losuj = new Random();

            gwiazdy = new Gwiazdy(obszarRysowania, losuj);
            statekGracza = new StatekGracza(lokalizacjaStatku,Gracze.Player1,form1.GraczName1);
            
            gra = new GraDla1(gwiazdy,statekGracza,obszarRysowania,losuj);

            gra.GameOver += new EventHandler(gra_GameOVer);
            gra.PlayerWins += new EventHandler(gra_PlayerWins);
        }
        /// <summary>
        /// Odczyt danych z pliku a następnie zapis danych w zmiennej
        /// typu string wyniki
        /// </summary>
        void OdczytajDane()
        {
            if (File.Exists("wyniki.txt"))
            {
                StreamReader odczytDanych = new StreamReader("wyniki.txt");
                //Odczyt danych z pliku do zmiennej w celu wpisania ich przy zapisie nowego pliku
                while (!odczytDanych.EndOfStream)
                {
                    wyniki = odczytDanych.ReadToEnd();
                }
                odczytDanych.Close();
            }
        }

        /// <summary>
        /// Zapis do pliku wyniki.txt. Plik zapisuje się w lokalizacja programu.
        /// </summary>
        void ZapiszDane()
        {
            //Zapis danych do pliku
            StreamWriter zapisDanych = new StreamWriter("wyniki.txt");
            int roznica = 20;
            int dlugoscLancucha = 0;
            /***Wyliczenie długości odstępu po Nazwie stattku***/
            if (statekGracza.NazwaStatku.Length < 20)
            {
                dlugoscLancucha = roznica - statekGracza.NazwaStatku.Length;
            }

            zapisDanych.Write("Gracz: " + statekGracza.NazwaStatku);
            for (int i = 0; i < dlugoscLancucha; i++)
            {
                zapisDanych.Write(" ");
            }
            /************************************************/
            zapisDanych.Write(" | punkty: " + gra.punkty);
            zapisDanych.WriteLine("\n");
            /***42 to suma wszystkich znaków w linie gdy wszystkie wartości są maksymalne***/
            for (int i = 0; i < 42; i++)
            {
                zapisDanych.Write("-");
            }
            zapisDanych.WriteLine("\n");
            /***********************************************************************************/
            //zapis danych z zmiennej wyniki czyli z poprzedniej wersji pliku
            zapisDanych.Write(wyniki);
            zapisDanych.Close();
        }
        /// <summary>
        /// Procedura obsługi zdarzenia PlayerWins
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gra_PlayerWins(object sender, EventArgs e)
        {
            gameTimer.Stop();
            form1.odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.SoundGameOver);
            form1.odtDzwiek.Play();
            graczWygral = true;
            //Gdy gracz wygra dane zostają zapisane do pliku
            OdczytajDane();
            ZapiszDane();
        }
        /// <summary>
        /// Procedura obsługi zdarzenia GameOver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gra_GameOVer(object sender, EventArgs e)
        {
            gameTimer.Stop();
            form1.odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.SoundGameOver);
            form1.odtDzwiek.Play();
            koniecGry = true;
            //Gdy gracz przegra dane zostają zapisane do pliku
            OdczytajDane();
            ZapiszDane();
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

        /// <summary>
        /// Zegar obsługi gry. Wywołuje metodę Go z klasy Game oraz odpowiadę za obsługę przycisków dla gracza.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
