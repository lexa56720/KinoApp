namespace KinoApiCache.DataBase.Tables.CachedType
{
    internal interface ICachedEntity
    {
        int Id { get; set; }

        int CallId { get; set; }
        CallDB Call { get; set; }
    }
}
