using KinoApp.Helpers;
using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace KinoApp.Services
{
    public class FavoriteChangedEventArgs : EventArgs
    {
        public int Id { get; }
        public bool IsAdded { get; }

        public FavoriteChangedEventArgs(int id, bool isAdded)
        {
            Id = id;
            IsAdded = isAdded;
        }
    }
    internal static class FavoriteService
    {
        private static List<int> FavoriteIds = new List<int>();
        public static IReadOnlyList<int> List => FavoriteIds.AsReadOnly();

        public static EventHandler<FavoriteChangedEventArgs> FavoriteChanged;
        private static ApplicationDataContainer storage;

        static FavoriteService()
        {
            storage = ApplicationData.Current.LocalSettings.CreateContainer("favorite", ApplicationDataCreateDisposition.Always);
            var saved = storage.Read<int[]>("favorite");
            if (saved != null)
                FavoriteIds = new List<int>(saved);
            else
                FavoriteIds = new List<int>();
        }

        public static bool Add(Movie movie)
        {
            if (FavoriteIds.Contains(movie.KinopoiskId))
                return false;

            FavoriteIds.Add(movie.KinopoiskId);
            Notify(movie.KinopoiskId, true);
            return true;
        }

        public static bool Remove(Movie movie)
        {
            if (!FavoriteIds.Contains(movie.KinopoiskId))
                return false;

            FavoriteIds.Remove(movie.KinopoiskId);
            Notify(movie.KinopoiskId, false);
            return true;
        }

        public static bool IsContains(Movie movie)
        {
            return FavoriteIds.Contains(movie.KinopoiskId);
        }

        public static void Save()
        {
            storage.Save("favorite", FavoriteIds.ToArray());
        }

        private static void Notify(int id, bool isAdded)
        {
            FavoriteChanged?.Invoke(null, new FavoriteChangedEventArgs(id, isAdded));
        }

        internal static void Clear()
        {
            while(FavoriteIds.Count > 0) 
            {
                var movie= FavoriteIds.First();
                FavoriteIds.Remove(movie);
                Notify(movie, false);
            }
            Save();
        }
    }
}
