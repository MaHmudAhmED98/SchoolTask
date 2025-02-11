using FastEndpoints;
using School.Context;

namespace School.Generic
{
    public abstract class BaseUpdateEndpoint<TEntity> : Endpoint<TEntity, TEntity>
        where TEntity : class
    {
        protected readonly SchoolContext _context;

        public BaseUpdateEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override async Task HandleAsync(TEntity req, CancellationToken ct)
        {
            _context.Set<TEntity>().Update(req);
            await _context.SaveChangesAsync(ct);
            await SendAsync(req);
        }
    }
}
