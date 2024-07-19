﻿using KinoTypes.DataProvider;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace KinoApp.Models
{
    public abstract class BaseMovieListModel
    {
        protected readonly IDataProvider dataProvider;

        public BaseMovieListModel(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }
    }
}
