namespace FarthorlPacMan
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    class Ghost
    {
        private Boolean isAlive = true;
        private int positionQuadrantX = 0;
        private int positionQuadrantY = 0;
        private int positionPacManQaundarntX;
        private int positionPacManQaundarntY;
        private const int diameter = 40;
        private const int quadrantDimension = 50;
        private const int pacManDistanceX = 3;
        private const int pacManDistanceY = 0;
        private const int speedDrawing = 6;
        private Color GhostColor = Color.Yellow;
        private string movedDirection;
        private string previousDirection;
        private Dictionary<string,bool> existDirections=new Dictionary<string, bool>();
        private static Image imageFile = Image.FromFile(@"DataFiles\Images\Ghost.bmp");
        private Engine engine;
        private Graphics graphicsGhost;

        public Ghost(int positionPacManQaundarntX, int positionPacManQaundarntY, Graphics graphicsGhost, Engine engine)
        {
            this.positionPacManQaundarntX = positionPacManQaundarntX;
            this.positionPacManQaundarntY = positionPacManQaundarntY;
            this.engine = engine;
            this.graphicsGhost = graphicsGhost;
            this.InicializeGhost();

        }

        private Dictionary<string,int> createCoordinatesXY(int pacManX, int pacManY)
        {
            List<int[]> ghostMatrix=new List<int[]>();
            Random random=new Random();
            Dictionary<string, int> resultXY = new Dictionary<string, int>();
      

            //Check and remove quandrant if the pacMan is near from 6 quadrants left or right by X
            for (int y = 0; y < engine.GetMaxY()-1; y++)
            {
                for (int x = 0; x < engine.GetMaxX() - 1; x++)
                {
                    if (x <= pacManX - pacManDistanceX && y <= pacManY-pacManDistanceY || x >= pacManX + pacManDistanceX && y >= pacManY+pacManDistanceY)
                    {
                        var elements = engine.GetQuadrantElements(x, y);

                        if (elements[0]=="0")
                        {
                            int[] coordinates=new int[2];
                            coordinates[0] = x;
                            coordinates[1] = y;
                            ghostMatrix.Add(coordinates);
                        }
                    }
                }
            }
            System.Threading.Thread.Sleep(random.Next(100,300));
            int quadrantIndex = random.Next(ghostMatrix.Count);

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
            existDirections.Add("Up",false);
            existDirections.Add("Down",false);
            existDirections.Add("Left",false);
            existDirections.Add("Right",false);
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

            if (positionQuadrantY<engine.GetMaxY() && engine.GetQuadrantElements(positionQuadrantX,positionQuadrantY+1)[0]=="0")
            {
                existDirections["Down"] = true;
            }
            else
            {
                existDirections["Down"] = false;
            }

            if (positionQuadrantX>0 && engine.GetQuadrantElements(positionQuadrantX-1,positionQuadrantY)[0]=="0")
            {
                existDirections["Left"] = true;
            }
            else
            {
                existDirections["Left"] = false;
            }

            if (positionQuadrantX<engine.GetMaxX() && engine.GetQuadrantElements(positionQuadrantX+1,positionQuadrantY)[0]=="0")
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

        }

        public void Draw()
        {
            graphicsGhost.DrawImage(imageFile,(positionQuadrantX*quadrantDimension)+8,positionQuadrantY*quadrantDimension+4);
        }

        public void Move()
        {
           
        }

        public int GetQuadrantX()
        {
            return positionQuadrantX;
        }

        public int GetQuadrantY()
        {
            return positionQuadrantY;
        }
    }
}
