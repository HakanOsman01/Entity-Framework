using Stealer;

namespace Reflection_Lab
{
    internal class Program
    {
        static void Main(string[] args)
        {
           Spy spy = new Spy();
            Type type = typeof(Hacker);
            string hackerFullName=type.FullName;    
            string result = spy.AnalyzeAccessModifiers(hackerFullName);
            Console.WriteLine(result);
        }
    }
}