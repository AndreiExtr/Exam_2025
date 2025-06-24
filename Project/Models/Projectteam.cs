using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Projectteam
{
    public int ProjectId { get; set; }

    public int TeamId { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
