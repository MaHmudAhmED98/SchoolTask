using FastEndpoints;
using School.Context;

namespace School.Generic
{
    public abstract class BaseDeleteEndpoint<TEntity> : EndpointWithoutRequest
     where TEntity : class
    {
        protected readonly SchoolContext _context;

        public BaseDeleteEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<int>("id");
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(ct);
            await SendNoContentAsync(ct);
        }
    }

}
