using System;
using System.Collections.Generic;

namespace RSManager.DataBase;

public partial class Worker
{
    public int Id { get; set; }

    public string Fio { get; set; } = null!;

    public string Post { get; set; } = null!;

    public int Experience { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}
