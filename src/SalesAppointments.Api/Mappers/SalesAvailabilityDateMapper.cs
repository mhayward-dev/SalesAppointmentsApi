using SalesAppointments.Data.Entities;
using SalesAppointments.Models;
using System.Collections.Immutable;

namespace SalesAppointments.Mappers;

public class SalesAvailabilityDateMapper
{
    public ImmutableList<SalesManagerAvailabilityDate> Map(ImmutableList<Slot> slots)
    {
        var availableTimeSlots = new List<Slot>();
        var salesManagerSlotGroups = slots.GroupBy(s => s.SalesManagerId)
            .ToDictionary(grp => grp.Key, grp => grp.ToList());

        foreach (var slotsForManager in salesManagerSlotGroups)
        {
            var freeSlotsForManager = GetNonConflictingTimeSlots(slotsForManager.Value);
            availableTimeSlots.AddRange(freeSlotsForManager);
        }

        return availableTimeSlots.GroupBy(slot => slot.StartDate)
            .Select(grp => new SalesManagerAvailabilityDate(grp.Count(), grp.Key))
            .OrderBy(x => x.StartDate)
            .ToImmutableList();
    }

    private ImmutableList<Slot> GetNonConflictingTimeSlots(List<Slot> slots)
    {
        var bookedSlots = new List<Slot>(slots.Where(s => s.Booked));
        var availableSlots = new List<Slot>(slots.Except(bookedSlots));

        foreach (var slot in availableSlots)
        {
            var hasConflicts = bookedSlots.Any(b =>
                (b.StartDate < slot.EndDate
                && b.EndDate > slot.EndDate) ||
                (b.EndDate > slot.StartDate
                && b.StartDate < slot.StartDate));

            if (hasConflicts)
            {
                slots.Remove(slot);
            }
        }

        slots = slots.Except(bookedSlots).ToList();

        return slots.ToImmutableList();
    }
}
