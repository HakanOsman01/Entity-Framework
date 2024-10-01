using CreditCardSimpleFactory.Contracts;

namespace CreditCardSimpleFactory.Models
{
    public class Margarita : IPizza
    {
        public Margarita(string name,decimal Price,int size) 
        {
            Name = name;
            this.Price = Price;
            this.Size = size;
        }
        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public int Size { get;private set; }
        public void HowIAmGreat(int grade)
        {
            Console.WriteLine($"My grade is:{grade}");
        }
        public override string ToString()
        {
            return $"I am {Name} with size {Size} and price {Price:f2}";
        }
    }
}
