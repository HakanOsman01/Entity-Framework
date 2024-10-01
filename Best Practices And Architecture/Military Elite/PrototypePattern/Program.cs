using PrototypePattern.Contracts;
using PrototypePattern.Models;

namespace PrototypePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Warrior warrior = new Warrior("Valhava", 100, 675);
            var cloneWarrior= warrior.Clone();
            cloneWarrior.AttackPower = 139;
            cloneWarrior.Health = 50;
            Console.WriteLine(warrior);
            Console.WriteLine(cloneWarrior);
        }
    }
}