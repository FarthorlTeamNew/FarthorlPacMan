using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using FarthorlPacMan.States;

namespace FarthorlPacMan
{
    public class Engine : IDisposable
    {
        public Graphics Graphics { get; private set; }
        public Graphics GraphicsGhost { get; private set; }
        public Graphics PointsGraphics { get; private set; }
        public Bitmap Buffer { get; private set; }
        public Task TaskRenderingPacMan { get; private set; }
        public Task TaskRenderingGhost { get; private set; }
        public string[,] PathsMatrix { get; private set; }
        public int XMax { get; private set; }
        public int YMax { get; private set; }
        public int LeftScore { get; private set; }
        public int GhostElements { get; private set; }
        public int PacManEatScores { get; private set; }
        public static bool Run { get;  set; }
        public string MoveDirection { get; private set; }
        public Color WallColor { get; private set; }
        public GameWindow Game { get; }
        public bool IsInicialize { get; private set; }
        private PacMan PacMan { get; set; }
        public List<Point> Points { get; private set; }
        public List<Ghost> Ghosts { get; private set; }
        public PlayerSound Player { get; private set; }
        public static string Level { get; set; }

        public Engine()
        {
            this.Buffer = new Bitmap(1200, 650);
            this.PathsMatrix = new string[24, 13];
            this.XMax = 24;
            this.YMax = 13;
            this.GhostElements = 4;
            Engine.Run = true;
            this.WallColor = Color.Cyan;
            this.IsInicialize = false;
            this.Points = new List<Point>();
            this.Ghosts = new List<Ghost>();
            this.Player = new PlayerSound();
            Engine.Level = @"DataFiles\Levels\Labirint.txt";
        }

        public Engine(Graphics graphic, Graphics graphicsGhost, Graphics pointsGraphics, GameWindow game)
            : this()
        {
            this.Graphics = graphic;
            this.GraphicsGhost = graphicsGhost;
            this.PointsGraphics = pointsGraphics;
            this.Game = game;
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
                PacMan = new PacMan(0, 0, this.Graphics, this);

                for (int i = 0; i < GhostElements; i++)
                {
                    Ghosts.Add(new Ghost(PacMan.getQuadrantX(), PacMan.getQuadrantY(), GraphicsGhost, this));
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
                drawing.DrawRectangle(new Pen(WallColor), 0, 0, GetMaxX() * 50, GetMaxY() * 50);
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
                        Point point = new Point((x * 50) + 25, (y * 50) + 25);
                        Points.Add(point);

                        using (Graphics drawing = Graphics.FromImage(Buffer))
                        {
                            drawing.FillEllipse(new SolidBrush(point.PointFillColor), (x * 50) + 25 - (point.PointDiameter / 2), (y * 50) + 25 - (point.PointDiameter / 2), point.PointDiameter, point.PointDiameter);
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
                using (var fileMatrix = new StreamReader(Engine.Level))
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
                        this.PathsMatrix[arrayX, arrayY] = arrayValue;
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
            foreach (var point in Points)
            {
                if (!point.IsPointCollected)
                {
                    LeftScore = LeftScore + 1;
                }
            }
            Game.UpdateLeftScore(LeftScore);
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
                for (int i = 0; i < 50; i++)
                {
                    foreach (var ghost in Ghosts)
                    {
                        if (Engine.Run)
                        {
                            await ghost.Move();
                        }
                    }
                }
            }
        }

        private void UpdateLeftSores(int pacManScores)
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

        public string[] GetQuadrantElements(int quadrantX, int quandrantY)
        {
            string[] elements = PathsMatrix[quadrantX, quandrantY].Trim().Split(',');
            return elements;
        }

        public void EatPointAndUpdateMatrix(int quadrantX, int quandrantY, string[] element)
        {

            var stringValue = $"{element[0]},{element[1]}";
            PathsMatrix[quadrantX, quandrantY] = stringValue;
            int pointDiameter = 0;
            foreach (var point in Points)
            {
                if (point.CenterX == (quadrantX * 50) + 25 && point.CenterY == (quandrantY * 50) + 25)
                {
                    point.EatPoint();
                    pointDiameter = point.PointDiameter;
                    PacManEatScores = PacManEatScores + 1;
                    break;
                }
            }

            using (Graphics drawing = Graphics.FromImage(Buffer))
            {
                drawing.FillEllipse(new SolidBrush(Color.Black), (quadrantX * 50) + 25 - (pointDiameter / 2), (quandrantY * 50) + 25 - (pointDiameter / 2), pointDiameter, pointDiameter);
                this.Game.pacMan.BackgroundImage = Buffer;
            }

            Game.UpdateScores(PacManEatScores);
            UpdateLeftSores(PacManEatScores);
        }

        //TODO Refactoring
        //Remove try section
        public void DrawPoint(int quadrantX, int quandrantY)
        {
            if (GetQuadrantElements(quadrantX, quandrantY)[1] == "1")
            {
                Point point = new Point();
                PointsGraphics.FillEllipse(new SolidBrush(point.PointFillColor), (quadrantX * 50) + 25 - (point.PointDiameter / 2), (quandrantY * 50) + 25 - (point.PointDiameter / 2), point.PointDiameter, point.PointDiameter);
            }
        }

        public void changeDirection(string newDirection)
        {
            this.MoveDirection = newDirection;
        }

        public bool isDirectionChanged(string myDirection)
        {
            if (myDirection == "Up" && this.MoveDirection == "Down" || myDirection == "Down" && this.MoveDirection == "Up")
            {
                return true;
            }

            if (myDirection == "Right" && this.MoveDirection == "Left" || myDirection == "Left" && this.MoveDirection == "Right")
            {
                return true;
            }

            return false;
        }

        public string GetDirection()
        {
            return MoveDirection;
        }

        public bool isExistGhost(int quadrantX, int quadrantY)
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

        public int GetMaxX()
        {
            return this.XMax;
        }

        public int GetMaxY()
        {
            return this.YMax;

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

                if (Player != null)
                {
                    Player.Dispose();
                }

                if (PacMan != null)
                {
                    PacMan.Dispose();
                }
            }
        }
    }
}