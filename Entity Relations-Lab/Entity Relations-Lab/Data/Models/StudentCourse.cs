using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity_Relations_Lab.Data.Models
{
    public class StudentCourse
    {

        public int StudentId { get; set; }

        [Required]
        [ForeignKey (nameof(StudentId))]
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }

        [Required]
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }=null!;

    }
}
