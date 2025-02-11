using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using School.Context;
using School.Model;

namespace School.Endpiont
{
    public class GetAllClassesEndpoint : EndpointWithoutRequest<List<Class>>
    {
        private readonly SchoolContext _context;

        public GetAllClassesEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Get("/api/classes");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var classes = await _context.Classes.ToListAsync(ct);
            await SendAsync(classes);
        }
    }
}
