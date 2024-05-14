namespace PruebaAldaba.Models.Services
{
    public class Movie
    {
        public string Title { get; init; }
        public string OriginalTitle { get; init; }
        public double AverageScore { get; init; }
        public DateTime ReleaseDate { get; init; }
        public string Overview { get; init; }
        public string SimilarMovies { get; init; }
    }
}
