using Entity_Framework_Lab.Models;
using Microsoft.EntityFrameworkCore;

using SoftUniContext context = new SoftUniContext();
var employee = await context.Employees.Select(e => new
{
    e.FirstName,
    e.LastName,
    Project=e.Projects.Select(p=>new
    {
        p.Name,
        p.Description,
    })
}).ToListAsync();
foreach(var e in employee)
{
    Console.WriteLine(e.FirstName);
}

           



        
      
    