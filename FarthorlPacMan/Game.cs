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
            this.GraphicEngine = new Engine(graphic, graphicsGhost, pointsGraphics, game);
            this.GraphicEngine.Initialize();
        }

        public Engine GraphicEngine
        {
            get { return graphicEngine; }
            set { graphicEngine = value; }
        }

        public void Redraw()
        {
            GraphicEngine.DrawContent();
        }

        public void ChangeDirection(string direction)
        {
            if (GraphicEngine.IsPaused())
            {
                GraphicEngine.ResumeGame();
            }
            GraphicEngine.changeDirection(direction);
        }

        public void PauseGame()
        {
            GraphicEngine.PauseGame();
        }

        public void stopGame()
        {
            this.GraphicEngine.StopGame();

        }

        public void Dispose()
        {
            GraphicEngine.Dispose();
        }
    }
}