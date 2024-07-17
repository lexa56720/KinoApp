using System;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

namespace KinoApp.Core.Helpers
{
    public static class Json
    {
        public static async Task<T> ToObjectAsync<T>(string value)
        {
            return await Task.Run<T>(() =>
            {
                return JsonSerializer.Deserialize<T>(value);
            });
        }

        public static async Task<string> StringifyAsync<T>(T value)
        {
            return await Task.Run<string>(() =>
            {
                return JsonSerializer.Serialize<T>(value);
            });
        }
    }
}
