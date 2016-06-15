using System;
using System.Windows.Forms;

namespace FarthorlPacMan
{
    using System.Drawing;

    class Game:IDisposable
    {
        private Engine graphicEngine;

        public void startDraw(Graphics graphic, Graphics graphicsGhost,Graphics pointsGraphics, GameWindow game)
        {
            this.graphicEngine = new Engine(graphic, graphicsGhost, pointsGraphics, game);
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (this.graphicEngine != null)
                {
                    this.graphicEngine.Dispose();
                }
            }
        }
    }
}