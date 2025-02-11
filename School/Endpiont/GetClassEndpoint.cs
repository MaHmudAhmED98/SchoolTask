using FastEndpoints;
using School.Context;
using School.Model;

namespace School.Endpiont
{
    public class GetClassEndpoint : EndpointWithoutRequest<Class>
    {
        private readonly SchoolContext _context;

        public GetClassEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Get("/api/class/{id}");
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

            await SendAsync(classEntity);
        }
    }

}
