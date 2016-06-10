namespace FarthorlPacMan
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Media;
    public class Engine
    {
        private Graphics graphics;
        private Bitmap buffer = new Bitmap(1200, 650);
        private Thread threadRenderingPacMan;
        private Thread threadRenderingGhost;
        private Thread threadRenderingSound;
        private string[,] pathsMatrix = new string[24, 13];
        private int xMax = 24; // columns
        private int yMax = 13; // rows
        private int leftScore = 0;
        private int ghostElements = 4;
        private bool run = true;
        private string moveDirection;
        private Color wallColor = Color.Cyan;
        private readonly GameWindow game;
        private bool isInicialize = false;
        private PacMan pacMan;
        private List<Point> points = new List<Point>();
        private List<Ghost> ghosts = new List<Ghost>();
        public Engine(Graphics graphic, GameWindow game)
        {
            this.graphics = graphic;
            this.game = game;
        }

        public void DrawContent()
        {
            DrawFontColor();
            DrawPaths();

        }

        public void Initialize()
        {
            //Inicialize game if started for the first time
            if (isInicialize == false)
            {
                this.isInicialize = true;
                threadRenderingPacMan = new Thread(new ThreadStart(RenderPacMan));
                threadRenderingGhost = new Thread(new ThreadStart(RenderGhost));
                threadRenderingSound = new Thread(new ThreadStart(PlaySound));
                this.initializeMatrix();
                pacMan = new PacMan(0, 0, this.graphics, this);
                for (int i = 0; i < ghostElements; i++)
                {
                    Ghost creatGhost = new Ghost(pacMan.getQuadrantX(), pacMan.getQuadrantY(), this.graphics, this);
                    ghosts.Add(creatGhost);
                }
                this.DrawContent();
                this.inicializeLeftScores();
                threadRenderingPacMan.Start();
                threadRenderingGhost.Start();
                threadRenderingSound.Start();
                Control.CheckForIllegalCrossThreadCalls = false;
            }
            else
            {
                this.DrawContent();
            }
        }

        public void StopGame()
        {

            threadRenderingPacMan.Abort();
			threadRenderingGhost.Abort();
			threadRenderingSound.Abort();        }

        public void PauseGame()
        {
            this.run = false;
            threadRenderingPacMan.Suspend();
			threadRenderingGhost.Suspend();
			threadRenderingSound.Suspend();
        }


        public void ResumeGame()
        {
            this.run = true;
            threadRenderingPacMan.Resume();
			threadRenderingGhost.Resume();
			threadRenderingSound.Resume();        }

        public bool IsPaused()
        {
            return !run;
        }

        //Heare is the logic for gaming
        private void RenderPacMan()
        {
            while (run)
            {

                pacMan.DrawPacMan(this.graphics);
                pacMan.move(this.graphics, this, moveDirection);
                game.UpdateScores(pacMan.getScore());
                UpdateLeftSores(pacMan.getScore());
            }
        }

        private void RenderGhost()
        {
            while (run)
            {

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
                pathsMatrix = pathsMatrix;
                throw new FileLoadException();
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

        private void DrawFontColor()
        {
            using (Graphics drawing = Graphics.FromImage(buffer))
            {
                drawing.FillRectangle(new SolidBrush(Color.Black), 0, 0, 1200, 650);
                game.pacMan.BackgroundImage = buffer;
            }


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
                    break;
                }
            }

            using (Graphics drawing = Graphics.FromImage(buffer))
            {
                drawing.FillEllipse(new SolidBrush(Color.Black), (quadrantX * 50) + 25 - (pointDiameter / 2), (quandrantY * 50) + 25 - (pointDiameter / 2), pointDiameter, pointDiameter);
                this.game.pacMan.BackgroundImage = buffer;
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

        private void UpdateLeftSores(int pacManScores)
        {
            game.UpdateLeftScore(leftScore - pacManScores);
            if (leftScore - pacManScores == 0)
            {

                game.Win();
                threadRenderingGhost.Suspend();
                threadRenderingPacMan.Suspend();
            }
        }

        private void PlaySound()
        {
            SoundPlayer intro = new SoundPlayer("DataFiles/Sounds/pacman_beginning.wav");
            intro.Play();
        }

    }
}
