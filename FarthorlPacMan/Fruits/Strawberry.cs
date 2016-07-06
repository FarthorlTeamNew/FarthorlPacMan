using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Strawberry : Fruit
    {
        private static Image strawberry = Image.FromFile(@"DataFiles\Images\Fruit\Strawberry.bmp");
        public Strawberry(int x, int y, Graphics graphicsFruit, Engine engine) : base(x, y, graphicsFruit, engine)
        {
        }

        public override void DrawFruit()
        {
            this.GraphicsFruit.DrawImage(strawberry, this.FruitPositionX * this.QuadrantDimension, this.FruitPositionY * this.QuadrantDimension);
        }
    }
}
