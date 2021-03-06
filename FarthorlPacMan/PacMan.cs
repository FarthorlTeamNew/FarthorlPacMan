﻿using System;
using System.Drawing;
using System.Threading.Tasks;

namespace FarthorlPacMan
{
    public class PacMan : Participant
    {
        private const int PacManDiameter = 35;
        private const string Left = "Left";
        private const string Up = "Up";
        private const string Right = "Right";
        private const string Down = "Down";
        private const string TouchWall = "1";
        private const string FreeDirection = "0";
        private int animateCoeficent;
        public bool IsAlive = true;
        private Graphics graphicsPacMan;

        public int EatPoints { get; private set; }

        public PacMan(int positionXQaundarnt, int positionYQuadrant, Graphics graphicsPacMan) : base(positionXQaundarnt, positionYQuadrant)
        {
            this.DrawingCoordinatesX = this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2;
            this.DrawingCoordinatesY = this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2;
            this.graphicsPacMan = graphicsPacMan;
            this.InitializePacMan();
        }

        private void InitializePacMan()
        {
            this.EatPoint(this.PositionQuadrantX, this.PositionQuadrantY);
        }

        private void TryMoveThere(string[] quadrantToMove, string toDirection)
        {
            if (this.IsAlive && quadrantToMove[0] == FreeDirection)
            {
                this.PreviousDirection = toDirection;
                this.MovedDirection = toDirection;
            }
            else if (quadrantToMove[0] == TouchWall)
            {
                this.MoveCheck();
            }
        }

        private void MoveCheck()
        {
            if (this.MovedDirection == string.Empty)
            {
                this.PreviousDirection = string.Empty;
            }
            this.MovedDirection = string.Empty;
            this.Move(this.PreviousDirection);
        }

        public void Move(string direction)
        {
            if (!string.IsNullOrEmpty(direction) && this.IsAlive)
            {
                if (direction == Right)
                {
                    this.MovePacManRight();
                    if (this.PositionQuadrantX < Global.XMax - 1)
                    {
                        string[] quadrantToMove = Engine.GetQuadrantElements(this.PositionQuadrantX + 1, this.PositionQuadrantY);
                        this.TryMoveThere(quadrantToMove, direction);
                    }
                    else
                    {
                        this.MoveCheck();
                    }
                }
                else if (direction == Left)
                {
                    this.MovePacManLeft();
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
                else if (direction == Up)
                {
                    this.MovePacManUp();

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
                else if (direction == Down)
                {
                    this.MovePacManDown();

                    if (this.PositionQuadrantY < Global.YMax - 1)
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

        private void MovePacManDown()
        {
            this.graphicsPacMan.FillEllipse(
                new SolidBrush(Global.pacManColor),
                this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2 - PacManDiameter / 2,
                this.DrawingCoordinatesY - PacManDiameter / 2,
                PacManDiameter,
                PacManDiameter
                );

            this.graphicsPacMan.FillEllipse(
               new SolidBrush(Color.Black),
               this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2 - (PacManDiameter / 2 - 5), this.DrawingCoordinatesY - (PacManDiameter / 2 - 17),
               PacManDiameter / 5,
               PacManDiameter / 5
               );


            this.graphicsPacMan.FillPolygon(new SolidBrush(Color.Black), new[] {
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2, this.DrawingCoordinatesY),
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + 15 + this.animateCoeficent, this.DrawingCoordinatesY + Global.QuadrantSize / 2),
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + 35 - this.animateCoeficent, this.DrawingCoordinatesY + Global.QuadrantSize / 2),
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2, this.DrawingCoordinatesY)
                            });

            this.ChangeCoefficient();

            if (this.DrawingCoordinatesY == (this.PositionQuadrantY + 1) * Global.QuadrantSize + Global.QuadrantSize / 2 - 22)
            {
                this.EatPoint(this.PositionQuadrantX, this.PositionQuadrantY + 1);
            }

            if (this.DrawingCoordinatesY == (this.PositionQuadrantY + 1) * Global.QuadrantSize + Global.QuadrantSize / 2)
            {
                this.PositionQuadrantY = this.PositionQuadrantY + 1;
            }
            System.Threading.Thread.Sleep(Global.DrawingSpeed);
        }

        private void MovePacManUp()
        {
            this.graphicsPacMan.FillEllipse(
            new SolidBrush(Global.pacManColor),
            this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2 - PacManDiameter / 2,
            this.DrawingCoordinatesY - PacManDiameter / 2,
            PacManDiameter,
            PacManDiameter
            );

            this.graphicsPacMan.FillEllipse(
            new SolidBrush(Color.Black),
            this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2 - (PacManDiameter / 2 - 5), this.DrawingCoordinatesY - (PacManDiameter / 2 - 10),
            PacManDiameter / 5,
            PacManDiameter / 5
            );

            this.graphicsPacMan.FillPolygon(new SolidBrush(Color.Black), new[] {
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2, this.DrawingCoordinatesY),
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + 15 + this.animateCoeficent, this.DrawingCoordinatesY - Global.QuadrantSize / 2 + 2),
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + 35 - this.animateCoeficent, this.DrawingCoordinatesY - Global.QuadrantSize / 2 + 2),
                                new System.Drawing.Point(this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2, this.DrawingCoordinatesY)
                            });

            this.ChangeCoefficient();

            if (this.DrawingCoordinatesY == (this.PositionQuadrantY - 1) * Global.QuadrantSize + Global.QuadrantSize / 2 + 22)
            {
                this.EatPoint(this.PositionQuadrantX, this.PositionQuadrantY - 1);
            }

            if (this.DrawingCoordinatesY == (this.PositionQuadrantY - 1) * Global.QuadrantSize + Global.QuadrantSize / 2)
            {
                this.PositionQuadrantY -= 1;
            }

            System.Threading.Thread.Sleep(Global.DrawingSpeed);
        }

        private void MovePacManLeft()
        {
            this.graphicsPacMan.FillEllipse(
                 new SolidBrush(Color.Black),
                 this.DrawingCoordinatesX - (PacManDiameter / 2 - 10),
                 this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2 - (PacManDiameter / 2 - 5),
                 PacManDiameter / 5,
                 PacManDiameter / 5
                 );

            this.graphicsPacMan.FillPolygon(new SolidBrush(Color.Black), new[] {
                            new System.Drawing.Point(this.DrawingCoordinatesX, this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2),
                            new System.Drawing.Point(this.DrawingCoordinatesX - Global.QuadrantSize / 2 + 2, this.PositionQuadrantY * Global.QuadrantSize + 15 + this.animateCoeficent),
                            new System.Drawing.Point(this.DrawingCoordinatesX - Global.QuadrantSize / 2 + 2, this.PositionQuadrantY * Global.QuadrantSize + 35 - this.animateCoeficent),
                            new System.Drawing.Point(this.DrawingCoordinatesX, this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2)
                        });

            this.ChangeCoefficient();

            if (this.DrawingCoordinatesX == (this.PositionQuadrantX - 1) * Global.QuadrantSize + Global.QuadrantSize / 2 + 22)
            {
                this.EatPoint(this.PositionQuadrantX - 1, this.PositionQuadrantY);
            }

            if (this.DrawingCoordinatesX == (this.PositionQuadrantX - 1) * Global.QuadrantSize + 25)
            {
                this.PositionQuadrantX -= 1;
            }

            System.Threading.Thread.Sleep(Global.DrawingSpeed);
        }

        private void MovePacManRight()
        {
            this.graphicsPacMan.FillEllipse(
            new SolidBrush(Color.Black),
            this.DrawingCoordinatesX - (PacManDiameter / 2 - 20),
            this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2 - (PacManDiameter / 2 - 5),
            PacManDiameter / 5,
            PacManDiameter / 5
            );

            this.graphicsPacMan.FillPolygon(new SolidBrush(Color.Black), new[] {
                                new System.Drawing.Point(this.DrawingCoordinatesX, this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2),
                                new System.Drawing.Point(this.DrawingCoordinatesX + Global.QuadrantSize / 2, this.PositionQuadrantY * Global.QuadrantSize + 15 + this.animateCoeficent),
                                new System.Drawing.Point(this.DrawingCoordinatesX + Global.QuadrantSize / 2, this.PositionQuadrantY * Global.QuadrantSize + 35 - this.animateCoeficent),
                                new System.Drawing.Point(this.DrawingCoordinatesX, this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2)
                            });

            this.ChangeCoefficient();

            if (this.DrawingCoordinatesX == (this.PositionQuadrantX + 1) * Global.QuadrantSize + Global.QuadrantSize / 2 - 22)
            {
                this.EatPoint(this.PositionQuadrantX + 1, this.PositionQuadrantY);
            }

            if (this.DrawingCoordinatesX == (this.PositionQuadrantX + 1) * Global.QuadrantSize + 25)
            {
                this.PositionQuadrantX = this.PositionQuadrantX + 1;

            }
            System.Threading.Thread.Sleep(Global.DrawingSpeed);
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
                case Right:

                    if (this.DrawingCoordinatesX < (Global.XMax - 1) * Global.QuadrantSize + Global.QuadrantSize / 2)
                    {
                        this.DrawingCoordinatesX += 1;
                    }

                    this.graphicsPacMan.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            this.DrawingCoordinatesX - 1 - PacManDiameter / 2,
                            this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2 - PacManDiameter / 2,
                            PacManDiameter,
                            PacManDiameter
                            )
                        );

                    this.graphicsPacMan.FillEllipse(
                           new SolidBrush(Global.pacManColor),
                           this.DrawingCoordinatesX - PacManDiameter / 2,
                           this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2 - PacManDiameter / 2,
                           PacManDiameter,
                           PacManDiameter
                           );
 
                    this.MovePacManRight();
                    break;

                case Left:

                    if (this.DrawingCoordinatesX > (this.PositionQuadrantX - 1) * Global.QuadrantSize + Global.QuadrantSize / 2 && this.DrawingCoordinatesX > Global.QuadrantSize / 2)
                    {
                        this.DrawingCoordinatesX -= 1;
                    }

                    this.graphicsPacMan.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            this.DrawingCoordinatesX + 1 - PacManDiameter / 2,
                            this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2 - PacManDiameter / 2,
                            PacManDiameter,
                            PacManDiameter
                            )
                        );

                    this.graphicsPacMan.FillEllipse(
                        new SolidBrush(Global.pacManColor), this.DrawingCoordinatesX - PacManDiameter / 2,
                        this.PositionQuadrantY * Global.QuadrantSize + Global.QuadrantSize / 2 - PacManDiameter / 2,
                        PacManDiameter,
                        PacManDiameter
                        );

                   this.MovePacManLeft();
                    break;

                case Up:

                    if (this.DrawingCoordinatesY > (this.PositionQuadrantY - 1) * Global.QuadrantSize + Global.QuadrantSize / 2 && this.DrawingCoordinatesY > Global.QuadrantSize / 2)
                    {
                        this.DrawingCoordinatesY -= 1;
                    }

                    this.graphicsPacMan.DrawEllipse(
                        new Pen(Color.Black),
                        new Rectangle(
                            this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2 - PacManDiameter / 2,
                            this.DrawingCoordinatesY + 1 - PacManDiameter / 2,
                            PacManDiameter,
                            PacManDiameter
                            )
                        );

                   this.MovePacManUp();
                   break;

                case Down:

                    if (this.DrawingCoordinatesY < (Global.YMax - 1) * Global.QuadrantSize + Global.QuadrantSize / 2)
                    {
                        this.DrawingCoordinatesY += 1;
                    }

                    this.graphicsPacMan.DrawEllipse(
                         new Pen(Color.Black),
                         new Rectangle(
                             this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2 - PacManDiameter / 2,
                             this.DrawingCoordinatesY - 1 - PacManDiameter / 2,
                             PacManDiameter,
                             PacManDiameter
                             )
                         );

                    this.graphicsPacMan.FillEllipse(
                        new SolidBrush(Global.pacManColor),
                        this.PositionQuadrantX * Global.QuadrantSize + Global.QuadrantSize / 2 - PacManDiameter / 2,
                        this.DrawingCoordinatesY - PacManDiameter / 2,
                        PacManDiameter,
                        PacManDiameter
                        );

                    this.MovePacManDown();
                    break;
            }

            return true;
        }

        private void ChangeCoefficient()
        {
            this.animateCoeficent += 1;
            if (this.animateCoeficent == 12)
            {
                this.animateCoeficent = 0;
            }
        }

        public void DrawPacMan()
        {
            this.graphicsPacMan.FillEllipse(
                new SolidBrush(Global.pacManColor),
                this.DrawingCoordinatesX - PacManDiameter / 2,
                this.DrawingCoordinatesY - PacManDiameter / 2,
                PacManDiameter,
                PacManDiameter
                );
        }

        public void EatPoint(int quadrantX, int quadrantY) // TODO make drawing of points
        {
            string[] elements = Engine.GetQuadrantElements(quadrantX, quadrantY);

            if (elements[1] == TouchWall)
            {
                this.EatPoints = this.EatPoints + int.Parse(elements[1]);
                elements[1] = FreeDirection;
                SoundPlayer.Play("eatfruit");
                Engine.EatPointAndUpdateMatrix(quadrantX, quadrantY, elements);
            }
        }
    }
}