using KinoApiWrapper.Api.RequestSender;
using KinoApiWrapper.Utils.Abstract;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper.Api
{
    internal class Genres
    {
        private readonly IRequester requester;
        private readonly IConverter converter;

        public Genres(IRequester requester, IConverter converter)
        {
            this.requester = requester;
            this.converter = converter;
        }

        public async Task<Genre[]> GetGenresAsync()
        {
            var result = await requester.Request(@"/api/v2.2/films/filters");
            if (string.IsNullOrEmpty(result))
                return Array.Empty<Genre>();

            return converter.ConvertGenres(result);
        }
    }
}
