using FastEndpoints;
using System.ComponentModel.DataAnnotations;

namespace School.Model
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new();
        public List<Course> Courses { get; set; } = new();
    }
}
