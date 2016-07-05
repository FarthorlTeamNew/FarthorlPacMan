using System;
using System.Drawing;
using System.Threading.Tasks;

namespace FarthorlPacMan
{
    public class PacMan : Participant
    {
        private int Diameter => Global.PacManDiameter;
        private int QuadrantDimension => Global.QuadrantSize;
        private int eatPoints;
        private string stopDirection = String.Empty;
        private int animateCoeficent;
        private Color pacManColor = Color.Yellow;
        private Boolean isAlive=true;
        private Graphics graphics;

        public PacMan(int positionXQaundarnt, int positionYQuadrant, Graphics graphics) : base(positionXQaundarnt, positionYQuadrant)
        {
            base.DrawingCoordinatesX = base.PositionQuadrantX * QuadrantDimension + QuadrantDimension / 2;
            base.DrawingCoordinatesY = base.PositionQuadrantY * QuadrantDimension + QuadrantDimension / 2;
            this.graphics = graphics;
            this.initializePacMan();
        }

        private void initializePacMan()
        {
            EatPoint(base.PositionQuadrantX, base.PositionQuadrantY);
        }

        private void tryMoveRight()
        {
            if (base.PositionQuadrantX < Engine.XMax - 1)
            {
                string[] elements = Engine.GetQuadrantElements(base.PositionQuadrantX + 1, base.PositionQuadrantY);

                if (isAlive && elements[0] == "0")
                {
                    base.PreviousDirection = "Right";
                    base.MovedDirection = "Right";
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
            if (base.PositionQuadrantX > 0)
            {
                string[] elements = Engine.GetQuadrantElements(base.PositionQuadrantX - 1, base.PositionQuadrantY);

                if (isAlive && elements[0] == "0")
                {
                    base.PreviousDirection = "Left";
                    base.MovedDirection = "Left";
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
            if (base.PositionQuadrantY > 0)
            {
                string[] elements = Engine.GetQuadrantElements(base.PositionQuadrantX, base.PositionQuadrantY - 1);

                if (isAlive && elements[0] == "0")
                {
                    base.PreviousDirection = "Up";
                    base.MovedDirection = "Up";
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
            if (base.PositionQuadrantY < Engine.YMax - 1)
            {
                string[] elements = Engine.GetQuadrantElements(base.PositionQuadrantX, base.PositionQuadrantY + 1);

                if (isAlive && elements[0] == "0")
                {
                    base.PreviousDirection = "Down";
                    base.MovedDirection = "Down";
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
            if (base.MovedDirection == String.Empty)
            {
                base.PreviousDirection = String.Empty;
                stopDirection = base.MovedDirection;
            }
            base.MovedDirection = String.Empty;
            this.Move(base.PreviousDirection);
        }

        public async Task<bool> Run(string direction)
        {
            if (base.DrawingCoordinatesX == base.PositionQuadrantX * QuadrantDimension + QuadrantDimension / 2 && base.DrawingCoordinatesY == base.PositionQuadrantY * QuadrantDimension + QuadrantDimension / 2)
            {
                Move(direction);
            }

            if (Engine.isDirectionChanged(base.MovedDirection))
            {
                base.MovedDirection = Engine.MoveDirection;
            }

            switch (base.MovedDirection)
            {
                case "Right":

                    if (base.DrawingCoordinatesX < (Engine.XMax - 1) * QuadrantDimension + QuadrantDimension / 2)
                    {
                        base.DrawingCoordinatesX += 1;
                    }

                    graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            base.DrawingCoordinatesX - 1 - (Diameter / 2),
                            (base.PositionQuadrantY * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                            Diameter,
                            Diameter
                            )
                        );

                    graphics.FillEllipse(
                           new SolidBrush(pacManColor),
                           base.DrawingCoordinatesX - (Diameter / 2),
                           (base.PositionQuadrantY * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                           Diameter,
                           Diameter
                           );

                    graphics.FillEllipse(
                       new SolidBrush(Color.Black),
                       base.DrawingCoordinatesX - (Diameter / 2 - 20),
                       (base.PositionQuadrantY * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2 - 5),
                       Diameter / 5,
                       Diameter / 5
                       );

                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(base.DrawingCoordinatesX, (base.PositionQuadrantY * QuadrantDimension)+QuadrantDimension/2),
                                new System.Drawing.Point(base.DrawingCoordinatesX + QuadrantDimension/2, (base.PositionQuadrantY * QuadrantDimension)+15+animateCoeficent),
                                new System.Drawing.Point(base.DrawingCoordinatesX + QuadrantDimension/2, (base.PositionQuadrantY * QuadrantDimension)+35-animateCoeficent),
                                new System.Drawing.Point(base.DrawingCoordinatesX, (base.PositionQuadrantY * QuadrantDimension)+QuadrantDimension/2)
                            });

                    ChangeCoefficient();

                    if (base.DrawingCoordinatesX == (base.PositionQuadrantX + 1) * QuadrantDimension + QuadrantDimension / 2 - 22)
                    {
                        EatPoint(base.PositionQuadrantX + 1, base.PositionQuadrantY);
                    }

                    if (base.DrawingCoordinatesX == ((base.PositionQuadrantX + 1) * QuadrantDimension) + 25)
                    {
                        base.PositionQuadrantX = base.PositionQuadrantX + 1;

                    }
                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;

                case "Left":

                    if (base.DrawingCoordinatesX > (base.PositionQuadrantX - 1) * QuadrantDimension + QuadrantDimension / 2 && base.DrawingCoordinatesX > QuadrantDimension / 2)
                    {
                        base.DrawingCoordinatesX -= 1;
                    }

                    graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            base.DrawingCoordinatesX + 1 - (Diameter / 2),
                            (base.PositionQuadrantY * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                            Diameter,
                            Diameter
                            )
                        );

                    graphics.FillEllipse(
                        new SolidBrush(pacManColor), base.DrawingCoordinatesX - (Diameter / 2),
                        (base.PositionQuadrantY * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                        Diameter,
                        Diameter
                        );

                    graphics.FillEllipse(
                        new SolidBrush(Color.Black),
                        base.DrawingCoordinatesX - (Diameter / 2 - 10),
                        (base.PositionQuadrantY * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2 - 5),
                        Diameter / 5,
                        Diameter / 5
                        );

                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                            new System.Drawing.Point(base.DrawingCoordinatesX, (base.PositionQuadrantY * QuadrantDimension)+QuadrantDimension/2),
                            new System.Drawing.Point(base.DrawingCoordinatesX-QuadrantDimension/2+2, (base.PositionQuadrantY * QuadrantDimension)+15+animateCoeficent),
                            new System.Drawing.Point(base.DrawingCoordinatesX-QuadrantDimension/2+2, (base.PositionQuadrantY * QuadrantDimension)+35-animateCoeficent),
                            new System.Drawing.Point(base.DrawingCoordinatesX, (base.PositionQuadrantY * QuadrantDimension)+QuadrantDimension/2)
                        });

                    ChangeCoefficient();

                    if (base.DrawingCoordinatesX == (base.PositionQuadrantX - 1) * QuadrantDimension + QuadrantDimension / 2 + 22)
                    {
                        EatPoint(base.PositionQuadrantX - 1, base.PositionQuadrantY);
                    }

                    if (base.DrawingCoordinatesX == ((base.PositionQuadrantX - 1) * QuadrantDimension) + 25)
                    {
                        base.PositionQuadrantX -= 1;
                    }

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;

                case "Up":

                    if (base.DrawingCoordinatesY > (base.PositionQuadrantY - 1) * QuadrantDimension + QuadrantDimension / 2 && base.DrawingCoordinatesY > QuadrantDimension / 2)
                    {
                        base.DrawingCoordinatesY -= 1;
                    }

                    graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            (base.PositionQuadrantX * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                            base.DrawingCoordinatesY + 1 - (Diameter / 2),
                            Diameter,
                            Diameter
                            )
                        );

                    graphics.FillEllipse(
                        new SolidBrush(pacManColor),
                        (base.PositionQuadrantX * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                        base.DrawingCoordinatesY - (Diameter / 2),
                        Diameter,
                        Diameter
                        );

                    graphics.FillEllipse(
                    new SolidBrush(Color.Black),
                    (base.PositionQuadrantX * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2 - 5), base.DrawingCoordinatesY - (Diameter / 2 - 10),
                    Diameter / 5,
                    Diameter / 5
                    );

                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(base.PositionQuadrantX * QuadrantDimension + QuadrantDimension/2, base.DrawingCoordinatesY),
                                new System.Drawing.Point(base.PositionQuadrantX * QuadrantDimension + 15+animateCoeficent, base.DrawingCoordinatesY - QuadrantDimension/2+2),
                                new System.Drawing.Point(base.PositionQuadrantX * QuadrantDimension + 35-animateCoeficent, base.DrawingCoordinatesY - QuadrantDimension/2+2),
                                new System.Drawing.Point(base.PositionQuadrantX * QuadrantDimension + QuadrantDimension/2, base.DrawingCoordinatesY)
                            });

                    ChangeCoefficient();

                    if (base.DrawingCoordinatesY == (base.PositionQuadrantY - 1) * QuadrantDimension + QuadrantDimension / 2 + 22)
                    {
                        EatPoint(base.PositionQuadrantX, base.PositionQuadrantY - 1);
                    }

                    if (base.DrawingCoordinatesY == ((base.PositionQuadrantY - 1) * QuadrantDimension) + QuadrantDimension / 2)
                    {
                        base.PositionQuadrantY -= 1;
                    }

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;

                case "Down":

                    if (base.DrawingCoordinatesY < (Engine.YMax - 1) * QuadrantDimension + QuadrantDimension / 2)
                    {
                        base.DrawingCoordinatesY += 1;
                    }

                    graphics.DrawEllipse(
                         new Pen(Color.Black),
                         new Rectangle(
                             (base.PositionQuadrantX * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                             base.DrawingCoordinatesY - 1 - (Diameter / 2),
                             Diameter,
                             Diameter
                             )
                         );

                    graphics.FillEllipse(
                        new SolidBrush(pacManColor),
                        (base.PositionQuadrantX * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2),
                        base.DrawingCoordinatesY - (Diameter / 2),
                        Diameter,
                        Diameter
                        );

                    graphics.FillEllipse(
                       new SolidBrush(Color.Black),
                       (base.PositionQuadrantX * QuadrantDimension) + (QuadrantDimension / 2) - (Diameter / 2 - 5), base.DrawingCoordinatesY - (Diameter / 2 - 17),
                       Diameter / 5,
                       Diameter / 5
                       );


                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(base.PositionQuadrantX * QuadrantDimension + QuadrantDimension/2, base.DrawingCoordinatesY),
                                new System.Drawing.Point(base.PositionQuadrantX * QuadrantDimension + 15+animateCoeficent, base.DrawingCoordinatesY + QuadrantDimension/2),
                                new System.Drawing.Point(base.PositionQuadrantX * QuadrantDimension+ 35-animateCoeficent, base.DrawingCoordinatesY + QuadrantDimension/2  ),
                                new System.Drawing.Point(base.PositionQuadrantX * QuadrantDimension + QuadrantDimension/2, base.DrawingCoordinatesY)
                            });

                    ChangeCoefficient();

                    if (base.DrawingCoordinatesY == (base.PositionQuadrantY + 1) * QuadrantDimension + QuadrantDimension / 2 - 22)
                    {
                        EatPoint(base.PositionQuadrantX, base.PositionQuadrantY + 1);
                    }

                    if (base.DrawingCoordinatesY == ((base.PositionQuadrantY + 1) * QuadrantDimension) + QuadrantDimension / 2)
                    {
                        base.PositionQuadrantY = base.PositionQuadrantY + 1;
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
                    (base.DrawingCoordinatesX) - (Diameter / 2),
                    ((base.DrawingCoordinatesY) - (Diameter / 2)),
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

        public int GetQuadrantX()
        {
            return base.PositionQuadrantX;
        }

        public int GetQuadrantY()
        {
            return base.PositionQuadrantY;
        }

        public string GetDirection()
        {
            return base.MovedDirection;
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

        public void ChangeDirection(string newDirection)
        {
            base.MovedDirection = newDirection;

            if (String.IsNullOrEmpty(base.PreviousDirection))
            {
                base.PreviousDirection = base.MovedDirection;
            }

            Move(base.MovedDirection);
        }
    }
}