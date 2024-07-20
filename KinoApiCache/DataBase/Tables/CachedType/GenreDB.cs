using KinoApiCache.DataBase.Tables.CachedType;

namespace KinoApiCache.DataBase.Tables
{
    internal class GenreDB : ICachedEntity
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}
