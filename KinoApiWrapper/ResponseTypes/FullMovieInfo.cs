using KinoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KinoApiWrapper.ResponseTypes
{
    internal class FullMovieInfo
    {
        [JsonPropertyName("kinopoiskId")]
        public int? KinopoiskId { get; set; }

        [JsonPropertyName("kinopoiskHDId")]
        public object KinopoiskHDId { get; set; }

        [JsonPropertyName("imdbId")]
        public string ImdbId { get; set; }

        [JsonPropertyName("nameRu")]
        public string NameRu { get; set; }

        [JsonPropertyName("nameEn")]
        public object NameEn { get; set; }

        [JsonPropertyName("nameOriginal")]
        public string NameOriginal { get; set; }

        [JsonPropertyName("posterUrl")]
        public string PosterUrl { get; set; }

        [JsonPropertyName("posterUrlPreview")]
        public string PosterUrlPreview { get; set; }

        [JsonPropertyName("coverUrl")]
        public object CoverUrl { get; set; }

        [JsonPropertyName("logoUrl")]
        public object LogoUrl { get; set; }

        [JsonPropertyName("reviewsCount")]
        public int? ReviewsCount { get; set; }

        [JsonPropertyName("ratingGoodReview")]
        public object RatingGoodReview { get; set; }

        [JsonPropertyName("ratingGoodReviewVoteCount")]
        public int? RatingGoodReviewVoteCount { get; set; }

        [JsonPropertyName("ratingKinopoisk")]
        public object RatingKinopoisk { get; set; }

        [JsonPropertyName("ratingKinopoiskVoteCount")]
        public int? RatingKinopoiskVoteCount { get; set; }

        [JsonPropertyName("ratingImdb")]
        public double? RatingImdb { get; set; }

        [JsonPropertyName("ratingImdbVoteCount")]
        public int? RatingImdbVoteCount { get; set; }

        [JsonPropertyName("ratingFilmCritics")]
        public object RatingFilmCritics { get; set; }

        [JsonPropertyName("ratingFilmCriticsVoteCount")]
        public int? RatingFilmCriticsVoteCount { get; set; }

        [JsonPropertyName("ratingAwait")]
        public object RatingAwait { get; set; }

        [JsonPropertyName("ratingAwaitCount")]
        public int? RatingAwaitCount { get; set; }

        [JsonPropertyName("ratingRfCritics")]
        public object RatingRfCritics { get; set; }

        [JsonPropertyName("ratingRfCriticsVoteCount")]
        public int? RatingRfCriticsVoteCount { get; set; }

        [JsonPropertyName("webUrl")]
        public string WebUrl { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }

        [JsonPropertyName("filmLength")]
        public int? FilmLength { get; set; }

        [JsonPropertyName("slogan")]
        public string Slogan { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("shortDescription")]
        public object ShortDescription { get; set; }

        [JsonPropertyName("editorAnnotation")]
        public object EditorAnnotation { get; set; }

        [JsonPropertyName("isTicketsAvailable")]
        public bool? IsTicketsAvailable { get; set; }

        [JsonPropertyName("productionStatus")]
        public object ProductionStatus { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("ratingMpaa")]
        public object RatingMpaa { get; set; }

        [JsonPropertyName("ratingAgeLimits")]
        public object RatingAgeLimits { get; set; }

        [JsonPropertyName("countries")]
        public ApiCountry[] Countries { get; set; }

        [JsonPropertyName("genres")]
        public ApiGenre[] Genres { get; set; }

        [JsonPropertyName("startYear")]
        public object StartYear { get; set; }

        [JsonPropertyName("endYear")]
        public object EndYear { get; set; }

        [JsonPropertyName("serial")]
        public bool? Serial { get; set; }

        [JsonPropertyName("shortFilm")]
        public bool? ShortFilm { get; set; }

        [JsonPropertyName("completed")]
        public bool? Completed { get; set; }

        [JsonPropertyName("hasImax")]
        public bool? HasImax { get; set; }

        [JsonPropertyName("has3D")]
        public bool? Has3D { get; set; }

        [JsonPropertyName("lastSync")]
        public DateTime? LastSync { get; set; }
    }
}
