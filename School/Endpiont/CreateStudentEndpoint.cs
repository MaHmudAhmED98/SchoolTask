using FastEndpoints;
using School.Context;
using School.Model;

namespace School.Endpiont
{
    public class CreateStudentEndpoint : Endpoint<Student, Student>
    {
        private readonly SchoolContext _context;

        public CreateStudentEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Post("/api/student");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Student req, CancellationToken ct)
        {
            _context.Students.Add(req);
            await _context.SaveChangesAsync(ct);
            await SendAsync(req);
        }
    }
}
