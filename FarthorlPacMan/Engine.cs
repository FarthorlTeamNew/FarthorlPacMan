﻿using System.Threading;

namespace FarthorlPacMan
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Threading.Tasks;

    public class Engine : IDisposable
    {
        private Graphics graphics;
        private Graphics graphicsGhost;
        private Graphics pointsGraphics;
        private Bitmap buffer = new Bitmap(1200, 650);
        private Task threadRenderingPacMan;
        private Task threadRenderingGhost;
        private string[,] pathsMatrix = new string[24, 13];
        private int xMax = 24; // columns
        private int yMax = 13; // rows
        private int leftScore;
        private int ghostElements = 4;
        private int pacManEatScores = 0;
        private bool run = true;
        private string moveDirection;
        private Color wallColor = Color.Cyan;
        private readonly GameWindow game;
        private bool isInicialize = false;
        private PacMan pacMan;
        private List<Point> points = new List<Point>();
        private List<Ghost> ghosts = new List<Ghost>();
        private PlayerSound player = new PlayerSound();

        public Engine(Graphics graphic, Graphics graphicsGhost, Graphics pointsGraphics, GameWindow game)
        {
            this.graphics = graphic;
            this.graphicsGhost = graphicsGhost;
            this.pointsGraphics = pointsGraphics;
            this.game = game;

        }

        public void Initialize()
        {
            //Initialize game if started for the first time
            if (isInicialize == false)
            {
                this.isInicialize = true;
                this.initializeMatrix();
                this.DrawContent();
                this.inicializeLeftScores();

                threadRenderingPacMan = new Task(RenderPacMan);
                threadRenderingGhost = new Task(RenderGhost);
                pacMan = new PacMan(0, 0, this.graphics, this);

                for (int i = 0; i < ghostElements; i++)
                {
                    ghosts.Add(new Ghost(pacMan.getQuadrantX(), pacMan.getQuadrantY(), graphicsGhost, this));
                }

                threadRenderingPacMan.Start();
                threadRenderingGhost.Start();

                Control.CheckForIllegalCrossThreadCalls = false;
            }
            else
            {
                this.DrawContent();
            }
        }

        private void DrawFontColor()
        {
            using (Graphics drawing = Graphics.FromImage(buffer))
            {
                drawing.FillRectangle(new SolidBrush(Color.Black), 0, 0, 1200, 650);
                game.pacMan.BackgroundImage = buffer;
            }
        }

        private void DrawPaths()
        {
            using (Graphics drawing = Graphics.FromImage(buffer))
            {
                drawing.DrawRectangle(new Pen(wallColor), 0, 0, GetMaxX() * 50, GetMaxY() * 50);
                game.pacMan.BackgroundImage = buffer;
            }

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    var elements = pathsMatrix[x, y].Trim().Split(',');
                    int quadrant = int.Parse(elements[0]);
                    int pointIndex = int.Parse(elements[1]);

                    if (quadrant == 1)
                    {
                        using (Graphics drawing = Graphics.FromImage(buffer))
                        {

                            drawing.DrawRectangle(new Pen(wallColor), (x * 50), (y * 50), 50, 50);
                            drawing.FillRectangle(new SolidBrush(wallColor), (x * 50), (y * 50), 50, 50);
                            game.pacMan.BackgroundImage = buffer;

                        }
                    }

                    if (pointIndex == 1)
                    {
                        Point point = new Point((x * 50) + 25, (y * 50) + 25);
                        points.Add(point);

                        using (Graphics drawing = Graphics.FromImage(buffer))
                        {
                            drawing.FillEllipse(new SolidBrush(point.fillColor()), (x * 50) + 25 - (point.getDiameter() / 2), (y * 50) + 25 - (point.getDiameter() / 2), point.getDiameter(), point.getDiameter());
                            game.pacMan.BackgroundImage = buffer;
                        }
                    }
                }

            }
        }

        private void initializeMatrix()
        {
            try
            {
                string level = @"DataFiles\Levels\coordinates.txt";

                using (var fileMatrix = new StreamReader(level))
                {
                    string inputLine;
                    while ((inputLine = fileMatrix.ReadLine()) != null)
                    {
                        // Get values from the coordinates.txt example splitLine[0]=1,0 splitLine[1]=1|0|0|1|1
                        var splitLine = inputLine.Trim().Split('=');

                        //Get the position values for the 2D array example arrayXYValues[0]=1 arrayXYValues[0]=0 
                        var arrayXYValues = splitLine[0].Trim().Split(',');
                        int arrayX;
                        int arrayY;

                        //This is the values of the array cell
                        string arrayValue = splitLine[1];
                        try
                        {
                            arrayX = int.Parse(arrayXYValues[0]);
                            arrayY = int.Parse(arrayXYValues[1]);
                        }
                        catch (Exception)
                        {
                            throw new ArgumentException("Cannot conver string to integer");

                        }

                        //Add element data in to the specific point in the 2D array
                        this.pathsMatrix[arrayX, arrayY] = arrayValue;
                    }
                }
            }
            catch (Exception)
            {
                throw new FileLoadException();
            }
        }

        private void inicializeLeftScores()
        {
            foreach (var point in points)
            {
                if (!point.isEatPoint())
                {
                    leftScore = leftScore + 1;
                }
            }
            game.UpdateLeftScore(leftScore);
        }

        //Heare is the logic for gaming
        private async void RenderPacMan()
        {
            while (run)
            {
                pacMan.DrawPacMan();
                await pacMan.Run(moveDirection);
            }
        }

        private async void RenderGhost()
        {
            while (run)
            {
                for (int i = 0; i < 50; i++)
                {
                    foreach (var ghost in ghosts)
                    {
                        await ghost.Move();
                    }
                }
            }
        }

        private void UpdateLeftSores(int pacManScores)
        {
            game.UpdateLeftScore(leftScore - pacManScores);
            if (leftScore - pacManScores == 0)
            {
                this.run = false;
                game.panel1.Visible = true;
            }
        }

        public void DrawContent()
        {
            DrawFontColor();
            DrawPaths();
        }

        public void StopGame()
        {

        }

        public void PauseGame()
        {
            this.run = false;
            game.PausePanel.Visible = true;
        }

        public void ResumeGame()
        {

            game.PausePanel.Visible = false;
            this.run = true;
        }

        public bool IsPaused()
        {
            return !run;
        }

        public string[] GetQuadrantElements(int quadrantX, int quandrantY)
        {
            string[] elements = pathsMatrix[quadrantX, quandrantY].Trim().Split(',');
            return elements;
        }

        public void EatPointAndUpdateMatrix(int quadrantX, int quandrantY, string[] element)
        {

            var stringValue = $"{element[0]},{element[1]}";
            pathsMatrix[quadrantX, quandrantY] = stringValue;
            int pointDiameter = 0;
            foreach (var point in points)
            {
                if (point.getX() == (quadrantX * 50) + 25 && point.getY() == (quandrantY * 50) + 25)
                {
                    point.EatPoint();
                    pointDiameter = point.getDiameter();
                    pacManEatScores = pacManEatScores + 1;
                    break;
                }
            }

            using (Graphics drawing = Graphics.FromImage(buffer))
            {
                drawing.FillEllipse(new SolidBrush(Color.Black), (quadrantX * 50) + 25 - (pointDiameter / 2), (quandrantY * 50) + 25 - (pointDiameter / 2), pointDiameter, pointDiameter);
                this.game.pacMan.BackgroundImage = buffer;
            }

            game.UpdateScores(pacManEatScores);
            UpdateLeftSores(pacManEatScores);
        }

        //TODO Refactoring
        //Remove try section
        public void DrawPoint(int quadrantX, int quandrantY)
        {
            if (GetQuadrantElements(quadrantX, quandrantY)[1] == "1")
            {
                Point point = new Point();
                try
                {
                    pointsGraphics.FillEllipse(new SolidBrush(point.fillColor()), (quadrantX * 50) + 25 - (point.getDiameter() / 2), (quandrantY * 50) + 25 - (point.getDiameter() / 2), point.getDiameter(), point.getDiameter());

                }
                catch (Exception)
                {

                }
            }

        }

        public void changeDirection(string newDirection)
        {
            this.moveDirection = newDirection;
        }

        public bool isDirectionChanged(string myDirection)
        {
            if (myDirection == "Up" && this.moveDirection == "Down" || myDirection == "Down" && this.moveDirection == "Up")
            {
                return true;
            }

            if (myDirection == "Right" && this.moveDirection == "Left" || myDirection == "Left" && this.moveDirection == "Right")
            {
                return true;
            }

            return false;
        }

        public string GetDirection()
        {
            return moveDirection;
        }

        public bool isExistGhost(int quadrantX, int quadrantY)
        {
            foreach (var ghost in ghosts)
            {
                if (ghost.GetQuadrantX() == quadrantX && ghost.GetQuadrantY() == quadrantY)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetMaxX()
        {
            return this.xMax;
        }

        public int GetMaxY()
        {
            return this.yMax;

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
                if (threadRenderingPacMan != null)
                {
                    threadRenderingPacMan.Dispose();
                }

                if (threadRenderingGhost != null)
                {
                    threadRenderingGhost.Dispose();
                }

                if (buffer != null)
                {
                    buffer.Dispose();
                }

                if (player != null)
                {
                    player.Dispose();
                }

                if (pacMan != null)
                {
                    pacMan.Dispose();
                }
            }
        }
    }
}