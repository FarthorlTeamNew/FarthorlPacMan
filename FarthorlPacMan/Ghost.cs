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

        public Ghost(int positionXQaundarnt, int positionYQuadrant, Graphics graphics, Engine engine)
        {
            this.positionQuadrantX = positionQuadrantX;
            this.positionQuadrantY = positionQuadrantY;
        }
    }
}
