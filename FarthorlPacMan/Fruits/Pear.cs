using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Pear : Fruit
    {
        private static Image _pear = Image.FromFile(@"DataFiles\Images\Fruit\Pear.bmp");
        public Pear(int x, int y, Graphics graphicsFruit, Engine engine) : base(x, y, graphicsFruit, engine)
        {
        }

        public override void DrawFruit()
        {
            this.GraphicsFruit.DrawImage(_pear, this.FruitPositionX *this.QuadrantDimension, this.FruitPositionY *this.QuadrantDimension);
        }
    }
}
