using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Equipment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public sbyte StatusUse { get; set; }

    public virtual ICollection<Equipmentuse> Equipmentuses { get; set; } = new List<Equipmentuse>();
}
