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

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            using (Graphics g = CreateGraphics())
            {
                animacjaTla.AnimujTlo(g);
            }
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            animacjaTla.AnimujTlo(g);
        }
    }
}
