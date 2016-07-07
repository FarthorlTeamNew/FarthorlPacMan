using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Peach : Fruit
    {
        public Peach(int x, int y, Graphics graphicsFruit) : base(x, y, graphicsFruit)
        {
            base.Image= Image.FromFile(@"DataFiles\Images\Fruit\Peach.bmp");
        }
        public override void ActivatePowerup()
        {

        }
    }
}
