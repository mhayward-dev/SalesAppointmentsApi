using SalesAppointments.Converters;
using System.Text.Json.Serialization;

namespace SalesAppointments.Models;

/// <summary>
/// Represents a request to search for calendar appointment.
/// </summary>
public record SalesManagerAvailabilityDate(int AvailableCount, DateTime StartDate)
{
    /// <summary>
    /// The number of available appointments for a given startDate
    /// </summary>
    /// <example>2</example>
    [JsonPropertyName("available_count")]
    public int AvailableCount { get; set; } = AvailableCount;

    /// <summary>
    /// The date of the appointment
    /// </summary>
    /// <example>2024-05-03T10:30:00.000Z</example> 
    [JsonPropertyName("start_date")]
    [JsonConverter(typeof(UtcDateTimeConverter))]
    public DateTime StartDate { get; set; } = StartDate;
}