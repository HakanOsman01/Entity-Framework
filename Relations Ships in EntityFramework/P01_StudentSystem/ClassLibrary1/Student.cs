using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public Student()
        {
            this.StudentsCourse = new HashSet<StudentCourse>();
            this.Homeworks= new HashSet<Homework>();   
           
        }
        [Key]
        public int StudentId { get; set; }
        [Required]
        [MaxLength(100)]
        
        public string Name { get; set; }=null!;
        
        public string? PhoneNumbe { get;set; }
        public bool RegisteredOn { get;set; }
        public DateTime Birthday { get;set; }
        public virtual ICollection<StudentCourse> StudentsCourse { get; set; }
        public virtual ICollection<Homework>Homeworks { get; set; } 
        



    }
}
