using System;
using System.Linq;
namespace _01._Recursive_Array_Sum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[]array=Console.ReadLine()
                .Split(' ',StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            int sum = RecurisveSumArray(array, 0);
            Console.WriteLine(sum);
        }
        public static int RecurisveSumArray(int[] array,int index)
        {
            if(index ==array.Length-1)
            {
                return array[index];
            }
            int sum = array[index]+RecurisveSumArray(array,index+1);
            return sum;

        }    
    }
}