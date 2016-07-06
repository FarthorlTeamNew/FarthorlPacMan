using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarthorlPacMan
{
    public partial class GameWindow : Form
    {
        public static string Level;
        private Game _game = new Game();
        private bool _isInicialize = false;
      
        //1. Initialize the Windows Forms Properties. menus, etc...
        public GameWindow()
        {
            this.InitializeComponent();
        }

        private void pacMan_Paint(object sender, PaintEventArgs e)
        {
            if (!this._isInicialize)
            {
                Graphics graphics = this.pacMan.CreateGraphics();
                Graphics graphicsGhost = this.pacMan.CreateGraphics();
                Graphics pointsGraphics = this.pacMan.CreateGraphics();
                if (Level!=null)
                {
                    this._game.SetNewLevel(Level);
                }
                this._game.StartDraw(graphics, graphicsGhost, pointsGraphics, this);
                this._isInicialize = true;
            }

        }

        // Game starts when any up, down, left, right key is pressed
        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) //If press right arrow
            {
                this._game.ChangeDirection("Right");
            }

            if (e.KeyCode == Keys.Left)
            {
                this._game.ChangeDirection("Left"); //If press left arrow 
            }

            if (e.KeyCode == Keys.Up)
            {
                this._game.ChangeDirection("Up"); //If press up arrow
            }

            if (e.KeyCode == Keys.Down)
            {
                this._game.ChangeDirection("Down"); //If press down arrow
            }
            if (e.KeyCode == Keys.P) //If press down arrow
            {
                if (Engine.Run == false)
                {
                    this._game.ResumeGame(); ;
                }
                else
                {
                    SoundPlayer.Play("pause");
                    this._game.PauseGame();
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
            if (this._isInicialize)
            {
                SoundPlayer.Play("pause");
                this._game.PauseGame();
            }
        }

        private void GameWindow_Closed(object sender, FormClosedEventArgs e)
        {
            Engine.Run = true;
            this._isInicialize = false;
        }

        public void StopGame()
        {
            this._game.StopGame();
        }
    }
}