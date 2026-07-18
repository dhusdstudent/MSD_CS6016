using System;
using System.Collections.Generic;

namespace Phase03.Entities;

public partial class Class
{
    public string? Professorid { get; set; }

    public short? Year { get; set; }

    public string? Season { get; set; }

    public TimeOnly? Starttime { get; set; }

    public TimeOnly? Endtime { get; set; }

    public int Classid { get; set; }

    public string? Location { get; set; }

    public string? Catalogid { get; set; }

    public virtual Course? Catalog { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Professor? Professor { get; set; }
}
