namespace NestedClases
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> numbers = Enumerable.Range(0, 100)
                 .Where(x => x is var r && r % 2 == 0);
            foreach (int x in numbers)
            {
                Console.WriteLine(x);
            }
        }
    }
}