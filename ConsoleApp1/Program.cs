using ConsoleApp1;

class Program
{
    public static void Main(string[] args)
    {
        int horRadius = 20;
        int vertRadius = 10;

        try
        {
            Ellipse ellipse = new Ellipse( horRadius, vertRadius );
            Console.WriteLine( $"Длина эллипса равна: {ellipse.GetLenght()}" );
            Console.WriteLine( $"Площадь эллипса равна: {ellipse.GetSquare()}" );
        }
        catch ( ArgumentException ex )
        {
            Console.WriteLine( ex.Message );
        }
        
    }
   
}