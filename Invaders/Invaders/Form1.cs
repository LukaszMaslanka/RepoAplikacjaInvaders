using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Invaders
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public System.Media.SoundPlayer odtDzwiek;

        public bool wycisz = false;
        bool statyWidocznosc = false;

        private bool dwochGraczy = false;

        public string Gracz1Nazwa;
        public string Gracz2Nazwa;

        Rysuj animacjaTla = new Rysuj();

        /// <summary>
        /// Zegar: Interval 150, Enabled True, wywolanie metody AnimujTlo z klasy Rysuj
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            using (Graphics g = CreateGraphics())
            {
                animacjaTla.AnimujTlo(g);
            }
        }

        /// <summary>
        /// Powiazanie animacji z formularzem. Brak zaklocen podczas przesuwania formy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            animacjaTla.AnimujTlo(g);
        }

        /// <summary>
        /// Utworzenie obiektu SoundPlayer oraz wywolanie metody wyciszDzwiek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.GameTheme);
            WyciszDzwiek();
        }

        /// <summary>
        /// Metoda wysterowywuje przycisk wyciszBtn oraz zatrzymuje i odtwarza dzwiek w zaleznosci od pola wycisz
        /// </summary>
        private void WyciszDzwiek()
        {
            if (wycisz)
            {
                wyciszBtn.BackgroundImage = Properties.Resources.playSound;
                odtDzwiek.Stop();
                wycisz = false;
            }
            else
            {
                wyciszBtn.BackgroundImage = Properties.Resources.muteSound;
                odtDzwiek.PlayLooping();
                wycisz = true;
            }
        }

        private void wyciszBtn_Click(object sender, EventArgs e)
        {
            WyciszDzwiek();
        }

        private void jedenGraczBtn_Click(object sender, EventArgs e)
        {
            dwochGraczy = false;
            panelPlayer1.Visible = true;
            panelPlayer2.Visible = false;
            grajBtn.Visible = true;
        }

        private void dwochGraczyBtn_Click(object sender, EventArgs e)
        {
            dwochGraczy = true;
            panelPlayer1.Visible = true;
            panelPlayer2.Visible = true;
            grajBtn.Visible = true;
        }

        private void wyjscie_Click(object sender, EventArgs e)
        {
            Close();
        }
      
        private void grajBtn_Click(object sender, EventArgs e)
        {
            ObslugaPlikow.KopiujWav();
            statyWidocznosc = false;

            this.Hide();

            if (dwochGraczy)
            {
                Gracz1Nazwa = player1Name.Text;
                Gracz2Nazwa = player2Name.Text;
                BattleField2 battlefield2 = new BattleField2(this);
                battlefield2.ShowDialog();
            }
            else
            {
                Gracz1Nazwa = player1Name.Text;
                BattleField1 battlefield1 = new BattleField1(this);
                battlefield1.ShowDialog();
            }
        }
        /// <summary>
        /// Wywołanie metody banerAnimation z obiketu InvadersBaner. Parametrem metody jest obiekt Timer. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void banerAnimationTimer_Tick(object sender, EventArgs e)
        {
            invadersBanner1.banerAnimation(banerAnimationTimer);
        }

        /// <summary>
        /// Odczyt danych z pliku wyniki.txt i przedstawienie danych w textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statystyki_Click(object sender, EventArgs e)
        {
            if (!statyWidocznosc)
            {
                statyWidocznosc = true;
                ObslugaPlikow.OdczytajDane();
                textBox1.Text = ObslugaPlikow.wyniki;
            }
            else
            {
                ObslugaPlikow.wyniki = " ";
                textBox1.Clear();
                statyWidocznosc = false;
            }
            textBox1.Visible = statyWidocznosc;
        }
    }
}
