using System;
using System.Drawing;
using System.Threading.Tasks;

namespace FarthorlPacMan
{
    public class PacMan
    {
        private int Diameter => Global.Diameter;
        private int QuadrantDimension => Global.QuadrantSize;
        private int positionQuadrantX;
        private int positionQuadrantY;
        private string movedDirection;
        private string previousDirection;
        private int eatPoints;
        private string stopDirection = String.Empty;
        private int drawingCoordinatesX;
        private int drawingCoordinatesY;
        private int animateCoeficent;
        private Color pacManColor;
        private Boolean isAlive;
        private Graphics graphics;

        public PacMan()
        {
            this.isAlive = true;
            this.pacManColor = Color.Yellow;
        }

        public PacMan(int positionXQaundarnt, int positionYQuadrant, Graphics graphics)
            : this()
        {
            this.positionQuadrantX = positionXQaundarnt;
            this.positionQuadrantY = positionYQuadrant;
            drawingCoordinatesX = positionQuadrantX * QuadrantDimension + QuadrantDimension / 2;
            drawingCoordinatesY = positionQuadrantY * QuadrantDimension + QuadrantDimension / 2;
            this.graphics = graphics;
            this.initializePacMan();
        }

        private void initializePacMan()
        {
            EatPoint(positionQuadrantX, positionQuadrantY);
        }

        private void tryMoveRight()
        {
            if (this.positionQuadrantX < Engine.XMax - 1)
            {
                string[] elements = Engine.GetQuadrantElements(this.positionQuadrantX + 1, this.positionQuadrantY);

                if (isAlive && elements[0] == "0")
                {
                    previousDirection = "Right";
                    movedDirection = "Right";
                    stopDirection = String.Empty;
                }
                else if (elements[0] == "1")
                {
                    MoveCheck();
                }
            }
        }

        private void tryMoveLeft()
        {
            if (positionQuadrantX > 0)
            {
                string[] elements = Engine.GetQuadrantElements(positionQuadrantX - 1, positionQuadrantY);

                if (isAlive && elements[0] == "0")
                {
                    previousDirection = "Left";
                    movedDirection = "Left";
                    stopDirection = String.Empty;
                }
                else if (elements[0] == "1")
                {
                    MoveCheck();
                }
            }
        }

        private void tryMoveUp()
        {
            if (positionQuadrantY > 0)
            {
                string[] elements = Engine.GetQuadrantElements(positionQuadrantX, positionQuadrantY - 1);

                if (isAlive && elements[0] == "0")
                {
                    previousDirection = "Up";
                    movedDirection = "Up";
                    stopDirection = String.Empty;
                }
                else if (elements[0] == "1")
                {
                    MoveCheck();
                }
            }
        }

        private void tryMoveDown()
        {
            if (positionQuadrantY < Engine.YMax - 1)
            {
                string[] elements = Engine.GetQuadrantElements(positionQuadrantX, positionQuadrantY + 1);

                if (isAlive && elements[0] == "0")
                {
                    this.previousDirection = "Down";
                    this.movedDirection = "Down";
                    stopDirection = String.Empty;

                }
                else if (elements[0] == "1")
                {
                    MoveCheck();
                }
            }
        }

        private void MoveCheck()
        {
            if (movedDirection == String.Empty)
            {
                previousDirection = String.Empty;
                stopDirection = movedDirection;
            }
            movedDirection = String.Empty;
            this.Move(previousDirection);
        }

        public async Task<bool> Run(string direction)
        {
            if (drawingCoordinatesX==positionQuadrantX * QuadrantDimension + QuadrantDimension /2 && drawingCoordinatesY==positionQuadrantY*QuadrantDimension + QuadrantDimension/2)
            {
                Move(direction);
            }

            if (Engine.isDirectionChanged(movedDirection))
            {
                movedDirection = Engine.MoveDirection;
            }

            switch (movedDirection)
            {
                case "Right":

                    if (drawingCoordinatesX < (Engine.XMax-1) * QuadrantDimension + QuadrantDimension/2)
                    {
                        drawingCoordinatesX += 1;
                    }

                    graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            drawingCoordinatesX - 1 - (Diameter / 2),
                            (this.positionQuadrantY * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                            Diameter,
                            Diameter
                            )
                        );

                    graphics.FillEllipse(
                           new SolidBrush(pacManColor),
                           drawingCoordinatesX - (Diameter / 2),
                           (this.positionQuadrantY * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                           Diameter,
                           Diameter
                           );

                    graphics.FillEllipse(
                       new SolidBrush(Color.Black),
                       drawingCoordinatesX - (Diameter / 2 - 20),
                       (this.positionQuadrantY * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2 - 5),
                       Diameter / 5,
                       Diameter / 5
                       );

                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(drawingCoordinatesX, (this.positionQuadrantY * QuadrantDimension)+QuadrantDimension/2),
                                new System.Drawing.Point(drawingCoordinatesX + QuadrantDimension/2, (this.positionQuadrantY * QuadrantDimension)+15+animateCoeficent),
                                new System.Drawing.Point(drawingCoordinatesX + QuadrantDimension/2, (this.positionQuadrantY * QuadrantDimension)+35-animateCoeficent),
                                new System.Drawing.Point(drawingCoordinatesX, (this.positionQuadrantY * QuadrantDimension)+QuadrantDimension/2)
                            });

                    ChangeCoefficient();

                    if (drawingCoordinatesX == (positionQuadrantX + 1) * QuadrantDimension + QuadrantDimension / 2 - 22)
                    {
                        EatPoint(positionQuadrantX + 1, positionQuadrantY);
                    }

                    if (drawingCoordinatesX == ((positionQuadrantX + 1) * QuadrantDimension) + 25)
                    {
                        positionQuadrantX = positionQuadrantX + 1;

                    }
                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;

                case "Left":

                    if (drawingCoordinatesX > (positionQuadrantX-1) * QuadrantDimension + QuadrantDimension / 2 && drawingCoordinatesX > QuadrantDimension / 2)
                    {
                        drawingCoordinatesX -= 1;
                    }

                    graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            drawingCoordinatesX + 1 - (Diameter / 2),
                            (this.positionQuadrantY * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                            Diameter,
                            Diameter
                            )
                        );

                    graphics.FillEllipse(
                        new SolidBrush(pacManColor), drawingCoordinatesX - (Diameter / 2),
                        (this.positionQuadrantY * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                        Diameter,
                        Diameter
                        );

                    graphics.FillEllipse(
                        new SolidBrush(Color.Black),
                        drawingCoordinatesX - (Diameter / 2 - 10),
                        (this.positionQuadrantY * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2 - 5),
                        Diameter / 5,
                        Diameter / 5
                        );

                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                            new System.Drawing.Point(drawingCoordinatesX, (this.positionQuadrantY * QuadrantDimension)+QuadrantDimension/2),
                            new System.Drawing.Point(drawingCoordinatesX-QuadrantDimension/2+2, (this.positionQuadrantY * QuadrantDimension)+15+animateCoeficent),
                            new System.Drawing.Point(drawingCoordinatesX-QuadrantDimension/2+2, (this.positionQuadrantY * QuadrantDimension)+35-animateCoeficent),
                            new System.Drawing.Point(drawingCoordinatesX, (this.positionQuadrantY * QuadrantDimension)+QuadrantDimension/2)
                        });

                    ChangeCoefficient();

                    if (drawingCoordinatesX == (positionQuadrantX - 1) * QuadrantDimension + QuadrantDimension / 2 + 22)
                    {
                        EatPoint(positionQuadrantX - 1, positionQuadrantY);
                    }

                    if (drawingCoordinatesX == ((positionQuadrantX - 1) * QuadrantDimension) + 25)
                    {
                        positionQuadrantX -= 1;
                    }

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;

                case "Up":

                    if (drawingCoordinatesY > (positionQuadrantY-1) * QuadrantDimension + QuadrantDimension / 2 && drawingCoordinatesY > QuadrantDimension / 2)
                    {
                        drawingCoordinatesY -= 1;
                    }

                    graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            (this.positionQuadrantX * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                            drawingCoordinatesY + 1 - (Diameter / 2),
                            Diameter,
                            Diameter
                            )
                        );

                    graphics.FillEllipse(
                        new SolidBrush(pacManColor),
                        (this.positionQuadrantX * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                        drawingCoordinatesY - (Diameter / 2),
                        Diameter,
                        Diameter
                        );

                    graphics.FillEllipse(
                    new SolidBrush(Color.Black),
                    (this.positionQuadrantX * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2 - 5), drawingCoordinatesY - (Diameter / 2 - 10),
                    Diameter / 5,
                    Diameter / 5
                    );

                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(this.positionQuadrantX * QuadrantDimension + QuadrantDimension/2, drawingCoordinatesY),
                                new System.Drawing.Point(this.positionQuadrantX * QuadrantDimension + 15+animateCoeficent, drawingCoordinatesY - QuadrantDimension/2+2),
                                new System.Drawing.Point(this.positionQuadrantX * QuadrantDimension + 35-animateCoeficent, drawingCoordinatesY - QuadrantDimension/2+2),
                                new System.Drawing.Point(this.positionQuadrantX * QuadrantDimension + QuadrantDimension/2, drawingCoordinatesY)
                            });

                    ChangeCoefficient();

                    if (drawingCoordinatesY == (positionQuadrantY - 1) * QuadrantDimension + QuadrantDimension / 2 + 22)
                    {
                        EatPoint(positionQuadrantX, positionQuadrantY - 1);
                    }

                    if (drawingCoordinatesY == ((positionQuadrantY - 1) * QuadrantDimension) + QuadrantDimension/2)
                    {
                        positionQuadrantY -= 1;
                    }

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;

                case "Down":

                    if (drawingCoordinatesY < (Engine.YMax-1) * QuadrantDimension + QuadrantDimension / 2)
                    {
                        drawingCoordinatesY += 1;
                    }

                    graphics.DrawEllipse(
                         new Pen(Color.Black),
                         new Rectangle(
                             (this.positionQuadrantX * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                             drawingCoordinatesY - 1 - (Diameter / 2),
                             Diameter,
                             Diameter
                             )
                         );

                    graphics.FillEllipse(
                        new SolidBrush(pacManColor),
                        (this.positionQuadrantX * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                        drawingCoordinatesY - (Diameter / 2),
                        Diameter,
                        Diameter
                        );

                    graphics.FillEllipse(
                       new SolidBrush(Color.Black),
                       (this.positionQuadrantX * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2 - 5), drawingCoordinatesY - (Diameter / 2 - 17),
                       Diameter / 5,
                       Diameter / 5
                       );


                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(this.positionQuadrantX * QuadrantDimension + QuadrantDimension/2, drawingCoordinatesY),
                                new System.Drawing.Point(this.positionQuadrantX * QuadrantDimension + 15+animateCoeficent, drawingCoordinatesY + QuadrantDimension/2),
                                new System.Drawing.Point(this.positionQuadrantX * QuadrantDimension+ 35-animateCoeficent, drawingCoordinatesY + QuadrantDimension/2  ),
                                new System.Drawing.Point(this.positionQuadrantX * QuadrantDimension + QuadrantDimension/2, drawingCoordinatesY)
                            });

                    ChangeCoefficient();

                    if (drawingCoordinatesY == (positionQuadrantY + 1) * QuadrantDimension + QuadrantDimension / 2 - 22)
                    {
                        EatPoint(positionQuadrantX, positionQuadrantY + 1);
                    }

                    if (drawingCoordinatesY == ((positionQuadrantY + 1) * QuadrantDimension) + QuadrantDimension / 2)
                    {
                        positionQuadrantY = positionQuadrantY + 1;
                    }
                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;
            }

            return true;
        }

        private void ChangeCoefficient()
        {
            animateCoeficent += 1;
            if (animateCoeficent == 12)
            {
                animateCoeficent = 0;
            }
        }

        public void Move(string direction)
        {
            if (!string.IsNullOrEmpty(direction) && direction != stopDirection)
            {
                if (direction == "Right")
                {
                    tryMoveRight();
                }
                else if (direction == "Left")
                {
                    tryMoveLeft();
                }
                else if (direction == "Up")
                {
                    tryMoveUp();
                }
                else if (direction == "Down")
                {
                    tryMoveDown();
                }
            }
        }

        public void DrawPacMan()
        {
            try
            {
                graphics.FillEllipse(
             new SolidBrush(pacManColor),
             (drawingCoordinatesX) - (Diameter / 2),
             ((drawingCoordinatesY) - (Diameter / 2)),
             Diameter,
             Diameter
             );
            }
            catch (Exception)
            {
                throw;
            }
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
            string[] elements = Engine.GetQuadrantElements(quadrantX, quadrantY);

            if (elements[1] == "1")
            {
                this.eatPoints = this.eatPoints + int.Parse(elements[1]);
                elements[1] = "0";
                SoundPlayer.Play("eatfruit");
                Engine.EatPointAndUpdateMatrix(quadrantX, quadrantY, elements);
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
    }
}