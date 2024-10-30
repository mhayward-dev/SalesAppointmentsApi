using SalesAppointments.Clients;
using SalesAppointments.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SalesAppointments.Mappers;
using SalesAppointments.Data.Contexts;
using SalesAppointments.Data.Repositories;

namespace SalesAppointments;

public static class HostBuilderExtensions
{
    public static IHostApplicationBuilder ConfigureApi(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<SalesAppointmentsDbContext>(options => 
            options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));

        // Repositories
        builder.Services.TryAddScoped<SlotRepository>();

        // Validators
        builder.Services.TryAddSingleton<SearchCalendarAppointmentsRequestValidator>();

        // Services
        builder.Services.TryAddScoped<ISalesAppointmentService, SalesAppointmentService>();

        //Mappers
        builder.Services.TryAddSingleton<SalesAvailabilityDateMapper>();

        return builder;
    }
}
