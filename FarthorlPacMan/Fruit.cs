using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarthorlPacMan
{
    public class Fruit
    {
        private Engine _engine;
        private Graphics _graphicsFruit;
        private int _fruitPositionX = 5;
        private int _fruitPositionY = 6;
        private int _quadrantDimension = Global.QuadrantSize;
        private static Image _apple = Image.FromFile(@"DataFiles\Images\Fruit\Apple.bmp");
        private static Image _banana = Image.FromFile(@"DataFiles\Images\Fruit\Banana.bmp");
        private static Image _brezel = Image.FromFile(@"DataFiles\Images\Fruit\Brezel.bmp");
        private static Image _cherry = Image.FromFile(@"DataFiles\Images\Fruit\Cherry.bmp");
        private static Image _peach = Image.FromFile(@"DataFiles\Images\Fruit\Peach.bmp");
        private static Image _pear = Image.FromFile(@"DataFiles\Images\Fruit\Pear.bmp");
        private static Image _strawberry = Image.FromFile(@"DataFiles\Images\Fruit\Strawberry.bmp");

        public Fruit(int x, int y, Graphics graphicsFruit, Engine engine)
        {
            this._fruitPositionX = x;
            this._fruitPositionY = y;
            this._engine = engine;
            this._graphicsFruit = graphicsFruit;
        }

        public void Draw()
        {
            this._graphicsFruit.DrawImage(_apple, this._fruitPositionX * this._quadrantDimension, this._fruitPositionY * this._quadrantDimension);
        }
    }
}
