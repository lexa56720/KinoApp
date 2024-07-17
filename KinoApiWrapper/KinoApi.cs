using KinoApiWrapper.Api;
using KinoApiWrapper.Api.RequestSender;
using KinoApiWrapper.Utils;
using KinoApiWrapper.Utils.Abstract;
using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper
{
    public class KinoApi : IDataProvider, IDisposable
    {
        public IMovies Movies { get; }
        public IGenres Genres { get; }

        private readonly IRequester requester;
        private readonly IConverter converter;

        public KinoApi(string apiKey, string url)
        {
            requester = new Requester(apiKey, url);
            converter = new Converter(new Mapper());

            Movies = new Movies(requester, converter);
            Genres = new Genres(requester, converter);
        }

        public void Dispose()
        {
            ((IDisposable)requester).Dispose();
        }
    }
}
