using FastEndpoints;
using School.Context;
using School.Model;

namespace School.Endpiont
{
    public class GetCourseEndpoint : EndpointWithoutRequest<Course>
    {
        private readonly SchoolContext _context;

        public GetCourseEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Get("/api/course/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<int>("id");
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendAsync(course);
        }
    }
}
