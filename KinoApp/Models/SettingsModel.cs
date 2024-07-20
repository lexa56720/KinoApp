using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApp.Models
{
    internal class SettingsModel
    {
        private readonly IDataProvider dataProvider;

        public SettingsModel(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public async Task<bool> IsKeyValid(string key)
        {
            return await dataProvider.ApiInfo.IsKeyValid(key);
        }
    }
}
