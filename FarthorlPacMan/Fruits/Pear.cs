using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Pear : Fruit
    {
        private static Image pear = Image.FromFile(@"DataFiles\Images\Fruit\Pear.bmp");
        public Pear(int X, int Y, Graphics graphicsFruit, Engine engine) : base(X, Y, graphicsFruit, engine)
        {
        }

        public override void DrawFruit()
        {
            this.graphicsFruit.DrawImage(pear, this.fruitPositionX *this.QuadrantDimension, this.fruitPositionY *this.QuadrantDimension);
        }
    }
}
