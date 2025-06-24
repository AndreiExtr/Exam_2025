using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Team
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }

    public int PositionId { get; set; }

    public int EmployeeId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual User Employee { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;
}
