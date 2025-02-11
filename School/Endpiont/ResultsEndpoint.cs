using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using School.Context;

namespace School.Endpiont
{
    public class ResultsEndpoint : EndpointWithoutRequest<List<StudentResult>>
    {
        private readonly SchoolContext _context;

        public ResultsEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Get("/api/results/{classId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var classId = Route<int>("classId");
            var students = await _context.Students
                .Include(s => s.CourseResults)
                .ThenInclude(cr => cr.Course)
                .Where(s => s.ClassId == classId)
                .ToListAsync(ct);

            var results = students.Select(s => new StudentResult
            {
                StudentName = s.Name,
                CourseResults = s.CourseResults.Select(cr => new CourseResultDto
                {
                    CourseName = cr.Course.Name,
                    Degree = cr.Degree
                }).ToList(),
                TotalDegree = s.CourseResults.Sum(cr => cr.Degree),
                Rating = GetRating(s.CourseResults.Sum(cr => cr.Degree))
            }).ToList();

            await SendAsync(results);
        }

        private string GetRating(double totalDegree)
        {
            return totalDegree switch
            {
                >= 85 => "Excellent",
                >= 75 => "Very Good",
                >= 65 => "Good",
                _ => "Accepted"
            };
        }
    }

    public class StudentResult
    {
        public string StudentName { get; set; }
        public List<CourseResultDto> CourseResults { get; set; }
        public double TotalDegree { get; set; }
        public string Rating { get; set; }
    }

    public class CourseResultDto
    {
        public string CourseName { get; set; }
        public double Degree { get; set; }
    }
}
