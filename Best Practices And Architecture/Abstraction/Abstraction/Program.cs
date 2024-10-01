using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Abstraction
{
    internal class Program
    {


        static void Main(string[] args)
        {

            ConstructorInfo[] constructorInfo=typeof(Student).GetConstructors();

            foreach(ConstructorInfo constructor in constructorInfo)
            {
                ParameterInfo[] parameterInfos = constructor.GetParameters();
                foreach(ParameterInfo parameterInfo in parameterInfos)
                {
                    if(parameterInfo.ParameterType != typeof(string))
                    {
                        continue;
                    }

                }
                    
                
            }

        }

    }
}