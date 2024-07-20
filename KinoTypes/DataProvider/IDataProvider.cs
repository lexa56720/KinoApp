﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoTypes.DataProvider
{
    public interface IDataProvider:IDisposable
    {
        IGenres Genres { get; }
        IMovies Movies { get; }

        IApiInfo ApiInfo { get; }
    }
}
