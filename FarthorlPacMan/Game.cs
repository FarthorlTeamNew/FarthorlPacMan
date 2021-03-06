﻿using System;
using System.Drawing;

namespace FarthorlPacMan
{
    public class Game : IDisposable
    {
        private Engine graphicEngine;
        private string level  = @"DataFiles\Levels\Labirint.txt";

        // 4. Starts drawing the graphics, Ghosts, points, and the level design itself
        public void StartDraw(Graphics graphic, Graphics graphicsGhost, Graphics pointsGraphics, Graphics graphicsFruit, GameWindow game)
        {
            this.graphicEngine = new Engine(graphic, graphicsGhost, pointsGraphics, graphicsFruit, game, this.level);
            this.graphicEngine.Initialize();
        }

        public  void SetNewLevel(string path)
        {
            this.level = path;
        }

        public void ChangeDirection(string direction)
        {
            if (this.graphicEngine.IsPaused())
            {
                this.graphicEngine.ResumeGame();
            }
            Engine.MoveDirection = direction;
        }

        public void PauseGame()
        {
            this.graphicEngine.PauseGame();
        }

        public void ResumeGame()
        {
            this.graphicEngine.ResumeGame();
        }

        public void StopGame()
        {
            this.graphicEngine.GameOver();
        }

        public void Dispose()
        {
            this.graphicEngine.Dispose();
        }
    }
}