using KinoApiWrapper.ResponseTypes;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper.Utils.Abstract
{
    internal interface IMapper
    {
        Movie Map(BriefMovieInfo response);

        MovieInfo Map(FullMovieInfo response);
    }
}
