using KinoApiWrapper.Api;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper
{
    internal class KinoApi:IDisposable
    {
        public Movies Movies { get; }

        private readonly Requester requester;
        private readonly Converter converter;

        public KinoApi(string apiKey,string url)
        {
            requester = new Requester(apiKey,url);
            converter=new Converter();

            Movies = new Movies(requester,converter);
        }

        public void Dispose()
        {
            ((IDisposable)requester).Dispose();
        }
    }
}
