using System.Media;
using System.Threading;

namespace FarthorlPacMan
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    class PacMan
    {
        private Boolean isAlive = true;
        private int positionQuadrantX = 0;
        private int positionQuadrantY = 0;
        private const int diameter = 35;
        private const int quadrantDimension = 50;
        private const int speedDrawing = 1;
        private Color pacManColor = Color.Yellow;
        private string movedDirection;
        private string previousDirection;
        private int eatPoints = 0;
        private string stopDirection = "";
        private int drawingCoordinatesX;
        private int drawingCoordinatesY;
        private Engine engine;
        private Graphics graphics;
        private Player player = new Player();
        public PacMan(int positionXQaundarnt, int positionYQuadrant, Graphics graphics, Engine engine)
        {
            this.positionQuadrantX = positionXQaundarnt;
            this.positionQuadrantY = positionYQuadrant;
            drawingCoordinatesX = positionQuadrantX * quadrantDimension + quadrantDimension / 2;
            drawingCoordinatesY = positionQuadrantY * quadrantDimension + quadrantDimension / 2;
            this.engine = engine;
            this.graphics = graphics;
            this.initializePacMan();
        }

        private void initializePacMan()
        {
            EatPoint(positionQuadrantX, positionQuadrantY);
        }

        private async void tryMoveRight()
        {
            if (this.positionQuadrantX < engine.GetMaxX() - 1)
            {
                int nextQuandrantX = this.positionQuadrantX + 1;
                int nextQuadrantY = this.positionQuadrantY;
                string[] elements = engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (isAlive && elements[0] == "0")
                {
                    previousDirection = "Right";
                    movedDirection = "Right";
                    stopDirection = "";
                    await movePacMan(nextQuandrantX, nextQuadrantY, "Right");
                }
                else if (elements[0] == "1")
                {
                    if (movedDirection == "")
                    {
                        previousDirection = "";
                        stopDirection = movedDirection;
                    }
                    movedDirection = "";
                    this.move(previousDirection);
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

                if (isAlive && elements[0] == "0")
                {
                    previousDirection = "Left";
                    movedDirection = "Left";
                    stopDirection = "";
                    await this.movePacMan(nextQuandrantX, nextQuadrantY, "Left");
                }
                else if (elements[0] == "1")
                {
                    if (movedDirection == "")
                    {
                        stopDirection = movedDirection;
                        previousDirection = "";
                    }
                    movedDirection = "";
                    this.move(previousDirection);
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

                if (isAlive && elements[0] == "0")
                {
                    previousDirection = "Up";
                    movedDirection = "Up";
                    stopDirection = "";
                    await this.movePacMan(nextQuandrantX, nextQuadrantY, "Up");
                }
                else if (elements[0] == "1")
                {

                    if (movedDirection == "")
                    {
                        previousDirection = "";
                        stopDirection = movedDirection;
                    }
                    movedDirection = "";
                    this.move(previousDirection);
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

                if (isAlive && elements[0] == "0")
                {
                    this.previousDirection = "Down";
                    this.movedDirection = "Down";
                    stopDirection = "";
                    await this.movePacMan(nextQuandrantX, nextQuadrantY, "Down");

                }
                else if (elements[0] == "1")
                {
                    if (this.movedDirection == "")
                    {
                        this.previousDirection = "";
                        stopDirection = movedDirection;
                    }
                    this.movedDirection = "";
                    this.move(previousDirection);
                }
            }
        }

        private async Task<bool> movePacMan(int nextX, int nextY, string moving)
        {
            int coef = 0;
            graphics.FillRectangle(
            new SolidBrush(Color.Black),
            (drawingCoordinatesX) - (diameter / 2) - 3,
            ((drawingCoordinatesY) - (diameter / 2) - 3),
            diameter + 6,
            diameter + 6
            );

            switch (moving)
            {
                case "Right":
                    for (int x = drawingCoordinatesX; x <= (nextX * quadrantDimension) + (quadrantDimension / 2); x++)
                    {
                        drawingCoordinatesX = x;
                        DrawPoint(nextX, nextY);
                        graphics.DrawEllipse(
                            new Pen(Color.Black),
                            new Rectangle(
                                x - 1 - (diameter / 2),
                                (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                                diameter,
                                diameter
                                )
                            );

                        graphics.FillEllipse(
                            new SolidBrush(pacManColor),
                            x - (diameter / 2),
                            (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                            diameter,
                            diameter
                            );
                        graphics.FillEllipse(
                           new SolidBrush(Color.Black),
                           x - (diameter / 2 - 20),
                           (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2 - 5),
                           diameter / 5,
                           diameter / 5
                           );

                        graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(x, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2),
                                new System.Drawing.Point(x + quadrantDimension/2, (this.positionQuadrantY * quadrantDimension)+15+coef),
                                new System.Drawing.Point(x + quadrantDimension/2, (this.positionQuadrantY * quadrantDimension)+35-coef),
                                new System.Drawing.Point(x, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2)
                            });

                        coef += 1;
                        if (coef == 12)
                        {
                            coef = 0;
                        }

                        if (engine.isDirectionChanged(movedDirection) == false && drawingCoordinatesX >= nextX * quadrantDimension + quadrantDimension / 2 - 22)
                        {
                            this.positionQuadrantX = nextX;
                            this.positionQuadrantY = nextY;
                            EatPoint(nextX, nextY);
                        }

                        /*
                        If is direction is change on the move period, 
                        the logic check is the packMan is going to the next quadrant or not
                        */
                        if (engine.isDirectionChanged(movedDirection))
                        {
                            if (drawingCoordinatesX < nextX * quadrantDimension && drawingCoordinatesX > (nextX - 1) * quadrantDimension)
                            {
                                this.positionQuadrantX = nextX - 1;
                                this.positionQuadrantY = nextY;
                            }
                            else
                            {
                                this.positionQuadrantX = nextX;
                                this.positionQuadrantY = nextY;
                            }
                            break;
                        }
                        System.Threading.Thread.Sleep(speedDrawing);
                    }

                    break;

                case "Left":
                    for (int x = drawingCoordinatesX; x >= (nextX * quadrantDimension) + (quadrantDimension / 2); x--)
                    {
                        drawingCoordinatesX = x;
                        DrawPoint(nextX, nextY);
                        graphics.DrawEllipse(
                            new Pen(Color.Black),
                            new Rectangle(
                                x + 1 - (diameter / 2),
                                (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                                diameter,
                                diameter
                                )
                                    );

                        graphics.FillEllipse(
                           new SolidBrush(pacManColor), x - (diameter / 2),
                           (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                           diameter,
                           diameter
                           );
                        graphics.FillEllipse(
                    new SolidBrush(Color.Black),
                    x - (diameter / 2 - 10),
                    (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2 - 5),
                    diameter / 5,
                    diameter / 5
                    );

                        graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                            new System.Drawing.Point(x, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2),
                            new System.Drawing.Point(x-quadrantDimension/2+2, (this.positionQuadrantY * quadrantDimension)+15+coef),
                            new System.Drawing.Point(x-quadrantDimension/2+2, (this.positionQuadrantY * quadrantDimension)+35-coef),
                            new System.Drawing.Point(x, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2)
                        });

                        coef += 1;
                        if (coef == 12)
                        {
                            coef = 0;
                        }

                        if (engine.isDirectionChanged(movedDirection) == false && drawingCoordinatesX <= nextX * quadrantDimension + quadrantDimension / 2 + 22)
                        {
                            this.positionQuadrantX = nextX;
                            this.positionQuadrantY = nextY;
                            EatPoint(nextX, nextY);
                        }

                        /*
                        If is direction is change on the move period, 
                        the logic check is the packMan is going to the next quadrant or not
                        */
                        if (engine.isDirectionChanged(movedDirection))
                        {
                            if (drawingCoordinatesX > nextX * quadrantDimension + quadrantDimension && drawingCoordinatesX < (nextX + 1) * quadrantDimension)
                            {
                                this.positionQuadrantX = nextX + 1;
                                this.positionQuadrantY = nextY;
                            }
                            else
                            {
                                this.positionQuadrantX = nextX;
                                this.positionQuadrantY = nextY;
                            }
                            break;
                        }
                        System.Threading.Thread.Sleep(speedDrawing);

                    }

                    break;

                case "Up":
                    for (int y = drawingCoordinatesY; y >= (nextY * quadrantDimension) + (quadrantDimension / 2); y--)
                    {
                        drawingCoordinatesY = y;
                        DrawPoint(nextX, nextY);
                        graphics.DrawEllipse(
                            new Pen(Color.Black),
                            new Rectangle(
                                (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                                y + 1 - (diameter / 2),
                                diameter,
                                diameter
                                )
                            );

                        graphics.FillEllipse(
                            new SolidBrush(pacManColor),
                            (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                            y - (diameter / 2),
                            diameter,
                            diameter
                            );
                        graphics.FillEllipse(
                        new SolidBrush(Color.Black),
                        (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2 - 5), y - (diameter / 2 - 10),
                        diameter / 5,
                        diameter / 5
                        );


                        graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, y),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + 15+coef, y - quadrantDimension/2+2),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + 35-coef, y - quadrantDimension/2+2  ),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, y)
                            });

                        coef += 1;
                        if (coef == 12)
                        {
                            coef = 0;
                        }

                        if (engine.isDirectionChanged(movedDirection) == false && drawingCoordinatesY <= nextY * quadrantDimension + quadrantDimension / 2 + 22)
                        {
                            this.positionQuadrantX = nextX;
                            this.positionQuadrantY = nextY;
                            EatPoint(nextX, nextY);
                        }
                        /*
                        If is direction is change on the move period, 
                        the logic check is the packMan is going to the next quadrant or not
                        */
                        if (engine.isDirectionChanged(movedDirection))
                        {

                            if (drawingCoordinatesY > nextY * 50 + quadrantDimension && drawingCoordinatesY <= (nextY + 1) * quadrantDimension)
                            {
                                {
                                    this.positionQuadrantX = nextX;
                                    this.positionQuadrantY = nextY + 1;
                                }
                            }
                            else
                            {
                                this.positionQuadrantX = nextX;
                                this.positionQuadrantY = nextY;
                            }
                            break;
                        }
                        System.Threading.Thread.Sleep(speedDrawing);

                    }

                    break;

                case "Down":
                    for (int y = drawingCoordinatesY; y <= (nextY * quadrantDimension) + (quadrantDimension / 2); y++)
                    {
                        drawingCoordinatesY = y;
                        DrawPoint(nextX, nextY);
                        graphics.DrawEllipse(
                            new Pen(Color.Black),
                            new Rectangle(
                                (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                                y - 1 - (diameter / 2),
                                diameter,
                                diameter
                                )
                            );

                        graphics.FillEllipse(
                            new SolidBrush(pacManColor),
                            (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                            y - (diameter / 2),
                            diameter,
                            diameter
                            );
                        graphics.FillEllipse(
                           new SolidBrush(Color.Black),
                           (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2 - 5), y - (diameter / 2 - 17),
                           diameter / 5,
                           diameter / 5
                           );


                        graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, y),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + 15+coef, y + quadrantDimension/2),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension+ 35-coef, y + quadrantDimension/2  ),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, y)
                            });

                        coef += 1;
                        if (coef == 12)
                        {
                            coef = 0;
                        }

                        if (engine.isDirectionChanged(movedDirection) == false && drawingCoordinatesY >= nextY * quadrantDimension + quadrantDimension / 2 - 22)
                        {
                            this.positionQuadrantX = nextX;
                            this.positionQuadrantY = nextY;
                            EatPoint(nextX, nextY);
                        }

                        /*
                        If is direction is change on the move period, 
                        the logic check is the packMan is going to the next quadrant or not
                        */
                        if (engine.isDirectionChanged(movedDirection))
                        {
                            if (drawingCoordinatesY < nextY * quadrantDimension && drawingCoordinatesY > (nextY - 1) * quadrantDimension)
                            {
                                {
                                    this.positionQuadrantX = nextX;
                                    this.positionQuadrantY = nextY - 1;
                                }
                            }
                            else
                            {
                                this.positionQuadrantX = nextX;
                                this.positionQuadrantY = nextY;
                            }

                            break;
                        }
                        System.Threading.Thread.Sleep(speedDrawing);
                    }

                    break;
            }

            return true;
        }

        private void DrawPoint(int quadrantX, int quadrantY)
        {
            string[] elements = engine.GetQuadrantElements(quadrantX, quadrantY);

            if (elements[1] == "1")
            {
                engine.DrawPoint(quadrantX, quadrantY);
            }
        }

        public void move(string direction)
        {
            if (!String.IsNullOrEmpty(direction))
            {
                if (direction == "Right" && direction != stopDirection)
                {
                    tryMoveRight();
                }
                else if (direction == "Left" && direction != stopDirection)
                {
                    tryMoveLeft();
                }
                else if (direction == "Up" && direction != stopDirection)
                {
                    tryMoveUp();
                }
                else if (direction == "Down" && direction != stopDirection)
                {
                    tryMoveDown();
                }
            }
        }

        public void DrawPacMan()
        {

            graphics.FillEllipse(
              new SolidBrush(pacManColor),
              (drawingCoordinatesX) - (diameter / 2),
              ((drawingCoordinatesY) - (diameter / 2)),
              diameter,
              diameter
              );

        }

        public int getScore()
        {
            return this.eatPoints;
        }

        public int getQuadrantX()
        {
            return this.positionQuadrantX;
        }

        public int getQuadrantY()
        {
            return this.positionQuadrantY;
        }

        public void EatPoint(int quadrantX, int quadrantY)
        {
            string[] elements = engine.GetQuadrantElements(quadrantX, quadrantY);

            if (elements[1] == "1")
            {
                this.eatPoints = this.eatPoints + int.Parse(elements[1]);
                elements[1] = "0";
                player.Play("eatfruit");
                engine.EatPointAndUpdateMatrix(this.positionQuadrantX, this.positionQuadrantY, elements);

            }
        }

    }
}
