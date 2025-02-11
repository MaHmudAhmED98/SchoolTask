using FastEndpoints;
using School.Context;
using School.Model;

namespace School.Endpiont
{
    public class UpdateStudentEndpoint : Endpoint<Student, Student>
    {
        private readonly SchoolContext _context;

        public UpdateStudentEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Put("/api/student/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Student req, CancellationToken ct)
        {
            var id = Route<int>("id");
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            student.Name = req.Name;
            student.ClassId = req.ClassId;
            await _context.SaveChangesAsync(ct);
            await SendAsync(student);
        }
    }
}
