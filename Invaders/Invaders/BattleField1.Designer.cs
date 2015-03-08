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
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.player1Ship1 = new Invaders.Player1Ship();
            ((System.ComponentModel.ISupportInitialize)(this.player1Ship1)).BeginInit();
            this.SuspendLayout();
            // 
            // animationTimer
            // 
            this.animationTimer.Enabled = true;
            this.animationTimer.Interval = 330;
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // player1Ship1
            // 
            this.player1Ship1.BackColor = System.Drawing.Color.Transparent;
            this.player1Ship1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("player1Ship1.BackgroundImage")));
            this.player1Ship1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.player1Ship1.Location = new System.Drawing.Point(367, 590);
            this.player1Ship1.Name = "player1Ship1";
            this.player1Ship1.Size = new System.Drawing.Size(51, 51);
            this.player1Ship1.TabIndex = 0;
            this.player1Ship1.TabStop = false;
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
            this.KeyPreview = true;
            this.Name = "BattleField1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invaders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BattleField1_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BattleField1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BattleField1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BattleField1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.player1Ship1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.Timer gameTimer;
        private Player1Ship player1Ship1;
    }
}