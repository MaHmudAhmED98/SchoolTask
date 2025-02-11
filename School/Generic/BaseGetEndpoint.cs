using FastEndpoints;
using School.Context;

namespace School.Generic
{
    public abstract class BaseGetEndpoint<TEntity> : EndpointWithoutRequest<TEntity>
       where TEntity : class
    {
        protected readonly SchoolContext _context;

        public BaseGetEndpoint(SchoolContext context)
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

            await SendAsync(entity);
        }
    }
}
