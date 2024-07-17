using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiCache.DataBase.Tables
{
    internal class CallDB
    {
        public int Id { get; set; }

        public int FuncId { get; set; }
        public DateTime Date { get; set; }

        public ICollection<ResultDB> Results { get; set; } = new List<ResultDB>();
        public ICollection<ArgumentDB> Arguments { get; set; } = new List<ArgumentDB>();
    }
}
