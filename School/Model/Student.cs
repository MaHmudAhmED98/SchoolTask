using Microsoft.AspNetCore.Cors.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace School.Model
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public List<CourseResult> CourseResults { get; set; } = new();
    }
}
