using System.Windows.Forms;

namespace FarthorlPacMan
{
    using System.Drawing;

    class Game
    {
        private Engine graphicEngine;

        public void startDraw(Graphics graphic, GameWindow game)
        {
            this.graphicEngine = new Engine(graphic, game);
            this.graphicEngine.Initialize(); 
        }

        public void Redraw()
        {
            graphicEngine.DrawContent();
        }

        public void stopGame()
        {
            this.graphicEngine.StopGame();
        }

        public void Direction(string direction)
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


    }
}
