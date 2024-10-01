using Military_Elite.Contracts;
using Military_Elite.Core.Contracts;
using Military_Elite.Enums;
using Military_Elite.Models;
using System.Text;

namespace Military_Elite.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string command=Console.ReadLine();
            List<Soldier>privates = new List<Soldier>();
            while(command != "End")
            {
                string[]cmdArgs=command
                    .Split(' ',StringSplitOptions.RemoveEmptyEntries);
                string typeCommandos = cmdArgs[0];
                if (typeCommandos=="Private")
                {
                    int id = int.Parse(cmdArgs[1]);
                    string firstName = cmdArgs[2];
                    string lastName = cmdArgs[3];
                    decimal salary = decimal.Parse(cmdArgs[4]);
                    Soldier @private = new Private(id, firstName, lastName, salary);
                    stringBuilder.AppendLine(@private.ToString());
                    privates.Add(@private);
                }
                else if(typeCommandos== "LieutenantGeneral")
                {
                    int id = int.Parse(cmdArgs[1]);
                    string firstName = cmdArgs[2];
                    string lastName = cmdArgs[3];
                    decimal salary = decimal.Parse(cmdArgs[4]);
                    ILieutenantGeneral lieutenantGeneral = 
                        new LeutenantGeneral(id,firstName,lastName,salary);
                    for (int i = 5; i < cmdArgs.Length; i++)
                    {
                        int index = int.Parse(cmdArgs[i]);
                        Private @private = (Private)privates.Where(p => p.Id == index).FirstOrDefault();
                        lieutenantGeneral.Privates.Add(@private);

                    }
                    stringBuilder.AppendLine(lieutenantGeneral.ToString());

                }
                else if(typeCommandos== "Spy")
                {
                    int id = int.Parse(cmdArgs[1]);
                    string firstName = cmdArgs[2];
                    string lastName = cmdArgs[3];
                    ISpy spy=new Spy(id, firstName, lastName);
                    stringBuilder.AppendLine(spy.ToString());   

                }
                else if (typeCommandos == "Engineer")
                {
                    int id = int.Parse(cmdArgs[1]);
                    string firstName = cmdArgs[2];
                    string lastName = cmdArgs[3];
                    decimal salary = decimal.Parse(cmdArgs[4]);
                    SpecialisedSoldier specialisedSoldier;


                    if (cmdArgs[5] == "Airforces")
                    {
                        specialisedSoldier = SpecialisedSoldier.Airforces;
                    }
                    else if (cmdArgs[5] == "Marines")
                    {
                        specialisedSoldier = SpecialisedSoldier.Marines;
                    }
                    else
                    {
                        command = Console.ReadLine();
                        continue;
                    }
                    
                    IEngineer enginer= new Enginner(id,firstName,lastName,salary,specialisedSoldier);
                    for (int i = 6; i <cmdArgs.Length; i+=2)
                    {
                        string partName= cmdArgs[i];
                        int hours = int.Parse(cmdArgs[i + 1]);
                        IRepair repair=new Repair(partName, hours);
                        enginer.Repairs.Add(repair);
                    }
                    stringBuilder.AppendLine(enginer.ToString());
                }
                else if(typeCommandos== "Commando")
                {
                    int id = int.Parse(cmdArgs[1]);
                    string firstName = cmdArgs[2];
                    string lastName = cmdArgs[3];
                    decimal salary = decimal.Parse(cmdArgs[4]);
                    SpecialisedSoldier specialisedSoldier;


                    if (cmdArgs[5] == "Airforces")
                    {
                        specialisedSoldier = SpecialisedSoldier.Airforces;
                    }
                    else if (cmdArgs[5] == "Marines")
                    {
                        specialisedSoldier = SpecialisedSoldier.Marines;
                    }
                    else
                    {
                        command = Console.ReadLine();
                        continue;
                    }

                    ICommando commando = new 
                        Commando(id, firstName, lastName, salary, specialisedSoldier);
                  
                    for (int i = 6; i < cmdArgs.Length; i += 2)
                    {
                        string codeName = cmdArgs[i];
                        string missionState = cmdArgs[i+1];
                        MissionState state;
                        if(missionState== "inProgress")
                        {
                            state = MissionState.inProgress;
                        }
                        else if(missionState== "Finished")
                        {
                            state = MissionState.Finished;
                        }
                        else
                        {
                            continue;
                        }
                        IMission mission = new Mission(codeName,state);
                        commando.Missions.Add(mission);
                    }
                    stringBuilder.AppendLine(commando.ToString());

                }

                command = Console.ReadLine();

            }
            Console.WriteLine(stringBuilder.ToString().Trim());

        }
    }
}
