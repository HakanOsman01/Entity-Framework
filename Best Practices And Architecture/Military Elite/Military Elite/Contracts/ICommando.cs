using Military_Elite.Enums;

namespace Military_Elite.Contracts
{
    public interface ICommando
    {
        List<IMission> Missions { get;}
        SpecialisedSoldier SpecialisedSoldier { get;}
    }
}
