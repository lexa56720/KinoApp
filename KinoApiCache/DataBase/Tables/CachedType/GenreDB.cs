using KinoApiCache.DataBase.Tables.CachedType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiCache.DataBase.Tables
{
    internal class GenreDB : ICachedEntity
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}
