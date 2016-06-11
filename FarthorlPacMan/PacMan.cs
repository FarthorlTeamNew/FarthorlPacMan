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
        private const int speedDrawing = 6;
        private Color pacManColor = Color.Yellow;
        private string movedDirection;
        private string previousDirection;
        private int eatPoints = 0;
        private string stopDirection = "";
        private int drawingCoordinatesX;
        private int drawingCoordinatesY;
        private Engine engine;
        public PacMan(int positionXQaundarnt, int positionYQuadrant, Graphics graphics, Engine engine)
        {
            this.positionQuadrantX = positionXQaundarnt;
            this.positionQuadrantY = positionYQuadrant;
            drawingCoordinatesX = positionQuadrantX * quadrantDimension + quadrantDimension / 2;
            drawingCoordinatesY = positionQuadrantY * quadrantDimension + quadrantDimension / 2;
            this.initializePacMan(engine);
            this.engine = engine;
        }

        public void move(Graphics graphic, Engine engine, string direction)
        {
            if (!String.IsNullOrEmpty(direction))
            {
                if (direction == "Right" && direction != stopDirection)
                {
                    tryMoveRight(graphic, engine);
                }
                else if (direction == "Left" && direction != stopDirection)
                {
                    tryMoveLeft(graphic, engine);
                }
                else if (direction == "Up" && direction != stopDirection)
                {
                    tryMoveUp(graphic, engine);
                }
                else if (direction == "Down" && direction != stopDirection)
                {
                    tryMoveDown(graphic, engine);
                }

            }
        }

        private async void tryMoveUp(Graphics graphic, Engine engine)
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
                    await this.movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Up");

                }
                else if (elements[0] == "1")
                {

                    if (movedDirection == "")
                    {
                        previousDirection = "";
                        stopDirection = movedDirection;
                    }
                    movedDirection = "";
                    this.move(graphic, engine, previousDirection);
                }
            }
        }

        private async void tryMoveRight(Graphics graphic, Engine engine)
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
                    await movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Right");
                }
                else if (elements[0] == "1")
                {
                    if (movedDirection == "")
                    {
                        previousDirection = "";
                        stopDirection = movedDirection;
                    }
                    movedDirection = "";
                    this.move(graphic, engine, previousDirection);
                }
            }
        }

        private async void tryMoveDown(Graphics graphic, Engine engine)
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
                    await this.movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Down");

                }
                else if (elements[0] == "1")
                {
                    if (this.movedDirection == "")
                    {
                        this.previousDirection = "";
                        stopDirection = movedDirection;
                    }
                    this.movedDirection = "";
                    this.move(graphic, engine, previousDirection);
                }
            }
        }

        private async void tryMoveLeft(Graphics graphic, Engine engine)
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
                    await this.movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Left");
                }
                else if (elements[0] == "1")
                {
                    if (movedDirection == "")
                    {
                        stopDirection = movedDirection;
                        previousDirection = "";
                    }
                    movedDirection = "";
                    this.move(graphic, engine, previousDirection);
                }
            }
        }

        public void DrawPacMan(Graphics graphics)
        {

            graphics.FillEllipse(
              new SolidBrush(pacManColor),
              (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
              ((this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2)),
              diameter,
              diameter
              );

        }

        private async Task<bool> movePacMan(Graphics graphics, int nextX, int nextY, string moving)
        {

            int coef = 0;
            graphics.FillRectangle(
            new SolidBrush(Color.Black),
            (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2) - 3,
            ((this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2) - 3),
            diameter + 6,
            diameter + 6
            );



            switch (moving)
            {
                case "Right":
                    for (int x = drawingCoordinatesX; x <= (nextX * quadrantDimension) + (quadrantDimension / 2); x++)
                    {
                        drawingCoordinatesX = x;
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
                                new System.Drawing.Point(x + quadrantDimension/2, (this.positionQuadrantY * quadrantDimension)+40-coef),
                                new System.Drawing.Point(x, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2)
                            });

                        coef += 1;
                        if (coef == 12)
                        {
                            coef = 0;
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
                    if (engine.isDirectionChanged(movedDirection)==false && drawingCoordinatesX == nextX * quadrantDimension + quadrantDimension / 2)
                    {
                            this.positionQuadrantX = nextX;
                            this.positionQuadrantY = nextY;
                            EatPoint(engine, nextX, nextY);
                    }
                    break;

                case "Left":
                    for (int x = drawingCoordinatesX; x >= (nextX * quadrantDimension) + (quadrantDimension / 2); x--)
                    {
                        drawingCoordinatesX = x;
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
                            new System.Drawing.Point(x-quadrantDimension/2+2, (this.positionQuadrantY * quadrantDimension)+40-coef),
                            new System.Drawing.Point(x, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2)
                        });

                        coef += 1;
                        if (coef == 12)
                        {
                            coef = 0;
                        }

                        /*
                        If is direction is change on the move period, 
                        the logic check is the packMan is going to the next quadrant or not
                        */
                        if (engine.isDirectionChanged(movedDirection))
                        {
                            if (drawingCoordinatesX > nextX * quadrantDimension + quadrantDimension && drawingCoordinatesX <= (nextX + 1) * quadrantDimension)
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
                    if (engine.isDirectionChanged(movedDirection) == false && drawingCoordinatesX == nextX * quadrantDimension + quadrantDimension / 2)
                    {
                        this.positionQuadrantX = nextX;
                        this.positionQuadrantY = nextY;
                        EatPoint(engine, nextX, nextY);
                    }
                    break;

                case "Up":
                    for (int y = drawingCoordinatesY; y >= (nextY * quadrantDimension) + (quadrantDimension / 2); y--)
                    {
                        drawingCoordinatesY = y;
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
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + 40-coef, y - quadrantDimension/2+2  ),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, y)
                            });

                        coef += 1;
                        if (coef == 12)
                        {
                            coef = 0;
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
                    if (engine.isDirectionChanged(movedDirection) == false && drawingCoordinatesY == nextY * quadrantDimension + quadrantDimension / 2)
                    {
                        this.positionQuadrantX = nextX;
                        this.positionQuadrantY = nextY;
                        EatPoint(engine, nextX, nextY);
                    }
                    break;

                case "Down":
                    for (int y = drawingCoordinatesY; y <= (nextY * quadrantDimension) + (quadrantDimension / 2); y++)
                    {
                        drawingCoordinatesY = y;
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
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension+ 40-coef, y + quadrantDimension/2  ),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, y)
                            });

                        coef += 1;
                        if (coef == 12)
                        {
                            coef = 0;
                        }

                        /*
                        If is direction is change on the move period, 
                        the logic check is the packMan is going to the next quadrant or not
                        */
                        if (engine.isDirectionChanged(movedDirection))
                        {
                            if (drawingCoordinatesY < nextY * 50 && drawingCoordinatesY > (nextY - 1) * quadrantDimension)
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
                    if (engine.isDirectionChanged(movedDirection) == false && drawingCoordinatesY == nextY * quadrantDimension + quadrantDimension/2)
                    {
                        this.positionQuadrantX = nextX;
                        this.positionQuadrantY = nextY;
                        EatPoint(engine, nextX, nextY);
                    }
                    break;
            }

            return true;
        }

        private void initializePacMan(Engine engine)
        {
            EatPoint(engine, positionQuadrantX, positionQuadrantY);
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

        public void EatPoint(Engine engine, int quadrantX, int quadrantY)
        {
            string[] elements = engine.GetQuadrantElements(quadrantX, quadrantY);
            SoundPlayer food = new SoundPlayer("DataFiles/Sounds/pacman_eatfruit.wav");

            if (elements[1] == "1")
            {
                this.eatPoints = this.eatPoints + int.Parse(elements[1]);
                elements[1] = "0";
                engine.EatPointAndUpdateMatrix(this.positionQuadrantX, this.positionQuadrantY, elements);
                food.Play();
            }
        }

    }
}
