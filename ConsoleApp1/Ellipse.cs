namespace ConsoleApp1
{
    public class Ellipse
    {
        public int HorRadius { get; private set; }
        public int VertRaduis { get; private set; }

        public Ellipse ( int horRadius, int vertRadius )
        {
            if ( horRadius <= 0 | vertRadius <= 0 )
            {
                throw new ArgumentException( "Enter a radius not equal to 0" );
            }
            HorRadius = horRadius;
            VertRaduis = vertRadius;
        }
        public double GetSquare()
        {
            return Math.Round( Math.PI * HorRadius * VertRaduis );
        }
        public double GetLenght()
        {
            return Math.Round( Math.PI * (HorRadius + VertRaduis) );
        }
    }
}
