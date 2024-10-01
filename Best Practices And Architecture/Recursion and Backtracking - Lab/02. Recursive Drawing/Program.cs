using System;
using System.Linq;

namespace _02._Recursive_Drawing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count=int.Parse(Console.ReadLine());
            DrawRecursive(count);
        }

        private static void DrawRecursive(int count)
        {
            if (count == 0)
            {
                return;
            }
            Console.WriteLine(new string('*',count));
            DrawRecursive(count-1);
            Console.WriteLine(new string('#',count));

        }
    }
}