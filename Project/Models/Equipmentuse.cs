using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Equipmentuse
{
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int ProjectId { get; set; }

    public int EquipmentId { get; set; }

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
