using System;
namespace _07._Recursive_Fibonacci
{
    internal class Program
    {
        static void Main(string[] args)
        {
           int number=int.Parse(Console.ReadLine());
            int getFibonachhiNumber = CalculateFibonachi(number);
            Console.WriteLine(getFibonachhiNumber);
        }

        private static int CalculateFibonachi(int number)
        {
            if (number <= 1)
            {
                return 1;
            }

            int firstNumber = CalculateFibonachi(number - 1);
            int secondNumber = CalculateFibonachi(number - 2);
            return firstNumber + secondNumber;
        }
    }
}