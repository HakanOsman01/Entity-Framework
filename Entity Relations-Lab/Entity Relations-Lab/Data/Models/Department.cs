using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity_Relations_Lab.Data.Models;

//[Table("Departmenttable")]
public partial class Department
{
    public int DepartmentId { get; set; }

    
    public string Name { get; set; } = null!;

    public int ManagerId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Employee Manager { get; set; } = null!;
}
