namespace FarthorlPacMan
{
    public class Participant
    {
        public int PositionQuadrantX { get; protected set; }
        public int PositionQuadrantY { get; protected set; }
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
