using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P01_StudentSystem.Data.Models.Enums;

namespace P01_StudentSystem.Data.Models
{
    public class Resource
    {
        [Key]
        public int ResourceId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        public string Url { get; set; } = null!;
        [Required]
        public ResourseType ResourseType { get; set; }
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set;}
    }
}
