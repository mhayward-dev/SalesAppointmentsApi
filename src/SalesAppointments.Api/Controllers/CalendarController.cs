using SalesAppointments.Core;
using SalesAppointments.Models;
using SalesAppointments.Clients;
using SalesAppointments.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace SalesAppointments.Controllers;

[ApiController]
[Route("calendar")]
public class CalendarController(
    SearchCalendarAppointmentsRequestValidator searchCalendarRequestValidator,
    ISalesAppointmentService calendarService)
{
    /// <summary>
    /// Searches for available calendar appointments
    /// </summary>
    /// <param name="searchCalendarAppointmentsRequest">Properties used to filter calendar appointments</param>
    [ProducesResponseType(typeof(ImmutableList<SalesManagerAvailabilityDate>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("query", Name = "SearchCalendarAppointments")]
    public async Task<IActionResult> SearchCalendarAppointments(
        [FromBody] SearchCalendarAppointmentsRequest searchCalendarAppointmentsRequest)
    {
        return await searchCalendarRequestValidator.ValidateAsync(searchCalendarAppointmentsRequest)
            .OnValidatedOk(async () => 
                await calendarService.GetAvailableAppointmentDates(searchCalendarAppointmentsRequest));
    }
}
