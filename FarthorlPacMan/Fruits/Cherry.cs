using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Cherry : Fruit
    {
        private static Image cherry = Image.FromFile(@"DataFiles\Images\Fruit\Cherry.bmp");
        public Cherry(int X, int Y, Graphics graphicsFruit, Engine engine) : base(X, Y, graphicsFruit, engine)
        {
        }

        public override void DrawFruit()
        {
            graphicsFruit.DrawImage(cherry, fruitPositionX * QuadrantDimension, fruitPositionY * QuadrantDimension);
        }
    }
}
