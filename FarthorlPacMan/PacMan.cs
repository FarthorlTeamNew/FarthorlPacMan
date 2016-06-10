using System.Media;

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
        private SoundPlayer food = new SoundPlayer("DataFiles/Sounds/pacman_eatfruit.wav");

        public PacMan(int positionXQaundarnt, int positionYQuadrant, Graphics graphics, Engine engine)
        {
            this.positionQuadrantX = positionXQaundarnt;
            this.positionQuadrantY = positionYQuadrant;
            this.initializePacMan(engine);
        }

        public void move(Graphics graphic, Engine engine, string direction)
        {
            if (!String.IsNullOrEmpty(direction))
            {
                if (direction == "Right")
                {
                    tryMoveRight(graphic, engine);
                }
                else if (direction == "Left")
                {
                    tryMoveLeft(graphic, engine);
                }
                else if (direction == "Up")
                {
                    tryMoveUp(graphic, engine);
                }
                else if (direction == "Down")
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
                    await this.movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Up");
                    this.positionQuadrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Up";
                    movedDirection = "Up";
                    EatPoint(engine, nextQuandrantX, nextQuadrantY);
                }
                else if (elements[0] == "1")
                {
                    if (movedDirection == "")
                    {
                        previousDirection = "";
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
                    await movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Right");
                    this.positionQuadrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Right";
                    movedDirection = "Right";
                    EatPoint(engine, nextQuandrantX, nextQuadrantY);
                }
                else if (elements[0] == "1")
                {
                    if (movedDirection == "")
                    {
                        previousDirection = "";
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
                    await this.movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Down");
                    this.positionQuadrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    this.previousDirection = "Down";
                    this.movedDirection = "Down";
                    EatPoint(engine, nextQuandrantX, nextQuadrantY);
                }
                else if (elements[0] == "1")
                {
                    if (this.movedDirection == "")
                    {
                        this.previousDirection = "";
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
                    await this.movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Left");
                    this.positionQuadrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Left";
                    movedDirection = "Left";
                    EatPoint(engine, nextQuandrantX, nextQuadrantY);
                }
                else if (elements[0] == "1")
                {
                    if (movedDirection == "")
                    {
                        if (positionQuadrantX == 0)
                        {
                            engine.changeDirection(previousDirection);
                        }
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
                    for (int x = (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2);
                        x < (nextX * quadrantDimension) + (quadrantDimension / 2);
                        x++)
                    {

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
                      (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2) - (diameter /2 - 5),
                      diameter/5,
                      diameter/5
                      );

                        graphics.FillPolygon(new SolidBrush(Color.Black),
                        new System.Drawing.Point[] {
                                new System.Drawing.Point(x, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2),
                                new System.Drawing.Point(x + quadrantDimension/2, (this.positionQuadrantY * quadrantDimension)+15),
                                new System.Drawing.Point(x + quadrantDimension/2, (this.positionQuadrantY * quadrantDimension)+40),
                                new System.Drawing.Point(x, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2)
                        });

                        System.Threading.Thread.Sleep(speedDrawing);
                    }
                    break;

                case "Left":
                    for (int x = (this.positionQuadrantX * quadrantDimension) + (quadrantDimension / 2);
                        x > (nextX * quadrantDimension) + (quadrantDimension / 2);
                        x--)
                    {

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

                        graphics.FillPolygon(new SolidBrush(Color.Black),
                            new System.Drawing.Point[] {
                                new System.Drawing.Point(x, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2),
                                new System.Drawing.Point(x-quadrantDimension/2+2, (this.positionQuadrantY * quadrantDimension)+15),
                                new System.Drawing.Point(x-quadrantDimension/2+2, (this.positionQuadrantY * quadrantDimension)+40),
                                new System.Drawing.Point(x, (this.positionQuadrantY * quadrantDimension)+quadrantDimension/2)
                            });

                        System.Threading.Thread.Sleep(speedDrawing);
                    }
                    break;

                case "Up":
                    for (int y = (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2);
                        y > (nextY * quadrantDimension) + (quadrantDimension / 2);
                        y--)
                    {

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

                        graphics.FillPolygon(new SolidBrush(Color.Black),
                           new System.Drawing.Point[] {
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, y),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + 15, y - quadrantDimension/2+2),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + 40, y - quadrantDimension/2+2  ),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, y)
                           });

                        System.Threading.Thread.Sleep(speedDrawing);
                    }
                    break;

                case "Down":
                    for (int y = (this.positionQuadrantY * quadrantDimension) + (quadrantDimension / 2);
                        y < (nextY * quadrantDimension) + (quadrantDimension / 2);
                        y++)
                    {
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
                      
                        graphics.FillPolygon(new SolidBrush(Color.Black),
                           new System.Drawing.Point[] {
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, y),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + 15, y + quadrantDimension/2),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension+ 40, y + quadrantDimension/2  ),
                                new System.Drawing.Point(this.positionQuadrantX * quadrantDimension + quadrantDimension/2, y)
                           });

                        System.Threading.Thread.Sleep(speedDrawing);
                    }
                    break;
            }

            return true;
        }

        private void initializePacMan(Engine engine)
        {
            string[] elements = engine.GetQuadrantElements(this.positionQuadrantX, this.positionQuadrantY);

            if (elements[1] == "1")
            {
                this.eatPoints = this.eatPoints + int.Parse(elements[1]);
                elements[0] = "0";
            }
            engine.EatPointAndUpdateMatrix(this.positionQuadrantX, this.positionQuadrantY, elements);
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
            if (elements[1]=="1")
            {
                this.eatPoints = this.eatPoints + int.Parse(elements[1]);
                elements[1] = "0";
                engine.EatPointAndUpdateMatrix(this.positionQuadrantX, this.positionQuadrantY, elements);
                food.Play();
            }
        }

    }
}
