namespace FarthorlPacMan
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Ghost : Participant
    {
        private const int PacManDistanceX = 3;
        private const int PacManDistanceY = 0;
        private Random random = new Random();
        private int randomIndex;
        private int positionPacManQaundarntX;
        private int positionPacManQaundarntY;
        private Dictionary<string, bool> existDirections = new Dictionary<string, bool>();
        private static Image image = Image.FromFile(@"DataFiles\Images\Ghost.bmp");
        private Graphics graphicsGhost;
        private Graphics graphicsFruit;
        public Ghost(int positionPacManQaundarntX, int positionPacManQaundarntY, Graphics graphicsGhost) : base()
        {
            this.positionPacManQaundarntX = positionPacManQaundarntX;
            this.positionPacManQaundarntY = positionPacManQaundarntY;
            this.graphicsGhost = graphicsGhost;
            this.InicializeGhost();
        }

        public Ghost(Graphics graphicsGhost, int positionQuadrantX, int positionQuadrantY) : base(positionQuadrantX, positionQuadrantY)
        {
            this.graphicsGhost = graphicsGhost;
        }

        private Dictionary<string, int> CreateCoordinatesXy(int pacManX, int pacManY)
        {
            List<int[]> ghostMatrix = new List<int[]>();
            Dictionary<string, int> resultXy = new Dictionary<string, int>();
            System.Threading.Thread.Sleep(this.random.Next(100, 500));

            //Check and remove quandrant if the pacMan is near from 6 quadrants left or right by X
            for (int y = 0; y < Engine.YMax - 1; y++)
            {
                for (int x = 0; x < Engine.XMax - 1; x++)
                {
                    if (x <= pacManX - PacManDistanceX && y <= pacManY - PacManDistanceY || x >= pacManX + PacManDistanceX && y >= pacManY + PacManDistanceY)
                    {
                        var elements = Engine.GetQuadrantElements(x, y);

                        if (elements[0] == "0")
                        {
                            int[] coordinates = new int[2];
                            coordinates[0] = x;
                            coordinates[1] = y;
                            ghostMatrix.Add(coordinates);
                        }
                    }
                }
            }
            int quadrantIndex = this.random.Next(0, ghostMatrix.Count - 1);

            if (Engine.IsExistGhost(ghostMatrix[quadrantIndex][0], ghostMatrix[quadrantIndex][1]))
            {
                this.CreateCoordinatesXy(pacManX, pacManY);
            }
            resultXy.Add("QuadrantX", ghostMatrix[quadrantIndex][0]);
            resultXy.Add("QuadrantY", ghostMatrix[quadrantIndex][1]);

            return resultXy;
        }

        private void InicializeGhost()
        {
            var getCoordinates = this.CreateCoordinatesXy(this.positionPacManQaundarntX, this.positionPacManQaundarntY);
            this.PositionQuadrantX = getCoordinates["QuadrantX"];
            this.PositionQuadrantY = getCoordinates["QuadrantY"];

            this.DrawingCoordinatesX = this.PositionQuadrantX * 50 + 8;
            this.DrawingCoordinatesY = this.PositionQuadrantY * 50 + 4;

            this.existDirections.Add("Up", false);
            this.existDirections.Add("Down", false);
            this.existDirections.Add("Left", false);
            this.existDirections.Add("Right", false);
            this.CheckExistDirections();
            this.SelectRandomDirection();
        }

        private void CheckExistDirections()
        {
            if (this.PositionQuadrantY > 0 && Engine.GetQuadrantElements(this.PositionQuadrantX, this.PositionQuadrantY - 1)[0] == "0")
            {
                this.existDirections["Up"] = true;
            }
            else
            {
                this.existDirections["Up"] = false;
            }

            if (this.PositionQuadrantY < Engine.YMax - 1 && Engine.GetQuadrantElements(this.PositionQuadrantX, this.PositionQuadrantY + 1)[0] == "0")
            {
                this.existDirections["Down"] = true;
            }
            else
            {
                this.existDirections["Down"] = false;
            }

            if (this.PositionQuadrantX > 0 && Engine.GetQuadrantElements(this.PositionQuadrantX - 1, this.PositionQuadrantY)[0] == "0")
            {
                this.existDirections["Left"] = true;
            }
            else
            {
                this.existDirections["Left"] = false;
            }

            if (this.PositionQuadrantX < Engine.XMax - 1 && Engine.GetQuadrantElements(this.PositionQuadrantX + 1, this.PositionQuadrantY)[0] == "0")
            {
                this.existDirections["Right"] = true;
            }
            else
            {
                this.existDirections["Right"] = false;
            }

            SelectRandomRoad(MovedDirection);
        }

        private void SelectRandomDirection()
        {

            List<string> existRoads = new List<string>();
            foreach (var exist in this.existDirections)
            {
                if (exist.Value == true)
                {
                    existRoads.Add(exist.Key);
                }
            }

            var index = this.random.Next(0, existRoads.Count - 1);
            this.MovedDirection = existRoads[index];
            this.PreviousDirection = this.MovedDirection;

        }

        private void SelectRandomRoad(string direction)
        {
            List<string> existRoads = new List<string>();
            string logString="";
            string notEqualDirection = "";

            if (direction == "Up")
            {
                notEqualDirection = "Down";
            }
            else if (direction == "Down")
            {
                notEqualDirection = "Up";
            }
            else if (direction == "Right")
            {
                notEqualDirection = "Left";
            }
            else if (direction == "Left")
            {
                notEqualDirection = "Right";
            }


            foreach (var exist in this.existDirections)
            {
                if (exist.Key != notEqualDirection && exist.Value == true)
                {
                    existRoads.Add(exist.Key);
                    logString += exist.Key + " / ";
                }
            }

            if (existRoads.Count > 0)
            {

                if (this.randomIndex>existRoads.Count-1)
                {
                    randomIndex = existRoads.Count - 1;
                }

                //Log.LogText(logString + " - select: " + existRoads[randomIndex]);
                MovedDirection = existRoads[randomIndex];

            }
            else
            {
                MovedDirection = notEqualDirection;
            }
        }

        private void MoveNext()
        {
            this.CheckExistDirections();
            if (!string.IsNullOrEmpty(MovedDirection))
            {
                if (this.MovedDirection == "Right")
                {
                    this.TryMoveRight();
                }
                else if (this.MovedDirection == "Left")
                {
                    this.TryMoveLeft();
                }
                else if (this.MovedDirection == "Up")
                {
                    this.TryMoveUp();
                }
                else if (this.MovedDirection == "Down")
                {
                    this.TryMoveDown();
                }
            }
        }

        private void TryMoveRight()
        {
            if (this.PositionQuadrantX < Engine.YMax - 1)
            {
                int nextQuandrantX = this.PositionQuadrantX + 1;
                int nextQuadrantY = this.PositionQuadrantY;
                string[] elements = Engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    this.PreviousDirection = "Right";
                    this.MovedDirection = "Right";
                    //await moveGhost(nextQuandrantX, nextQuadrantY, "Right");
                }
                else
                {
                    this.GetNewDirectionRight();
                }
            }
            else
            {
                this.GetNewDirectionRight();
            }
        }

        private void GetNewDirectionRight()
        {
            this.CheckExistDirections();
            if (!string.IsNullOrEmpty(MovedDirection))
            {
                if (MovedDirection == "Left")
                {
                    this.TryMoveLeft();
                }
                else if (MovedDirection == "Up")
                {
                    this.TryMoveUp();
                }
                else if (MovedDirection == "Down")
                {
                    this.TryMoveDown();
                }
            }
        }

        private void TryMoveLeft()
        {
            if (this.PositionQuadrantX > 0)
            {
                int nextQuandrantX = this.PositionQuadrantX - 1;
                int nextQuadrantY = this.PositionQuadrantY;
                string[] elements = Engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    this.PreviousDirection = "Left";
                    this.MovedDirection = "Left";
                }
                else
                {
                    this.GetNewDirectionLeft();
                }
            }
            else
            {
                this.GetNewDirectionLeft();
            }
        }

        private void GetNewDirectionLeft()
        {
            this.CheckExistDirections();
            if (!string.IsNullOrEmpty(MovedDirection))
            {
                if (MovedDirection == "Right")
                {
                    this.TryMoveRight();
                }
                else if (MovedDirection == "Up")
                {
                    this.TryMoveUp();
                }
                else if (MovedDirection == "Down")
                {
                    this.TryMoveDown();
                }
            }
        }

        private void TryMoveUp()
        {
            if (this.PositionQuadrantY > 0)
            {
                int nextQuandrantX = this.PositionQuadrantX;
                int nextQuadrantY = this.PositionQuadrantY - 1;
                string[] elements = Engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    this.PreviousDirection = "Up";
                    this.MovedDirection = "Up";
                }
                else
                {
                    this.GetNewDirectionUp();
                }
            }
            else
            {
                this.GetNewDirectionUp();
            }
        }

        private void GetNewDirectionUp()
        {
            this.CheckExistDirections();
            if (!string.IsNullOrEmpty(MovedDirection))
            {
                if (MovedDirection == "Right")
                {
                    this.TryMoveRight();
                }
                else if (MovedDirection == "Left")
                {
                    this.TryMoveLeft();
                }
                else if (MovedDirection == "Down")
                {
                    this.TryMoveDown();
                }
            }
        }

        private void TryMoveDown()
        {
            if (this.PositionQuadrantY < Engine.YMax - 1)
            {
                int nextQuandrantX = this.PositionQuadrantX;
                int nextQuadrantY = this.PositionQuadrantY + 1;
                string[] elements = Engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    this.PreviousDirection = "Down";
                    this.MovedDirection = "Down";
                }
                else
                {
                    this.GetNewDirectionDown();
                }
            }
            else
            {
                this.GetNewDirectionDown();
            }
        }

        private void GetNewDirectionDown()
        {
            this.CheckExistDirections();
            if (!string.IsNullOrEmpty(MovedDirection))
            {
                if (MovedDirection == "Right")
                {
                    this.TryMoveRight();
                }
                else if (MovedDirection == "Left")
                {
                    this.TryMoveLeft();
                }
                else if (MovedDirection == "Up")
                {
                    this.TryMoveUp();
                }
            }
        }

        public void Draw()
        {
            this.graphicsGhost.DrawImage(image, this.PositionQuadrantX * Global.QuadrantSize + 8, this.PositionQuadrantY * Global.QuadrantSize + 4);
        }

        public async Task<bool>   Move()
        {
            randomIndex = random.Next(0, 3);
            string moving = this.MovedDirection;

            switch (moving)
            {
                case "Right":

                    if (this.DrawingCoordinatesX == 0)
                    {
                        this.MoveNext();
                    }

                    this.DrawingCoordinatesX += 1;
                    this.graphicsGhost.DrawLine(new Pen(Color.Black), base.DrawingCoordinatesX - 1, base.PositionQuadrantY * Global.QuadrantSize + 1, base.DrawingCoordinatesX - 1, this.PositionQuadrantY * Global.QuadrantSize + 49);
                    if (base.DrawingCoordinatesX > base.PositionQuadrantX * Global.QuadrantSize + 20)
                    {
                        Engine.DrawPoint(base.PositionQuadrantX, base.PositionQuadrantY);
                        Engine.DrawFruit(base.PositionQuadrantX, base.PositionQuadrantY);
                    }

                    this.graphicsGhost.DrawImage(image, base.DrawingCoordinatesX, base.PositionQuadrantY * Global.QuadrantSize + 4);

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);

                    if (this.DrawingCoordinatesX == (this.PositionQuadrantX + 1) * Global.QuadrantSize + 8)
                    {
                        this.PositionQuadrantX += 1;
                        this.MoveNext();
                    }
                    break;

                case "Left":
                    if (this.DrawingCoordinatesX == 0)
                    {
                        this.MoveNext();
                    }
                    this.DrawingCoordinatesX -= 1;
                    this.graphicsGhost.DrawLine(new Pen(Color.Black), this.DrawingCoordinatesX + image.Width + 1,
                        this.PositionQuadrantY * Global.QuadrantSize + 1,
                        this.DrawingCoordinatesX + image.Width + 1,
                        this.PositionQuadrantY * Global.QuadrantSize + 49);

                    if (this.DrawingCoordinatesX < this.PositionQuadrantX * Global.QuadrantSize + 20)
                    {
                        Engine.DrawPoint(this.PositionQuadrantX, this.PositionQuadrantY);
                        Engine.DrawFruit(base.PositionQuadrantX, base.PositionQuadrantY);
                    }

                    this.graphicsGhost.DrawImage(image, this.DrawingCoordinatesX, this.PositionQuadrantY * Global.QuadrantSize + 4);

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);

                    if (this.DrawingCoordinatesX == (this.PositionQuadrantX - 1) * Global.QuadrantSize + 8)
                    {
                        this.PositionQuadrantX -= 1;
                        this.MoveNext();
                    }
                    break;

                case "Up":
                    if (this.DrawingCoordinatesY == 0)
                    {
                        this.MoveNext();
                    }

                    this.DrawingCoordinatesY -= 1;

                    this.graphicsGhost.DrawLine(new Pen(Color.Black), this.PositionQuadrantX * Global.QuadrantSize + 1, this.DrawingCoordinatesY + 4 + image.Height + 1, this.PositionQuadrantX * Global.QuadrantSize + 49, this.DrawingCoordinatesY + 4 + image.Height + 1);
                    if (this.DrawingCoordinatesY < this.PositionQuadrantY * Global.QuadrantSize - 11)
                    {
                        Engine.DrawPoint(this.PositionQuadrantX, this.PositionQuadrantY);
                        Engine.DrawFruit(base.PositionQuadrantX, base.PositionQuadrantY);
                    }

                    this.graphicsGhost.DrawImage(image, this.PositionQuadrantX * Global.QuadrantSize + 8, this.DrawingCoordinatesY + 4);
                    System.Threading.Thread.Sleep(Global.DrawingSpeed);

                    if (this.DrawingCoordinatesY == (this.PositionQuadrantY - 1) * Global.QuadrantSize + 4)
                    {
                        this.PositionQuadrantY -= 1;
                        this.MoveNext();
                    }
                    break;
                case "Down":
                    if (this.DrawingCoordinatesY == 0)
                    {
                        this.MoveNext();
                    }

                    this.DrawingCoordinatesY += 1;
                    this.graphicsGhost.DrawLine(new Pen(Color.Black), this.PositionQuadrantX * Global.QuadrantSize + 1, this.DrawingCoordinatesY + 4 - 1, this.PositionQuadrantX * Global.QuadrantSize + 49, this.DrawingCoordinatesY + 4 - 1);
                    if (this.DrawingCoordinatesY > this.PositionQuadrantY * Global.QuadrantSize + 20)
                    {
                        Engine.DrawPoint(this.PositionQuadrantX, this.PositionQuadrantY);
                        Engine.DrawFruit(base.PositionQuadrantX, base.PositionQuadrantY);
                    }

                    this.graphicsGhost.DrawImage(image, this.PositionQuadrantX * Global.QuadrantSize + 8, this.DrawingCoordinatesY + 4);
                    System.Threading.Thread.Sleep(Global.DrawingSpeed);

                    if (this.DrawingCoordinatesY == (this.PositionQuadrantY + 1) * Global.QuadrantSize + 4)
                    {

                        this.PositionQuadrantY += 1;
                        this.MoveNext();
                    }
                    break;
            }
            return true;
        }

        public string GetDirection()
        {
            return this.MovedDirection;
        }
    }
}