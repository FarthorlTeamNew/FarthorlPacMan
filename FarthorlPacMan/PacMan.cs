namespace FarthorlPacMan
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    class PacMan : IDisposable
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
        private int animateCoeficent = 0;
        private Engine engine;
        private Graphics graphics;
        private PlayerSound player = new PlayerSound();

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

        private void tryMoveRight()
        {
            if (this.positionQuadrantX < engine.GetMaxX() - 1)
            {
                string[] elements = engine.GetQuadrantElements(this.positionQuadrantX + 1, this.positionQuadrantY);

                if (isAlive && elements[0] == "0")
                {
                    previousDirection = "Right";
                    movedDirection = "Right";
                    stopDirection = "";
                }
                else if (elements[0] == "1")
                {
                    if (movedDirection == "")
                    {
                        previousDirection = "";
                        stopDirection = movedDirection;
                    }
                    movedDirection = "";
                    this.Move(previousDirection);
                }
            }
        }

        private void tryMoveLeft()
        {
            if (positionQuadrantX > 0)
            {
                string[] elements = engine.GetQuadrantElements(positionQuadrantX - 1, positionQuadrantY);

                if (isAlive && elements[0] == "0")
                {
                    previousDirection = "Left";
                    movedDirection = "Left";
                    stopDirection = "";
                }
                else if (elements[0] == "1")
                {
                    if (movedDirection == "")
                    {
                        stopDirection = movedDirection;
                        previousDirection = "";
                    }
                    movedDirection = "";
                    this.Move(previousDirection);
                }
            }
        }

        private void tryMoveUp()
        {
            if (positionQuadrantY > 0)
            {
                string[] elements = engine.GetQuadrantElements(positionQuadrantX, positionQuadrantY - 1);

                if (isAlive && elements[0] == "0")
                {
                    previousDirection = "Up";
                    movedDirection = "Up";
                    stopDirection = "";

                }
                else if (elements[0] == "1")
                {

                    if (movedDirection == "")
                    {
                        previousDirection = "";
                        stopDirection = movedDirection;
                    }
                    movedDirection = "";
                    this.Move(previousDirection);
                }
            }
        }

        private void tryMoveDown()
        {
            if (positionQuadrantY < engine.GetMaxY() - 1)
            {
                string[] elements = engine.GetQuadrantElements(positionQuadrantX, positionQuadrantY + 1);

                if (isAlive && elements[0] == "0")
                {

                    this.previousDirection = "Down";
                    this.movedDirection = "Down";
                    stopDirection = "";

                }
                else if (elements[0] == "1")
                {
                    if (this.movedDirection == "")
                    {
                        this.previousDirection = "";
                        stopDirection = movedDirection;
                    }
                    this.movedDirection = "";
                    this.Move(previousDirection);
                }
            }
        }

        public async Task<bool> Run(string direction)
        {
            if (drawingCoordinatesX==positionQuadrantX * quadrantDimension + quadrantDimension /2 && drawingCoordinatesY==positionQuadrantY*quadrantDimension + quadrantDimension/2)
            {
                Move(direction);
            }

            if (engine.isDirectionChanged(movedDirection))
            {
                this.movedDirection = engine.GetDirection();
            }

            switch (movedDirection)
            {
                case "Right":

                    if (drawingCoordinatesX < (engine.GetMaxX()-1) * quadrantDimension + quadrantDimension/2)
                    {
                        drawingCoordinatesX = drawingCoordinatesX + 1;
                    }

                    graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            drawingCoordinatesX - 1 - (diameter / 2),
                            (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                            diameter,
                            diameter
                            )
                        );

                    graphics.FillEllipse(
                           new SolidBrush(pacManColor),
                           drawingCoordinatesX - (diameter / 2),
                           (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                           diameter,
                           diameter
                           );

                    graphics.FillEllipse(
                       new SolidBrush(Color.Black),
                       drawingCoordinatesX - (diameter / 2 - 20),
                       (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2 - 5),
                       diameter / 5,
                       diameter / 5
                       );

                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(drawingCoordinatesX, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2),
                                new System.Drawing.Point(drawingCoordinatesX + quadrantDimension/2, (this.positionQuadrantY * quadrantDimension)+15+animateCoeficent),
                                new System.Drawing.Point(drawingCoordinatesX + quadrantDimension/2, (this.positionQuadrantY * quadrantDimension)+35-animateCoeficent),
                                new System.Drawing.Point(drawingCoordinatesX, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2)
                            });

                    animateCoeficent += 1;
                    if (animateCoeficent == 12)
                    {
                        animateCoeficent = 0;
                    }

                    if (drawingCoordinatesX == (positionQuadrantX + 1) * quadrantDimension + quadrantDimension / 2 - 22)
                    {
                        EatPoint(positionQuadrantX + 1, positionQuadrantY);
                    }

                    System.Threading.Thread.Sleep(speedDrawing);

                    if (drawingCoordinatesX == ((positionQuadrantX + 1) * quadrantDimension) + 25)
                    {
                        positionQuadrantX = positionQuadrantX + 1;

                    }
                    break;

                case "Left":

                    if (drawingCoordinatesX > (positionQuadrantX-1) * quadrantDimension + quadrantDimension / 2 && drawingCoordinatesX > quadrantDimension / 2)
                    {
                        drawingCoordinatesX = drawingCoordinatesX - 1;
                    }

                    graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            drawingCoordinatesX + 1 - (diameter / 2),
                            (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                            diameter,
                            diameter
                            )
                        );

                    graphics.FillEllipse(
                        new SolidBrush(pacManColor), drawingCoordinatesX - (diameter / 2),
                        (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                        diameter,
                        diameter
                        );

                    graphics.FillEllipse(
                        new SolidBrush(Color.Black),
                        drawingCoordinatesX - (diameter / 2 - 10),
                        (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2 - 5),
                        diameter / 5,
                        diameter / 5
                        );

                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                            new System.Drawing.Point(drawingCoordinatesX, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2),
                            new System.Drawing.Point(drawingCoordinatesX-quadrantDimension/2+2, (this.positionQuadrantY * quadrantDimension)+15+animateCoeficent),
                            new System.Drawing.Point(drawingCoordinatesX-quadrantDimension/2+2, (this.positionQuadrantY * quadrantDimension)+35-animateCoeficent),
                            new System.Drawing.Point(drawingCoordinatesX, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2)
                        });

                    animateCoeficent += 1;
                    if (animateCoeficent == 12)
                    {
                        animateCoeficent = 0;
                    }

                    System.Threading.Thread.Sleep(speedDrawing);

                    if (drawingCoordinatesX == (positionQuadrantX - 1) * quadrantDimension + quadrantDimension / 2 + 22)
                    {
                        EatPoint(positionQuadrantX - 1, positionQuadrantY);
                    }

                    if (drawingCoordinatesX == ((positionQuadrantX - 1) * quadrantDimension) + 25)
                    {
                        positionQuadrantX = positionQuadrantX - 1;
                    }
                    break;

                case "Up":

                    if (drawingCoordinatesY > (positionQuadrantY-1) * quadrantDimension + quadrantDimension / 2 && drawingCoordinatesY > quadrantDimension / 2)
                    {
                        drawingCoordinatesY = drawingCoordinatesY - 1;
                    }

                    graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                            drawingCoordinatesY + 1 - (diameter / 2),
                            diameter,
                            diameter
                            )
                        );

                    graphics.FillEllipse(
                        new SolidBrush(pacManColor),
                        (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                        drawingCoordinatesY - (diameter / 2),
                        diameter,
                        diameter
                        );

                    graphics.FillEllipse(
                    new SolidBrush(Color.Black),
                    (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2 - 5), drawingCoordinatesY - (diameter / 2 - 10),
                    diameter / 5,
                    diameter / 5
                    );


                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, drawingCoordinatesY),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + 15+animateCoeficent, drawingCoordinatesY - quadrantDimension/2+2),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + 35-animateCoeficent, drawingCoordinatesY - quadrantDimension/2+2),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, drawingCoordinatesY)
                            });

                    animateCoeficent += 1;
                    if (animateCoeficent == 12)
                    {
                        animateCoeficent = 0;
                    }

                    System.Threading.Thread.Sleep(speedDrawing);


                    if (drawingCoordinatesY == (positionQuadrantY - 1) * quadrantDimension + quadrantDimension / 2 + 22)
                    {
                        EatPoint(positionQuadrantX, positionQuadrantY - 1);
                    }

                    if (drawingCoordinatesY == ((positionQuadrantY - 1) * quadrantDimension) + quadrantDimension/2)
                    {
                        positionQuadrantY = positionQuadrantY - 1;
                    }
                    break;

                case "Down":

                    if (drawingCoordinatesY < (engine.GetMaxY()-1) * quadrantDimension + quadrantDimension / 2)
                    {
                        drawingCoordinatesY = drawingCoordinatesY + 1;
                    }

                    graphics.DrawEllipse(
                         new Pen(Color.Black),
                         new Rectangle(
                             (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                             drawingCoordinatesY - 1 - (diameter / 2),
                             diameter,
                             diameter
                             )
                         );

                    graphics.FillEllipse(
                        new SolidBrush(pacManColor),
                        (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2),
                        drawingCoordinatesY - (diameter / 2),
                        diameter,
                        diameter
                        );

                    graphics.FillEllipse(
                       new SolidBrush(Color.Black),
                       (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2) - (diameter / 2 - 5), drawingCoordinatesY - (diameter / 2 - 17),
                       diameter / 5,
                       diameter / 5
                       );


                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, drawingCoordinatesY),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + 15+animateCoeficent, drawingCoordinatesY + quadrantDimension/2),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension+ 35-animateCoeficent, drawingCoordinatesY + quadrantDimension/2  ),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, drawingCoordinatesY)
                            });

                    animateCoeficent += 1;
                    if (animateCoeficent == 12)
                    {
                        animateCoeficent = 0;
                    }

                    System.Threading.Thread.Sleep(speedDrawing);

                    if (drawingCoordinatesY == (positionQuadrantY + 1) * quadrantDimension + quadrantDimension / 2 - 22)
                    {
                        EatPoint(positionQuadrantX, positionQuadrantY + 1);
                    }

                    if (drawingCoordinatesY == ((positionQuadrantY + 1) * quadrantDimension) + quadrantDimension / 2)
                    {
                        positionQuadrantY = positionQuadrantY + 1;
                    }
                    break;
            }

            return true;
        }

       /* private void movePacMan(int moveToX, int MoveToY, string moving)
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
                    for (int x = drawingCoordinatesX; x <= (moveToX * quadrantDimension) + (quadrantDimension / 2); x++)
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
                                new System.Drawing.Point(x + quadrantDimension/2, (this.positionQuadrantY * quadrantDimension)+35-coef),
                                new System.Drawing.Point(x, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2)
                            });

                        coef += 1;
                        if (coef == 12)
                        {
                            coef = 0;
                        }

                        if (engine.isDirectionChanged(movedDirection) == false && drawingCoordinatesX >= moveToX * quadrantDimension + quadrantDimension / 2 - 22)
                        {
                            this.positionQuadrantX = moveToX;
                            this.positionQuadrantY = MoveToY;
                            EatPoint(moveToX, MoveToY);
                        }

                        /*
                        If is direction is change on the move period, 
                        the logic check is the packMan is going to the next quadrant or not
                        #1#
                        if (engine.isDirectionChanged(movedDirection))
                        {
                            if (drawingCoordinatesX < moveToX * quadrantDimension && drawingCoordinatesX > (moveToX - 1) * quadrantDimension)
                            {
                                this.positionQuadrantX = moveToX - 1;
                                this.positionQuadrantY = MoveToY;
                            }
                            else
                            {
                                this.positionQuadrantX = moveToX;
                                this.positionQuadrantY = MoveToY;
                            }
                            break;
                        }
                        System.Threading.Thread.Sleep(speedDrawing);
                    }

                    break;

                case "Left":
                    for (int x = drawingCoordinatesX; x >= (moveToX * quadrantDimension) + (quadrantDimension / 2); x--)
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
                            new System.Drawing.Point(x-quadrantDimension/2+2, (this.positionQuadrantY * quadrantDimension)+35-coef),
                            new System.Drawing.Point(x, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2)
                        });

                        coef += 1;
                        if (coef == 12)
                        {
                            coef = 0;
                        }

                        if (engine.isDirectionChanged(movedDirection) == false && drawingCoordinatesX <= moveToX * quadrantDimension + quadrantDimension / 2 + 22)
                        {
                            this.positionQuadrantX = moveToX;
                            this.positionQuadrantY = MoveToY;
                            EatPoint(moveToX, MoveToY);
                        }

                        /*
                        If is direction is change on the move period, 
                        the logic check is the packMan is going to the next quadrant or not
                        #1#
                        if (engine.isDirectionChanged(movedDirection))
                        {
                            if (drawingCoordinatesX > moveToX * quadrantDimension + quadrantDimension && drawingCoordinatesX < (moveToX + 1) * quadrantDimension)
                            {
                                this.positionQuadrantX = moveToX + 1;
                                this.positionQuadrantY = MoveToY;
                            }
                            else
                            {
                                this.positionQuadrantX = moveToX;
                                this.positionQuadrantY = MoveToY;
                            }
                            break;
                        }
                        System.Threading.Thread.Sleep(speedDrawing);

                    }

                    break;

                case "Up":
                    for (int y = drawingCoordinatesY; y >= (MoveToY * quadrantDimension) + (quadrantDimension / 2); y--)
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
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + 35-coef, y - quadrantDimension/2+2  ),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, y)
                            });

                        coef += 1;
                        if (coef == 12)
                        {
                            coef = 0;
                        }

                        if (engine.isDirectionChanged(movedDirection) == false && drawingCoordinatesY <= MoveToY * quadrantDimension + quadrantDimension / 2 + 22)
                        {
                            this.positionQuadrantX = moveToX;
                            this.positionQuadrantY = MoveToY;
                            EatPoint(moveToX, MoveToY);
                        }
                        /*
                        If is direction is change on the move period, 
                        the logic check is the packMan is going to the next quadrant or not
                        #1#
                        if (engine.isDirectionChanged(movedDirection))
                        {

                            if (drawingCoordinatesY > MoveToY * 50 + quadrantDimension && drawingCoordinatesY <= (MoveToY + 1) * quadrantDimension)
                            {
                                {
                                    this.positionQuadrantX = moveToX;
                                    this.positionQuadrantY = MoveToY + 1;
                                }
                            }
                            else
                            {
                                this.positionQuadrantX = moveToX;
                                this.positionQuadrantY = MoveToY;
                            }
                            break;
                        }
                        System.Threading.Thread.Sleep(speedDrawing);

                    }

                    break;

                case "Down":
                    for (int y = drawingCoordinatesY; y <= (MoveToY * quadrantDimension) + (quadrantDimension / 2); y++)
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
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension+ 35-coef, y + quadrantDimension/2  ),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, y)
                            });

                        coef += 1;
                        if (coef == 12)
                        {
                            coef = 0;
                        }

                        if (engine.isDirectionChanged(movedDirection) == false && drawingCoordinatesY >= MoveToY * quadrantDimension + quadrantDimension / 2 - 22)
                        {
                            this.positionQuadrantX = moveToX;
                            this.positionQuadrantY = MoveToY;
                            EatPoint(moveToX, MoveToY);
                        }

                        /*
                        If is direction is change on the move period, 
                        the logic check is the packMan is going to the next quadrant or not
                        #1#
                        if (engine.isDirectionChanged(movedDirection))
                        {
                            if (drawingCoordinatesY < MoveToY * quadrantDimension && drawingCoordinatesY > (MoveToY - 1) * quadrantDimension)
                            {
                                {
                                    this.positionQuadrantX = moveToX;
                                    this.positionQuadrantY = MoveToY - 1;
                                }
                            }
                            else
                            {
                                this.positionQuadrantX = moveToX;
                                this.positionQuadrantY = MoveToY;
                            }

                            break;
                        }
                        System.Threading.Thread.Sleep(speedDrawing);
                    }

                    break;
            }

        }*/

        private void DrawPoint(int quadrantX, int quadrantY)
        {
            string[] elements = engine.GetQuadrantElements(quadrantX, quadrantY);

            if (elements[1] == "1")
            {
                engine.DrawPoint(quadrantX, quadrantY);
            }
        }

        public void Move(string direction)
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

        public string getDirection()
        {
            return movedDirection;
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

        public void changeDirection(string newDirection)
        {
            this.movedDirection = newDirection;

            if (String.IsNullOrEmpty(previousDirection))
            {
                previousDirection = movedDirection;
            }

            Move(movedDirection);
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
                if (player != null)
                {
                    player.Dispose();
                }
            }
        }

    }
}