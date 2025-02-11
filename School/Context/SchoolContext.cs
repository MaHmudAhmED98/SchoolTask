using Microsoft.EntityFrameworkCore;
using School.Model;
using System;

namespace School.Context
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseResult> CourseResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Class)
                .HasForeignKey(s => s.ClassId);

            modelBuilder.Entity<Class>()
                .HasMany(c => c.Courses)
                .WithOne(c => c.Class)
                .HasForeignKey(c => c.ClassId);

            modelBuilder.Entity<CourseResult>()
                .HasKey(cr => new { cr.StudentId, cr.CourseId });

            modelBuilder.Entity<CourseResult>()
                .HasOne(cr => cr.Student)
                .WithMany(s => s.CourseResults)
                .HasForeignKey(cr => cr.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseResult>()
                .HasOne(cr => cr.Course)
                .WithMany()
                .HasForeignKey(cr => cr.CourseId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
