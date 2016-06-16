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
        PlayerSound player = new PlayerSound();
      
        //1. Initialize the Windows Forms Properties. menus, etc...
        public GameWindow()
        {
            InitializeComponent();
        }

        private void pacMan_Paint(object sender, PaintEventArgs e)
        {
            if (!isInicialize)
            {
                Graphics graphics = pacMan.CreateGraphics();
                Graphics graphicsGhost = pacMan.CreateGraphics();
                Graphics pointsGraphics = pacMan.CreateGraphics();
                this.game.startDraw(graphics, graphicsGhost, pointsGraphics, this);
                this.isInicialize = true;
            }

        }

        // Game starts when any up, down, left, right key is pressed
        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 39) //If press right arrow
            {
                game.ChangeDirection("Right");
            }

            if (e.KeyValue == 37)
            {
                game.ChangeDirection("Left"); //If press left arrow 
            }

            if (e.KeyValue == 38)
            {
                game.ChangeDirection("Up"); //If press up arrow
            }

            if (e.KeyValue == 40)
            {
                game.ChangeDirection("Down"); //If press down arrow
            }
        }

        public void UpdateScores(int score)
        {
            ScoreLabel.Text = $"Points: {score}";

        }

        public void UpdateLeftScore(int score)
        {
            LeftScore.Text = $"Points Left: {score}";
        }

        //2. Checks whether the level is drawn and initialized
        private void GameWindow_Move(object sender, EventArgs e)
        {
            if (isInicialize)
            {
                player.Play("pause");
                game.PauseGame();
            }
        }

        private void GameWindow_Closed(object sender, FormClosedEventArgs e)
        {
           // Environment.Exit(0);    
          
        }

        public void stopGame()
        {
            game.stopGame();
        }
    }
}