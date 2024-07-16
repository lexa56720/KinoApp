using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiCache.DataBase
{
    internal class DbContextFactory
    {
        private readonly string connectionString;

        public DbContextFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public CacheDbContext Create()
        {
            return new CacheDbContext(connectionString);
        }
    }
}
