using System;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarthorlPacMan
{
    public class Ghost
    {
        private int QuadrantDimension => Global.QuadrantSize;
        private const int PacManDistanceX = 3;
        private const int PacManDistanceY = 0;
        private Random random = new Random();
        private int positionQuadrantX;
        private int positionQuadrantY;
        private int drawingCoordinatesX;
        private int drawingCoordinatesY;
        private int positionPacManQaundarntX;
        private int positionPacManQaundarntY;
        private string movedDirection;
        private string previousDirection;
        private Dictionary<string, bool> existDirections = new Dictionary<string, bool>();
        private static Image image = Image.FromFile(@"DataFiles\Images\Ghost.bmp");
        private Graphics graphicsGhost;
        private Graphics graphicsFruit;

        public Ghost(int positionPacManQaundarntX, int positionPacManQaundarntY, Graphics graphicsGhost)
        {
            this.positionPacManQaundarntX = positionPacManQaundarntX;
            this.positionPacManQaundarntY = positionPacManQaundarntY;
            this.graphicsGhost = graphicsGhost;
            this.InicializeGhost();
        }

        public Ghost(Graphics graphicsGhost,int X, int Y)
        {
            this.graphicsGhost = graphicsGhost;
            positionQuadrantX = X;
            positionQuadrantY = Y;
            
        }

        private Dictionary<string, int> createCoordinatesXY(int pacManX, int pacManY)
        {
            List<int[]> ghostMatrix = new List<int[]>();
            Dictionary<string, int> resultXY = new Dictionary<string, int>();
            System.Threading.Thread.Sleep(random.Next(100, 500));

            //Check and remove quandrant if the pacMan is near from 6 quadrants left or right by X
            for (int y = 0; y < Engine.YMax- 1; y++)
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
            this.positionQuadrantX = getCoordinates["QuadrantX"];
            this.positionQuadrantY = getCoordinates["QuadrantY"];

            this.drawingCoordinatesX = positionQuadrantX * 50 + 8;
            this.drawingCoordinatesY = positionQuadrantY * 50 + 4;

            existDirections.Add("Up", false);
            existDirections.Add("Down", false);
            existDirections.Add("Left", false);
            existDirections.Add("Right", false);
            CheckExistDirections();
            SelectRandomDirection();
        }

        private void CheckExistDirections()
        {
            if (positionQuadrantY > 0 && Engine.GetQuadrantElements(positionQuadrantX, positionQuadrantY - 1)[0] == "0")
            {
                existDirections["Up"] = true;
            }
            else
            {
                existDirections["Up"] = false;
            }

            if (positionQuadrantY < Engine.YMax - 1 && Engine.GetQuadrantElements(positionQuadrantX, positionQuadrantY + 1)[0] == "0")
            {
                existDirections["Down"] = true;
            }
            else
            {
                existDirections["Down"] = false;
            }

            if (positionQuadrantX > 0 && Engine.GetQuadrantElements(positionQuadrantX - 1, positionQuadrantY)[0] == "0")
            {
                existDirections["Left"] = true;
            }
            else
            {
                existDirections["Left"] = false;
            }

            if (positionQuadrantX < Engine.XMax - 1 && Engine.GetQuadrantElements(positionQuadrantX + 1, positionQuadrantY)[0] == "0")
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
            this.movedDirection = existRoads[index];
            this.previousDirection = movedDirection;

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
            var newDirection = SelectRandomRoad(movedDirection);

            if (!string.IsNullOrEmpty(newDirection))
            {
                if (movedDirection == "Right")
                {
                    tryMoveRight();
                }
                else if (movedDirection == "Left")
                {
                    tryMoveLeft();
                }
                else if (movedDirection == "Up")
                {
                    tryMoveUp();
                }
                else if (movedDirection == "Down")
                {
                    tryMoveDown();
                }
            }
        }

        private void tryMoveRight()
        {
            if (positionQuadrantX < Engine.YMax - 1)
            {
                int nextQuandrantX = this.positionQuadrantX + 1;
                int nextQuadrantY = this.positionQuadrantY;
                string[] elements = Engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    previousDirection = "Right";
                    movedDirection = "Right";
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
            if (positionQuadrantX > 0)
            {
                int nextQuandrantX = this.positionQuadrantX - 1;
                int nextQuadrantY = this.positionQuadrantY;
                string[] elements = Engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    previousDirection = "Left";
                    movedDirection = "Left";
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
            if (positionQuadrantY > 0)
            {
                int nextQuandrantX = this.positionQuadrantX;
                int nextQuadrantY = this.positionQuadrantY - 1;
                string[] elements = Engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    previousDirection = "Up";
                    movedDirection = "Up";
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
            if (positionQuadrantY < Engine.YMax - 1)
            {
                int nextQuandrantX = this.positionQuadrantX;
                int nextQuadrantY = this.positionQuadrantY + 1;
                string[] elements = Engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    previousDirection = "Down";
                    movedDirection = "Down";
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
            graphicsGhost.DrawImage(image, (positionQuadrantX * QuadrantDimension) + 8, positionQuadrantY * QuadrantDimension + 4);
        }

        public async Task<bool> Move()
        {
            string moving = this.movedDirection;

            switch (moving)
            {
                case "Right":

                    if (drawingCoordinatesX == 0)
                    {
                        this.MoveNext();
                    }

                    drawingCoordinatesX += 1;
                    graphicsGhost.DrawLine(new Pen(Color.Black), drawingCoordinatesX - 1, (positionQuadrantY * QuadrantDimension) + 1, drawingCoordinatesX - 1, (positionQuadrantY * QuadrantDimension) + 49);
                    if (drawingCoordinatesX > (positionQuadrantX * QuadrantDimension) + 20)
                    {
                        Engine.DrawPoint(positionQuadrantX, positionQuadrantY);
                    }

                    graphicsGhost.DrawImage(image, (drawingCoordinatesX), positionQuadrantY * QuadrantDimension + 4);

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);

                    if (drawingCoordinatesX == ((positionQuadrantX + 1) * QuadrantDimension) + 8)
                    {
                        DrawBlackPolygon();

                        positionQuadrantX += 1;
                        this.MoveNext();
                    }
                    break;

                case "Left":
                    if (drawingCoordinatesX == 0)
                    {
                        this.MoveNext();
                    }
                    drawingCoordinatesX -= 1;
                    graphicsGhost.DrawLine(new Pen(Color.Black), (drawingCoordinatesX + image.Width) + 1, (positionQuadrantY * QuadrantDimension) + 1, (drawingCoordinatesX + image.Width) + 1, (positionQuadrantY * QuadrantDimension) + 49);
                    if (drawingCoordinatesX < (positionQuadrantX * QuadrantDimension) + 20)
                    {
                        Engine.DrawPoint(positionQuadrantX, positionQuadrantY);
                    }

                    graphicsGhost.DrawImage(image, (drawingCoordinatesX), positionQuadrantY * QuadrantDimension + 4);

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);

                    if (drawingCoordinatesX == ((positionQuadrantX - 1) * QuadrantDimension) + 8)
                    {
                        DrawBlackPolygon();

                        positionQuadrantX -= 1;
                        this.MoveNext();
                    }
                    break;

                case "Up":
                    if (drawingCoordinatesY == 0)
                    {
                        this.MoveNext();
                    }

                    drawingCoordinatesY -= 1;

                    graphicsGhost.DrawLine(new Pen(Color.Black), (positionQuadrantX * QuadrantDimension) + 1, (drawingCoordinatesY + 4 + image.Height) + 1, (positionQuadrantX * QuadrantDimension) + 49, (drawingCoordinatesY + 4 + image.Height) + 1);
                    if (drawingCoordinatesY < (positionQuadrantY * QuadrantDimension) - 11)
                    {
                        Engine.DrawPoint(positionQuadrantX, positionQuadrantY);
                    }

                    graphicsGhost.DrawImage(image, (positionQuadrantX * QuadrantDimension) + 8, drawingCoordinatesY + 4);
                    System.Threading.Thread.Sleep(Global.DrawingSpeed);

                    if (drawingCoordinatesY == ((positionQuadrantY - 1) * QuadrantDimension) + 4)
                    {
                        DrawBlackPolygon();

                        positionQuadrantY -= 1;
                        this.MoveNext();
                    }
                    break;
                case "Down":
                    if (drawingCoordinatesY == 0)
                    {
                        this.MoveNext();
                    }

                    drawingCoordinatesY += 1;
                    graphicsGhost.DrawLine(new Pen(Color.Black), (positionQuadrantX * QuadrantDimension) + 1, (drawingCoordinatesY + 4) - 1, (positionQuadrantX * QuadrantDimension) + 49, (drawingCoordinatesY + 4) - 1);
                    if (drawingCoordinatesY > (positionQuadrantY * QuadrantDimension) + 20)
                    {
                        Engine.DrawPoint(positionQuadrantX, positionQuadrantY);
                    }

                    graphicsGhost.DrawImage(image, (positionQuadrantX * QuadrantDimension) + 8, drawingCoordinatesY + 4);
                    System.Threading.Thread.Sleep(Global.DrawingSpeed);

                    if (drawingCoordinatesY == ((positionQuadrantY + 1) * QuadrantDimension) + 4)
                    {
                        DrawBlackPolygon();

                        positionQuadrantY += 1;
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
                new System.Drawing.Point(drawingCoordinatesX, drawingCoordinatesY),
                new System.Drawing.Point(drawingCoordinatesX + image.Width, drawingCoordinatesY),
                new System.Drawing.Point(drawingCoordinatesX + image.Width, drawingCoordinatesY + image.Height),
                new System.Drawing.Point(drawingCoordinatesX, drawingCoordinatesY + image.Height),
                new System.Drawing.Point(drawingCoordinatesX, drawingCoordinatesY),
            });
        }

        public int GetQuadrantX()
        {
            return positionQuadrantX;
        }

        public int GetQuadrantY()
        {
            return positionQuadrantY;
        }

        public string getDirection()
        {
            return this.movedDirection;
        }
    }
}