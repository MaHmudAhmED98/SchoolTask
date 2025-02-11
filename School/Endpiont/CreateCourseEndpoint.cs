using FastEndpoints;
using School.Context;
using School.Model;

namespace School.Endpiont
{
    public class CreateCourseEndpoint : Endpoint<Course, Course>
    {
        private readonly SchoolContext _context;

        public CreateCourseEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Post("/api/course");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Course req, CancellationToken ct)
        {
            _context.Courses.Add(req);
            await _context.SaveChangesAsync(ct);
            await SendAsync(req);
        }
    }
}
