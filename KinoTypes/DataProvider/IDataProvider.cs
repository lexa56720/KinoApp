using System;

namespace KinoTypes.DataProvider
{
    public interface IDataProvider : IDisposable
    {
        IGenres Genres { get; }
        IMovies Movies { get; }

        IApiInfo ApiInfo { get; }
    }
}
