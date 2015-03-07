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
        public BattleField1()
        {
            InitializeComponent();
            rect = new Rectangle(0, 0, this.Width, this.Height);
            rand = new Random();
            gwiazdy = new Gwiazdy(rect, rand);
        }

        public static void ThreadProc()
        {
            Application.Run(new Form1());
        }

        private void BattleField1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
            t.Start();
        }

        Gwiazdy gwiazdy;
        Random rand;
        Rectangle rect;
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            using (Graphics g = CreateGraphics())
            {
                gwiazdy.RysujGwiazdy(g);
                gwiazdy.Migotanie(rand);
                this.Invalidate();
            }
        }

        private void BattleField1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            gwiazdy.RysujGwiazdy(g);
        }

        //Point ruszaj = new Point();
        private void timer2_Tick(object sender, EventArgs e)
        {
            player1Ship1.StatekAnimation();
            
            /*ruszaj = player1Ship1.Location;
            ruszaj.Y +=1;
            player1Ship1.Location = ruszaj;*/
        }
    }
}
