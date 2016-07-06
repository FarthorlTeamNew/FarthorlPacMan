using System;
using System.Drawing;

namespace FarthorlPacMan
{
    public class Game : IDisposable
    {
        private Engine _graphicEngine;
        private string _level  = @"DataFiles\Levels\Labirint.txt";

        // 4. Starts drawing the graphics, Ghosts, points, and the level design itself
        public void StartDraw(Graphics graphic, Graphics graphicsGhost,Graphics pointsGraphics, GameWindow game)
        {
            this._graphicEngine = new Engine(graphic, graphicsGhost, pointsGraphics, game,this._level);
            this._graphicEngine.Initialize();
        }

        public  void SetNewLevel(string path)
        {
            this._level = path;
        }

        public void ChangeDirection(string direction)
        {
            if (this._graphicEngine.IsPaused())
            {
                this._graphicEngine.ResumeGame();
            }
            Engine.MoveDirection=direction;
        }

        public void PauseGame()
        {
            this._graphicEngine.PauseGame();
        }

        public void ResumeGame()
        {
            this._graphicEngine.ResumeGame();
        }

        public void StopGame()
        {
            this._graphicEngine.StopGame();
        }

        public void Dispose()
        {
            this._graphicEngine.Dispose();
        }
    }
}