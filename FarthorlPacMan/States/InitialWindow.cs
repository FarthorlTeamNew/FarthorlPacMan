using System;
using System.Windows.Forms;
using FarthorlPacMan.States;

namespace FarthorlPacMan
{
    public partial class InitialWindow : Form
    {
        public GameWindow GameWindow ;
        private LevelsWindow _levelWindow;

        public InitialWindow()
        {
            this.InitializeComponent();
        }

        private void Exit_Button(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.GameWindow = new GameWindow();
            this.GameWindow.FormClosed += new FormClosedEventHandler(this.gameWindow_FormClosed);
            this.Hide();
            
            this.GameWindow.Show();
        }
        private void gameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.GameWindow.StopGame();
            this.Show();
        }

        private void InitialWindow_Load(object sender, EventArgs e)
        {
            SoundPlayer.Play("begining");
        }


        //Change level 
        private void Change_Level_Click(object sender, EventArgs e)
        {
            this._levelWindow = new LevelsWindow();
            this._levelWindow.FormClosed += new FormClosedEventHandler(this.levelWindow_FormClosed);
            this.Hide();
            this._levelWindow.Show();
        }
        private void levelWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._levelWindow.Hide();
            this.Show();
        }
    }
}

