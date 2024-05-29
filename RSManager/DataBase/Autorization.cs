using System;
using System.Collections.Generic;

namespace RSM.DataBase;

public partial class Autorization
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;
}
