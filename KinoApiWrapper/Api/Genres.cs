using KinoApiWrapper.Api.RequestSender;
using KinoApiWrapper.Utils.Abstract;
using KinoTypes;
using KinoTypes.DataProvider;
using System.Threading.Tasks;

namespace KinoApiWrapper.Api
{
    internal class Genres : IGenres
    {
        private readonly IRequester requester;
        private readonly IConverter converter;

        public Genres(IRequester requester, IConverter converter)
        {
            this.requester = requester;
            this.converter = converter;
        }

        //Получение списка жанров
        public async Task<Genre[]> GetGenresAsync()
        {
            var result = await requester.Request(@"/api/v2.2/films/filters");
            if (string.IsNullOrEmpty(result))
                return null;

            return converter.ConvertGenres(result);
        }
    }
}
