namespace FarthorlPacMan
{
    using System.Drawing;

    public abstract class Fruit
    {
        protected Engine Engine;
        protected Graphics GraphicsFruit;
        protected int FruitPositionX;
        protected int FruitPositionY;
        protected int QuadrantDimension = Global.QuadrantSize;

        protected Fruit(int x, int y, Graphics graphicsFruit, Engine engine)
        {
            this.FruitPositionX = x;
            this.FruitPositionY = y;
            this.Engine = engine;
            this.GraphicsFruit = graphicsFruit;
        }

        // graphicsFruit.DrawImage(apple, fruitPositionX * QuadrantDimension, fruitPositionY * QuadrantDimension);
        public abstract void DrawFruit();
    }
}
