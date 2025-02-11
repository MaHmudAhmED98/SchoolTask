using FastEndpoints;
using School.Context;
using School.Model;

namespace School.Endpiont
{
    public class DeleteStudentEndpoint : EndpointWithoutRequest
    {
        private readonly SchoolContext _context;

        public DeleteStudentEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Delete("/api/student/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<int>("id");
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync(ct);
            await SendNoContentAsync(ct);
        }

    }
}

