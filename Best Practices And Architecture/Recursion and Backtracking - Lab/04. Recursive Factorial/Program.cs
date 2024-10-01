using System;

namespace _04._Recursive_Factorial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long facNumber=long.Parse(Console.ReadLine());
            long factorial = CalculateFactorial(facNumber);
            Console.WriteLine(factorial);
        }

        private static long CalculateFactorial(long facNumber)
        {
            if (facNumber == 1)
            {
                return 1;
            }
            long fac=facNumber*CalculateFactorial(facNumber-1);
            return fac;
        }
    }
}