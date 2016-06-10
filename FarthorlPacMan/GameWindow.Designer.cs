namespace FarthorlPacMan
{
    partial class GameWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameWindow));
            this.pacMan = new System.Windows.Forms.Panel();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.LeftScore = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pacMan.SuspendLayout();
            this.message.SuspendLayout();
            this.SuspendLayout();
            // 
            // pacMan
            // 
            this.pacMan.Controls.Add(this.message);
            this.pacMan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pacMan.Location = new System.Drawing.Point(0, 0);
            this.pacMan.MaximumSize = new System.Drawing.Size(1200, 650);
            this.pacMan.MinimumSize = new System.Drawing.Size(1200, 650);
            this.pacMan.Name = "pacMan";
            this.pacMan.Size = new System.Drawing.Size(1200, 650);
            this.pacMan.TabIndex = 0;
            this.pacMan.Paint += new System.Windows.Forms.PaintEventHandler(this.pacMan_Paint);
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScoreLabel.Location = new System.Drawing.Point(3, 652);
            this.ScoreLabel.MinimumSize = new System.Drawing.Size(250, 30);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(250, 33);
            this.ScoreLabel.TabIndex = 2;
            this.ScoreLabel.Text = "Scores:";
            this.ScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LeftScore
            // 
            this.LeftScore.AutoSize = true;
            this.LeftScore.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LeftScore.Location = new System.Drawing.Point(304, 652);
            this.LeftScore.MinimumSize = new System.Drawing.Size(250, 30);
            this.LeftScore.Name = "LeftScore";
            this.LeftScore.Size = new System.Drawing.Size(250, 33);
            this.LeftScore.TabIndex = 3;
            this.LeftScore.Text = "Left scores:";
            this.LeftScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // message
            // 
            this.message.Controls.Add(this.label1);
            this.message.Location = new System.Drawing.Point(262, 239);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(617, 174);
            this.message.TabIndex = 0;
            this.message.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 687);
            this.Controls.Add(this.LeftScore);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.pacMan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1216, 750);
            this.MinimumSize = new System.Drawing.Size(1216, 596);
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Farthorl PacMan Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameWindow_FormClosed);
            this.LocationChanged += new System.EventHandler(this.GameWindow_LocationChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyDown);
            this.Move += new System.EventHandler(this.GameWindow_Move);
            this.pacMan.ResumeLayout(false);
            this.message.ResumeLayout(false);
            this.message.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label ScoreLabel;
        public System.Windows.Forms.Label LeftScore;
        public System.Windows.Forms.Panel pacMan;
        private System.Windows.Forms.Panel message;
        private System.Windows.Forms.Label label1;
    }
}

