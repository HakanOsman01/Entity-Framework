using Military_Elite.Contracts;

namespace Military_Elite.Models
{
    public class Spy : Soldier,ISpy
    {
        public Spy(int id, string fistName, string lastName) : base(id, fistName, lastName)
        {
        }

        public Spy(int id, string fistName, string lastName,int codeNumber) 
            : base(id, fistName, lastName)
        {
            this.CodeNumber = codeNumber;

        }

        public int CodeNumber { get;private set; }
        public override string ToString()
        {
            return base.ToString()+Environment.NewLine+ $"Code Number: {CodeNumber}";
        }
    }
}
