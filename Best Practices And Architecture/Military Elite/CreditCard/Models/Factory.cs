using CreditCardSimpleFactory.Contracts;

namespace CreditCardSimpleFactory.Models
{
    public class Factory
    {
        public IPizza GeneratePizza(string type)
        {
            if (type == "Margarita")
            {
                Console.WriteLine("The order size is: ");
                int size=int.Parse(Console.ReadLine());
                Console.WriteLine("The Price is: ");
                decimal price = decimal.Parse(Console.ReadLine());
                return new Margarita("Margarita",price,size);
            }
            return null;
        }
    }
}
