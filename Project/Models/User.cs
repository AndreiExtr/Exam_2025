using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool BlockCount { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    [NotMapped]
    public int BlockCountIndex
    {
        get => BlockCount ? 1 : 0;
        set => BlockCount = value == 1;
    }
}
