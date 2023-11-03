using System;
using System.Collections.Generic;
namespace ValidBrackets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string expression = Console.ReadLine();
            var count = CountValidBrackets(expression);
            Console.WriteLine(count);
        }

        private static int CountValidBrackets(string expression)
        {
            int maxCount = int.MinValue;
            int count = 0;
            for(int i=0;i<expression.Length; i++)
            {
                if (i+1 >= expression.Length)
                {
                    break;
                }
                if (expression[i] == '(' && expression[i+1]==')')
                {
                    count += 2;
                    i++;
                    continue;
                }
                if(maxCount<count)
                {
                    maxCount = count;
                    count = 0;
                    
                }
            }
            if (maxCount==0 || maxCount==int.MinValue)
            {
                maxCount = count;
            }
           
            return maxCount;

        }
    }
}