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

        Rysuj animacjaTla = new Rysuj();
        //Point p = new Point();

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            using (Graphics g = CreateGraphics())
            {
                animacjaTla.AnimujTlo(g);
            }
            
            /*animationTimer.Interval = 10;
            p = pictureBox1.Location;
            p.Y -= 1;
            pictureBox1.Location = p;*/
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            animacjaTla.AnimujTlo(g);
        }

        System.Media.SoundPlayer odtDzwiek;
        bool wycisz;

        private void Form1_Load(object sender, EventArgs e)
        {
            wycisz = true;
            odtDzwiek = new System.Media.SoundPlayer(Properties.Resources.GameTheme);
            odtDzwiek.PlayLooping();
        }

        private void wyciszBtn_Click(object sender, EventArgs e)
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

        public static void ThreadProc()
        {
            BattleField1 form2;
            Application.Run(new BattleField1());
        }
        
        
        private void grajBtn_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
            t.Start();
            this.Dispose();
            odtDzwiek.Stop();
        }
    }
}
