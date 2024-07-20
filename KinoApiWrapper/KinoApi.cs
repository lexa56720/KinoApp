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
        public IApiInfo ApiInfo { get; }


        private readonly Requester requester;
        private readonly IConverter converter;
        private bool isDisposed;
        public KinoApi(string apiKey, string url)
        {
            requester = new Requester(apiKey, url);
            converter = new Converter(new Mapper());

            Movies = new Movies(requester, converter);
            Genres = new Genres(requester, converter);
            ApiInfo = new ApiInfo(requester);
        }

        public void UpdateApiKey(string key)
        {
            requester.UpdateApiKey(key);
        }

        public void Dispose()
        {
            if (isDisposed)
                return;
            ((IDisposable)requester).Dispose();
            isDisposed = true;
        }
    }
}
