using System;
using System.Collections.Generic;

namespace SalesAppointments.Data.Entities;

public partial class SalesManager
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public List<string>? Languages { get; set; }

    public List<string>? Products { get; set; }

    public List<string>? CustomerRatings { get; set; }

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
}
