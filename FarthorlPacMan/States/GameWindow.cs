using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarthorlPacMan
{
    public partial class GameWindow : Form
    {
        public static string Level;
        private Game game = new Game();
        private bool isInicialize = false;
      
        //1. Initialize the Windows Forms Properties. menus, etc...
        public GameWindow()
        {
            this.InitializeComponent();
        }

        private void pacMan_Paint(object sender, PaintEventArgs e)
        {
            if (!this.isInicialize)
            {
                Graphics graphics = this.pacMan.CreateGraphics();
                Graphics graphicsGhost = this.pacMan.CreateGraphics();
                Graphics pointsGraphics = this.pacMan.CreateGraphics();
                Graphics graphicsFruit = this.pacMan.CreateGraphics();
                if (Level != null)
                {
                    this.game.SetNewLevel(Level);
                }
                this.game.startDraw(graphics, graphicsGhost, pointsGraphics, graphicsFruit, this);
                this.isInicialize = true;
            }

        }

        // Game starts when any up, down, left, right key is pressed
        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) //If press right arrow
            {
                this.game.ChangeDirection("Right");
            }

            if (e.KeyCode == Keys.Left)
            {
                this.game.ChangeDirection("Left"); //If press left arrow 
            }

            if (e.KeyCode == Keys.Up)
            {
                this.game.ChangeDirection("Up"); //If press up arrow
            }

            if (e.KeyCode == Keys.Down)
            {
                this.game.ChangeDirection("Down"); //If press down arrow
            }
            if (e.KeyCode == Keys.P) //If press down arrow
            {
                if (Engine.Run == false)
                {
                    this.game.ResumeGame(); ;
                }
                else
                {
                    SoundPlayer.Play("pause");
                    this.game.PauseGame();
                }
            }
        }

        public void UpdateScores(int score)
        {
            this.ScoreLabel.Text = $"Points: {score}";

        }

        public void UpdateLeftScore(int score)
        {
            this.LeftScore.Text = $"Points Left: {score}";
        }

        //2. Checks whether the level is drawn and initialized
        private void GameWindow_Move(object sender, EventArgs e)
        {
            if (this.isInicialize)
            {
                SoundPlayer.Play("pause");
                this.game.PauseGame();
            }
        }

        private void GameWindow_Closed(object sender, FormClosedEventArgs e)
        {
            Engine.Run = true;
            this.isInicialize = false;
        }

        public void StopGame()
        {
            this.game.StopGame();
        }
    }
}