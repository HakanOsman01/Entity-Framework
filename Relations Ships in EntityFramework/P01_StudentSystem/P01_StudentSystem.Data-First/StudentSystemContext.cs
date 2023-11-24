using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data_First
{
    public class StudentSystemContext: DbContext
    {
        public const string ConnectionString = @"Server=(LocalDB)\MSSQLLocalDB;Database
         = StudentSystem;Trusted_Connection = True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(s => s.Name)
                .IsUnicode(true)
                .HasMaxLength(100);

                entity
                .Property(e => e.PhoneNumbe)
                .HasPrecision(10);
               
            });
            modelBuilder.Entity<Course>(entity =>
            {
                entity
                .Property(c => c.Name)
                .IsUnicode(true);

               entity
                .Property(c=>c.Description)
                .IsUnicode(true);

                entity.Property(c=>c.Price)
                .HasPrecision(18,2);
            });
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(pk => new
                { pk.StudentId, pk.CourseId });

                entity.HasOne(s => s.Student)
                .WithMany(s => s.StudentsCourse)
                .HasForeignKey(s => s.StudentId);

                entity.HasOne(c => c.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(c => c.CourseId);

            });
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.Property(r=>r.Name)
                .IsUnicode(true);

                entity.Property(r=>r.Url)
                .IsUnicode(false);
            });
         
        }

    }
}
