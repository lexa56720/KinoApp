using System.Threading.Tasks;

namespace KinoTypes.DataProvider
{
    public interface IApiInfo
    {
        Task<bool> IsKeyValid(string key);
    }
}
