using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using School.Context;

namespace School.Generic
{
    public abstract class BaseGetAllEndpoint<TEntity> : EndpointWithoutRequest<List<TEntity>>
       where TEntity : class
    {
        protected readonly SchoolContext _context;

        public BaseGetAllEndpoint(SchoolContext context)
        {
            _context = context;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var entities = await _context.Set<TEntity>().ToListAsync(ct);
            await SendAsync(entities);
        }
    }
}
