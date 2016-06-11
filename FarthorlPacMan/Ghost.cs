using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace FarthorlPacMan
{
    using System;
    using System.Drawing;
    class Ghost
    {
        private Boolean isAlive = true;
        private int positionQuadrantX = 0;
        private int positionQuadrantY = 0;
        private const int diameter = 40;
        private const int quadrantDimension = 50;
        private const int speedDrawing = 6;
        private Color GhostColor = Color.Yellow;
        private string movedDirection;
        private string previousDirection;
        private Dictionary<string,bool> existDirections=new Dictionary<string, bool>();
        private Bitmap gostImage = (Bitmap) Image.FromFile(@"DataFiles\Images\Ghost.bmp", true);
        public Ghost(int positionPacManQaundarntX, int positionPacManQaundarntY, Graphics graphics, Engine engine)
        {
            var getCoordinates = this.createCoordinatesXY(engine, positionPacManQaundarntX, positionPacManQaundarntY);
            this.positionQuadrantX = getCoordinates["QuadrantX"];
            this.positionQuadrantY = getCoordinates["QuadrantY"];
            this.InicializeGhost(engine);

        }

        private Dictionary<string,int> createCoordinatesXY(Engine engine, int pacManX, int pacManY)
        {
            List<int> matrxiX=new List<int>();
            Random random=new Random();
            Dictionary<string, int> resultXY = new Dictionary<string, int>();
            int x, y;

            //Check and remove quandrant if the pacMan is near from 6 quadrants left or right by X
            for (int i = 0; i < engine.GetMaxX(); i++)
            {
                if (i <= pacManX-6 || i >= pacManX+6)
                {
                    matrxiX.Add(i);
                }
            }

            while (true)
            {
                x = random.Next(matrxiX.Count);
                y = random.Next(pacManY);

                var quadrant = engine.GetQuadrantElements(x, y);
                if (quadrant[0]=="0")
                {
                    break;
                }
            }

            resultXY.Add("QuadrantX", x);
            resultXY.Add("QuadrantY", y);

            //check is select quadrant is not wall.
            //If is wall call method again to selech another random quandrant
            if (!isExistQuadrant(engine,resultXY))
            {
                createCoordinatesXY(engine, pacManX, pacManY);
            }


            return resultXY;
        }

        private bool isExistQuadrant(Engine engine, Dictionary<string, int> quandrant)
        {

            string[] elements=engine.GetQuadrantElements(quandrant["QuadrantX"], quandrant["QuadrantY"]);

            if (elements[0]=="0")
            {
                return true;
            }

            return false;
        }

        private void InicializeGhost(Engine engine)
        {
            existDirections.Add("Up",false);
            existDirections.Add("Down",false);
            existDirections.Add("Left",false);
            existDirections.Add("Right",false);
            CheckExistDirections(engine);
            SelectRandomDirection();

        }

        private void CheckExistDirections(Engine engine)
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
    }
}
