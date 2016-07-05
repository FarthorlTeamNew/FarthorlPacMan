namespace FarthorlPacMan.States
{
    partial class LevelsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelsWindow));
            this.Labirint_Level = new System.Windows.Forms.Button();
            this.CsharpLevel = new System.Windows.Forms.Button();
            this.Labirint2 = new System.Windows.Forms.Button();
            this.EuroLevel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ChooseLevel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Labirint_Level
            // 
            this.Labirint_Level.Font = new System.Drawing.Font("Snap ITC", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Labirint_Level.ForeColor = System.Drawing.Color.Maroon;
            this.Labirint_Level.Location = new System.Drawing.Point(398, 30);
            this.Labirint_Level.Name = "Labirint_Level";
            this.Labirint_Level.Size = new System.Drawing.Size(174, 54);
            this.Labirint_Level.TabIndex = 0;
            this.Labirint_Level.Text = "LABIRINT";
            this.Labirint_Level.UseVisualStyleBackColor = true;
            this.Labirint_Level.Click += new System.EventHandler(this.Labirint_Level_Click);
            // 
            // CsharpLevel
            // 
            this.CsharpLevel.Font = new System.Drawing.Font("Snap ITC", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CsharpLevel.ForeColor = System.Drawing.Color.Maroon;
            this.CsharpLevel.Location = new System.Drawing.Point(398, 150);
            this.CsharpLevel.Name = "CsharpLevel";
            this.CsharpLevel.Size = new System.Drawing.Size(174, 54);
            this.CsharpLevel.TabIndex = 1;
            this.CsharpLevel.Text = "C# LOVE";
            this.CsharpLevel.UseVisualStyleBackColor = true;
            this.CsharpLevel.Click += new System.EventHandler(this.CsharpLevel_Click);
            // 
            // Labirint2
            // 
            this.Labirint2.Font = new System.Drawing.Font("Snap ITC", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Labirint2.ForeColor = System.Drawing.Color.Maroon;
            this.Labirint2.Location = new System.Drawing.Point(398, 90);
            this.Labirint2.Name = "Labirint2";
            this.Labirint2.Size = new System.Drawing.Size(174, 54);
            this.Labirint2.TabIndex = 2;
            this.Labirint2.Text = "LABIRINT 2";
            this.Labirint2.UseVisualStyleBackColor = true;
            this.Labirint2.Click += new System.EventHandler(this.button1_Click);
            // 
            // EuroLevel
            // 
            this.EuroLevel.Font = new System.Drawing.Font("Snap ITC", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EuroLevel.ForeColor = System.Drawing.Color.Maroon;
            this.EuroLevel.Location = new System.Drawing.Point(398, 210);
            this.EuroLevel.Name = "EuroLevel";
            this.EuroLevel.Size = new System.Drawing.Size(174, 54);
            this.EuroLevel.TabIndex = 3;
            this.EuroLevel.Text = "Euro 2016";
            this.EuroLevel.UseVisualStyleBackColor = true;
            this.EuroLevel.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::FarthorlPacMan.Properties.Resources.Change_level_background;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(31, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(341, 208);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // ChooseLevel
            // 
            this.ChooseLevel.Font = new System.Drawing.Font("Snap ITC", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseLevel.ForeColor = System.Drawing.Color.Maroon;
            this.ChooseLevel.Location = new System.Drawing.Point(31, 234);
            this.ChooseLevel.Name = "ChooseLevel";
            this.ChooseLevel.Size = new System.Drawing.Size(341, 77);
            this.ChooseLevel.TabIndex = 5;
            this.ChooseLevel.Text = "I LIKE THAT";
            this.ChooseLevel.UseVisualStyleBackColor = true;
            this.ChooseLevel.Click += new System.EventHandler(this.ChooseLevel_Click);
            // 
            // LevelsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(584, 408);
            this.Controls.Add(this.ChooseLevel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.EuroLevel);
            this.Controls.Add(this.Labirint2);
            this.Controls.Add(this.CsharpLevel);
            this.Controls.Add(this.Labirint_Level);
            this.MaximumSize = new System.Drawing.Size(600, 446);
            this.MinimumSize = new System.Drawing.Size(600, 446);
            this.Name = "LevelsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PACMAN LEVELS";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Labirint_Level;
        private System.Windows.Forms.Button CsharpLevel;
        private System.Windows.Forms.Button Labirint2;
        private System.Windows.Forms.Button EuroLevel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ChooseLevel;
    }
}