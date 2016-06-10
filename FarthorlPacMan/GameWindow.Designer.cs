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
            this.PausePanel = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.pauseText = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.LeftScore = new System.Windows.Forms.Label();
            this.pacMan.SuspendLayout();
            this.PausePanel.SuspendLayout();
            this.message.SuspendLayout();
            this.SuspendLayout();
            // 
            // pacMan
            // 
            this.pacMan.Controls.Add(this.PausePanel);
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
            // PausePanel
            // 
            this.PausePanel.Controls.Add(this.label11);
            this.PausePanel.Controls.Add(this.pauseText);
            this.PausePanel.Location = new System.Drawing.Point(190, 466);
            this.PausePanel.Name = "PausePanel";
            this.PausePanel.Size = new System.Drawing.Size(799, 70);
            this.PausePanel.TabIndex = 1;
            this.PausePanel.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(199, 37);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(383, 25);
            this.label11.TabIndex = 1;
            this.label11.Text = "Please, press controls key to continue!";
            // 
            // pauseText
            // 
            this.pauseText.AutoSize = true;
            this.pauseText.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pauseText.Location = new System.Drawing.Point(334, 0);
            this.pauseText.Name = "pauseText";
            this.pauseText.Size = new System.Drawing.Size(125, 37);
            this.pauseText.TabIndex = 0;
            this.pauseText.Text = "PAUSE";
            // 
            // message
            // 
            this.message.BackColor = System.Drawing.Color.Transparent;
            this.message.Controls.Add(this.label1);
            this.message.Controls.Add(this.label12);
            this.message.Controls.Add(this.label10);
            this.message.Controls.Add(this.label9);
            this.message.Controls.Add(this.label8);
            this.message.Controls.Add(this.label7);
            this.message.Controls.Add(this.label6);
            this.message.Controls.Add(this.label5);
            this.message.Controls.Add(this.label4);
            this.message.Controls.Add(this.label3);
            this.message.Controls.Add(this.label2);
            this.message.Location = new System.Drawing.Point(192, 90);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(800, 355);
            this.message.TabIndex = 0;
            this.message.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.MaximumSize = new System.Drawing.Size(790, 50);
            this.label1.MinimumSize = new System.Drawing.Size(790, 50);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Size = new System.Drawing.Size(790, 50);
            this.label1.TabIndex = 12;
            this.label1.Text = "Congratulation!!! You Win Farthorl PacMan Game!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(5, 300);
            this.label12.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.MaximumSize = new System.Drawing.Size(790, 25);
            this.label12.MinimumSize = new System.Drawing.Size(790, 25);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.Size = new System.Drawing.Size(790, 25);
            this.label12.TabIndex = 11;
            this.label12.Text = "Stoyan Ivanov";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(5, 275);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.MaximumSize = new System.Drawing.Size(790, 25);
            this.label10.MinimumSize = new System.Drawing.Size(790, 25);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Size = new System.Drawing.Size(790, 25);
            this.label10.TabIndex = 9;
            this.label10.Text = "vani4ka66";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(5, 250);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.MaximumSize = new System.Drawing.Size(790, 25);
            this.label9.MinimumSize = new System.Drawing.Size(790, 25);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Size = new System.Drawing.Size(790, 25);
            this.label9.TabIndex = 8;
            this.label9.Text = "TerezaBiserina";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(5, 225);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.MaximumSize = new System.Drawing.Size(790, 25);
            this.label8.MinimumSize = new System.Drawing.Size(790, 25);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Size = new System.Drawing.Size(790, 25);
            this.label8.TabIndex = 7;
            this.label8.Text = "petar.dimitrov.86";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(5, 200);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.MaximumSize = new System.Drawing.Size(790, 25);
            this.label7.MinimumSize = new System.Drawing.Size(790, 25);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Size = new System.Drawing.Size(790, 25);
            this.label7.TabIndex = 6;
            this.label7.Text = "minspi";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(5, 175);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.MaximumSize = new System.Drawing.Size(790, 25);
            this.label6.MinimumSize = new System.Drawing.Size(790, 25);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Size = new System.Drawing.Size(790, 25);
            this.label6.TabIndex = 5;
            this.label6.Text = "Ivailo_Kodov";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(5, 150);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.MaximumSize = new System.Drawing.Size(790, 25);
            this.label5.MinimumSize = new System.Drawing.Size(790, 25);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Size = new System.Drawing.Size(790, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Gruychev";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(5, 125);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.MaximumSize = new System.Drawing.Size(790, 25);
            this.label4.MinimumSize = new System.Drawing.Size(790, 25);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Size = new System.Drawing.Size(790, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "deniz";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(5, 100);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.MaximumSize = new System.Drawing.Size(790, 25);
            this.label3.MinimumSize = new System.Drawing.Size(790, 25);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Size = new System.Drawing.Size(790, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Dean788";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(5, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.MaximumSize = new System.Drawing.Size(790, 50);
            this.label2.MinimumSize = new System.Drawing.Size(790, 50);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Size = new System.Drawing.Size(790, 50);
            this.label2.TabIndex = 1;
            this.label2.Text = "Special Tanks to:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.PausePanel.ResumeLayout(false);
            this.PausePanel.PerformLayout();
            this.message.ResumeLayout(false);
            this.message.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label ScoreLabel;
        public System.Windows.Forms.Label LeftScore;
        public System.Windows.Forms.Panel pacMan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label pauseText;
        public System.Windows.Forms.Panel PausePanel;
        public System.Windows.Forms.Panel message;
        private System.Windows.Forms.Label label1;
    }
}

