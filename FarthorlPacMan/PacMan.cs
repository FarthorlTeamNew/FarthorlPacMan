using System;
using System.Drawing;
using System.Threading.Tasks;

namespace FarthorlPacMan
{
    public class PacMan : Participant
    {
        private const int PacManDiameter = 35;
        private int _eatPoints;
        //private string stopDirection = String.Empty;
        private int _animateCoeficent;
        private Color _pacManColor = Color.Yellow;
        public Boolean IsAlive = true;
        private Graphics _graphics;

        public PacMan(int positionXQaundarnt, int positionYQuadrant, Graphics graphics) : base(positionXQaundarnt, positionYQuadrant)
        {
            this.DrawingCoordinatesX = this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2;
            this.DrawingCoordinatesY = this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2;
            this._graphics = graphics;
            this.InitializePacMan();
        }

        private void InitializePacMan()
        {
            this.EatPoint(this.PositionQuadrantX, this.PositionQuadrantY);
        }

        private void TryMoveThere(string[] quadrantToMove, string toDirection)
        {
            if (this.IsAlive && quadrantToMove[0] == "0")
            {
                this.PreviousDirection = toDirection;
                this.MovedDirection = toDirection;
               // stopDirection = String.Empty;
            }
            else if (quadrantToMove[0] == "1")
            {
                this.MoveCheck();
            }
        }

        private void MoveCheck()
        {
            if (this.MovedDirection == String.Empty)
            {
                this.PreviousDirection = String.Empty;
               // stopDirection = base.MovedDirection;
            }
            this.MovedDirection = String.Empty;
            this.Move(this.PreviousDirection);
        }

        public void Move(string direction)
        {
            if (!string.IsNullOrEmpty(direction) && this.IsAlive /*&& direction != stopDirection*/)
            {
                if (direction == "Right")
                {
                    if (this.PositionQuadrantX < Engine.XMax - 1)
                    {
                        string[] quadrantToMove = Engine.GetQuadrantElements(this.PositionQuadrantX + 1, this.PositionQuadrantY);
                        this.TryMoveThere(quadrantToMove, direction);
                    }
                    else
                    {
                        this.MoveCheck();
                    }
                }
                else if (direction == "Left")
                {
                    if (this.PositionQuadrantX > 0)
                    {
                        string[] quadrantToMove = Engine.GetQuadrantElements(this.PositionQuadrantX - 1, this.PositionQuadrantY);
                        this.TryMoveThere(quadrantToMove, direction);
                    }
                    else
                    {
                        this.MoveCheck();
                    }
                }
                else if (direction == "Up")
                {
                    if (this.PositionQuadrantY > 0)
                    {
                        string[] quadrantToMove = Engine.GetQuadrantElements(this.PositionQuadrantX, this.PositionQuadrantY - 1);
                        this.TryMoveThere(quadrantToMove, direction);
                    }
                    else
                    {
                        this.MoveCheck();
                    }
                }
                else if (direction == "Down")
                {
                    if (this.PositionQuadrantY < Engine.YMax - 1)
                    {
                        string[] quadrantToMove = Engine.GetQuadrantElements(this.PositionQuadrantX, this.PositionQuadrantY + 1);
                        this.TryMoveThere(quadrantToMove, direction);
                    }
                    else
                    {
                        this.MoveCheck();
                    }
                }
            }
        }

        public void ChangeDirection(string newDirection)
        {
            this.MovedDirection = newDirection;

            if (string.IsNullOrEmpty(this.PreviousDirection))
            {
                this.PreviousDirection = this.MovedDirection;
            }

            this.Move(this.MovedDirection);
        }

        public async Task<bool> Run(string direction)
        {
            if (this.DrawingCoordinatesX == this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2
                && this.DrawingCoordinatesY == this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2)
            {
                this.Move(direction);
            }

            if (Engine.IsDirectionChanged(this.MovedDirection) && this.IsAlive)
            {
                this.MovedDirection = Engine.MoveDirection;
                // next we try to return to the same quadrant we have been. This prevent bug to pass throught walls.
                string[] quadrantToMove = Engine.GetQuadrantElements(this.PositionQuadrantX, this.PositionQuadrantY);
                this.TryMoveThere(quadrantToMove, this.MovedDirection);
            }

            if (!this.IsAlive)
            {
                this.MovedDirection = string.Empty;
            }

            switch (this.MovedDirection)
            {
                case "Right":

                    if (this.DrawingCoordinatesX < (Engine.XMax - 1) * Global.QuadrantSize + Global.QuadrantSize / 2)
                    {
                        this.DrawingCoordinatesX += 1;
                    }

                    this._graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            this.DrawingCoordinatesX - 1 - (PacManDiameter / 2),
                            (this.PositionQuadrantY * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                            PacManDiameter,
                            PacManDiameter
                            )
                        );

                    this._graphics.FillEllipse(
                           new SolidBrush(this._pacManColor),
                           this.DrawingCoordinatesX - (PacManDiameter / 2),
                           (this.PositionQuadrantY * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                           PacManDiameter,
                           PacManDiameter
                           );

                    this._graphics.FillEllipse(
                       new SolidBrush(Color.Black),
                       this.DrawingCoordinatesX - (PacManDiameter / 2 - 20),
                       (this.PositionQuadrantY * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2 - 5),
                       PacManDiameter / 5,
                       PacManDiameter / 5
                       );

                    this._graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(this.DrawingCoordinatesX, (this.PositionQuadrantY * Global.QuadrantSize)+Global.QuadrantSize/2),
                                new System.Drawing.Point(this.DrawingCoordinatesX + Global.QuadrantSize/2, (this.PositionQuadrantY * Global.QuadrantSize)+15+this._animateCoeficent),
                                new System.Drawing.Point(this.DrawingCoordinatesX + Global.QuadrantSize/2, (this.PositionQuadrantY * Global.QuadrantSize)+35-this._animateCoeficent),
                                new System.Drawing.Point(this.DrawingCoordinatesX, (this.PositionQuadrantY * Global.QuadrantSize)+Global.QuadrantSize/2)
                            });

                    this.ChangeCoefficient();

                    if (this.DrawingCoordinatesX == (this.PositionQuadrantX + 1) * Global.QuadrantSize + Global.QuadrantSize / 2 - 22)
                    {
                        this.EatPoint(this.PositionQuadrantX + 1, this.PositionQuadrantY);
                    }

                    if (this.DrawingCoordinatesX == ((this.PositionQuadrantX + 1) * Global.QuadrantSize) + 25)
                    {
                        this.PositionQuadrantX = this.PositionQuadrantX + 1;

                    }
                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;

                case "Left":

                    if (this.DrawingCoordinatesX > (this.PositionQuadrantX - 1) * Global.QuadrantSize + Global.QuadrantSize / 2 && this.DrawingCoordinatesX > Global.QuadrantSize / 2)
                    {
                        this.DrawingCoordinatesX -= 1;
                    }

                    this._graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            this.DrawingCoordinatesX + 1 - (PacManDiameter / 2),
                            (this.PositionQuadrantY * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                            PacManDiameter,
                            PacManDiameter
                            )
                        );

                    this._graphics.FillEllipse(
                        new SolidBrush(this._pacManColor), this.DrawingCoordinatesX - (PacManDiameter / 2),
                        (this.PositionQuadrantY * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                        PacManDiameter,
                        PacManDiameter
                        );

                    this._graphics.FillEllipse(
                        new SolidBrush(Color.Black),
                        this.DrawingCoordinatesX - (PacManDiameter / 2 - 10),
                        (this.PositionQuadrantY * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2 - 5),
                        PacManDiameter / 5,
                        PacManDiameter / 5
                        );

                    this._graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                            new System.Drawing.Point(this.DrawingCoordinatesX, (this.PositionQuadrantY * Global.QuadrantSize)+Global.QuadrantSize/2),
                            new System.Drawing.Point(this.DrawingCoordinatesX-Global.QuadrantSize/2+2, (this.PositionQuadrantY * Global.QuadrantSize)+15+this._animateCoeficent),
                            new System.Drawing.Point(this.DrawingCoordinatesX-Global.QuadrantSize/2+2, (this.PositionQuadrantY * Global.QuadrantSize)+35-this._animateCoeficent),
                            new System.Drawing.Point(this.DrawingCoordinatesX, (this.PositionQuadrantY * Global.QuadrantSize)+Global.QuadrantSize/2)
                        });

                    this.ChangeCoefficient();

                    if (this.DrawingCoordinatesX == (this.PositionQuadrantX - 1) * Global.QuadrantSize + Global.QuadrantSize / 2 + 22)
                    {
                        this.EatPoint(this.PositionQuadrantX - 1, this.PositionQuadrantY);
                    }

                    if (this.DrawingCoordinatesX == ((this.PositionQuadrantX - 1) * Global.QuadrantSize) + 25)
                    {
                        this.PositionQuadrantX -= 1;
                    }

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;

                case "Up":

                    if (this.DrawingCoordinatesY > (this.PositionQuadrantY - 1) * Global.QuadrantSize + Global.QuadrantSize / 2 && this.DrawingCoordinatesY > Global.QuadrantSize / 2)
                    {
                        this.DrawingCoordinatesY -= 1;
                    }

                    this._graphics.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            (this.PositionQuadrantX * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                            this.DrawingCoordinatesY + 1 - (PacManDiameter / 2),
                            PacManDiameter,
                            PacManDiameter
                            )
                        );

                    this._graphics.FillEllipse(
                        new SolidBrush(this._pacManColor),
                        (this.PositionQuadrantX * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                        this.DrawingCoordinatesY - (PacManDiameter / 2),
                        PacManDiameter,
                        PacManDiameter
                        );

                    this._graphics.FillEllipse(
                    new SolidBrush(Color.Black),
                    (this.PositionQuadrantX * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2 - 5), this.DrawingCoordinatesY - (PacManDiameter / 2 - 10),
                    PacManDiameter / 5,
                    PacManDiameter / 5
                    );

                    this._graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize/2, this.DrawingCoordinatesY),
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + 15+this._animateCoeficent, this.DrawingCoordinatesY - Global.QuadrantSize/2+2),
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + 35-this._animateCoeficent, this.DrawingCoordinatesY - Global.QuadrantSize/2+2),
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize/2, this.DrawingCoordinatesY)
                            });

                    this.ChangeCoefficient();

                    if (this.DrawingCoordinatesY == (this.PositionQuadrantY - 1) * Global.QuadrantSize + Global.QuadrantSize / 2 + 22)
                    {
                        this.EatPoint(this.PositionQuadrantX, this.PositionQuadrantY - 1);
                    }

                    if (this.DrawingCoordinatesY == ((this.PositionQuadrantY - 1) * Global.QuadrantSize) + Global.QuadrantSize / 2)
                    {
                        this.PositionQuadrantY -= 1;
                    }

                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;

                case "Down":

                    if (this.DrawingCoordinatesY < (Engine.YMax - 1) * Global.QuadrantSize + Global.QuadrantSize / 2)
                    {
                        this.DrawingCoordinatesY += 1;
                    }

                    this._graphics.DrawEllipse(
                         new Pen(Color.Black),
                         new Rectangle(
                             (this.PositionQuadrantX * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                             this.DrawingCoordinatesY - 1 - (PacManDiameter / 2),
                             PacManDiameter,
                             PacManDiameter
                             )
                         );

                    this._graphics.FillEllipse(
                        new SolidBrush(this._pacManColor),
                        (this.PositionQuadrantX * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2),
                        this.DrawingCoordinatesY - (PacManDiameter / 2),
                        PacManDiameter,
                        PacManDiameter
                        );

                    this._graphics.FillEllipse(
                       new SolidBrush(Color.Black),
                       (this.PositionQuadrantX * Global.QuadrantSize) + (Global.QuadrantSize / 2) - (PacManDiameter / 2 - 5), this.DrawingCoordinatesY - (PacManDiameter / 2 - 17),
                       PacManDiameter / 5,
                       PacManDiameter / 5
                       );


                    this._graphics.FillPolygon(new SolidBrush(Color.Black), new System.Drawing.Point[] {
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize/2, this.DrawingCoordinatesY),
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + 15+this._animateCoeficent, this.DrawingCoordinatesY + Global.QuadrantSize/2),
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize+ 35-this._animateCoeficent, this.DrawingCoordinatesY + Global.QuadrantSize/2  ),
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize/2, this.DrawingCoordinatesY)
                            });

                    this.ChangeCoefficient();

                    if (this.DrawingCoordinatesY == (this.PositionQuadrantY + 1) * Global.QuadrantSize + Global.QuadrantSize / 2 - 22)
                    {
                        this.EatPoint(this.PositionQuadrantX, this.PositionQuadrantY + 1);
                    }

                    if (this.DrawingCoordinatesY == ((this.PositionQuadrantY + 1) * Global.QuadrantSize) + Global.QuadrantSize / 2)
                    {
                        this.PositionQuadrantY = this.PositionQuadrantY + 1;
                    }
                    System.Threading.Thread.Sleep(Global.DrawingSpeed);
                    break;
            }

            return true;
        }

        private void ChangeCoefficient()
        {
            this._animateCoeficent += 1;
            if (this._animateCoeficent == 12)
            {
                this._animateCoeficent = 0;
            }
        }

        public void DrawPacMan()
        {
            this._graphics.FillEllipse(
                new SolidBrush(this._pacManColor),
                (this.DrawingCoordinatesX) - (PacManDiameter / 2),
                ((this.DrawingCoordinatesY) - (PacManDiameter / 2)),
                PacManDiameter,
                PacManDiameter
                );
        }

        public int GetScore()
        {
            return this._eatPoints;
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
            return this.MovedDirection;
        }

        public void EatPoint(int quadrantX, int quadrantY) // TODO make drawing of points
        {
            string[] elements = Engine.GetQuadrantElements(quadrantX, quadrantY);

            if (elements[1] == "1")
            {
                this._eatPoints = this._eatPoints + int.Parse(elements[1]);
                elements[1] = "0";
                SoundPlayer.Play("eatfruit");
                Engine.EatPointAndUpdateMatrix(quadrantX, quadrantY, elements);
            }
        }
    }
}