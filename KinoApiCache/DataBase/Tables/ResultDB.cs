using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiCache.DataBase.Tables
{
    internal class ResultDB
    {
        public int Id { get; set; }
        public int CallId { get; set; }

        public int IndexId { get; set; }
        public int ValueId { get; set; }
        public CallDB Call { get; set; }
    }
}
