using System;
using System.Windows.Forms;
using FarthorlPacMan.States;

namespace FarthorlPacMan
{
    public partial class InitialWindow : Form
    {
        public GameWindow gameWindow ;
        private LevelsWindow levelWindow;

        public InitialWindow()
        {
            InitializeComponent();
        }

        private void Exit_Button(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gameWindow = new GameWindow();
            gameWindow.FormClosed += new FormClosedEventHandler(gameWindow_FormClosed);
            Hide();
            
            gameWindow.Show();
        }
        private void gameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            gameWindow.stopGame();
            this.Show();
        }

        private void InitialWindow_Load(object sender, EventArgs e)
        {
            SoundPlayer.Play("begining");
        }


        //Change level 
        private void Change_Level_Click(object sender, EventArgs e)
        {
            levelWindow = new LevelsWindow();
            levelWindow.FormClosed += new FormClosedEventHandler(levelWindow_FormClosed);
            Hide();
            levelWindow.Show();
        }
        private void levelWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            levelWindow.Hide();
            this.Show();
        }
    }
}

