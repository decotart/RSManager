using System;
using System.Collections.Generic;

namespace RSManager.DataBase;

public partial class Part
{
    public int Id { get; set; }

    public string PartName { get; set; } = null!;

    public int PartCount { get; set; }

    public int PartSum { get; set; }

    public int SuitableCar { get; set; }

    public virtual Car SuitableCarNavigation { get; set; } = null!;

    public virtual ICollection<WorksPart> WorksParts { get; set; } = new List<WorksPart>();
}
