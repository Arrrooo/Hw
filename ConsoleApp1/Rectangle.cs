namespace ConsoleApp1
{
    public class Rectangle
    {
        public Point LeftTopPoint { get; private set; }
        public Point RightBottomPoint { get; private set; }

        public Rectangle( Point leftTopPoint, Point rightbBottomPoint )
        {
            if ( leftTopPoint.Y < rightbBottomPoint.Y )
            {
                throw new ArgumentException( "Right bottom point is higher than left top point" );
            }
            if ( leftTopPoint.X > rightbBottomPoint.X )
            {
                throw new ArgumentException( "Left top point is righter than right bottom point" );
            }
            
            LeftTopPoint = leftTopPoint;
            RightBottomPoint = rightbBottomPoint;
        }

        public int GetSquare()
        {
            int height = LeftTopPoint.Y - RightBottomPoint.Y;
            int width = RightBottomPoint.X - LeftTopPoint.X;
            return height * width;
        }
    }
}
