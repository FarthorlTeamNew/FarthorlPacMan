using System.Drawing;

namespace FarthorlPacMan.Fruits
{
    public class Cherry : Fruit
    {
        public Cherry(int x, int y, Graphics graphicsFruit) : base(x, y, graphicsFruit)
        {
            base.Image = Image.FromFile(@"DataFiles\Images\Fruit\Cherry.bmp");
        }
        public override void ActivatePowerup()
        {

        }
    }
}