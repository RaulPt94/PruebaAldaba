using PruebaAldaba.Models.Brokers;
using PruebaAldaba.Models.Brokers.Exceptions;
using System.Text.Json;

namespace PruebaAldaba.Brokers
{
    public class MovieBroker : IMovieBroker
    {
        private readonly Credential _credential;

        public MovieBroker(Credential credential)
        {
            this._credential = credential;
        }

        private async Task<ExternalMovieSearchResponse?> SendMovieRequest(string url)
        {
            string responseBody = "";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_credential.Token}");
                HttpResponseMessage? response = null;
                try
                {
                    response = await httpClient.GetAsync(url);
                }
                catch (Exception)
                {
                    throw new UnattainableHostException();
                }

                if (response.IsSuccessStatusCode)
                {
                    responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        throw new BadTokenException();
                }
            }

            return !string.IsNullOrEmpty(responseBody)
                ? JsonSerializer.Deserialize<ExternalMovieSearchResponse>(responseBody)
                : null;
        }

        public async Task<ExternalMovieResponse?> GetMovieByTitle(string title)
        {
            string url = $"https://api.themoviedb.org/3/search/movie?query={title}";

            ExternalMovieSearchResponse? externalMovieSearchResponse = await SendMovieRequest(url);

            return externalMovieSearchResponse?.Results != null
                    ? externalMovieSearchResponse.Results.FirstOrDefault()
                    : null;
        }

        public async Task<ExternalMovieResponse[]> GetSimilarMovies(int mainMovieId, int numberOfMovies)
        {
            string url = $"https://api.themoviedb.org/3/movie/{mainMovieId}/similar";

            ExternalMovieSearchResponse? externalMovieSearchResponse = await SendMovieRequest(url);

            return externalMovieSearchResponse?.Results != null
                    && externalMovieSearchResponse.Results.Count() > numberOfMovies
                        ? externalMovieSearchResponse.Results.Take(numberOfMovies).ToArray()
                        : externalMovieSearchResponse?.Results?.ToArray()
                            ?? Array.Empty<ExternalMovieResponse>();
        }

    }
}
