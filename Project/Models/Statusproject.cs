using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Statusproject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
