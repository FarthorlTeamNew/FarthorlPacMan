﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using FarthorlPacMan.States;

namespace FarthorlPacMan
{

    public class Engine : IDisposable
    {
        public Graphics GraphicsPacMan { get; private set; }
        public Graphics GraphicsGhost { get; private set; }
        public static Graphics PointsGraphics { get; private set; }
        public Graphics GraphicsFruit { get; private set; }
        public static Bitmap Buffer { get; private set; }
        public Task TaskRenderingPacMan { get; private set; }
        public Task TaskRenderingGhost { get; private set; }
        public static string[,] PathsMatrix { get; private set; }
        public static int XMax { get; private set; }
        public static int YMax { get; private set; }
        public static int LeftScore { get; private set; }
        public int GhostElements { get; private set; }
        public static int PacManEatScores { get; private set; }
        public static bool Run { get; set; }
        public static string MoveDirection { get; set; }
        public Color WallColor { get; private set; }
        public static GameWindow Game { get; private set; }
        public bool IsInicialize { get; private set; }
        private PacMan PacMan { get; set; }
        public static List<Point> Points { get; private set; }
        private static List<Ghost> Ghosts { get; set; }
        private string level { get; set; }
        public Fruit Fruit { get; set; }

        public Engine(Graphics graphicPacMan, Graphics graphicsGhost, Graphics pointsGraphics, GameWindow game, string level)
        {
            this.GraphicsPacMan = graphicPacMan;
            this.GraphicsGhost = graphicsGhost;
            PointsGraphics = pointsGraphics;
            Game = game;
            Buffer = new Bitmap(1200, 650);
            PathsMatrix = new string[24, 13];
            XMax = 24;
            YMax = 13;
            this.GhostElements = 4;
            Engine.Run = true;
            this.WallColor = Color.Cyan;
            this.IsInicialize = false;
            Points = new List<Point>();
            Ghosts = new List<Ghost>();
            this.level = level;
        }

        public void Initialize()
        {
            //Initialize game if started for the first time
            if (IsInicialize == false)
            {
                this.IsInicialize = true;
                this.initializeMatrix();
                this.DrawContent();
                this.inicializeLeftScores();

                TaskRenderingPacMan = new Task(RenderPacMan);
                TaskRenderingGhost = new Task(RenderGhost);
                PacMan = new PacMan(0, 0, this.GraphicsPacMan);

                for (int i = 0; i < GhostElements; i++)
                {
                    Ghosts.Add(new Ghost(PacMan.GetQuadrantX(), PacMan.GetQuadrantY(), GraphicsGhost));
                }

                TaskRenderingPacMan.Start();
                TaskRenderingGhost.Start();

                Control.CheckForIllegalCrossThreadCalls = false;
            }
            else
            {
                this.DrawContent();
            }
        }

        private void DrawFontColor()
        {
            using (Graphics drawing = Graphics.FromImage(Buffer))
            {
                drawing.FillRectangle(new SolidBrush(Color.Black), 0, 0, 1200, 650);
                Game.pacMan.BackgroundImage = Buffer;
            }
        }

        private void DrawPaths()
        {
            using (Graphics drawing = Graphics.FromImage(Buffer))
            {
                drawing.DrawRectangle(new Pen(WallColor), 0, 0, XMax * 50, YMax * 50);
                Game.pacMan.BackgroundImage = Buffer;
            }

            for (int y = 0; y < YMax; y++)
            {
                for (int x = 0; x < XMax; x++)
                {
                    var elements = PathsMatrix[x, y].Trim().Split(',');
                    int quadrant = int.Parse(elements[0]);
                    int pointIndex = int.Parse(elements[1]);

                    if (quadrant == 1)
                    {
                        using (Graphics drawing = Graphics.FromImage(Buffer))
                        {

                            drawing.DrawRectangle(new Pen(WallColor), (x * 50), (y * 50), 50, 50);
                            drawing.FillRectangle(new SolidBrush(WallColor), (x * 50), (y * 50), 50, 50);
                            Game.pacMan.BackgroundImage = Buffer;

                        }
                    }

                    if (pointIndex == 1)
                    {

                        Points.Add(new Point((x * 50) + 25, (y * 50) + 25));

                        using (Graphics drawing = Graphics.FromImage(Buffer))
                        {
                            drawing.FillEllipse(new SolidBrush(Point.PointFillColor), (x * 50) + 25 - (Point.PointDiameter / 2), (y * 50) + 25 - (Point.PointDiameter / 2), Point.PointDiameter, Point.PointDiameter);
                            Game.pacMan.BackgroundImage = Buffer;
                        }
                    }
                }

            }
        }

        private void initializeMatrix()
        {
            try
            {
                using (var fileMatrix = new StreamReader(this.level))
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
                        PathsMatrix[arrayX, arrayY] = arrayValue;
                    }
                }
            }
            catch (Exception)
            {
                throw new FileLoadException("Level file didn't load!");
            }
        }

        private void inicializeLeftScores()
        {
            Game.UpdateLeftScore(Points.Count(p => p.IsPointCollected == false));
        }

        //Heare is the logic for gaming
        private async void RenderPacMan()
        {
            while (Run)
            {
                PacMan.DrawPacMan();
                await PacMan.Run(MoveDirection);

            }
        }

        private async void RenderGhost()
        {
            while (Run)
            {
                foreach (var ghost in Ghosts)
                {
                    await ghost.Move();
                }
            }
        }


        private static void UpdateLeftSores(int pacManScores)
        {
            Game.UpdateLeftScore(LeftScore - pacManScores);
            if (LeftScore - pacManScores == 0)
            {
                Engine.Run = false;
                Game.panel1.Visible = true;
            }
        }

        public void DrawContent()
        {
            DrawFontColor();
            DrawPaths();
        }

        public void StopGame()
        {
            Engine.Run = false;
        }

        public void PauseGame()
        {
            Engine.Run = false;
            Game.PausePanel.Visible = true;
        }

        public void ResumeGame()
        {
            Game.PausePanel.Visible = false;
            Engine.Run = true;
        }

        public bool IsPaused()
        {
            return !Run;
        }

        public static string[] GetQuadrantElements(int quadrantX, int quandrantY)
        {
            string[] elements = PathsMatrix[quadrantX, quandrantY].Trim().Split(',');
            return elements;
        }

        public static void EatPointAndUpdateMatrix(int quadrantX, int quandrantY, string[] element)
        {
            var stringValue = $"{element[0]},{element[1]}";
            PathsMatrix[quadrantX, quandrantY] = stringValue;
            var point = Points.FirstOrDefault(p => p.CenterX == (quadrantX * 50) + 25 && p.CenterY == (quandrantY * 50) + 25);
            if (point != null)
            {
                point.EatPoint();
                PacManEatScores += 1;
            }

            using (Graphics drawing = Graphics.FromImage(Buffer))
            {
                drawing.FillEllipse(new SolidBrush(Color.Black), (quadrantX * 50) + 25 - (Point.PointDiameter / 2), (quandrantY * 50) + 25 - (Point.PointDiameter / 2), Point.PointDiameter, Point.PointDiameter);
                Game.pacMan.BackgroundImage = Buffer;
            }

            Game.UpdateScores(PacManEatScores);
            UpdateLeftSores(PacManEatScores);
        }

        //TODO Refactoring
        //Remove try section
        public static void DrawPoint(int quadrantX, int quandrantY)
        {
            if (GetQuadrantElements(quadrantX, quandrantY)[1] == "1")
            {
                PointsGraphics.FillEllipse(new SolidBrush(Point.PointFillColor), (quadrantX * 50) + 25 - (Point.PointDiameter / 2), (quandrantY * 50) + 25 - (Point.PointDiameter / 2), Point.PointDiameter, Point.PointDiameter);
            }
        }

        public static bool isDirectionChanged(string myDirection)
        {

            if (myDirection == "Up" && MoveDirection == "Down" || myDirection == "Down" && MoveDirection == "Up")
            {
                return true;
            }

            if (myDirection == "Right" && MoveDirection == "Left" || myDirection == "Left" && MoveDirection == "Right")
            {
                return true;
            }

            return false;
        }

        public static bool isExistGhost(int quadrantX, int quadrantY)
        {
            foreach (var ghost in Ghosts)
            {
                if (ghost.GetQuadrantX() == quadrantX && ghost.GetQuadrantY() == quadrantY)
                {
                    return true;
                }
            }
            return false;
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
                if (TaskRenderingPacMan != null)
                {
                    TaskRenderingPacMan.Dispose();
                }

                if (TaskRenderingGhost != null)
                {
                    TaskRenderingGhost.Dispose();
                }

                if (Buffer != null)
                {
                    Buffer.Dispose();
                }
            }
        }
    }
}