using CommpositePattern.Shapes;

namespace CommpositePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shape page = new Shape(new Position(0, 0));
            Shape leftPage=new Shape(new Position(0, 0));


            Rectangle rectangle = new Rectangle(new Position(5, 12), 4, 7);

            Line line=new Line(new Position(1, 10),5);

            Line line2 = new Line(new Position(1, 20), 5);

            Line line3 = new Line(new Position(9, 10), 5);

            Line line4 = new Line(new Position(9, 20), 5);

            rectangle.AddChild(line);
            rectangle.AddChild(line2);
            rectangle.AddChild(line3);
            rectangle.AddChild(line4);
            leftPage.AddChild(rectangle);

            Shape rightPage = new Shape(new Position(0, 0));


            Rectangle rectangle1 = new Rectangle(new Position(5,18), 4, 7);

            Line rightLine = new Line(new Position(9, 25), 5) ;

            Line rightline2 = new Line(new Position(9, 25), 5);

            Line rightline3 = new Line(new Position(9,25), 5);

            Line rightline4 = new Line(new Position(9, 25), 5);

            rectangle1.AddChild(rightLine);
            rectangle1.AddChild(rightline2);
            rectangle1.AddChild(rightline3);
            rectangle1.AddChild(rightline4);
            rightPage.AddChild(rectangle1);


            page.AddChild(leftPage);
           

            page.AddChild(rightPage);

            page.Draw();

            Console.ReadLine();

        }
    }
}