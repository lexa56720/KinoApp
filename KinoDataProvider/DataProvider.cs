using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoDataProvider
{
    public class DataProvider : IDataProvider
    {
        public IGenres Genres { get; }

        public IMovies Movies { get; }

        public DataProvider(string apiKey, string url, string connectionString, int itemsPerPage)
        {
            Genres = new GenreProvider();
            Movies = new MovieProvider();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
