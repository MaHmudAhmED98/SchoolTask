using FastEndpoints;
using School.Context;
using School.Model;

namespace School.Endpiont
{
    public class UpdateClassEndpoint : Endpoint<Class, Class>
    {
        private readonly SchoolContext _context;

        public UpdateClassEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Put("/api/class/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Class req, CancellationToken ct)
        {
            var id = Route<int>("id");
            var classEntity = await _context.Classes.FindAsync(id);

            if (classEntity == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            classEntity.Name = req.Name;
            await _context.SaveChangesAsync(ct);
            await SendAsync(classEntity);
        }
    }
}
