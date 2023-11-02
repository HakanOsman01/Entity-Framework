using System.ComponentModel.DataAnnotations;

namespace Entity_Relations_Lab.Data.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }=new List<Course>();

    }
}
