using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiCache.DataBase.Tables
{
    internal class ArgumentDB
    {
        public int Id { get; set; }
        public int CallId { get; set; }

        public int Index { get; set; }

        public string Value { get; set; }

        public CallDB Call { get; set; }
    }
}
