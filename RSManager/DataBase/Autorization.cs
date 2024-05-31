using System;
using System.Collections.Generic;

namespace RSManager.DataBase;

public partial class Autorization
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;
}
