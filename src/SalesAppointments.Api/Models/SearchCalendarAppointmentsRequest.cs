using System.Collections.Immutable;

namespace SalesAppointments.Models
{
    /// <summary>
    /// Represents a request to search for calendar appointment.
    /// </summary>
    public record SearchCalendarAppointmentsRequest(string Date, ImmutableList<string> Products, string Language, string Rating)
    {
        /// <summary>
        /// Search date for the appointment
        /// </summary>
        /// <example>2024-05-03</example>
        public string Date { get; init; } = Date;

        /// <summary>
        /// Search by product for the appointment
        /// </summary>
        /// <example>["SolarPanels", "Heatpumps"]</example>
        public ImmutableList<string> Products { get; init; } = Products;

        /// <summary>
        /// Search by language for the appointment
        /// </summary>
        /// <example>German</example>
        public string Language { get; init; } = Language;

        /// <summary>
        /// Search by rating for the appointment
        /// </summary>
        /// <example>Gold</example>
        public string Rating { get; init; } = Rating;
    }
}
