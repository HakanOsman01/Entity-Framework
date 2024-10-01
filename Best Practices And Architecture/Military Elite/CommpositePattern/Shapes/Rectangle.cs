namespace CommpositePattern.Shapes
{
    public class Rectangle : Shape
    {
        public Rectangle(Position position,int width,int height) 
            : base(position)
        {
            this.Width = width;
            this.Height = height;
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public override void Draw()
        {
            base.Draw();
            SetCursorPosition();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    SetCursorPosition(j,i);
                    Console.Write("#");
                }
               
            }
        }

    }
}
