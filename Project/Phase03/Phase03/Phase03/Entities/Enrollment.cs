using System;
using System.Collections.Generic;

namespace Phase03.Entities;

public partial class Enrollment
{
    public string Userid { get; set; } = null!;

    public string Subject { get; set; }
    
    public int Classid { get; set; }
    
    public string Semester { get; set; }
    
    public short Year { get; set; }

    public string? Grade { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Student User { get; set; } = null!;
}
