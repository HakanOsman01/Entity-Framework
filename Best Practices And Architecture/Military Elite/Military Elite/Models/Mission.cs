using Military_Elite.Contracts;
using Military_Elite.Enums;

namespace Military_Elite.Models
{
    public class Mission : IMission
    {
        public Mission(string codeName, MissionState missionState)
        {
            this.CodeName = codeName;
            this.missionState = missionState;
        }
        public string CodeName { get; private set; }

        public MissionState missionState { get; private set; }
        public override string ToString()
        {
            return $"Code name: {this.CodeName} State: {missionState.ToString()}";
        }
        public MissionState ComplteMission(MissionState missionState)
        {
            return missionState = MissionState.Finished;
        }
    }
}
