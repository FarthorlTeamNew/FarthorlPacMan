using System;
using System.Windows.Forms;

namespace FarthorlPacMan
{
    using System.Drawing;

    class Game
    {
        private Engine graphicEngine;

        public void startDraw(Graphics graphic, Graphics graphicsGhost, GameWindow game)
        {
            this.graphicEngine = new Engine(graphic, graphicsGhost, game);
            this.graphicEngine.Initialize();
        }

        public void Redraw()
        {
            graphicEngine.DrawContent();
        }

        public void ChangeDirection(string direction)
        {
            if (graphicEngine.IsPaused())
            {
                graphicEngine.ResumeGame();
            }
            graphicEngine.changeDirection(direction);
        }

        public void PauseGame()
        {
            graphicEngine.PauseGame();
        }

        public void stopGame()
        {
            this.graphicEngine.StopGame();
            Environment.Exit(0);
        }
    }
}