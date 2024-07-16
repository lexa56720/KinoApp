using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoTypes.DataProvider
{
    public interface IGenres
    {
        public async Task<Genre[]> GetGenresAsync();
    }
}
