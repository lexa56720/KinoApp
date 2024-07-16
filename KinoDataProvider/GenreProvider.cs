using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoDataProvider
{
    internal class GenreProvider : IGenres
    {
        public Task<Genre[]> GetGenresAsync()
        {
            throw new NotImplementedException();
        }
    }
}
