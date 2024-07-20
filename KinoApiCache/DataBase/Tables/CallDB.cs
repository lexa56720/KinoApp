using System;
using System.Collections.Generic;

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
