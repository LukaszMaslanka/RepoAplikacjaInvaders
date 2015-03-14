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
        /// <summary>
        /// Konstruktor Form1 ustawienie pola wycisz na false
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            wycisz = true;
        }

        public string GraczName;
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

        System.Media.SoundPlayer odtDzwiek;
        bool wycisz;

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
            
            panelPlayer1.Visible = true;
            panelPlayer2.Visible = false;
            grajBtn.Visible = true;
        }

        private void dwochGraczyBtn_Click(object sender, EventArgs e)
        {
            panelPlayer1.Visible = true;
            panelPlayer2.Visible = true;
            grajBtn.Visible = true;
        }

        private void wyjscie_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Metoda stworzona na potrzeby utworzenia nowego wątku
        /// </summary>
        /*public static void ThreadProc()
        {
            Application.Run(new BattleField1());
        }*/
        
        /// <summary>
        /// Utworzenie nowego watku który wywołuje nową formę BattlField1. Zamknięcie Form1. Zatrzymanie dzwieku.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grajBtn_Click(object sender, EventArgs e)
        {
            /*System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
            t.Start();
            this.Dispose();
            odtDzwiek.Stop();*/
            GraczName = player1Name.Text;
            this.Hide();
            BattleField1 battlefield1 = new BattleField1(this);
            battlefield1.Owner = this;
            battlefield1.ShowDialog();
        }

        private void banerAnimationTimer_Tick(object sender, EventArgs e)
        {
            invadersBanner1.banerAnimation(banerAnimationTimer);
        }
    }
}
