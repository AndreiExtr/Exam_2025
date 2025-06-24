using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Task
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime EndDate { get; set; }

    public int StatusTaskId { get; set; }

    public int ProjectId { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual Statustask StatusTask { get; set; } = null!;
}
