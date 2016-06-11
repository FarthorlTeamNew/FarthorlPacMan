using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace FarthorlPacMan
{
    public partial class GameWindow : Form
    {
        private Game game = new Game();
        private bool isInicialize = false;
        private SoundPlayer pause = new SoundPlayer("DataFiles/Sounds/pause.wav");
        public GameWindow()
        {
            InitializeComponent();
        }

        private void pacMan_Paint(object sender, PaintEventArgs e)
        {
            if (!isInicialize)
            {
                Graphics graphics = pacMan.CreateGraphics();
                this.game.startDraw(graphics, this);
                this.isInicialize = true;
            }

        }

        private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.game.stopGame();
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 39) //If press right arrow
            {
                game.Direction("Right");
            }

            if (e.KeyValue == 37)
            {
                game.Direction("Left"); //If press left arrow 
            }

            if (e.KeyValue == 38)
            {
                game.Direction("Up"); //If press up arrow
            }

            if (e.KeyValue == 40)
            {
                game.Direction("Down"); //If press down arrow
            }
        }

        public void UpdateScores(int score)
        {
            ScoreLabel.Text = $"Scores: {score}";

        }

        public void UpdateLeftScore(int score)
        {
            LeftScore.Text = $"Left scores: {score}";
        }


        private void GameWindow_Move(object sender, EventArgs e)
        {
            if (isInicialize)
            {
                pause.Play();
                game.PauseGame();
            }

        }

        private void GameWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.game.stopGame();
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
