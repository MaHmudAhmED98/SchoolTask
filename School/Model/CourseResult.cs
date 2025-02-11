
namespace School.Model
{
    public class CourseResult
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public double Degree { get; set; } = 0;
    }
}
