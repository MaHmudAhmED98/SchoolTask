using FastEndpoints;
using School.Context;
using School.Model;

namespace School.Endpiont
{
    public class UpdateCourseEndpoint : Endpoint<Course, Course>
    {
        private readonly SchoolContext _context;

        public UpdateCourseEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Put("/api/course/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Course req, CancellationToken ct)
        {
            var id = Route<int>("id");
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            course.Name = req.Name;
            course.ClassId = req.ClassId;
            await _context.SaveChangesAsync(ct);
            await SendAsync(course);
        }
    }
}
