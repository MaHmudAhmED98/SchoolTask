using FastEndpoints;
using School.Context;
using School.Model;

namespace School.Endpiont
{
    public class CreateClassEndpoint : Endpoint<Class, Class>
    {
        private readonly SchoolContext _context;

        public CreateClassEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Post("/api/class");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Class req, CancellationToken ct)
        {
            _context.Classes.Add(req);
            await _context.SaveChangesAsync(ct);
            await SendAsync(req);
        }
    }

}
