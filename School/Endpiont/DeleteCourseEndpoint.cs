using FastEndpoints;
using School.Context;

namespace School.Endpiont
{
    public class DeleteCourseEndpoint : EndpointWithoutRequest
    {
        private readonly SchoolContext _context;

        public DeleteCourseEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Delete("/api/course/{id}");
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

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(ct);
            await SendNoContentAsync(ct);
        }
    }
}
