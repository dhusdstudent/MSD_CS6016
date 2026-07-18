using System;
using System.Collections.Generic;

namespace Phase03.Entities;

public partial class Submission
{
    public DateTime Submittedat { get; set; }

    public string? Contents { get; set; }

    public int? Score { get; set; }

    public string Userid { get; set; } = null!;

    public int? Assignmentid { get; set; }

    public virtual Assignment? Assignment { get; set; }

    public virtual Student User { get; set; } = null!;
}
