using System;
using System.Collections.Generic;

namespace Phase03.Entities;

public partial class Lmsuser
{
    public DateOnly? Dob { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string Userid { get; set; } = null!;
}
