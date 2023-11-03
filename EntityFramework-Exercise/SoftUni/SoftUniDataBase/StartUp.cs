
namespace SoftUni
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using SoftUni.Models;
    using System;
    using System.Text;
    
    public class StartUp
    {
        static void Main(string[] args)
        {
           SoftUniContext softUniContext = new SoftUniContext();
            
           // string allEmpolyeesInfo = GetEmployeesFullInformation(softUniContext);
           // //Console.WriteLine(allEmpolyeesInfo);
           
           // string employeesWithSalaryOver50_000 = GetEmployeesWithSalaryOver50000(softUniContext);
           // //Console.WriteLine(employeesWithSalaryOver50_000);
           // string employeeFormDepartmentResearchAndDevelopment 
           //     = GetEmployeesFromResearchAndDevelopment(softUniContext);
           // //Console.WriteLine(employeeFormDepartmentResearchAndDevelopment);
           // //string employeeAdress = AddNewAddressToEmployee(softUniContext);
           // //Console.WriteLine(employeeAdress);
           // string employeeProjects = GetEmployeesInPeriod(softUniContext);
           // //Console.WriteLine(employeeProjects);
           // string eployeeAdress = GetAddressesByTown(softUniContext);
           //// Console.WriteLine(eployeeAdress);
           // var employee147 = GetEmployee147(softUniContext);
           //// Console.WriteLine(employee147);
           // var employeeWithDepartments=GetDepartmentsWithMoreThan5Employees
           //     (softUniContext);
           // //Console.WriteLine(employeeWithDepartments);
           // var empoyeeIncreaseSalary = IncreaseSalaries(softUniContext);
           // // Console.WriteLine(empoyeeIncreaseSalary);
           // var employeeFirstNameStartsWithSa = GetEmployeesByFirstNameStartingWithSa(softUniContext);
           // Console.WriteLine(employeeFirstNameStartsWithSa);
           //var projects=DeleteProjectById(softUniContext);
           // Console.WriteLine(projects);
           var deleteTown=RemoveTown(softUniContext);
            Console.WriteLine(deleteTown);



        }
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employes = context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary

                }).OrderBy(e => e.EmployeeId)
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var employee in employes)
            {
                sb.AppendLine($"{employee.FirstName} " +
                    $"{employee.LastName} {employee.MiddleName} {employee.JobTitle} " +
                    $"{employee.Salary:f2}");
            }
            return sb.ToString().Trim();

        }
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb= new StringBuilder();
            var emoplyees = context.Employees.Select(e => new
            {
                e.FirstName,
                e.Salary

            }).Where(e=>e.Salary>50000).OrderBy(e=>e.FirstName)
            .ToList();
            foreach (var employee in emoplyees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }
            return sb.ToString().Trim();
        }
        public static string GetEmployeesFromResearchAndDevelopment
            (SoftUniContext context)
        {
            const string searchDepartmnet = "Research and Development";
            StringBuilder sb= new StringBuilder();
            var employeesAndDepartment = context.Employees 
            .Where(e => e.Department.Name == searchDepartmnet)
              .Select(e => new
              {
                  e.FirstName,
                  e.LastName,
                  e.Department.Name,
                  e.Salary

              }).OrderBy(e => e.Salary)
              .ThenByDescending(e => e.FirstName)
              .ToList();
            

         
            foreach (var employee in employeesAndDepartment)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from " +
                    $"Research and Development - ${employee.Salary:f2}");
            }
            return sb.ToString().Trim();
        }
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            
            Address address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };
            var employee = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");
           employee.Address = address;
           context.SaveChanges();

        StringBuilder sb=new StringBuilder();
            var adresses = context.Employees.Select(e => new
            {
                e.AddressId,
                e.Address.AddressText

            }).OrderByDescending(e=>e.AddressId)
            .Take(10)
            .ToList();
            foreach (var adress in adresses)
            {
                sb.AppendLine($"{adress.AddressText}");

            }
           

            return sb.ToString().Trim();

        }
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder sb=new StringBuilder();
            var employeesProjects = context.Employees
                .AsNoTracking()
                   .Where(e => e.EmployeesProjects
                       .Any(ep => ep.Project.StartDate.Year >= 2001 
                       && ep.Project.StartDate.Year <= 2003))
                   .Take(10)
                   .Select(e => new
                   {
                       e.FirstName,
                       e.LastName,
                       ManagerFirstName = e.Manager.FirstName,
                       ManagerLastName = e.Manager.LastName,
                       Projects = e.EmployeesProjects
                           .Select(ep => ep.Project)
                   })
                   .ToList();
            foreach (var ep in employeesProjects)
            {
                sb.AppendLine($"{ep.FirstName} {ep.LastName} – Manager: {ep.ManagerFirstName} {ep.ManagerLastName}");

                foreach (var project in ep.Projects)
                {
                    if (project.EndDate == null)
                    {
                        sb.AppendLine($"--{project.Name} - {project.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - not finished");
                    }
                    else
                    {
                        sb.AppendLine($"--{project.Name} - {project.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - {((DateTime)(project.EndDate)).ToString("M/d/yyyy h:mm:ss tt")}");//project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt") also works locally, but not in Judge!
                    }
                }
            }
            return sb.ToString().TrimEnd();
        }
        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb=new StringBuilder();
            var result = context.Addresses
             .AsNoTracking()
             .OrderByDescending(a => a.Employees.Count)
             .ThenBy(a => a.Town.Name)
             .Take(10)
          .Select(a => new
          {
            Text = a.AddressText,
            Town = a.Town.Name,
            EmployeesCount = a.Employees.Count
          })
          .ToList();
            foreach (var ep in result) 
             {
                sb.AppendLine($"{ep.Text}, {ep.Town} - {ep.EmployeesCount} employees");
             } 
             return sb.ToString().TrimEnd();
        }
         public static string GetEmployee147(SoftUniContext context)
         {
            StringBuilder sb=new StringBuilder();
            var employeeWithProjects = context.Employees.Find(147);

            var projects = context.EmployeesProjects
                .Select(ep => new
                {
                    ep.EmployeeId,
                    ProjectName = ep.Project.Name

                }).Where(e => e.EmployeeId == 147)
                .OrderBy(p=>p.ProjectName)
                .ToList();
              
            sb.Append($"{employeeWithProjects.FirstName} " +
                $"{employeeWithProjects.LastName} - {employeeWithProjects.JobTitle} " +
                $"{string.Join(' ',projects.Select(p=>p.ProjectName))}");
            return sb.ToString().TrimEnd();

         }
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var employeesDepartments = context.Departments
                .AsNoTracking()
               .Where(d => d.Employees.Count > 5)
               .OrderBy(e => e.Employees.Count)
               .ThenBy(d => d.Name).Select(d => new
               {
                   d.Name,
                   d.Manager.FirstName,
                   d.Manager.LastName,
                   Employees = d.Employees.Select(e => new
                   {
                       e.FirstName,
                       e.LastName,
                       e.JobTitle

                   })


               }).ToList();
            foreach (var ed in employeesDepartments)
            {
                stringBuilder.AppendLine($"{ed.Name} - {ed.FirstName} " +
                    $"{ed.LastName}");
                var employees = ed.Employees;
                foreach (var employee in employees)
                {
                    stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} " +
                        $"- {employee.JobTitle}");
                }
            }
            return stringBuilder.ToString().TrimEnd();
            
        }
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var employees = context.Employees
             .Where(e => e.Department.Name == "Engineering" ||
             e.Department.Name == "Tool Design" ||
             e.Department.Name == "Marketing" ||
             e.Department.Name == "Information Services")
             .OrderBy(e=>e.FirstName).ThenBy(e=>e.LastName)
             .ToList();
            foreach (var em in employees)
            {
                em.Salary *= 1.12M;
                stringBuilder.AppendLine($"{em.FirstName} {em.LastName} ${em.Salary:f2}");

            }
            context.SaveChanges();
            
           return stringBuilder.ToString().TrimEnd();
           

        }
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
  
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees
                         .AsNoTracking()
                         .Where(e => EF.Functions.Like(e.FirstName.ToLower(), "sa%"))
                         .OrderBy(e=>e.FirstName)
                         .ThenBy(e=>e.LastName)
                         .ToList();
            foreach (var em in employees)
            {
                sb.AppendLine($"{em.FirstName} {em.LastName} - {em.JobTitle} - " +
                    $"(${em.Salary:f2})");
            }
            return sb.ToString().TrimEnd();

            
               
               
                
                
        }
        public static string DeleteProjectById(SoftUniContext context)
        {

            StringBuilder sb = new StringBuilder();
            var project = context.Projects.Find(2);
            var employeesProject = context.EmployeesProjects
                .Where(e => e.ProjectId == project.ProjectId)
                .ToList();
            for (int i = 0; i < employeesProject.Count; i++)
            {
                employeesProject.Remove(employeesProject[i]);
            }
           

            context.Projects.Remove(project);
            // context.SaveChanges();

            foreach (var pr in context.Projects.Take(10))
            {
                sb.AppendLine($"{pr.Name}");

            }
            return sb.ToString().TrimEnd();


        }
        public static string RemoveTown(SoftUniContext context)
        {
            var townId = context.Towns
                .FirstOrDefault(t => t.Name == "Seattle");
            var adresses = context.Addresses
               .Where(a => a.Town.Equals(townId))
               .ToList();
            int countDeleted = adresses.Count;
            
            var employeesAdresses = context.Employees.ToArray();
            for (int i = 0; i < employeesAdresses.Length; i++)
            {
                if (employeesAdresses[i].Address is null)
                {
                    continue;
                }
                if (employeesAdresses[i].Address.Town.Equals(townId))
                {
                    employeesAdresses[i].AddressId = null;
                }
                
            }
            for (int i = 0; i < adresses.Count; i++)
            {
                adresses.Remove(adresses[i]);
            }
            context.Towns.Remove(townId);
            //context.SaveChanges();

            return $"{countDeleted} addresses in Seattle were deleted";

        }
    }
}