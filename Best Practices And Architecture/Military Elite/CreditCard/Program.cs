using CreditCardSimpleFactory.Contracts;
using CreditCardSimpleFactory.Models;

namespace CreditCard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string command=Console.ReadLine();
            while(true)
            {
                if (command == "end")
                {
                    break;
                }
                Factory factory = new Factory();
                IPizza pizza=factory.GeneratePizza(command);
                if(pizza is null)
                {
                    Console.WriteLine("The pizza is uncorrect");
                    command = Console.ReadLine();
                    continue;
                }
                Console.WriteLine(pizza.ToString());
                command = Console.ReadLine();
            }
        }
    }
}