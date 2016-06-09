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
            this.positionQuadrantX = createCoordinatesX(engine, positionPacManQaundarntX);
            this.positionQuadrantY = createCoordinatesY(engine, positionPacManQaundarntX);

        }

        private int createCoordinatesX(Engine engine, int pacManX)
        {
            List<int> matrxiX=new List<int>();
            Random random=new Random();

            for (int i = 0; i < engine.GetMaxX(); i++)
            {
                if (i <= pacManX-6 || i >= pacManX+6)
                {
                    matrxiX.Add(i);
                }
            }

            return random.Next(matrxiX.Count);
        }

        private int createCoordinatesY(Engine engine, int pacManY)
        {
            Random random = new Random();

            return random.Next(pacManY);
        }
    }
}
