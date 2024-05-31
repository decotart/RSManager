using System;
using System.Collections.Generic;

namespace RSManager.DataBase;

public partial class Work
{
    public int Id { get; set; }

    public int Client { get; set; }

    public int Car { get; set; }

    public int Worker { get; set; }

    public int SumOfWork { get; set; }

    public virtual Car CarNavigation { get; set; } = null!;

    public virtual Client ClientNavigation { get; set; } = null!;

    public virtual Worker WorkerNavigation { get; set; } = null!;

    public virtual ICollection<WorksPart> WorksParts { get; set; } = new List<WorksPart>();
}
