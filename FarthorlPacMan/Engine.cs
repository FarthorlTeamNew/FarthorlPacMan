﻿namespace FarthorlPacMan
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Drawing;
    using System.Windows.Forms;

    public class Engine
    {
        private Graphics graphics;
        private Thread threadRendering;
        private Thread threadMonsters;
        private string[,] pathsMatrix = new string[24, 16];
        private int xMax = 24;
        private int yMax = 16;
        private int leftScore;
        private bool run = true;
        private string moveDirection;
        private Color wallColor = Color.Cyan;
        private readonly GameWindow game;
        private bool isInicialize = false;
        private PacMan pacMan;
        List<Point> points = new List<Point>();
        public Engine(Graphics graphic, GameWindow game)
        {
            this.graphics = graphic;
            this.game = game;

        }

        public void DrawContent()
        {
            DrawFontColor();
            DrawPaths();
            foreach (var point in points)
            {
                point.DrawPoint(graphics);
            }
        }

        public void Initialize()
        {
            if (!isInicialize)
            {
                isInicialize = true;
                threadRendering = new Thread(new ThreadStart(render));
                this.initializeMatrix();
                this.FillPoints();
                inicializeLeftScores();
                pacMan = new PacMan(0, 0, this.graphics, this);
                this.DrawContent();
                threadRendering.Start();
                Control.CheckForIllegalCrossThreadCalls = false;
            }
            else
            {
                this.DrawContent();
            }
        }

        public void stopGame()
        {
            try
            {
                threadRendering.Resume();
            }
            catch (Exception)
            {

            }

            threadRendering.Abort();
        }

        public void PauseGame()
        {
            this.run = false;
            threadRendering.Suspend();
        }

        public void ResumeGame()
        {
            this.run = true;
            threadRendering.Resume();
        }

        public bool IsPaused()
        {
            return !run;
        }

        //Heare is the logic for gaming
        private void render()
        {
            while (run)
            {
                pacMan.move(this.graphics, this, moveDirection);
                game.UpdateScores(pacMan.getScore());
                UpdateLeftSores(pacMan.getScore());
            }
        }

        private void initializeMatrix()
        {
            try
            {
                using (var fileMatrix = new StreamReader("DataFiles/coordinates.txt"))
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

        private void DrawPaths()
        {
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    var elements = pathsMatrix[x, y].Trim().Split('|');
                    int topIndex = int.Parse(elements[0]);
                    int rightIndex = int.Parse(elements[1]);
                    int bottomIndex = int.Parse(elements[2]);
                    int leftIndex = int.Parse(elements[3]);
                    int pointIndex = int.Parse(elements[4]);

                    if (topIndex == 1)
                    {
                        graphics.DrawLine(new Pen(wallColor), (x * 50), (y * 50), (x * 50) + 50, (y * 50));
                    }

                    if (rightIndex == 1)
                    {
                        graphics.DrawLine(new Pen(wallColor), (x * 50) + 50, (y * 50), (x * 50) + 50, (y * 50) + 50);
                    }

                    if (bottomIndex == 1)
                    {
                        graphics.DrawLine(new Pen(wallColor), (x * 50), (y * 50) + 50, (x * 50) + 50, (y * 50) + 50);
                    }

                    if (leftIndex == 1)
                    {
                        graphics.DrawLine(new Pen(wallColor), (x * 50), (y * 50), (x * 50), (y * 50) + 50);
                    }


                    if (topIndex == 1 && rightIndex == 1 && bottomIndex == 1 && bottomIndex == 1)
                    {
                        graphics.FillRectangle(new SolidBrush(wallColor), (x * 50), (y * 50), 50, 50);
                    }

                }
            }

        }

        private void FillPoints()
        {

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    var elements = pathsMatrix[x, y].Trim().Split('|');
                    int pointIndex = int.Parse(elements[4]);
                    if (pointIndex == 1)
                    {
                        Point point = new Point((x * 50) + 25, (y * 50) + 25);
                        points.Add(point);
                    }
                }
            }
        }

        private void DrawFontColor()
        {
            graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, 1200, 800);
        }

        public int GetMaxX()
        {
            return this.xMax;
        }

        public int GetMaxY()
        {
            return this.yMax;
        }

        public string[] GetQuadrantElements(int quadrantX, int quandrantY)
        {
            string[] elements = pathsMatrix[quadrantX, quandrantY].Trim().Split('|');
            return elements;
        }

        public void EatPointAdnUpdateMatrix(int quadrantX, int quandrantY, string[] element)
        {
            var stringValue = $"{element[0]}|{element[1]}|{element[2]}|{element[3]}|{element[4]}";
            pathsMatrix[quadrantX, quandrantY] = stringValue;

            foreach (var point in points)
            {
                if (point.getX() == (quadrantX * 50) + 25 && point.getY() == (quandrantY * 50) + 25)
                {
                    point.EatPoint();
                    break;
                }
            }
        }

        public void changeDirection(string newDirection)
        {
            this.moveDirection = newDirection;
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

        private void UpdateLeftSores(int pacMandScores)
        {
            game.UpdateLeftScore(leftScore - pacMandScores);
            if (leftScore - pacMandScores == 0)
            {
                PauseGame();
                game.Win();
            }
        }

    }
}
