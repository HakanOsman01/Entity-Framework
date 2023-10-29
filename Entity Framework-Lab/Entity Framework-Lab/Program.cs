using Entity_Framework_Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Lab
{
    internal class Program
    {
        static void Main(string[] args)
        {

           using SoftUniContext context = new SoftUniContext();


            // GetEmployee(context).GetAwaiter().GetResult();
            AddProject(context).GetAwaiter().GetResult();
           



        }
        private static async Task GetEmployee(SoftUniContext context)
        {
            var employee = await context.Employees
                .Include(e => e.Department)
                .Include(e => e.Manager)
                .Include(e => e.Projects)
                .Select(e => new
                {
                    e.Manager,
                    e.Department,
                    e.FirstName,
                    Projects=e.Projects.Select(p => new
                    {
                        p.ProjectId,
                        p.Description

                    })


                }).FirstOrDefaultAsync();
             //Console.WriteLine($"Name: {employee.FirstName} " +
             //   $"{employee.LastName}, Department: {employee.Department.Name}, Manager: " +
             //   $"{employee.Manager.FirstName} {employee.Manager.LastName}" +
             //   $"First Project {employee.Projects.OrderBy(p => p.StartDate).FirstOrDefault().Name}");
            
        }
        private static async Task AddProject(SoftUniContext context)
        {
            await context.Projects.AddAsync(new Project
            {
                Name = "Judge System",
                StartDate = new DateTime(2023, 1, 26),
                Description = "Exam System"



            }
           );
            await context.SaveChangesAsync();

        }
    }
}