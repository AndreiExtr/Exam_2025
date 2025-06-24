using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Price { get; set; }

    public int StatusProjectId { get; set; }

    public int ClientId { get; set; }

    public virtual User Client { get; set; } = null!;

    public virtual ICollection<Equipmentuse> Equipmentuses { get; set; } = new List<Equipmentuse>();

    public virtual Statusproject StatusProject { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
