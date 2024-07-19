using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApp.Models
{
    public class FavoriteModel : BaseMovieListModel
    {
        private readonly IReadOnlyList<int> ids;
        private const int pageSize = 20;
        public FavoriteModel(IReadOnlyList<int> ids, IDataProvider provider) : base(provider)
        {
            this.ids = ids;
        }

        public async Task<Movie[]> GetMoviesAsync(int[] alreadyLoaded)
        {
            var filmsToLoad = ids.Where(id => !alreadyLoaded.Contains(id));
            return await dataProvider.Movies.GetMovieByIdAsync(filmsToLoad.Take(pageSize).ToArray());
        }
        public async Task<Movie> GetMovieAsync(int id)
        {
            return await dataProvider.Movies.GetMovieByIdAsync(id);
        }
    }
}
