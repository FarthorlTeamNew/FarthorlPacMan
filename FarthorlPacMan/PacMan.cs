using System;
using System.Drawing;
using System.Threading.Tasks;

namespace FarthorlPacMan
{
    public class PacMan : Participant
    {
        private const int PacManDiameter = 35;
        private int eatPoints;
        //private string stopDirection = String.Empty;
        private int animateCoeficent;
        private Color pacManColor = Color.Yellow;
        public Boolean isAlive = true;
        private Graphics graphics;

        public PacMan(int positionXQaundarnt, int positionYQuadrant, Graphics graphics) : base(positionXQaundarnt, positionYQuadrant)
        {
            base.DrawingCoordinatesX = base.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2;
            base.DrawingCoordinatesY = base.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2;
            this.graphics = graphics;
            this.initializePacMan();
        }

        private void initializePacMan()
        {
            EatPoint(base.PositionQuadrantX, base.PositionQuadrantY);
        }

        private void TryMoveThere(string[] quadrantToMove, string toDirection)
        {
            if (isAlive && quadrantToMove[0] == "0")
            {
                base.PreviousDirection = toDirection;
                base.MovedDirection = toDirection;
               // stopDirection = String.Empty;
            }
            else if (quadrantToMove[0] == "1")
            {
                MoveCheck();
            }
        }

        private void MoveCheck()
        {
            if (base.MovedDirection == String.Empty)
            {
                base.PreviousDirection = String.Empty;
               // stopDirection = base.MovedDirection;
            }
            base.MovedDirection = String.Empty;
            this.Move(base.PreviousDirection);
        }

        public void Move(string direction)
        {
            if (!string.IsNullOrEmpty(direction) && isAlive /*&& direction != stopDirection*/)
            {
                if (direction == "Right")
                {
                    if (base.PositionQuadrantX < Engine.XMax - 1)
                    {
                        string[] quadrantToMove = Engine.GetQuadrantElements(base.PositionQuadrantX + 1, base.PositionQuadrantY);
                        TryMoveThere(quadrantToMove, direction);
                    }
                    else
                    {
                        MoveCheck();
                    }
                }
                else if (direction == "Left")
                {
                    if (base.PositionQuadrantX > 0)
                    {
                        string[] quadrantToMove = Engine.GetQuadrantElements(base.PositionQuadrantX - 1, base.PositionQuadrantY);
                        TryMoveThere(quadrantToMove, direction);
                    }
                    else
                    {
                        MoveCheck();
                    }
                }
                else if (direction == "Up")
                {
                    if (base.PositionQuadrantY > 0)
                    {
                        string[] quadrantToMove = Engine.GetQuadrantElements(base.PositionQuadrantX, base.PositionQuadrantY - 1);
                        TryMoveThere(quadrantToMove, direction);
                    }
                    else
                    {
                        MoveCheck();
                    }
                }
                else if (direction == "Down")
                {
                    if (base.PositionQuadrantY < Engine.YMax - 1)
                    {
                        string[] quadrantToMove = Engine.GetQuadrantElements(base.PositionQuadrantX, base.PositionQuadrantY + 1);
                        TryMoveThere(quadrantToMove, direction);
                    }
                    else
                    {
                        MoveCheck();
                    }
                }
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

        public async Task<bool> Run(string direction)
        {
            if (base.DrawingCoordinatesX == base.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2
                && base.DrawingCoordinatesY == base.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2)
            {
                Move(direction);
            }

            if (Engine.isDirectionChanged(base.MovedDirection) && isAlive)
            {
                base.MovedDirection = Engine.MoveDirection;
                // next we try to return to the same quadrant we have been. This prevent bug to pass throught walls.
                string[] quadrantToMove = Engine.GetQuadrantElements(base.PositionQuadrantX, base.PositionQuadrantY);
                TryMoveThere(quadrantToMove, base.MovedDirection);
            }

            if (!isAlive)
            {
                this.MovedDirection = string.Empty;
            }

            switch (base.MovedDirection)
            {
                case "Right":

                    if (base.DrawingCoordinatesX < (Engine.XMax - 1) * Global.QuadrantSize + Global.QuadrantSize / 2)
                    {
                        base.DrawingCoordinatesX += 1;
                    }

                    graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            base.DrawingCoordinatesX - 1 - (PacManDiameter / 2),
                            (base.PositionQuadrantY * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                            PacManDiameter,
                            PacManDiameter
                            )
                        );

                    graphics.FillEllipse(
                           new SolidBrush(pacManColor),
                           base.DrawingCoordinatesX - (PacManDiameter / 2),
                           (base.PositionQuadrantY * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                           PacManDiameter,
                           PacManDiameter
                           );

                    graphics.FillEllipse(
                       new SolidBrush(Color.Black),
                       base.DrawingCoordinatesX - (PacManDiameter / 2 - 20),
                       (base.PositionQuadrantY * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2 - 5),
                       PacManDiameter / 5,
                       PacManDiameter / 5
                       );

                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(base.DrawingCoordinatesX, (base.PositionQuadrantY * Global.QuadrantSize)+Global.QuadrantSize/2),
                                new System.Drawing.Point(base.DrawingCoordinatesX + Global.QuadrantSize/2, (base.PositionQuadrantY * Global.QuadrantSize)+15+animateCoeficent),
                                new System.Drawing.Point(base.DrawingCoordinatesX + Global.QuadrantSize/2, (base.PositionQuadrantY * Global.QuadrantSize)+35-animateCoeficent),
                                new System.Drawing.Point(base.DrawingCoordinatesX, (base.PositionQuadrantY * Global.QuadrantSize)+Global.QuadrantSize/2)
                            });

                    ChangeCoefficient();

                    if (base.DrawingCoordinatesX == (base.PositionQuadrantX + 1) * Global.QuadrantSize + Global.QuadrantSize / 2 - 22)
                    {
                        EatPoint(base.PositionQuadrantX + 1, base.PositionQuadrantY);
                    }

                    if (base.DrawingCoordinatesX == ((base.PositionQuadrantX + 1) * Global.QuadrantSize) + 25)
                    {
                        base.PositionQuadrantX = base.PositionQuadrantX + 1;

                    }
                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;

                case "Left":

                    if (base.DrawingCoordinatesX > (base.PositionQuadrantX - 1) * Global.QuadrantSize + Global.QuadrantSize / 2 && base.DrawingCoordinatesX > Global.QuadrantSize / 2)
                    {
                        base.DrawingCoordinatesX -= 1;
                    }

                    graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            base.DrawingCoordinatesX + 1 - (PacManDiameter / 2),
                            (base.PositionQuadrantY * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                            PacManDiameter,
                            PacManDiameter
                            )
                        );

                    graphics.FillEllipse(
                        new SolidBrush(pacManColor), base.DrawingCoordinatesX - (PacManDiameter / 2),
                        (base.PositionQuadrantY * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                        PacManDiameter,
                        PacManDiameter
                        );

                    graphics.FillEllipse(
                        new SolidBrush(Color.Black),
                        base.DrawingCoordinatesX - (PacManDiameter / 2 - 10),
                        (base.PositionQuadrantY * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2 - 5),
                        PacManDiameter / 5,
                        PacManDiameter / 5
                        );

                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                            new System.Drawing.Point(base.DrawingCoordinatesX, (base.PositionQuadrantY * Global.QuadrantSize)+Global.QuadrantSize/2),
                            new System.Drawing.Point(base.DrawingCoordinatesX-Global.QuadrantSize/2+2, (base.PositionQuadrantY * Global.QuadrantSize)+15+animateCoeficent),
                            new System.Drawing.Point(base.DrawingCoordinatesX-Global.QuadrantSize/2+2, (base.PositionQuadrantY * Global.QuadrantSize)+35-animateCoeficent),
                            new System.Drawing.Point(base.DrawingCoordinatesX, (base.PositionQuadrantY * Global.QuadrantSize)+Global.QuadrantSize/2)
                        });

                    ChangeCoefficient();

                    if (base.DrawingCoordinatesX == (base.PositionQuadrantX - 1) * Global.QuadrantSize + Global.QuadrantSize / 2 + 22)
                    {
                        EatPoint(base.PositionQuadrantX - 1, base.PositionQuadrantY);
                    }

                    if (base.DrawingCoordinatesX == ((base.PositionQuadrantX - 1) * Global.QuadrantSize) + 25)
                    {
                        base.PositionQuadrantX -= 1;
                    }

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;

                case "Up":

                    if (base.DrawingCoordinatesY > (base.PositionQuadrantY - 1) * Global.QuadrantSize + Global.QuadrantSize / 2 && base.DrawingCoordinatesY > Global.QuadrantSize / 2)
                    {
                        base.DrawingCoordinatesY -= 1;
                    }

                    graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            (base.PositionQuadrantX * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                            base.DrawingCoordinatesY + 1 - (PacManDiameter / 2),
                            PacManDiameter,
                            PacManDiameter
                            )
                        );

                    graphics.FillEllipse(
                        new SolidBrush(pacManColor),
                        (base.PositionQuadrantX * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                        base.DrawingCoordinatesY - (PacManDiameter / 2),
                        PacManDiameter,
                        PacManDiameter
                        );

                    graphics.FillEllipse(
                    new SolidBrush(Color.Black),
                    (base.PositionQuadrantX * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2 - 5), base.DrawingCoordinatesY - (PacManDiameter / 2 - 10),
                    PacManDiameter / 5,
                    PacManDiameter / 5
                    );

                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(base.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize/2, base.DrawingCoordinatesY),
                                new System.Drawing.Point(base.PositionQuadrantX * Global.QuadrantSize + 15+animateCoeficent, base.DrawingCoordinatesY - Global.QuadrantSize/2+2),
                                new System.Drawing.Point(base.PositionQuadrantX * Global.QuadrantSize + 35-animateCoeficent, base.DrawingCoordinatesY - Global.QuadrantSize/2+2),
                                new System.Drawing.Point(base.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize/2, base.DrawingCoordinatesY)
                            });

                    ChangeCoefficient();

                    if (base.DrawingCoordinatesY == (base.PositionQuadrantY - 1) * Global.QuadrantSize + Global.QuadrantSize / 2 + 22)
                    {
                        EatPoint(base.PositionQuadrantX, base.PositionQuadrantY - 1);
                    }

                    if (base.DrawingCoordinatesY == ((base.PositionQuadrantY - 1) * Global.QuadrantSize) + Global.QuadrantSize / 2)
                    {
                        base.PositionQuadrantY -= 1;
                    }

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;

                case "Down":

                    if (base.DrawingCoordinatesY < (Engine.YMax - 1) * Global.QuadrantSize + Global.QuadrantSize / 2)
                    {
                        base.DrawingCoordinatesY += 1;
                    }

                    graphics.DrawEllipse(
                         new Pen(Color.Black),
                         new Rectangle(
                             (base.PositionQuadrantX * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                             base.DrawingCoordinatesY - 1 - (PacManDiameter / 2),
                             PacManDiameter,
                             PacManDiameter
                             )
                         );

                    graphics.FillEllipse(
                        new SolidBrush(pacManColor),
                        (base.PositionQuadrantX * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                        base.DrawingCoordinatesY - (PacManDiameter / 2),
                        PacManDiameter,
                        PacManDiameter
                        );

                    graphics.FillEllipse(
                       new SolidBrush(Color.Black),
                       (base.PositionQuadrantX * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2 - 5), base.DrawingCoordinatesY - (PacManDiameter / 2 - 17),
                       PacManDiameter / 5,
                       PacManDiameter / 5
                       );


                    graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(base.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize/2, base.DrawingCoordinatesY),
                                new System.Drawing.Point(base.PositionQuadrantX * Global.QuadrantSize + 15+animateCoeficent, base.DrawingCoordinatesY + Global.QuadrantSize/2),
                                new System.Drawing.Point(base.PositionQuadrantX * Global.QuadrantSize+ 35-animateCoeficent, base.DrawingCoordinatesY + Global.QuadrantSize/2  ),
                                new System.Drawing.Point(base.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize/2, base.DrawingCoordinatesY)
                            });

                    ChangeCoefficient();

                    if (base.DrawingCoordinatesY == (base.PositionQuadrantY + 1) * Global.QuadrantSize + Global.QuadrantSize / 2 - 22)
                    {
                        EatPoint(base.PositionQuadrantX, base.PositionQuadrantY + 1);
                    }

                    if (base.DrawingCoordinatesY == ((base.PositionQuadrantY + 1) * Global.QuadrantSize) + Global.QuadrantSize / 2)
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

        public void DrawPacMan()
        {
            try
            {
                graphics.FillEllipse(
                    new SolidBrush(pacManColor),
                    (base.DrawingCoordinatesX) - (PacManDiameter / 2),
                    ((base.DrawingCoordinatesY) - (PacManDiameter / 2)),
                    PacManDiameter,
                    PacManDiameter
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

        public int GetPositionX()
        {
            return this.PositionQuadrantX;
        }

        public int GetPositionY()
        {
            return this.PositionQuadrantY;
        }

        public string GetDirection()
        {
            return base.MovedDirection;
        }

        public void EatPoint(int quadrantX, int quadrantY) // TODO make drawing of points
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
    }
}