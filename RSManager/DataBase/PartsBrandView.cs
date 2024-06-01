using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSManager.DataBase
{
    public class PartsBrandsView
    {
        public int Id { get; set; }

        public string PartName { get; set; } = null!;      

        public int PartCount { get; set; }

        public int PartSum { get; set; }

        public string Brand { get; set; }
    }
}
