namespace Invaders
{
    partial class BattleField1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleField1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.player1Ship1 = new Invaders.Player1Ship();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 350;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // player1Ship1
            // 
            this.player1Ship1.AutoSize = true;
            this.player1Ship1.BackColor = System.Drawing.Color.Transparent;
            this.player1Ship1.BackgroundImage = global::Invaders.Properties.Resources.Player1;
            this.player1Ship1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.player1Ship1.Location = new System.Drawing.Point(367, 598);
            this.player1Ship1.Name = "player1Ship1";
            this.player1Ship1.Size = new System.Drawing.Size(51, 51);
            this.player1Ship1.TabIndex = 0;
            // 
            // BattleField1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.player1Ship1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BattleField1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invaders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BattleField1_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BattleField1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Player1Ship player1Ship1;
        private System.Windows.Forms.Timer timer2;
    }
}