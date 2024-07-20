using KinoTypes.DataProvider;

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
