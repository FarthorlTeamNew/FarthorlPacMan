using System;
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
        public Task TaskRenderingFruit { get; private set; }
        public Task TaskCheckCollision { get; private set; }
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
        private string Level { get; set; }
        public Fruit Fruit { get; set; }

        public Engine(Graphics graphicPacMan, Graphics graphicsGhost, Graphics pointsGraphics, Graphics graphicsFruit, GameWindow game, string level)
        {
            this.GraphicsPacMan = graphicPacMan;
            this.GraphicsGhost = graphicsGhost;
            this.GraphicsFruit = graphicsFruit;
            PointsGraphics = pointsGraphics;
            Game = game;
            Buffer = new Bitmap(1200, 650);
            PathsMatrix = new string[24, 13];
            XMax = 24;
            YMax = 13;
            this.GhostElements = 4;
            Run = true;
            this.WallColor = Color.Cyan;
            this.IsInicialize = false;
            Points = new List<Point>();
            Ghosts = new List<Ghost>();
            this.Level = level;
        }

        public void Initialize()
        {
            //Initialize game if started for the first time
            if (this.IsInicialize == false)
            {
                this.IsInicialize = true;
                this.InitializeMatrix();
                this.DrawContent();
                this.InicializeLeftScores();

                this.TaskRenderingPacMan = new Task(this.RenderPacMan);
                this.TaskRenderingGhost = new Task(this.RenderGhost);
                this.TaskRenderingFruit = new Task(GenerateFruit);
                this.TaskCheckCollision = new Task(this.CheckForCollision);
                this.PacMan = new PacMan(0, 0, this.GraphicsPacMan);

                for (int i = 0; i < this.GhostElements; i++)
                {
                    Ghosts.Add(new Ghost(this.PacMan.GetPositionX(), this.PacMan.GetPositionY(), this.GraphicsGhost));
                }

                this.TaskRenderingPacMan.Start();
                this.TaskRenderingGhost.Start();
                this.TaskCheckCollision.Start();
                this.TaskRenderingFruit.Start();

                Control.CheckForIllegalCrossThreadCalls = false;
            }
            else
            {
                this.DrawContent();
            }
        }

        private void CheckForCollision()
        {
            while (Run)
            {
                foreach (var ghost in Ghosts)
                {
                    if (this.PacMan.GetPositionX() == ghost.GetQuadrantX()
                        && this.PacMan.GetPositionY() == ghost.GetQuadrantY())
                    {
                        this.PacMan.IsAlive = false;
                        Run = false;
                        SoundPlayer.Play("death");
                    }
                }
            }
        }

        private void GenerateFruit()
        {
            while (Run)
            {
                //Should be implemented by randomly dropping the fruits on coordinates that have points on them, and then the points are removed
                //Fruit apple = new Fruit(2, 3, GraphicsFruit, this);
                //apple.DrawApple();
                //Fruit banana = new Fruit(6, 8, GraphicsFruit, this);
                //banana.DrawBanana();
                //Fruit brezel = new Fruit(15, 6, GraphicsFruit, this);
                //brezel.DrawBrezel();
                //Fruit cherry = new Fruit(19, 2, GraphicsFruit, this);
                //cherry.DrawCherry();
                //Fruit peach = new Fruit(13, 4, GraphicsFruit, this);
                //peach.DrawPeach();
                //Fruit pear = new Fruit(11, 12, GraphicsFruit, this);
                //pear.DrawPear();
                //Fruit strawberry = new Fruit(4, 10, GraphicsFruit, this);
                //strawberry.DrawStrawberry();
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
                drawing.DrawRectangle(new Pen(this.WallColor), 0, 0, XMax * 50, YMax * 50);
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

                            drawing.DrawRectangle(new Pen(this.WallColor), (x * 50), (y * 50), 50, 50);
                            drawing.FillRectangle(new SolidBrush(this.WallColor), (x * 50), (y * 50), 50, 50);
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

        private void InitializeMatrix()
        {
            try
            {
                using (var fileMatrix = new StreamReader(this.Level))
                {
                    string inputLine;
                    while ((inputLine = fileMatrix.ReadLine()) != null)
                    {
                        // Get values from the coordinates.txt example splitLine[0]=1,0 splitLine[1]=1|0|0|1|1
                        var splitLine = inputLine.Trim().Split('=');

                        //Get the position values for the 2D array example arrayXYValues[0]=1 arrayXYValues[0]=0 
                        var arrayXyValues = splitLine[0].Trim().Split(',');
                        int arrayX;
                        int arrayY;

                        //This is the values of the array cell
                        string arrayValue = splitLine[1];
                        try
                        {
                            arrayX = int.Parse(arrayXyValues[0]);
                            arrayY = int.Parse(arrayXyValues[1]);
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

        private void InicializeLeftScores()
        {
            int scores = Points.Count(p => p.IsPointCollected == false);
            LeftScore = scores;
            Game.UpdateLeftScore(scores);
        }

        //Heare is the logic for gaming
        private async void RenderPacMan()
        {
            while (Run)
            {
                this.PacMan.DrawPacMan();
                await this.PacMan.Run(MoveDirection);
            }
        }

        private async void RenderGhost()
        {
            while (Run)
            {
                foreach (var ghost in Ghosts)
                {
                    if (Run)
                    {
                        await ghost.Move();
                    }
                    
                }
            }
        }

        private static void UpdateLeftSores(int pacManScores)
        {
            Game.UpdateLeftScore(LeftScore - pacManScores);
            if (LeftScore - pacManScores == 0)
            {
                Run = false;
                Game.panel1.Visible = true;
            }
        }

        public void DrawContent()
        {
            this.DrawFontColor();
            this.DrawPaths();
        }

        public void StopGame()
        {
            Run = false;
            //Dispose();
        }

        public void PauseGame()
        {
            Run = false;
            Game.PausePanel.Visible = true;
        }

        public void ResumeGame()
        {
            Game.PausePanel.Visible = false;
            Run = true;
            TaskRenderingPacMan = new Task(RenderPacMan);
            TaskRenderingGhost = new Task(RenderGhost);
            TaskRenderingFruit = new Task(GenerateFruit);
            TaskCheckCollision = new Task(CheckForCollision);
            TaskRenderingPacMan.Start();
            TaskRenderingGhost.Start();
            TaskRenderingFruit.Start();
            TaskCheckCollision.Start();
        }

        public bool IsPaused()
        {
            return !Run;
        }

        public static string[] GetQuadrantElements(int quadrantX, int quandrantY)
        {
            return PathsMatrix[quadrantX, quandrantY].Trim().Split(',');
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

        public static bool IsDirectionChanged(string myDirection)
        {

            if ( myDirection == "Up" && MoveDirection == "Down" || myDirection == "Down" && MoveDirection == "Up"
               || myDirection == "Right" && MoveDirection == "Left" || myDirection == "Left" && MoveDirection == "Right")
            {
                return true;
            }

            return false;
        }

        public static bool IsExistGhost(int quadrantX, int quadrantY)
        {
            return Convert.ToBoolean(Ghosts.FirstOrDefault(g => g.GetQuadrantX() == quadrantX && g.GetQuadrantY() == quadrantY));
        }

        public void Dispose()
        {
            this.Dispose(true);
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

                if (TaskRenderingFruit != null)
                {
                    TaskRenderingFruit.Dispose();
                }
                if (TaskCheckCollision != null)
                {
                    TaskCheckCollision.Dispose();
                }

                if (Buffer != null)
                {
                    Buffer.Dispose();
                }
            }
        }
    }
}