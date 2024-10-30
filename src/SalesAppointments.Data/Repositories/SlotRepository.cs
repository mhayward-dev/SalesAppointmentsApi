using SalesAppointments.Data.Contexts;
using SalesAppointments.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace SalesAppointments.Data.Repositories;

public class SlotRepository : BaseRepository<Slot, SlotRepository>
{
    public SlotRepository(SalesAppointmentsDbContext context) : base(context)
    {
        CurrentRepository = this;
    }

    public override SlotRepository EagerLoad()
    {
        if (Query is not null)
        {
            Query = Query.Include(s => s.SalesManager);
        }

        return this;
    }

    public SlotRepository ForSlotDate(DateTime dateTime)
    {
        var date = dateTime.Date;

        if (Query is not null)
        {
            Query = Query.Where(s => s.StartDate > dateTime
                && s.EndDate < dateTime.AddDays(1));
        }

        return this;
    }

    public SlotRepository ForProducts(ImmutableList<string> products)
    {
        if (Query is not null)
        {
            Query = Query.Where(s => s.SalesManager.Products != null
                && products.All(p => s.SalesManager.Products.Contains(p)));
        }

        return this;
    }

    public SlotRepository ForLanguage(string language)
    {
        if (Query is not null)
        {
            Query = Query.Where(s => s.SalesManager.Languages != null
                && s.SalesManager.Languages.Contains(language));
        }

        return this;
    }

    public SlotRepository ForRating(string rating)
    {
        if (Query is not null)
        {
            Query = Query.Where(s => s.SalesManager.CustomerRatings != null
                && s.SalesManager.CustomerRatings.Contains(rating));
        }

        return this;
    }
}
