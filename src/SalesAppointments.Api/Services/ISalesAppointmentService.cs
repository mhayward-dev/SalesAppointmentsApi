using SalesAppointments.Models;
using Microsoft.AspNetCore.Mvc;

namespace SalesAppointments.Clients;

public interface ISalesAppointmentService
{
    Task<IActionResult> GetAvailableAppointmentDates(SearchCalendarAppointmentsRequest request);
}
