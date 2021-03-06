﻿namespace FarthorlPacMan
{
    partial class InitialWindow
    {

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialWindow));
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.Change_Level = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.BackgroundImage = global::FarthorlPacMan.Properties.Resources.StartGame_Button;
            this.button4.Font = new System.Drawing.Font("Snap ITC", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Maroon;
            this.button4.Location = new System.Drawing.Point(262, 92);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(255, 34);
            this.button4.TabIndex = 0;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackgroundImage = global::FarthorlPacMan.Properties.Resources.Exit_Button;
            this.button5.Font = new System.Drawing.Font("Snap ITC", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.Maroon;
            this.button5.Location = new System.Drawing.Point(262, 216);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(255, 35);
            this.button5.TabIndex = 1;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Exit_Button);
            // 
            // Change_Level
            // 
            this.Change_Level.BackgroundImage = global::FarthorlPacMan.Properties.Resources.ChangeLevel_Button;
            this.Change_Level.Font = new System.Drawing.Font("Snap ITC", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Change_Level.ForeColor = System.Drawing.Color.Maroon;
            this.Change_Level.Location = new System.Drawing.Point(262, 132);
            this.Change_Level.Name = "Change_Level";
            this.Change_Level.Size = new System.Drawing.Size(255, 37);
            this.Change_Level.TabIndex = 3;
            this.Change_Level.UseVisualStyleBackColor = true;
            this.Change_Level.Click += new System.EventHandler(this.Change_Level_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::FarthorlPacMan.Properties.Resources.Scores_Button;
            this.button2.Font = new System.Drawing.Font("Snap ITC", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Maroon;
            this.button2.Location = new System.Drawing.Point(262, 175);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(255, 35);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::FarthorlPacMan.Properties.Resources.TeamLogo;
            this.pictureBox1.Location = new System.Drawing.Point(262, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(255, 40);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // InitialWindow
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.BackgroundImage = global::FarthorlPacMan.Properties.Resources.MenuBackGround;
            this.ClientSize = new System.Drawing.Size(550, 307);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Change_Level);
            this.Controls.Add(this.button5);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(566, 345);
            this.MinimumSize = new System.Drawing.Size(566, 345);
            this.Name = "InitialWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Farthorl PacMan Game";
            this.Load += new System.EventHandler(this.InitialWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button Change_Level;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}