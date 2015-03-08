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
        Najezdzca Dron = new Najezdzca(TypNajezdzcy.Dron,new Point(10,10),100,Rysuj.KonwertujNaBitmap(Properties.Resources.Dron, 51,51));
        Rectangle obszarRysowania;
        List<Keys> keysPressed = new List<Keys>();

        public BattleField1()
        {
            InitializeComponent();
            obszarRysowania = new Rectangle(0, 0, this.Width, this.Height);
            losuj = new Random();
            gwiazdy = new Gwiazdy(obszarRysowania, losuj);
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
        Random losuj;

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            using (Graphics g = CreateGraphics())
            {
                gwiazdy.RysujGwiazdy(g);
                gwiazdy.Migotanie(losuj);
                this.Refresh();
            }
        }

        private void BattleField1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            gwiazdy.RysujGwiazdy(g);
            Dron.RysujStatek(g);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (player1Ship1.Zywy)
            {
                player1Ship1.StatekAnimation();
            }
            else
            {
                player1Ship1.StatekDestroy();
            }


            Point p = new Point();
            foreach (Keys key in keysPressed)
            {
                if (key == Keys.Right)
                {
                    p = player1Ship1.Location;
                    p.X += 10;
                    player1Ship1.Location = p;
                    return;
                }
                if (key == Keys.Left)
                {
                    p = player1Ship1.Location;
                    p.X -= 10;
                    player1Ship1.Location = p;
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

            //Przy zakonczeniu gry nalezy zapisac wynik do bazy. trzeba bedzie utworzyc metoda zakonczgre!!
            if (e.KeyCode == Keys.Q)
            {
                gameTimer.Stop();
                animationTimer.Stop();
                DialogResult result = MessageBox.Show("Czy przerwać grę?" , "Koniec gry" , MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    this.Close();
                }
                else
                {
                    gameTimer.Start();
                    animationTimer.Start();
                }
            }
        }

        private void BattleField1_KeyUp(object sender, KeyEventArgs e)
        {
            if (keysPressed.Contains(e.KeyCode))
            {
                keysPressed.Remove(e.KeyCode);
            }
        }
    }
}
