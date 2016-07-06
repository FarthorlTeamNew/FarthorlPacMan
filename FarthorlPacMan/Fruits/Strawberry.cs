using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Strawberry : Fruit
    {
        private static Image strawberry = Image.FromFile(@"DataFiles\Images\Fruit\Strawberry.bmp");
        public Strawberry(int X, int Y, Graphics graphicsFruit, Engine engine) : base(X, Y, graphicsFruit, engine)
        {
        }

        public override void DrawFruit()
        {
            this.graphicsFruit.DrawImage(strawberry, this.fruitPositionX *this.QuadrantDimension, this.fruitPositionY *this.QuadrantDimension);
        }
    }
}
