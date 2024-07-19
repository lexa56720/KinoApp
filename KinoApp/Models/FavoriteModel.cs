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
        private readonly List<int> loaded = new List<int>();
        private const int pageSize = 20;
        public FavoriteModel(IReadOnlyList<int> ids, IDataProvider provider) : base(provider)
        {
            this.ids = ids;
        }

        public override async Task<Movie[]> GetMoviesAsync()
        {
            var filmsToLoad = ids.Where(id => !loaded.Contains(id));
            return await dataProvider.Movies.GetMovieByIdAsync(filmsToLoad.Take(pageSize).ToArray());
        }
        public async Task<Movie> GetMovieAsync(int id)
        {
            return await dataProvider.Movies.GetMovieByIdAsync(id);
        }

        public void AddLoaded(params int[] ids)
        {
            loaded.AddRange(ids);
        }
        public void RemoveLoaded(params int[] ids)
        {
            foreach (var id in ids)
                loaded.Remove(id);
        }
    }
}
