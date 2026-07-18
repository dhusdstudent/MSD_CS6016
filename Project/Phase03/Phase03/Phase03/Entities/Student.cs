using System;
using System.Collections.Generic;

namespace Phase03.Entities;

public partial class Student
{
    public DateOnly? Dob { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string Userid { get; set; } = null!;

    public int? Major { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
