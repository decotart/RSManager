using System;
using System.Collections.Generic;

namespace RSManager;

public partial class Client
{
    public int Id { get; set; }

    public string Fio { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}
