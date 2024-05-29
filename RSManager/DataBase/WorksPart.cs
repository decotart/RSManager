using System;
using System.Collections.Generic;

namespace RSM.DataBase;

public partial class WorksPart
{
    public int WorksId { get; set; }

    public int PartId { get; set; }

    public int CountOfParts { get; set; }

    public virtual Part Part { get; set; } = null!;

    public virtual Work Works { get; set; } = null!;
}
