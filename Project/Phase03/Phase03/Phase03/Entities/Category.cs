using System;
using System.Collections.Generic;

namespace Phase03.Entities;

public partial class Category
{
    public int? Gradingweight { get; set; }

    public string? Catname { get; set; }

    public int Catid { get; set; }

    public int? Classid { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual Class? Class { get; set; }
}
