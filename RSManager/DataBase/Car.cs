using System;
using System.Collections.Generic;

namespace RSM.DataBase;

public partial class Car
{
    public int Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int HorsePower { get; set; }

    public virtual ICollection<Part> Parts { get; set; } = new List<Part>();

    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}
