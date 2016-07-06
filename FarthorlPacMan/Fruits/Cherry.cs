using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Cherry : Fruit
    {
        private static Image cherry = Image.FromFile(@"DataFiles\Images\Fruit\Cherry.bmp");
        public Cherry(int x, int y, Graphics graphicsFruit, Engine engine) : base(x, y, graphicsFruit, engine)
        {
        }

        public override void DrawFruit()
        {
            this.GraphicsFruit.DrawImage(cherry, this.FruitPositionX *this.QuadrantDimension, this.FruitPositionY *this.QuadrantDimension);
        }
    }
}
