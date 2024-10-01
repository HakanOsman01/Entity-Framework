namespace CommpositePattern.Shapes
{
    public class Text : Shape
    {
        public Text(Position position, string text) : base(position)
        {
            this.text = text;
        }
        public string text { get;set; }

        public override void Draw()
        {
            base.Draw();
            SetCursorPosition();
            Console.WriteLine(text);
           
        }
    }
}
