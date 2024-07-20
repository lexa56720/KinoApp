using System.Threading.Tasks;

namespace KinoTypes.DataProvider
{
    public interface IGenres
    {
        Task<Genre[]> GetGenresAsync();
    }
}
