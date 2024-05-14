using PruebaAldaba.Models.Services;

namespace PruebaAldaba.Services
{
    public interface IMovieService
    {
        Task<Movie> GetMovieByTitle(string title);
    }
}
