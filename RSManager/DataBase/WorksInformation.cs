using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSManager.DataBase
{
    public partial class WorksInformation
    {
        public int Id { get; set; }

        public string Client { get; set; } = null!;

        public string Car { get; set; } = null!;

        public string Worker { get; set; } = null!;

        public int SuOfWork { get; set; }
    }
}
