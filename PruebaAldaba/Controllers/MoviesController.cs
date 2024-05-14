using Microsoft.AspNetCore.Mvc;
using PruebaAldaba.Models.Brokers.Exceptions;
using PruebaAldaba.Models.Services;
using PruebaAldaba.Models.Services.Exceptions;
using PruebaAldaba.Services;

namespace PruebaAldaba.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet("{title}")]
        [ProducesResponseType(typeof(Movie), 200)]
        public async Task<IActionResult> GetMovieByTitle(string title)
        {
            try
            {
                var movie = await movieService.GetMovieByTitle(title);

                return Ok(movie);
            }
            catch (InvalidParametersException)
            {
                return BadRequest("Title must be specified");
            }
            catch (MovieNotFoundException)
            {
                return NotFound("The movie was not found");
            }
            catch (BadTokenException)
            {
                return Unauthorized("Request token is not valid");
            }
            catch (UnattainableHostException)
            {
                return StatusCode(StatusCodes.Status502BadGateway, "Can't access to the upstream server.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal error has been produced.");
            }
        }
    }
}
