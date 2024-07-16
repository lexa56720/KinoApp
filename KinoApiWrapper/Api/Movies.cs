using KinoApiWrapper.ResponseTypes;
using KinoApiWrapper.Utils;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper.Api
{
    public class Movies
    {
        private readonly Requester requester;
        private readonly Converter converter;

        internal Movies(Requester requester, Converter converter)
        {
            this.requester = requester;
            this.converter = converter;
        }

        public async Task<Movie[]> GetMovieByIdAsync(int[] ids)
        {
            var funcs = new Func<Task<Movie>>[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                funcs[i] = () => GetMovieByIdAsync(ids[i]);
            }
            var movies = await RequestLimiter.Call(funcs, 20);
            return movies;
        }
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var result = await requester.Request($@"/api/v2.2/films/{id}");
            if (string.IsNullOrEmpty(result))
                return null;
            return converter.ConvertMovie(result);
        }

        public async Task<Movie[]> GetMovieByYearAsync(int year)
        {
            var result = await requester.Request(@"/api/v2.2/films", new Dictionary<string, string>()
            {
                { "yearFrom",$"{year}" },
                { "yearTo",$"{year}" },
            }); 
            if (string.IsNullOrEmpty(result))
                return null;
            return converter.ConvertSearchResult(result);
        }
        public async Task<Movie[]> GetMovieByGenreAsync(Genre genre)
        {
            var result = await requester.Request(@"/api/v2.2/films", new Dictionary<string, string>()
            {
                { "genres",$"{genre}" },
            });
            if (string.IsNullOrEmpty(result))
                return Array.Empty<Movie>();
            return converter.ConvertSearchResult(result);
        }
    }
}
