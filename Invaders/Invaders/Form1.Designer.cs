namespace Invaders
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.wyciszBtn = new System.Windows.Forms.Button();
            this.panelPlayer1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.player1Name = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panelPlayer2 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.player2Name = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.grajBtn = new System.Windows.Forms.Button();
            this.jedenGraczBtn = new System.Windows.Forms.Button();
            this.dwochGraczyBtn = new System.Windows.Forms.Button();
            this.statystyki = new System.Windows.Forms.Button();
            this.wyjscie = new System.Windows.Forms.Button();
            this.banerAnimationTimer = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.invadersBanner1 = new Invaders.InvadersBanner();
            this.panelPlayer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panelPlayer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // animationTimer
            // 
            this.animationTimer.Enabled = true;
            this.animationTimer.Interval = 125;
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // wyciszBtn
            // 
            this.wyciszBtn.BackgroundImage = global::Invaders.Properties.Resources.muteSound;
            this.wyciszBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.wyciszBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wyciszBtn.Location = new System.Drawing.Point(12, 12);
            this.wyciszBtn.Name = "wyciszBtn";
            this.wyciszBtn.Size = new System.Drawing.Size(25, 25);
            this.wyciszBtn.TabIndex = 1;
            this.wyciszBtn.UseVisualStyleBackColor = true;
            this.wyciszBtn.Click += new System.EventHandler(this.wyciszBtn_Click);
            // 
            // panelPlayer1
            // 
            this.panelPlayer1.BackColor = System.Drawing.Color.Transparent;
            this.panelPlayer1.Controls.Add(this.pictureBox1);
            this.panelPlayer1.Controls.Add(this.player1Name);
            this.panelPlayer1.Controls.Add(this.pictureBox2);
            this.panelPlayer1.Location = new System.Drawing.Point(538, 229);
            this.panelPlayer1.Name = "panelPlayer1";
            this.panelPlayer1.Size = new System.Drawing.Size(170, 60);
            this.panelPlayer1.TabIndex = 2;
            this.panelPlayer1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Invaders.Properties.Resources.Player1;
            this.pictureBox1.Location = new System.Drawing.Point(114, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(51, 51);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // player1Name
            // 
            this.player1Name.Location = new System.Drawing.Point(0, 38);
            this.player1Name.MaxLength = 20;
            this.player1Name.Name = "player1Name";
            this.player1Name.Size = new System.Drawing.Size(108, 20);
            this.player1Name.TabIndex = 3;
            this.player1Name.Text = "Pilot1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Invaders.Properties.Resources.gracz1;
            this.pictureBox2.Location = new System.Drawing.Point(0, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(108, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // panelPlayer2
            // 
            this.panelPlayer2.BackColor = System.Drawing.Color.Transparent;
            this.panelPlayer2.Controls.Add(this.pictureBox4);
            this.panelPlayer2.Controls.Add(this.player2Name);
            this.panelPlayer2.Controls.Add(this.pictureBox3);
            this.panelPlayer2.Location = new System.Drawing.Point(78, 229);
            this.panelPlayer2.Name = "panelPlayer2";
            this.panelPlayer2.Size = new System.Drawing.Size(170, 60);
            this.panelPlayer2.TabIndex = 4;
            this.panelPlayer2.Visible = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Invaders.Properties.Resources.Player2;
            this.pictureBox4.Location = new System.Drawing.Point(3, 3);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(51, 51);
            this.pictureBox4.TabIndex = 10;
            this.pictureBox4.TabStop = false;
            // 
            // player2Name
            // 
            this.player2Name.Location = new System.Drawing.Point(60, 38);
            this.player2Name.MaxLength = 20;
            this.player2Name.Name = "player2Name";
            this.player2Name.Size = new System.Drawing.Size(108, 20);
            this.player2Name.TabIndex = 3;
            this.player2Name.Text = "Pilot2";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Invaders.Properties.Resources.gracz2;
            this.pictureBox3.Location = new System.Drawing.Point(60, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(108, 30);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // grajBtn
            // 
            this.grajBtn.BackgroundImage = global::Invaders.Properties.Resources.graj;
            this.grajBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.grajBtn.Location = new System.Drawing.Point(338, 141);
            this.grajBtn.Name = "grajBtn";
            this.grajBtn.Size = new System.Drawing.Size(108, 30);
            this.grajBtn.TabIndex = 5;
            this.grajBtn.UseVisualStyleBackColor = true;
            this.grajBtn.Visible = false;
            this.grajBtn.Click += new System.EventHandler(this.grajBtn_Click);
            // 
            // jedenGraczBtn
            // 
            this.jedenGraczBtn.BackgroundImage = global::Invaders.Properties.Resources._1gracz;
            this.jedenGraczBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.jedenGraczBtn.Location = new System.Drawing.Point(338, 229);
            this.jedenGraczBtn.Name = "jedenGraczBtn";
            this.jedenGraczBtn.Size = new System.Drawing.Size(108, 30);
            this.jedenGraczBtn.TabIndex = 6;
            this.jedenGraczBtn.UseVisualStyleBackColor = true;
            this.jedenGraczBtn.Click += new System.EventHandler(this.jedenGraczBtn_Click);
            // 
            // dwochGraczyBtn
            // 
            this.dwochGraczyBtn.BackgroundImage = global::Invaders.Properties.Resources._2graczy;
            this.dwochGraczyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dwochGraczyBtn.Location = new System.Drawing.Point(338, 274);
            this.dwochGraczyBtn.Name = "dwochGraczyBtn";
            this.dwochGraczyBtn.Size = new System.Drawing.Size(108, 30);
            this.dwochGraczyBtn.TabIndex = 7;
            this.dwochGraczyBtn.UseVisualStyleBackColor = true;
            this.dwochGraczyBtn.Click += new System.EventHandler(this.dwochGraczyBtn_Click);
            // 
            // statystyki
            // 
            this.statystyki.BackgroundImage = global::Invaders.Properties.Resources.statystyki;
            this.statystyki.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.statystyki.Location = new System.Drawing.Point(338, 319);
            this.statystyki.Name = "statystyki";
            this.statystyki.Size = new System.Drawing.Size(108, 30);
            this.statystyki.TabIndex = 8;
            this.statystyki.UseVisualStyleBackColor = true;
            this.statystyki.Click += new System.EventHandler(this.statystyki_Click);
            // 
            // wyjscie
            // 
            this.wyjscie.BackgroundImage = global::Invaders.Properties.Resources.wyjscie;
            this.wyjscie.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.wyjscie.Location = new System.Drawing.Point(338, 364);
            this.wyjscie.Name = "wyjscie";
            this.wyjscie.Size = new System.Drawing.Size(108, 30);
            this.wyjscie.TabIndex = 9;
            this.wyjscie.UseVisualStyleBackColor = true;
            this.wyjscie.Click += new System.EventHandler(this.wyjscie_Click);
            // 
            // banerAnimationTimer
            // 
            this.banerAnimationTimer.Enabled = true;
            this.banerAnimationTimer.Interval = 125;
            this.banerAnimationTimer.Tick += new System.EventHandler(this.banerAnimationTimer_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(199, 416);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(386, 158);
            this.textBox1.TabIndex = 11;
            this.textBox1.Visible = false;
            // 
            // invadersBanner1
            // 
            this.invadersBanner1.BackColor = System.Drawing.Color.Transparent;
            this.invadersBanner1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("invadersBanner1.BackgroundImage")));
            this.invadersBanner1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.invadersBanner1.Location = new System.Drawing.Point(184, 10);
            this.invadersBanner1.Name = "invadersBanner1";
            this.invadersBanner1.Size = new System.Drawing.Size(416, 65);
            this.invadersBanner1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.wyjscie);
            this.Controls.Add(this.statystyki);
            this.Controls.Add(this.dwochGraczyBtn);
            this.Controls.Add(this.jedenGraczBtn);
            this.Controls.Add(this.grajBtn);
            this.Controls.Add(this.panelPlayer2);
            this.Controls.Add(this.panelPlayer1);
            this.Controls.Add(this.wyciszBtn);
            this.Controls.Add(this.invadersBanner1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invaders";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.panelPlayer1.ResumeLayout(false);
            this.panelPlayer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panelPlayer2.ResumeLayout(false);
            this.panelPlayer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer animationTimer;
        private InvadersBanner invadersBanner1;
        private System.Windows.Forms.Button wyciszBtn;
        private System.Windows.Forms.Panel panelPlayer1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox player1Name;
        private System.Windows.Forms.Panel panelPlayer2;
        private System.Windows.Forms.TextBox player2Name;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button grajBtn;
        private System.Windows.Forms.Button jedenGraczBtn;
        private System.Windows.Forms.Button dwochGraczyBtn;
        private System.Windows.Forms.Button statystyki;
        private System.Windows.Forms.Button wyjscie;
        private System.Windows.Forms.Timer banerAnimationTimer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TextBox textBox1;
    }
}

