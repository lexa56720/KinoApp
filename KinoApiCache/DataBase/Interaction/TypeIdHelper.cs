using KinoApiCache.DataBase.Tables;
using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KinoApiCache.DataBase.Interaction
{
    internal static class TypeIdHelper
    {
        //Идентификаторы кэшируемых функций
        private static Dictionary<string, int> FuncsId = new Dictionary<string, int>()
        {
            {nameof(IMovies.GetMovieByIdAsync), 0},
            {nameof(IMovies.GetMoviesFilteredAsync), 1},
            {nameof(IMovies.GetBestMoviesAsync),2 },
            {nameof(IGenres.GetGenresAsync), 3},
        };

        public static int GetFuncId(string name)
        {
            if (FuncsId.TryGetValue(name, out var id))
            {
                return id;
            }
            return -1;
        }
    }
}
