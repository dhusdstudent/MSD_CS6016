using System;
using System.Collections.Generic;

namespace Phase03.Entities;

public partial class Professor
{
    public DateOnly? Dob { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string Userid { get; set; } = null!;

    public string? Employer { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
