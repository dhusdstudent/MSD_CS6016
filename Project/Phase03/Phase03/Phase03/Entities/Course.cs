using System;
using System.Collections.Generic;

namespace Phase03.Entities;

public partial class Course
{
    public string? Coursename { get; set; }

    public string Catalogid { get; set; } = null!;

    public string Coursenum { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
