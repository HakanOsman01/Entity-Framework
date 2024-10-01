using System.Runtime.CompilerServices;
using System.Text;

namespace Anonymous_Threat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number=int.Parse(Console.ReadLine());
            StringBuilder sb = new StringBuilder();
            RecursiveConvert(number, sb);
            if (number % 2 == 0)
            {
                sb.Append("0");
            }
            char[]charArray= sb.ToString().ToCharArray();
            Array.Reverse(charArray);
            string convertNumber=new string(charArray);
            
            Console.WriteLine(convertNumber);
            
        }

        private static void  RecursiveConvert(int number,StringBuilder sb)
        {
            if (number <= 0)
            {
               
                return;
            }
           
            if (number % 2 == 0)
            {
                RecursiveConvert(number / 2, sb.Append("0"));
            }
            else
            {
                RecursiveConvert(number/2, sb.Append("1"));
            }
            
            


        }
    }
}