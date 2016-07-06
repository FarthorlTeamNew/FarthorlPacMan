using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Banana : Fruit
    {
        private static Image banana = Image.FromFile(@"DataFiles\Images\Fruit\Banana.bmp");

        public Banana(int x, int y, Graphics graphicsFruit, Engine engine) : base(x, y, graphicsFruit, engine)
        {
        }

        public override void DrawFruit()
        {
            this.GraphicsFruit.DrawImage(banana, this.FruitPositionX * this.QuadrantDimension, this.FruitPositionY * this.QuadrantDimension);
        }
    }
}
