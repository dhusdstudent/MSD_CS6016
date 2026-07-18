using System;
using System.Collections.Generic;

namespace Phase03.Entities;

public partial class Assignment
{
    public string? Assignmentname { get; set; }

    public int Assignmentid { get; set; }

    public int? Pointval { get; set; }

    public int? Catid { get; set; }

    public DateTime? Duedate { get; set; }

    public virtual Category? Cat { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
