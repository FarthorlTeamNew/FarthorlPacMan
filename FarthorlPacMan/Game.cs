using System;
using System.Drawing;

namespace FarthorlPacMan
{
    public class Game : IDisposable
    {
        private Engine graphicEngine;

        // 4. Starts drawing the graphics, Ghosts, points, and the level design itself
        public void startDraw(Graphics graphic, Graphics graphicsGhost,Graphics pointsGraphics, GameWindow game)
        {
            this.graphicEngine = new Engine(graphic, graphicsGhost, pointsGraphics, game);
            this.graphicEngine.Initialize();
        }

        public Engine GraphicEngine
        {
            get { return graphicEngine; }
            set { graphicEngine = value; }
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
        }

        public void Dispose()
        {
            graphicEngine.Dispose();
        }
    }
}