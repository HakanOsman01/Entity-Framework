using Stealer;
using System.Reflection;
using System.Text;

namespace Reflection_Lab
{
    public class Spy
    {
        public string StealFieldInfo(string className,params string[]namesFields)
        {
            StringBuilder sb= new StringBuilder();
            Type type = Type.GetType(className);
            sb.AppendLine($"Class under investigation: {className}");
            FieldInfo[]fieldInfos = type.GetFields(BindingFlags.Public 
                | BindingFlags.NonPublic | BindingFlags.Instance 
                | BindingFlags.Static);
            Object hacker=(Hacker)Activator.CreateInstance(type);
            foreach (FieldInfo fieldInfo in fieldInfos.Where(f=>namesFields.Contains(f.Name)))
            {
                sb.AppendLine($"{fieldInfo.Name} = {fieldInfo.GetValue(hacker)}");
            }


            return sb.ToString().Trim();
        }
        public string AnalyzeAccessModifiers(string className)
        {
            Type type = Type.GetType(className);
            StringBuilder sb= new StringBuilder();
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic
                | BindingFlags.Static);
            MethodInfo[] publicMethods = type.GetMethods( BindingFlags.Instance | BindingFlags.Public);
            MethodInfo[]nonPublicMethods=type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var fieldInfo in fieldInfos)
            {
                if (fieldInfo.IsPublic)
                {
                    sb.AppendLine($"{fieldInfo.Name} must be private!");
                }
              
               
                

            }
            foreach (var method in publicMethods.Where(p=>p.Name.StartsWith("get")))
            {

               
                    sb.AppendLine($"{method.Name} have to be public!");

                

            }
            foreach (var method in nonPublicMethods.Where(p => p.Name.StartsWith("set")))
            {
              
                   sb.AppendLine($"{method.Name} have to be private!");
               

            }
            return sb.ToString().Trim();


        }
    }
}
