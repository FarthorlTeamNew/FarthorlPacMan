using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarthorlPacMan
{
    public class Participant
    {

        protected int PositionQuadrantX { get; set; }
        protected int PositionQuadrantY { get; set; }
        protected int DrawingCoordinatesX { get; set; }
        protected int DrawingCoordinatesY { get; set; }
        protected string MovedDirection { get; set; }
        protected string PreviousDirection { get; set; }

        public Participant(int positionXQaundarnt, int positionYQuadrant)
        {
            this.PositionQuadrantX = positionXQaundarnt;
            this.PositionQuadrantY = positionYQuadrant;
        }

        public Participant()
        {
        }

    }
}
