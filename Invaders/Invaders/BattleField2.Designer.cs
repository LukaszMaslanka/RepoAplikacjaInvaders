﻿namespace Invaders
{
    partial class BattleField2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleField2));
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.gameOverBanner1 = new Invaders.GameOverBanner();
            this.SuspendLayout();
            // 
            // animationTimer
            // 
            this.animationTimer.Enabled = true;
            this.animationTimer.Interval = 55;
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // gameOverBanner1
            // 
            this.gameOverBanner1.BackColor = System.Drawing.Color.Transparent;
            this.gameOverBanner1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gameOverBanner1.BackgroundImage")));
            this.gameOverBanner1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gameOverBanner1.Location = new System.Drawing.Point(325, 32);
            this.gameOverBanner1.Name = "gameOverBanner1";
            this.gameOverBanner1.Size = new System.Drawing.Size(535, 65);
            this.gameOverBanner1.TabIndex = 0;
            this.gameOverBanner1.Visible = false;
            // 
            // BattleField2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1184, 651);
            this.Controls.Add(this.gameOverBanner1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BattleField2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invaders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BattleField2_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BattleField2_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BattleField2_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BattleField2_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.Timer gameTimer;
        private GameOverBanner gameOverBanner1;

    }
}