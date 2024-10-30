using System;
using System.Collections.Generic;

namespace SalesAppointments.Data.Entities;

public partial class Slot
{
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool Booked { get; set; }

    public int SalesManagerId { get; set; }

    public virtual SalesManager SalesManager { get; set; } = null!;
}
