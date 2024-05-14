using Microsoft.Extensions.Caching.Memory;
using PruebaAldaba.Brokers;
using PruebaAldaba.Models.Brokers;
using PruebaAldaba.Models.Services;

namespace PruebaAldaba.Services
{
    public partial class MovieService : IMovieService
    {
        private readonly IMemoryCache _cache;
        private IMovieBroker movieBroker;

        public MovieService(
            IMemoryCache cache,
            IMovieBroker movieBroker)
        {
            this._cache = cache;
            this.movieBroker = movieBroker;
        }

        public async Task<Movie> GetMovieByTitle(string title)
        {
            Movie movie = null;
            ValidateMovieParameters(title);

            if (!_cache.TryGetValue(title.ToLower(), out Movie movieInCache))
            {
                ExternalMovieResponse? externalMovieResponse = await movieBroker.GetMovieByTitle(title);

                ValidateMovieExistence(externalMovieResponse);

                ExternalMovieResponse[] similarExternalMoviesResponse =
                    (await movieBroker.GetSimilarMovies(
                        mainMovieId: externalMovieResponse.Id,
                        numberOfMovies: 5))
                    .ToArray();

                movie = MovieMap(externalMovieResponse, similarExternalMoviesResponse);

                _cache.Set(
                    title.ToLower(),
                    movie,
                    TimeSpan.FromMinutes(10));
            }
            else
            {
                movie = movieInCache;
            }

            return movie;
        }
    }
}
