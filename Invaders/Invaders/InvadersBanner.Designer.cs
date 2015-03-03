namespace Invaders
{
    partial class InvadersBanner
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.banerAnimationTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // banerAnimationTimer
            // 
            this.banerAnimationTimer.Enabled = true;
            this.banerAnimationTimer.Tick += new System.EventHandler(this.banerAnimationTimer_Tick);
            // 
            // InvadersBanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Name = "InvadersBanner";
            this.Size = new System.Drawing.Size(416, 65);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer banerAnimationTimer;
    }
}
