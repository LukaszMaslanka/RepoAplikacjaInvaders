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
    public partial class BattleField2 : Form
    {
        Form1 form1;
        public BattleField2(Form1 form1)
        {
            this.form1 = form1;
            form1.odtDzwiek.Stop();
            InitializeComponent();
            label1.Text = form1.GraczName1 + " " + form1.GraczName2;
        }

        private void BattleField2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //animationTimer.Stop();
           // gameTimer.Stop();
            if (e.Cancel = MessageBox.Show("Czy przerwać grę?", "Koniec gry", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                /*if (koniecGry == false && graczWygral == false)
                {
                    gameTimer.Start();
                    animationTimer.Start();
                }*/
            }
            else
            {
                form1.Show();
                if (form1.wycisz)
                    form1.odtDzwiek.PlayLooping();
                else
                    form1.odtDzwiek.Stop();
            }
        }
    }
}
