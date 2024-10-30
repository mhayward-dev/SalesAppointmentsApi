using SalesAppointments.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace SalesAppointments.Data.Repositories;

public class BaseRepository<T, Repository>
    where T : class
    where Repository : class
{
    public SalesAppointmentsDbContext Context { get; private set; }
    protected Repository? CurrentRepository { get; set; }
    public IQueryable<T>? Query { get; protected set; }

    public BaseRepository(DbContext context)
    {
        Context = (SalesAppointmentsDbContext)context;
    }

    public virtual Repository EagerLoad()
    {
        return CurrentRepository!;
    }

    public virtual Repository All()
    {
        Query = Context.Set<T>();

        return CurrentRepository!;
    }

    public virtual Repository For(Expression<Func<T, bool>> filter)
    {
        if (Query is not null)
        {
            Query = Query.Where(filter);
        }

        return CurrentRepository!;
    }

    public virtual Task<ImmutableList<T>> ResultsAsync()
    {
        return Task.FromResult(Query!.ToImmutableList());
    }
}
