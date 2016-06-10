using System.Collections.Generic;
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

        public Ghost(int positionPacManQaundarntX, int positionPacManQaundarntY, Graphics graphics, Engine engine)
        {
            var getCoordinates = this.createCoordinatesXY(engine, positionPacManQaundarntX, positionPacManQaundarntY);
            this.positionQuadrantX = getCoordinates["QuadrantX"];
            this.positionQuadrantY = getCoordinates["QuadrantY"];

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

            return resultXY;
        }

        private void InicializeGhost(Engine engine)
        {

        }

    }
}
