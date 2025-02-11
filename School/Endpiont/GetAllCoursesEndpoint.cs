using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using School.Context;
using School.Model;

namespace School.Endpiont
{
    public class GetAllCoursesEndpoint : EndpointWithoutRequest<List<Course>>
    {
        private readonly SchoolContext _context;

        public GetAllCoursesEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Get("/api/courses");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var courses = await _context.Courses.ToListAsync(ct);
            await SendAsync(courses);
        }
    }
}
