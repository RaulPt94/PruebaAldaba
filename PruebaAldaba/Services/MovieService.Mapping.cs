using PruebaAldaba.Models.Brokers;
using PruebaAldaba.Models.Services;

namespace PruebaAldaba.Services
{
    public partial class MovieService
    {
        public Movie MovieMap(ExternalMovieResponse externalMovieResponse, ExternalMovieResponse[]? similarExternalMoviesResponse) =>
            new Movie
            {
                Title = externalMovieResponse.Title,
                OriginalTitle = externalMovieResponse.OriginalTitle,
                AverageScore = externalMovieResponse.VoteAverage,
                ReleaseDate = DateTime.Parse(externalMovieResponse.ReleaseDate).Date,
                Overview = externalMovieResponse.Overview,
                SimilarMovies = 
                    String.Join(
                        ", ",
                        similarExternalMoviesResponse
                            .Select(similarExternalMovieResponse => $"{similarExternalMovieResponse.Title} ({DateTime.Parse(similarExternalMovieResponse.ReleaseDate).Year})"))
            };
    }
}
