﻿using KinoTypes;
using KinoTypes.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApp.Models
{
    internal class MovieDetailModel
    {
        private readonly IDataProvider dataProvider;

        public MovieDetailModel(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }
        public async Task<MovieInfo> GetMovieInfo(Movie movie)
        {
            return await dataProvider.Movies.GetMovieByIdAsync(movie.KinopoiskId);
        }
    }
}