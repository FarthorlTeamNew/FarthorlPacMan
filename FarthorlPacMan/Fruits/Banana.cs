using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Banana : Fruit
    {
        public Banana(int x, int y, Graphics graphicsFruit) : base(x, y, graphicsFruit)
        {
            base.Image = Image.FromFile(@"DataFiles\Images\Fruit\Banana.bmp");
        }
        public override void ActivatePowerup()
        {

        }
    }
}
