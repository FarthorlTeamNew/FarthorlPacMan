using System;
using System.Windows.Forms;
using FarthorlPacMan.States;

namespace FarthorlPacMan
{
    public partial class InitialWindow : Form
    {
        GameWindow gameWindow = new GameWindow();
        LevelsWindow levelWindow=new LevelsWindow();
        PlayerSound player =new PlayerSound();

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
            player.Play("begining");
        }


        //Change level switch
        private void Change_Level_Click(object sender, EventArgs e)
        {
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

