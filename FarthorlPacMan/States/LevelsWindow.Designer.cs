﻿namespace FarthorlPacMan.States
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
            this.SuspendLayout();
            // 
            // Labirint_Level
            // 
            this.Labirint_Level.Font = new System.Drawing.Font("Snap ITC", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Labirint_Level.ForeColor = System.Drawing.Color.Maroon;
            this.Labirint_Level.Location = new System.Drawing.Point(398, 30);
            this.Labirint_Level.Name = "Labirint_Level";
            this.Labirint_Level.Size = new System.Drawing.Size(155, 54);
            this.Labirint_Level.TabIndex = 0;
            this.Labirint_Level.Text = "LABIRINT";
            this.Labirint_Level.UseVisualStyleBackColor = true;
            this.Labirint_Level.Click += new System.EventHandler(this.Labirint_Level_Click);
            // 
            // CsharpLevel
            // 
            this.CsharpLevel.Font = new System.Drawing.Font("Snap ITC", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CsharpLevel.ForeColor = System.Drawing.Color.Maroon;
            this.CsharpLevel.Location = new System.Drawing.Point(398, 90);
            this.CsharpLevel.Name = "CsharpLevel";
            this.CsharpLevel.Size = new System.Drawing.Size(155, 54);
            this.CsharpLevel.TabIndex = 1;
            this.CsharpLevel.Text = "C# LOVE";
            this.CsharpLevel.UseVisualStyleBackColor = true;
            this.CsharpLevel.Click += new System.EventHandler(this.CsharpLevel_Click);
            // 
            // LevelsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(584, 408);
            this.Controls.Add(this.CsharpLevel);
            this.Controls.Add(this.Labirint_Level);
            this.MaximumSize = new System.Drawing.Size(600, 446);
            this.MinimumSize = new System.Drawing.Size(600, 446);
            this.Name = "LevelsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PACMAN LEVELS";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Labirint_Level;
        private System.Windows.Forms.Button CsharpLevel;
    }
}