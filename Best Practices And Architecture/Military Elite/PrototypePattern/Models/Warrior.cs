using PrototypePattern.Contracts;

namespace PrototypePattern.Models
{
    public class Warrior : IPrototype<Warrior>
    {
        public Warrior(string name, int health, int attackPower)
        {
            this.Name = name;
            this.Health = health;
            this.AttackPower = attackPower;
        }
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }

        public override string ToString()
        {
            return $"Warrior name: {Name} with helth {Health} and power{AttackPower}";
        }
        public Warrior Clone()
            =>MemberwiseClone() as Warrior;
        
    }
}
