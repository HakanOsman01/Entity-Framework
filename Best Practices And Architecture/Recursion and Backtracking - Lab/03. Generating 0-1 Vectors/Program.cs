using System;
using System.Linq;

namespace _03._Generating_0_1_Vectors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count=int.Parse(Console.ReadLine());
            int[] arr = new int[count];
            GenerateVec(arr, 0);

        }
        public static void GenerateVec(int[]arr,int count)
        {
            if(count>=arr.Length)
            {
                Console.WriteLine($"{string.Join(string.Empty,arr)}");
                return;
            }
            for(int idx = 0; idx < 2; idx++)
            {
                arr[count] = idx;
                GenerateVec(arr,count+1);
            }
        }
    }
}