namespace FarthorlPacMan
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Snap ITC", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Maroon;
            this.button4.Location = new System.Drawing.Point(262, 99);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(255, 72);
            this.button4.TabIndex = 0;
            this.button4.Text = "START GAME";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Snap ITC", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.Maroon;
            this.button5.Location = new System.Drawing.Point(262, 177);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(255, 72);
            this.button5.TabIndex = 1;
            this.button5.Text = "EXIT";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Exit_Button);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Navy;
            this.textBox2.Font = new System.Drawing.Font("Showcard Gothic", 21F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Orange;
            this.textBox2.Location = new System.Drawing.Point(262, 51);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(255, 42);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "TEAM FARTHORL";
            // 
            // InitialWindow
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.BackgroundImage = global::FarthorlPacMan.Properties.Resources.MenuBackGround;
            this.ClientSize = new System.Drawing.Size(550, 306);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(566, 345);
            this.MinimumSize = new System.Drawing.Size(566, 345);
            this.Name = "InitialWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Farthorl PacMan Game";
            this.Load += new System.EventHandler(this.InitialWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox2;
    }
}