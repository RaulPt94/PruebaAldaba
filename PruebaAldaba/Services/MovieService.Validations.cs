using PruebaAldaba.Models.Brokers;
using PruebaAldaba.Models.Services;
using PruebaAldaba.Models.Services.Exceptions;

namespace PruebaAldaba.Services
{
    public partial class MovieService
    {
        private void ValidateMovieParameters(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new InvalidParametersException();
        }

        private void ValidateMovieExistence(ExternalMovieResponse? movieResponse)
        {
            if (movieResponse == null)
                throw new MovieNotFoundException();
        }
    }
}
