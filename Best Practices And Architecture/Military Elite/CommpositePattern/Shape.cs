using CommpositePattern.Enums;
using System.ComponentModel;

namespace CommpositePattern
{
    public class Shape
    {
        private List<Shape> children;
       
        public Shape(Position position)
        {
            this.Position = position;
            this.children = new List<Shape>();
            ConsoleColor=ConsoleColor.White;
        }
        public ConsoleColor ConsoleColor { get; set; }
        public Position Position { get; set; }
        public virtual void Draw()
        {
            Console.ForegroundColor = ConsoleColor;
            foreach (var child in children)
            {
                child.Draw();
            }
        }
        public virtual void Color(ConsoleColor consoleColor)
        {
            this.ConsoleColor = consoleColor;
            foreach (var child in children)
            {
                child.Color(consoleColor);   
            }
        }
        public virtual void Direction(Direction direction)
        {
            foreach (var child in children)
            {
                child.Direction(direction);
            }
        }
        public void AddChild(Shape child)
        {
            children.Add(child);
        }
        protected void SetCursorPosition(int leftOffSet=0,int rightOffset=0)
        {
            Console.SetCursorPosition(Position.Left+leftOffSet, Position.Top+rightOffset); 
        }
    }
}
