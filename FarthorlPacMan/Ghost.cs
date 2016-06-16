using System.Threading.Tasks;

namespace FarthorlPacMan
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    class Ghost
    {
        private Random random = new Random();
        private int positionQuadrantX = 0;
        private int positionQuadrantY = 0;
        private int drawingX = 0;
        private int drawingY = 0;
        private int positionPacManQaundarntX;
        private int positionPacManQaundarntY;
        private const int diameter = 40;
        private const int quadrantDimension = 50;
        private const int pacManDistanceX = 3;
        private const int pacManDistanceY = 0;
        private const int speedDrawing = 6;
        private string movedDirection;
        private string previousDirection;
        private Dictionary<string, bool> existDirections = new Dictionary<string, bool>();
        private static Image image = Image.FromFile(@"DataFiles\Images\Ghost.bmp");
        private Engine engine;
        private Graphics graphicsGhost;
        private Point point;

        public Ghost(int positionPacManQaundarntX, int positionPacManQaundarntY, Graphics graphicsGhost, Engine engine)
        {
            this.positionPacManQaundarntX = positionPacManQaundarntX;
            this.positionPacManQaundarntY = positionPacManQaundarntY;
            this.engine = engine;
            this.graphicsGhost = graphicsGhost;
            this.InicializeGhost();
        }

        public Ghost(Engine engine, int X, int Y, Graphics graphicsGhost)
        {
            positionQuadrantX = X;
            positionQuadrantY = Y;
            this.engine = engine;
            this.graphicsGhost = graphicsGhost;
        }

        private Dictionary<string, int> createCoordinatesXY(int pacManX, int pacManY)
        {
            List<int[]> ghostMatrix = new List<int[]>();
            Dictionary<string, int> resultXY = new Dictionary<string, int>();
            System.Threading.Thread.Sleep(random.Next(100, 500));

            //Check and remove quandrant if the pacMan is near from 6 quadrants left or right by X
            for (int y = 0; y < engine.GetMaxY() - 1; y++)
            {
                for (int x = 0; x < engine.GetMaxX() - 1; x++)
                {
                    if (x <= pacManX - pacManDistanceX && y <= pacManY - pacManDistanceY || x >= pacManX + pacManDistanceX && y >= pacManY + pacManDistanceY)
                    {
                        var elements = engine.GetQuadrantElements(x, y);

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

            if (engine.isExistGhost(ghostMatrix[quadrantIndex][0], ghostMatrix[quadrantIndex][1]))
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

            this.drawingX = positionQuadrantX * 50 + 8;
            this.drawingY = positionQuadrantY * 50 + 4;

            existDirections.Add("Up", false);
            existDirections.Add("Down", false);
            existDirections.Add("Left", false);
            existDirections.Add("Right", false);
            CheckExistDirections();
            SelectRandomDirection();
        }

        private void CheckExistDirections()
        {
            if (positionQuadrantY > 0 && engine.GetQuadrantElements(positionQuadrantX, positionQuadrantY - 1)[0] == "0")
            {
                existDirections["Up"] = true;
            }
            else
            {
                existDirections["Up"] = false;
            }

            if (positionQuadrantY < engine.GetMaxY() - 1 && engine.GetQuadrantElements(positionQuadrantX, positionQuadrantY + 1)[0] == "0")
            {
                existDirections["Down"] = true;
            }
            else
            {
                existDirections["Down"] = false;
            }

            if (positionQuadrantX > 0 && engine.GetQuadrantElements(positionQuadrantX - 1, positionQuadrantY)[0] == "0")
            {
                existDirections["Left"] = true;
            }
            else
            {
                existDirections["Left"] = false;
            }

            if (positionQuadrantX < engine.GetMaxX() - 1 && engine.GetQuadrantElements(positionQuadrantX + 1, positionQuadrantY)[0] == "0")
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


            if (!String.IsNullOrEmpty(newDirection))
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

        private async void tryMoveRight()
        {
            if (positionQuadrantX < engine.GetMaxX() - 1)
            {
                int nextQuandrantX = this.positionQuadrantX + 1;
                int nextQuadrantY = this.positionQuadrantY;
                string[] elements = engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    previousDirection = "Right";
                    movedDirection = "Right";
                    //await moveGhost(nextQuandrantX, nextQuadrantY, "Right");
                }
                else
                {
                    CheckExistDirections();
                    var newDirection = SelectRandomRoad("Right");

                    if (!String.IsNullOrEmpty(newDirection))
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

            }
            else
            {
                CheckExistDirections();
                var newDirection = SelectRandomRoad("Right");

                if (!String.IsNullOrEmpty(newDirection))
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

        }

        private async void tryMoveLeft()
        {
            if (positionQuadrantX > 0)
            {
                int nextQuandrantX = this.positionQuadrantX - 1;
                int nextQuadrantY = this.positionQuadrantY;
                string[] elements = engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    previousDirection = "Left";
                    movedDirection = "Left";
                }
                else
                {
                    CheckExistDirections();
                    var newDirection = SelectRandomRoad("Left");

                    if (!String.IsNullOrEmpty(newDirection))
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
            }
            else
            {
                CheckExistDirections();
                var newDirection = SelectRandomRoad("Left");

                if (!String.IsNullOrEmpty(newDirection))
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
        }

        private async void tryMoveUp()
        {
            if (positionQuadrantY > 0)
            {
                int nextQuandrantX = this.positionQuadrantX;
                int nextQuadrantY = this.positionQuadrantY - 1;
                string[] elements = engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    previousDirection = "Up";
                    movedDirection = "Up";
                    //await moveGhost(nextQuandrantX, nextQuadrantY, "Up");
                }
                else
                {
                    CheckExistDirections();
                    var newDirection = SelectRandomRoad("Up");

                    if (!String.IsNullOrEmpty(newDirection))
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
            }
            else
            {

                CheckExistDirections();
                var newDirection = SelectRandomRoad("Up");

                if (!String.IsNullOrEmpty(newDirection))
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
        }

        private async void tryMoveDown()
        {
            if (positionQuadrantY < engine.GetMaxY() - 1)
            {
                int nextQuandrantX = this.positionQuadrantX;
                int nextQuadrantY = this.positionQuadrantY + 1;
                string[] elements = engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (elements[0] == "0")
                {
                    previousDirection = "Down";
                    movedDirection = "Down";
                }
                else
                {
                    CheckExistDirections();
                    var newDirection = SelectRandomRoad("Down");

                    if (!String.IsNullOrEmpty(newDirection))
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
            }
            else
            {
                CheckExistDirections();
                var newDirection = SelectRandomRoad("Down");

                if (!String.IsNullOrEmpty(newDirection))
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
        }

        public void Draw()
        {
            graphicsGhost.DrawImage(image, (positionQuadrantX * quadrantDimension) + 8, positionQuadrantY * quadrantDimension + 4);
        }

        public async Task<bool> Move()
        {
            string moving = this.movedDirection;

            switch (moving)
            {
                case "Right":

                    if (drawingX == 0)
                    {
                        this.MoveNext();
                    }

                    drawingX = drawingX + 1;
                    graphicsGhost.DrawLine(new Pen(Color.Black), drawingX - 1, (positionQuadrantY * quadrantDimension) + 1, drawingX - 1, (positionQuadrantY * quadrantDimension) + 49);
                    if (drawingX > (positionQuadrantX * quadrantDimension) + 20)
                    {
                        engine.DrawPoint(positionQuadrantX, positionQuadrantY);
                    }

                    graphicsGhost.DrawImage(image, (drawingX), positionQuadrantY * quadrantDimension + 4);

                    System.Threading.Thread.Sleep(speedDrawing);

                    if (drawingX == ((positionQuadrantX + 1) * quadrantDimension) + 8)
                    {
                        graphicsGhost.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[]
                        {
                        new System.Drawing.Point(drawingX,drawingY),
                        new System.Drawing.Point(drawingX+image.Width,drawingY),
                        new System.Drawing.Point(drawingX+image.Width,drawingY+image.Height),
                        new System.Drawing.Point(drawingX,drawingY+image.Height),
                        new System.Drawing.Point(drawingX,drawingY),
                        });

                        positionQuadrantX = positionQuadrantX + 1;
                        this.MoveNext();
                    }
                    break;

                case "Left":
                    if (drawingX == 0)
                    {
                        this.MoveNext();
                    }
                    drawingX = drawingX - 1;
                    graphicsGhost.DrawLine(new Pen(Color.Black), (drawingX + image.Width) + 1, (positionQuadrantY * quadrantDimension) + 1, (drawingX + image.Width) + 1, (positionQuadrantY * quadrantDimension) + 49);
                    if (drawingX < (positionQuadrantX * quadrantDimension) + 20)
                    {
                        engine.DrawPoint(positionQuadrantX, positionQuadrantY);
                    }

                    graphicsGhost.DrawImage(image, (drawingX), positionQuadrantY * quadrantDimension + 4);

                    System.Threading.Thread.Sleep(speedDrawing);

                    if (drawingX == ((positionQuadrantX - 1) * quadrantDimension) + 8)
                    {
                        graphicsGhost.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[]
                        {
                        new System.Drawing.Point(drawingX,drawingY),
                        new System.Drawing.Point(drawingX+image.Width,drawingY),
                        new System.Drawing.Point(drawingX+image.Width,drawingY+image.Height),
                        new System.Drawing.Point(drawingX,drawingY+image.Height),
                        new System.Drawing.Point(drawingX,drawingY),
                        });

                        positionQuadrantX = positionQuadrantX - 1;
                        this.MoveNext();
                    }
                    break;

                case "Up":
                    if (drawingY == 0)
                    {
                        this.MoveNext();
                    }

                    drawingY = drawingY - 1;

                    graphicsGhost.DrawLine(new Pen(Color.Black), (positionQuadrantX * quadrantDimension) + 1, (drawingY + 4 + image.Height) + 1, (positionQuadrantX * quadrantDimension) + 49, (drawingY + 4 + image.Height) + 1);
                    if (drawingY < (positionQuadrantY * quadrantDimension) - 11)
                    {
                        engine.DrawPoint(positionQuadrantX, positionQuadrantY);
                    }

                    graphicsGhost.DrawImage(image, (positionQuadrantX * quadrantDimension) + 8, drawingY + 4);
                    System.Threading.Thread.Sleep(speedDrawing);

                    if (drawingY == ((positionQuadrantY - 1) * quadrantDimension) + 4)
                    {
                        graphicsGhost.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[]
                        {
                        new System.Drawing.Point(drawingX,drawingY),
                        new System.Drawing.Point(drawingX+image.Width,drawingY),
                        new System.Drawing.Point(drawingX+image.Width,drawingY+image.Height),
                        new System.Drawing.Point(drawingX,drawingY+image.Height),
                        new System.Drawing.Point(drawingX,drawingY),
                        });

                        positionQuadrantY = positionQuadrantY - 1;
                        this.MoveNext();
                    }
                    break;
                case "Down":
                    if (drawingY == 0)
                    {
                        this.MoveNext();
                    }

                    drawingY = drawingY + 1;
                    graphicsGhost.DrawLine(new Pen(Color.Black), (positionQuadrantX * quadrantDimension) + 1, (drawingY + 4) - 1, (positionQuadrantX * quadrantDimension) + 49, (drawingY + 4) - 1);
                    if (drawingY > (positionQuadrantY * quadrantDimension) + 20)
                    {
                        engine.DrawPoint(positionQuadrantX, positionQuadrantY);
                    }

                    graphicsGhost.DrawImage(image, (positionQuadrantX * quadrantDimension) + 8, drawingY + 4);
                    System.Threading.Thread.Sleep(speedDrawing);

                    if (drawingY == ((positionQuadrantY + 1) * quadrantDimension) + 4)
                    {
                        graphicsGhost.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[]
                        {
                        new System.Drawing.Point(drawingX,drawingY),
                        new System.Drawing.Point(drawingX+image.Width,drawingY),
                        new System.Drawing.Point(drawingX+image.Width,drawingY+image.Height),
                        new System.Drawing.Point(drawingX,drawingY+image.Height),
                        new System.Drawing.Point(drawingX,drawingY),
                        });

                        positionQuadrantY = positionQuadrantY + 1;
                        this.MoveNext();
                    }
                    break;
            }

            return true;

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