using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using FarthorlPacMan.Fruits;

namespace FarthorlPacMan
{
    public class Engine : IDisposable
    {
        public Engine(Graphics graphicPacMan, Graphics graphicsGhost, Graphics pointsGraphics, Graphics graphicsFruit, GameWindow game, string level)
        {
            this.GraphicsPacMan = graphicPacMan;
            this.GraphicsGhost = graphicsGhost;
            GraphicsFruit = graphicsFruit;
            PointsGraphics = pointsGraphics;
            Game = game;
            Buffer = new Bitmap(1200, 650);
            PathsMatrix = new string[24, 13];
            Run = true;
            this.IsInicialize = false;
            Points = new List<Point>();
            Ghosts = new List<Ghost>();
            this.Level = level;
        }

        public Graphics GraphicsPacMan { get; private set; }
        public Graphics GraphicsGhost { get; private set; }
        public static Graphics PointsGraphics { get; private set; }
        public static Graphics GraphicsFruit { get; private set; }
        public static Bitmap Buffer { get; private set; }
        public Task TaskRenderingPacMan { get; private set; }
        public Task TaskRenderingGhost { get; private set; }
        public static string[,] PathsMatrix { get; private set; }
        public static int LeftScore { get; private set; }
        public static int PacManEatScores { get; private set; }
        public static bool Run { get; set; }
        public static string MoveDirection { get; set; }
        public static GameWindow Game { get; private set; }
        public bool IsInicialize { get; private set; }
        private PacMan PacMan { get; set; }
        public static List<Point> Points { get; private set; }
        private static List<Ghost> Ghosts { get; set; }
        private string Level { get; set; }
        static List<Fruit> fruits = new List<Fruit>();
        public void Initialize()
        {
            //Initialize game if started for the first time
            if (this.IsInicialize == false)
            {
                this.IsInicialize = true;
                this.InitializeMatrix();
                this.InicializeFruits();
                this.DrawContent();
                this.InicializeLeftScores();

                this.TaskRenderingPacMan = new Task(this.RenderPacMan);
                this.TaskRenderingGhost = new Task(this.RenderGhost);
                this.PacMan = new PacMan(0, 0, this.GraphicsPacMan);
                
                for (int i = 0; i < Global.GhostElements; i++)
                {
                    Ghosts.Add(new Ghost(PacMan.PositionQuadrantX, PacMan.PositionQuadrantY, this.GraphicsGhost));
                }

                this.TaskRenderingPacMan.Start();
                this.TaskRenderingGhost.Start();

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
                drawing.DrawRectangle(new Pen(Global.WallColor), 0, 0, Global.XMax * 50, Global.YMax * 50);
                Game.pacMan.BackgroundImage = Buffer;
            }

            for (int y = 0; y < Global.YMax; y++)
            {
                for (int x = 0; x < Global.XMax; x++)
                {
                    var elements = PathsMatrix[x, y].Trim().Split(',');
                    int quadrant = int.Parse(elements[0]);
                    int pointIndex = int.Parse(elements[1]);

                    if (quadrant == 1)
                    {
                        using (Graphics drawing = Graphics.FromImage(Buffer))
                        {

                            drawing.DrawRectangle(new Pen(Global.WallColor), x * 50, y * 50, 50, 50);
                            drawing.FillRectangle(new SolidBrush(Global.WallColor), x * 50, y * 50, 50, 50);
                            Game.pacMan.BackgroundImage = Buffer;
                        }
                    }

                    if (pointIndex == 1)
                    {

                        Points.Add(new Point(x * 50 + 25, y * 50 + 25));

                        using (Graphics drawing = Graphics.FromImage(Buffer))
                        {
                            drawing.FillEllipse(new SolidBrush(Point.PointFillColor), x * 50 + 25 - Point.PointDiameter / 2, y * 50 + 25 - Point.PointDiameter / 2, Point.PointDiameter, Point.PointDiameter);
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

        private void InicializeFruits()
        {
            Fruit apple = new Apple(2, 3, GraphicsFruit);
            Fruit banana = new Banana(6, 8, GraphicsFruit);
            Fruit brezel = new Brezel(15, 6, GraphicsFruit);
            Fruit cherry = new Cherry(19, 2, GraphicsFruit);
            Fruit peach = new Peach(13, 4, GraphicsFruit);
            Fruit pear = new Pear(11, 12, GraphicsFruit);
            Fruit strawberry = new Strawberry(4, 10, GraphicsFruit);

            fruits = new List<Fruit> { apple, banana, brezel, cherry, peach, pear, strawberry };

            foreach (var fruit in fruits)
            {
                string[] placeAvailable = AvailableFruitXY().Split();
                int placeFruitX = int.Parse(placeAvailable[0]);
                int placeFruitY = int.Parse(placeAvailable[1]);
                fruit.FruitPositionX = placeFruitX;
                fruit.FruitPositionY = placeFruitY;
            }

            fruits.RemoveAll(x => x.FruitPositionX >= Global.XMax || x.FruitPositionY >= Global.YMax);
        }

        private string AvailableFruitXY()
        {
            while (true)
            {
                int tryX = new Random(DateTime.Now.Millisecond).Next(1, 24);
                int tryY = new Random(DateTime.Now.Millisecond).Next(1, 13);
                var elements = PathsMatrix[tryX, tryY].Trim().Split(',');
                int placeAvailable = int.Parse(elements[1]);
                if (placeAvailable == 1)
                {
                    PathsMatrix[tryX, tryY] = "0,0";
                    return $"{tryX} {tryY}";
                }
            }
        }

        private void DrawFruits()
        {
            Fruit fruitToDelete = null;
            foreach (var fruit in fruits)
            {
                using (Graphics drawing = Graphics.FromImage(Buffer))
                {
                    drawing.DrawImage(fruit.Image, fruit.FruitPositionX * Global.QuadrantSize + (Global.QuadrantSize / 2 - fruit.fruitWidth / 2), fruit.FruitPositionY * Global.QuadrantSize + (Global.QuadrantSize / 2 - fruit.fruitHeigt / 2), fruit.fruitWidth, fruit.fruitHeigt);
                    Game.pacMan.BackgroundImage = Buffer;
                }

            }
        }

        private async void RenderGhost()
        {
            while (Run)
            {
                foreach (var ghost in Ghosts)
                {
                    if (PacMan.DrawingCoordinatesX > ghost.DrawingCoordinatesX && 
                        PacMan.DrawingCoordinatesX< ghost.DrawingCoordinatesX+42 && 
                        PacMan.DrawingCoordinatesY > ghost.DrawingCoordinatesY && 
                        PacMan.DrawingCoordinatesY< ghost.DrawingCoordinatesY+42)
                    {
                        GameOver();
                    }
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
            this.DrawFruits();
        }

        public void GameOver()
        {
            Run = false;
            SoundPlayer.PlaySync("death");
            Environment.Exit(1);
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
            this.TaskRenderingPacMan = new Task(this.RenderPacMan);
            this.TaskRenderingGhost = new Task(this.RenderGhost);
            this.TaskRenderingPacMan.Start();
            this.TaskRenderingGhost.Start();
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
            var point = Points.FirstOrDefault(p => p.CenterX == quadrantX * 50 + 25 && p.CenterY == quandrantY * 50 + 25);
            if (point != null)
            {
                point.EatPoint();
                PacManEatScores += 1;
            }

            using (Graphics drawing = Graphics.FromImage(Buffer))
            {
                drawing.FillEllipse(new SolidBrush(Color.Black), quadrantX * 50 + 25 - Point.PointDiameter / 2, quandrantY * 50 + 25 - Point.PointDiameter / 2, Point.PointDiameter, Point.PointDiameter);
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
                PointsGraphics.FillEllipse(new SolidBrush(Point.PointFillColor), quadrantX * 50 + 25 - Point.PointDiameter / 2, quandrantY * 50 + 25 - Point.PointDiameter / 2, Point.PointDiameter, Point.PointDiameter);
            }
        }

        public static void DrawFruit(int quadrantX, int quadrantY)
        {

            var fruit = fruits.FirstOrDefault(f => f.FruitPositionX == quadrantX && f.FruitPositionY == quadrantY);
            if (fruit!=null)
            {
                GraphicsFruit.DrawImage(fruit.Image, fruit.FruitPositionX * Global.QuadrantSize + (Global.QuadrantSize / 2 - fruit.fruitWidth / 2), fruit.FruitPositionY * Global.QuadrantSize + (Global.QuadrantSize / 2 - fruit.fruitHeigt / 2), fruit.fruitWidth, fruit.fruitHeigt);
            }
        }

        public static bool IsDirectionChanged(string myDirection)
        {

            if (myDirection == "Up" && MoveDirection == "Down" || myDirection == "Down" && MoveDirection == "Up"
               || myDirection == "Right" && MoveDirection == "Left" || myDirection == "Left" && MoveDirection == "Right")
            {
                return true;
            }

            return false;
        }

        public static bool IsExistGhost(int quadrantX, int quadrantY)
        {
            return Convert.ToBoolean(Ghosts.FirstOrDefault(g => g.PositionQuadrantX == quadrantX && g.PositionQuadrantY == quadrantY));
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
                this.TaskRenderingPacMan.Wait();
                this.TaskRenderingPacMan?.Dispose();

                this.TaskRenderingGhost.Wait();
                this.TaskRenderingGhost?.Dispose();

                Buffer?.Dispose();
            }
        }
    }
}