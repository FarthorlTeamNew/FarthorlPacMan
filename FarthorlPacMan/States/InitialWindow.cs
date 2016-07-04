using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FarthorlPacMan
{
    public partial class InitialWindow : Form
    {
        GameWindow gameWindow = new GameWindow();
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
    }
}
