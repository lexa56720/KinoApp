using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiCache
{
    internal class CacheDbContext:DbContext
    {
        private readonly string connectionString;

        public CacheDbContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
