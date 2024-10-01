using Military_Elite.Enums;

namespace Military_Elite.Contracts
{
    public interface IMission 
    {
        string CodeName { get;}
        MissionState missionState { get;}

    }
}
