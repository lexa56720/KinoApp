using KinoApiWrapper.Api.RequestSender;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper.Api
{
    internal class ApiInfo : IApiInfo
    {
        private readonly IRequester requester;

        public ApiInfo(IRequester requester)
        {
            this.requester = requester;
        }

        //Проверка валидности ключа API
        public async Task<bool> IsKeyValid(string key)
        {
            var result = await requester.Request($"/api/v1/api_keys/{key}");
            if (string.IsNullOrEmpty(result))
                return false;
            return true;
        }
    }
}
