using KinoApiCache.DataBase.Tables.CachedType;

namespace KinoApiCache.Utils
{
    internal interface IMapper
    {
        TKino Map<TKino, TDb>(TDb item)
            where TDb : class, ICachedEntity
            where TKino : class;

        TDb ReverseMap<TKino, TDb>(TKino item)
            where TDb : class, ICachedEntity
            where TKino : class;
    }
}
