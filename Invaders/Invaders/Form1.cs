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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public System.Media.SoundPlayer odtDzwiek;
        public bool wycisz = true;
        private bool dwochGraczy = false;
        public string GraczName1;
        public string GraczName2;
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
        /// Powiazanie animacji z formularzem. Btak zaklocen podczas przesuwania formy.
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
             wyciszDzwiek();
        }

        /// <summary>
        /// Metoda wysterowywuje przycisk wyciszBtn oraz zatrzymuje i odtwarza dzwiek w zaleznosci od pola wycisz
        /// </summary>
        private void wyciszDzwiek()
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
            wyciszDzwiek();
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
            this.Hide();

            if (dwochGraczy)
            {
                GraczName1 = player1Name.Text;
                GraczName2 = player2Name.Text;
                BattleField2 battlefield2 = new BattleField2(this);
                battlefield2.ShowDialog();
            }
            else
            {
                GraczName1 = player1Name.Text;
                BattleField1 battlefield1 = new BattleField1(this);
                battlefield1.ShowDialog();
            }
        }

        private void banerAnimationTimer_Tick(object sender, EventArgs e)
        {
            invadersBanner1.banerAnimation(banerAnimationTimer);
        }
    }
}
