using Military_Elite.Enums;

namespace Military_Elite.Contracts
{
    public interface IEngineer
    {
        public List<IRepair> Repairs { get;}
        SpecialisedSoldier SpecialisedSoldier { get;}
    }
}
