using PruebaAldaba.Models.Brokers;

namespace PruebaAldaba.Brokers
{
    public interface IMovieBroker
    {
        Task<ExternalMovieResponse?> GetMovieByTitle(string title);
        Task<ExternalMovieResponse[]> GetSimilarMovies(int mainMovieId, int numberOfMovies);
    }
}
