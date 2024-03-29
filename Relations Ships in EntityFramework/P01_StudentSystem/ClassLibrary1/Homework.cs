﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace P01_StudentSystem.Data.Models
{
    public class Homework
    {
        [Key]
        public int HomeworkId{ get; set; }
        [Required]
        public string Content { get; set; } = null!;
        public ResourceType ResourceType { get; set; }
        public DateTime SubmissionTime { get;set; }
        public int StudentId { get;set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set;}
    }
}
