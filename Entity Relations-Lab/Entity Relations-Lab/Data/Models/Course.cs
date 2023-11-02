using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Relations_Lab.Data.Models
{
    public class Course
    {
       
        [Key]
       
        public int Id { get; set; }
        [StringLength(150)]
        [Required]
        public string Name { get; set; } = null!;
        public ICollection<Student> Students { get; set; }= new List<Student>();


    }
}
