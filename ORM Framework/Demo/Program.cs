namespace Demo
{
    internal class Program
    {
        //private static HashSet<string> permutaions;
        static void Main(string[] args)
        {

            var context = new ApplicationDbContext();
            var result=from  employee in context.Employees 
                       where employee.FirstName=="Pesho"
                       select employee;
            var result1=
                context.Employees
                .Where(e=>e.FirstName=="Pesho").ToList();
        }

        
    }
}