using System;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarthorlPacMan
{
    public class Ghost : Participant
    {
        private const int PacManDistanceX = 3;
        private const int PacManDistanceY = 0;
        private Random random = new Random();
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

        private Dictionary<string, int> createCoordinatesXY(int pacManX, int pacManY)
        {
            List<int[]> ghostMatrix = new List<int[]>();
            Dictionary<string, int> resultXY = new Dictionary<string, int>();
            System.Threading.Thread.Sleep(random.Next(100, 500));

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
            int quadrantIndex = random.Next(0, ghostMatrix.Count - 1);

            if (Engine.isExistGhost(ghostMatrix[quadrantIndex][0], ghostMatrix[quadrantIndex][1]))
            {
                createCoordinatesXY(pacManX, pacManY);
            }
            resultXY.Add("QuadrantX", ghostMatrix[quadrantIndex][0]);
            resultXY.Add("QuadrantY", ghostMatrix[quadrantIndex][1]);

            return resultXY;
        }

        private void InicializeGhost()
        {
            var getCoordinates = this.createCoordinatesXY(positionPacManQaundarntX, positionPacManQaundarntY);
            base.PositionQuadrantX = getCoordinates["QuadrantX"];
            base.PositionQuadrantY = getCoordinates["QuadrantY"];

            base.DrawingCoordinatesX = base.PositionQuadrantX * 50 + 8;
            base.DrawingCoordinatesY = base.PositionQuadrantY * 50 + 4;

            existDirections.Add("Up", false);
            existDirections.Add("Down", false);
            existDirections.Add("Left", false);
            existDirections.Add("Right", false);
            CheckExistDirections();
            SelectRandomDirection();
        }

        private void CheckExistDirections()
        {
            if (base.PositionQuadrantY > 0 && Engine.GetQuadrantElements(base.PositionQuadrantX, base.PositionQuadrantY - 1)[0] == "0")
            {
                existDirections["Up"] = true;
            }
            else
            {
                existDirections["Up"] = false;
            }

            if (base.PositionQuadrantY < Engine.YMax - 1 && Engine.GetQuadrantElements(base.PositionQuadrantX, base.PositionQuadrantY + 1)[0] == "0")
            {
                existDirections["Down"] = true;
            }
            else
            {
                existDirections["Down"] = false;
            }

            if (base.PositionQuadrantX > 0 && Engine.GetQuadrantElements(base.PositionQuadrantX - 1, base.PositionQuadrantY)[0] == "0")
            {
                existDirections["Left"] = true;
            }
            else
            {
                existDirections["Left"] = false;
            }

            if (base.PositionQuadrantX < Engine.XMax - 1 && Engine.GetQuadrantElements(base.PositionQuadrantX + 1, base.PositionQuadrantY)[0] == "0")
            {
                existDirections["Right"] = true;
            }
            else
            {
                existDirections["Right"] = false;
            }
        }

        private void SelectRandomDirection()
        {

            List<string> existRoads = new List<string>();
            foreach (var exist in existDirections)
            {
                if (exist.Value == true)
                {
                    existRoads.Add(exist.Key);
                }
            }

            var index = random.Next(0, existRoads.Count - 1);
            base.MovedDirection = existRoads[index];
            base.PreviousDirection = base.MovedDirection;

        }

        private string SelectRandomRoad(string existDirection)
        {
            List<string> existRoads = new List<string>();
            string notEqualDirection = "";

            if (existDirection == "Up")
            {
                notEqualDirection = "Down";
            }
            else if (existDirection == "Down")
            {
                notEqualDirection = "Up";
            }
            else if (existDirection == "Right")
            {
                notEqualDirection = "Left";
            }
            else if (existDirection == "Left")
            {
                notEqualDirection = "Right";
            }


            foreach (var exist in existDirections)
            {
                if (exist.Key != notEqualDirection && exist.Value == true)
                {
                    existRoads.Add(exist.Key);
                }
            }

            if (existRoads.Count > 0)
            {
                int randomIndex = random.Next(0, existRoads.Count - 1);

                return existRoads[randomIndex];
            }
            return notEqualDirection;
        }

        private void MoveNext()
        {
            CheckExistDirections();
            var newDirection = SelectRandomRoad(base.MovedDirection);

            if (!string.IsNullOrEmpty(newDirection))
            {
                if (base.MovedDirection == "Right")
                {
                    tryMoveRight();
                }
                else if (base.MovedDirection == "Left")
                {
                    tryMoveLeft();
                }
                else if (base.MovedDirection == "Up")
                {
                    tryMoveUp();
                }
                else if (base.MovedDirection == "Down")
                {
                    tryMoveDown();
                }
            }
        }

        private void tryMoveRight()
        {
            if (base.PositionQuadrantX < Engine.YMax - 1)
            {
                int nextQuandrantX = base.PositionQuadrantX + 1;
                int nextQuadrantY = base.PositionQuadrantY;
                string[] elements = Engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    base.PreviousDirection = "Right";
                    base.MovedDirection = "Right";
                    //await moveGhost(nextQuandrantX, nextQuadrantY, "Right");
                }
                else
                {
                    GetNewDirectionRight();
                }

            }
            else
            {
                GetNewDirectionRight();
            }
        }

        private void GetNewDirectionRight()
        {
            CheckExistDirections();
            var newDirection = SelectRandomRoad("Right");
            if (!string.IsNullOrEmpty(newDirection))
            {
                if (newDirection == "Left")
                {
                    tryMoveLeft();
                }
                else if (newDirection == "Up")
                {
                    tryMoveUp();
                }
                else if (newDirection == "Down")
                {
                    tryMoveDown();
                }
            }
        }

        private void tryMoveLeft()
        {
            if (base.PositionQuadrantX > 0)
            {
                int nextQuandrantX = base.PositionQuadrantX - 1;
                int nextQuadrantY = base.PositionQuadrantY;
                string[] elements = Engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    base.PreviousDirection = "Left";
                    base.MovedDirection = "Left";
                }
                else
                {
                    GetNewDirectionLeft();
                }
            }
            else
            {
                GetNewDirectionLeft();
            }
        }

        private void GetNewDirectionLeft()
        {
            CheckExistDirections();
            var newDirection = SelectRandomRoad("Left");
            if (!string.IsNullOrEmpty(newDirection))
            {
                if (newDirection == "Right")
                {
                    tryMoveRight();
                }
                else if (newDirection == "Up")
                {
                    tryMoveUp();
                }
                else if (newDirection == "Down")
                {
                    tryMoveDown();
                }
            }
        }

        private void tryMoveUp()
        {
            if (base.PositionQuadrantY > 0)
            {
                int nextQuandrantX = base.PositionQuadrantX;
                int nextQuadrantY = base.PositionQuadrantY - 1;
                string[] elements = Engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    base.PreviousDirection = "Up";
                    base.MovedDirection = "Up";
                }
                else
                {
                    GetNewDirectionUp();
                }
            }
            else
            {
                GetNewDirectionUp();
            }
        }

        private void GetNewDirectionUp()
        {
            CheckExistDirections();
            var newDirection = SelectRandomRoad("Up");
            if (!string.IsNullOrEmpty(newDirection))
            {
                if (newDirection == "Right")
                {
                    tryMoveRight();
                }
                else if (newDirection == "Left")
                {
                    tryMoveLeft();
                }
                else if (newDirection == "Down")
                {
                    tryMoveDown();
                }
            }
        }

        private void tryMoveDown()
        {
            if (base.PositionQuadrantY < Engine.YMax - 1)
            {
                int nextQuandrantX = base.PositionQuadrantX;
                int nextQuadrantY = base.PositionQuadrantY + 1;
                string[] elements = Engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    base.PreviousDirection = "Down";
                    base.MovedDirection = "Down";
                }
                else
                {
                    GetNewDirectionDown();
                }
            }
            else
            {
                GetNewDirectionDown();
            }
        }

        private void GetNewDirectionDown()
        {
            CheckExistDirections();
            var newDirection = SelectRandomRoad("Down");
            if (!string.IsNullOrEmpty(newDirection))
            {
                if (newDirection == "Right")
                {
                    tryMoveRight();
                }
                else if (newDirection == "Left")
                {
                    tryMoveLeft();
                }
                else if (newDirection == "Up")
                {
                    tryMoveUp();
                }
            }
        }

        public void Draw()
        {
            graphicsGhost.DrawImage(image, (base.PositionQuadrantX * Global.QuadrantSize) + 8, base.PositionQuadrantY * Global.QuadrantSize + 4);
        }

        public async Task<bool> Move()
        {
            string moving = base.MovedDirection;

            switch (moving)
            {
                case "Right":

                    if (base.DrawingCoordinatesX == 0)
                    {
                        this.MoveNext();
                    }

                    base.DrawingCoordinatesX += 1;
                    graphicsGhost.DrawLine(new Pen(Color.Black), base.DrawingCoordinatesX - 1, (base.PositionQuadrantY * Global.QuadrantSize) + 1, base.DrawingCoordinatesX - 1, (base.PositionQuadrantY * Global.QuadrantSize) + 49);
                    if (base.DrawingCoordinatesX > (base.PositionQuadrantX * Global.QuadrantSize) + 20)
                    {
                        Engine.DrawPoint(base.PositionQuadrantX, base.PositionQuadrantY);
                    }

                    graphicsGhost.DrawImage(image, (base.DrawingCoordinatesX), base.PositionQuadrantY * Global.QuadrantSize + 4);

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);

                    if (base.DrawingCoordinatesX == ((base.PositionQuadrantX + 1) * Global.QuadrantSize) + 8)
                    {
                        //DrawBlackPolygon();

                        base.PositionQuadrantX += 1;
                        this.MoveNext();
                    }
                    break;

                case "Left":
                    if (base.DrawingCoordinatesX == 0)
                    {
                        this.MoveNext();
                    }
                    base.DrawingCoordinatesX -= 1;
                    graphicsGhost.DrawLine(new Pen(Color.Black), (base.DrawingCoordinatesX + image.Width) + 1,
                        (base.PositionQuadrantY * Global.QuadrantSize) + 1,
                        (base.DrawingCoordinatesX + image.Width) + 1,
                        (base.PositionQuadrantY * Global.QuadrantSize) + 49);

                    if (base.DrawingCoordinatesX < (base.PositionQuadrantX * Global.QuadrantSize) + 20)
                    {
                        Engine.DrawPoint(base.PositionQuadrantX, base.PositionQuadrantY);
                    }

                    graphicsGhost.DrawImage(image, (base.DrawingCoordinatesX), base.PositionQuadrantY * Global.QuadrantSize + 4);

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);

                    if (base.DrawingCoordinatesX == ((base.PositionQuadrantX - 1) * Global.QuadrantSize) + 8)
                    {
                        //DrawBlackPolygon();

                        base.PositionQuadrantX -= 1;
                        this.MoveNext();
                    }
                    break;

                case "Up":
                    if (base.DrawingCoordinatesY == 0)
                    {
                        this.MoveNext();
                    }

                    base.DrawingCoordinatesY -= 1;

                    graphicsGhost.DrawLine(new Pen(Color.Black), (base.PositionQuadrantX * Global.QuadrantSize) + 1, (base.DrawingCoordinatesY + 4 + image.Height) + 1, (base.PositionQuadrantX * Global.QuadrantSize) + 49, (base.DrawingCoordinatesY + 4 + image.Height) + 1);
                    if (base.DrawingCoordinatesY < (base.PositionQuadrantY * Global.QuadrantSize) - 11)
                    {
                        Engine.DrawPoint(base.PositionQuadrantX, base.PositionQuadrantY);
                    }

                    graphicsGhost.DrawImage(image, (base.PositionQuadrantX * Global.QuadrantSize) + 8, base.DrawingCoordinatesY + 4);
                    System.Threading.Thread.Sleep(Global.DrawingSpeed);

                    if (base.DrawingCoordinatesY == ((base.PositionQuadrantY - 1) * Global.QuadrantSize) + 4)
                    {
                        //DrawBlackPolygon();

                        base.PositionQuadrantY -= 1;
                        this.MoveNext();
                    }
                    break;
                case "Down":
                    if (base.DrawingCoordinatesY == 0)
                    {
                        this.MoveNext();
                    }

                    base.DrawingCoordinatesY += 1;
                    graphicsGhost.DrawLine(new Pen(Color.Black), (base.PositionQuadrantX * Global.QuadrantSize) + 1, (base.DrawingCoordinatesY + 4) - 1, (base.PositionQuadrantX * Global.QuadrantSize) + 49, (base.DrawingCoordinatesY + 4) - 1);
                    if (base.DrawingCoordinatesY > (base.PositionQuadrantY * Global.QuadrantSize) + 20)
                    {
                        Engine.DrawPoint(base.PositionQuadrantX, base.PositionQuadrantY);
                    }

                    graphicsGhost.DrawImage(image, (base.PositionQuadrantX * Global.QuadrantSize) + 8, base.DrawingCoordinatesY + 4);
                    System.Threading.Thread.Sleep(Global.DrawingSpeed);

                    if (base.DrawingCoordinatesY == ((base.PositionQuadrantY + 1) * Global.QuadrantSize) + 4)
                    {
                        //DrawBlackPolygon();

                        base.PositionQuadrantY += 1;
                        this.MoveNext();
                    }
                    break;
            }
            return true;
        }

        private void DrawBlackPolygon()
        {
            graphicsGhost.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[]
            {
                new System.Drawing.Point(base.DrawingCoordinatesX, base.DrawingCoordinatesY),
                new System.Drawing.Point(base.DrawingCoordinatesX + image.Width, base.DrawingCoordinatesY),
                new System.Drawing.Point(base.DrawingCoordinatesX + image.Width, base.DrawingCoordinatesY + image.Height),
                new System.Drawing.Point(base.DrawingCoordinatesX, base.DrawingCoordinatesY + image.Height),
                new System.Drawing.Point(base.DrawingCoordinatesX, base.DrawingCoordinatesY),
            });
        }

        public int GetQuadrantX()
        {
            return base.PositionQuadrantX;
        }

        public int GetQuadrantY()
        {
            return base.PositionQuadrantY;
        }

        public string getDirection()
        {
            return base.MovedDirection;
        }
    }
}