using SalesAppointments.Data.Repositories;
using SalesAppointments.Mappers;
using SalesAppointments.Models;
using Microsoft.AspNetCore.Mvc;

namespace SalesAppointments.Clients;

public class SalesAppointmentService(
    SlotRepository slotRepository,
    SalesAvailabilityDateMapper availabilityDateMapper) : ISalesAppointmentService
{
    public async Task<IActionResult> GetAvailableAppointmentDates(
        SearchCalendarAppointmentsRequest request)
    {
        var slotDateUtc = DateTime.SpecifyKind(DateTime.Parse(request.Date), DateTimeKind.Utc);

        var allMatchingSlots = await slotRepository
            .All()
            .EagerLoad()
            .ForSlotDate(slotDateUtc)
            .ForLanguage(request.Language)
            .ForProducts(request.Products)
            .ForRating(request.Rating)
            .ResultsAsync();

        var availableTimeSlots = availabilityDateMapper.Map(allMatchingSlots);

        return new OkObjectResult(availableTimeSlots);
    }
}
