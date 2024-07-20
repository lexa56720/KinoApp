﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace KinoApiWrapper.Api.RequestSender
{
    internal interface IRequester
    {
        Task<string> Request(string apiUrl, Dictionary<string, string> args);
        Task<string> Request(string apiUrl);
    }
}
