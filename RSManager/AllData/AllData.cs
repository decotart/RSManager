using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSManager.DataBase;

namespace RSManager.AllData
{
    public static class Data
    {
        public static Car? carTable;
        
        public static Client? clientTable;

        public static Part? partTable;

        public static Work? workTable;

        public static Worker? workerTable;

        public static int? selectedTableIndex;
    }
}
