using SalesAppointments.Data.Entities;
using SalesAppointments.Mappers;
using System.Collections.Immutable;
using FluentAssertions;

namespace SalesAppointments.UnitTests;

public class SalesAvailabilityDateMapperTests
{
    // TODO - expand these tests for more test cases

    [Theory, MemberData(nameof(ConflictingTimeSlotTestData))]
    public void AvailabilityDateMapper_ShouldRemoveConflictingTimeSlots(ImmutableList<Slot> slots, string reason)
    {
        var mapper = new SalesAvailabilityDateMapper();
        var result = mapper.Map(slots);

        result.Should().BeEmpty(because: reason);
    }

    [Theory, MemberData(nameof(NonConflictingTimeSlotTestData))]
    public void AvailabilityDateMapper_ShouldNotRemoveNonConflictingTimeSlots(ImmutableList<Slot> slots, int expectedCount, string reason)
    {
        var mapper = new SalesAvailabilityDateMapper();
        var result = mapper.Map(slots);

        result.Should().NotBeEmpty(because: reason);
        result!.Count.Should().Be(expectedCount);
    }

    public static TheoryData<ImmutableList<Slot>, string> ConflictingTimeSlotTestData =
        new()
        {
            {[
                new() { StartDate = new DateTime(2024, 10, 29, 9, 0, 0), EndDate = new DateTime(2024, 10, 29, 10, 0, 0) },
                new() { Booked = true, StartDate = new DateTime(2024, 10, 29, 9, 30, 0), EndDate = new DateTime(2024, 10, 29, 10, 30, 0) },
                new() { StartDate = new DateTime(2024, 10, 29, 10, 0, 0), EndDate = new DateTime(2024, 10, 29, 10, 0, 0) },
            ], "no slots should be returned because 10:30 to 11:30 conflicts with all other slots" },
            {[
                new() { StartDate = new DateTime(2024, 10, 29, 11, 0, 0), EndDate = new DateTime(2024, 10, 29, 12, 0, 0) },
                new() { Booked = true, StartDate = new DateTime(2024, 10, 29, 11, 45, 0), EndDate = new DateTime(2024, 10, 29, 12, 15, 0) },
                new() { StartDate = new DateTime(2024, 10, 29, 12, 0, 0), EndDate = new DateTime(2024, 10, 29, 13, 0, 0) },
                new() { Booked = true, StartDate = new DateTime(2024, 10, 29, 13, 0, 0), EndDate = new DateTime(2024, 10, 29, 14, 0, 0) },
                new() { StartDate = new DateTime(2024, 10, 29, 13, 30, 0), EndDate = new DateTime(2024, 10, 29, 14, 30, 0) },
             ], "no slots should be returned because 11:45 to 12:15 and 13:00 to 14:00 conflicts with all other slots" }
        };

    public static TheoryData<ImmutableList<Slot>, int, string> NonConflictingTimeSlotTestData =
        new()
        {
            {[
                new() { StartDate = new DateTime(2024, 10, 29, 9, 0, 0), EndDate = new DateTime(2024, 10, 29, 10, 0, 0) },
                new() { StartDate = new DateTime(2024, 10, 29, 9, 30, 0), EndDate = new DateTime(2024, 10, 29, 10, 30, 0) },
                new() { StartDate = new DateTime(2024, 10, 29, 10, 0, 0), EndDate = new DateTime(2024, 10, 29, 10, 0, 0) },
            ], 3, "all slots should be returned as nothing has been booked" },
            {[
                new() { StartDate = new DateTime(2024, 10, 29, 11, 0, 0), EndDate = new DateTime(2024, 10, 29, 12, 0, 0) },
                new() { Booked = true, StartDate = new DateTime(2024, 10, 29, 12, 00, 0), EndDate = new DateTime(2024, 10, 29, 13, 0, 0) },
                new() { StartDate = new DateTime(2024, 10, 29, 13, 0, 0), EndDate = new DateTime(2024, 10, 29, 14, 0, 0) },
            ], 2, "all slots should be returned because there are no conflicts" }
        };
}