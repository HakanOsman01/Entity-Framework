using System.Threading.Channels;

namespace Filter_by_Age
{
    internal class Program
    {
        static void Main(string[] args)
        {
           int n=int.Parse(Console.ReadLine());
           List<Student> students = new List<Student>();
            ReadInfoAboutStudents(n, students);
            string condition=Console.ReadLine();
            int age = int.Parse(Console.ReadLine());
            Predicate<Student> pred = GetConditionAge(condition, age);
            students=students.Where(s=>pred(s))
                .ToList();
            string formant = Console.ReadLine();
            Action<Student> formatOutput = GetFormatOutput(formant);
            students.ForEach(s => { formatOutput.Invoke(s); });

         
           
        }

        private static Action<Student> GetFormatOutput(string formant)
        {
            switch(formant)
            {
                case "name":
                    return s => Console.WriteLine(s.Name);
                case "age":
                    return s=>Console.WriteLine($"{s.Age}");
                case "name age":
                    return s => Console.WriteLine($"{s.Name} - {s.Age}");
                default : return s => Console.WriteLine();
            }
        }

        private static Predicate<Student> GetConditionAge(string condition, int age)
        {
            switch (condition)
            {
                case "older":
                    return s => s.Age >= age;
                    
                case "younger": 
                    return s => s.Age < age;
                default:
                    return s=>s.Age == age;
            }
        }

        private static void  ReadInfoAboutStudents(int n,List<Student> students)
        {
            for (int i = 0; i < n; i++)
            {
                string[] studnetParams = Console.ReadLine().Split(", "
                    , StringSplitOptions.RemoveEmptyEntries);
                string name = studnetParams[0];
                int age = int.Parse(studnetParams[1]);
                Student student = new Student(name, age);
                students.Add(student);
            }

        }
       
    }
    class Student
    {
        public Student(string name,int age)
        {
            this.Name= name;
            this.Age= age;
            
        }
        public string Name { get; private set; }
        public int Age { get; private set; }
    }
}