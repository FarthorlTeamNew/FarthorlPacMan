using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Apple : Fruit
    {
        private static Image apple = Image.FromFile(@"DataFiles\Images\Fruit\Apple.bmp");

        public Apple(int X, int Y, Graphics graphicsFruit, Engine engine) : base(X, Y, graphicsFruit, engine)
        {            
        }

        public override void DrawFruit()
        {
            graphicsFruit.DrawImage(apple, fruitPositionX * QuadrantDimension, fruitPositionY * QuadrantDimension);
        }
    }
}