using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using School.Context;

namespace School.Generic
{
    public abstract class BaseAdeEndpoint<TEntity> : Endpoint<TEntity, TEntity>
    where TEntity : class
    {
        protected readonly SchoolContext _context;

        public BaseAdeEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override async Task HandleAsync(TEntity req, CancellationToken ct)
        {
            _context.Set<TEntity>().Add(req);
            await _context.SaveChangesAsync(ct);
            await SendAsync(req);
        }
    }

}
