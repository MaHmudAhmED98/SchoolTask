using FastEndpoints;
using School.Context;

namespace School.Endpiont
{
    public class DeleteClassEndpoint : EndpointWithoutRequest
    {
        private readonly SchoolContext _context;

        public DeleteClassEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Delete("/api/class/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<int>("id");
            var classEntity = await _context.Classes.FindAsync(id);

            if (classEntity == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync(ct);
            await SendNoContentAsync(ct);
        }
    }
}
