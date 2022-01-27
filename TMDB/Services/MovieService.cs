using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMDB.Models;

namespace TMDB.Services
{
    public class MovieService : IMovieService
    {
        readonly HttpClient _client;
        public ObservableCollection<MovieCard> MoviesList { get; set; }

        public MovieService()
        {
            _client = new HttpClient();
        }

        public async Task<ObservableCollection<MovieCard>> GetMoviesAsync(string section)
        {
            Uri uri = DeterminateUriMoviesList(section);
            MoviesList = new ObservableCollection<MovieCard>();

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(content);
                    var results = json["results"].Children().Take(10);
                    foreach (var item in results)
                    {
                        var movie = JsonConvert.DeserializeObject<MovieCard>(item.ToString());
                        movie.PosterPath = Constants.BaseAddresImg + Constants.Size + movie.PosterPath;
                        movie.Starts = (int)(float)Math.Floor(movie.VoteAverage / 2);
                        MoviesList.Add(movie);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return MoviesList;
        }

        public Uri DeterminateUriMoviesList(string section)
        {
            Uri uri;
            switch (section)
            {
                case "topRated":
                    uri = new Uri(string.Format(Constants.BaseAddres +
                        Constants.TopRated +
                        "?api_key=" + Constants.APIKey +
                        "&language=" + Constants.Language +
                        "&page=1", string.Empty));
                    break;
                case "upcoming":
                    uri = new Uri(string.Format(Constants.BaseAddres +
                        Constants.UpComing +
                        "?api_key=" + Constants.APIKey +
                        "&language=" + Constants.Language +
                        "&page=1", string.Empty));
                    break;
                default:
                    uri = new Uri(string.Format(Constants.BaseAddres +
                        Constants.Pupular +
                        "?api_key=" + Constants.APIKey +
                        "&language=" + Constants.Language +
                        "&page=1", string.Empty));
                    break;
            }
            return uri;
        }

        public async Task<Movie> GetMovieDetailsAsync(long id)
        {
            Movie movie = new Movie();

            try
            {
                movie = await GetMovieInfo(id);
                movie.Characters = await GetMovieCredits(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return movie;
        }

        public async Task<Movie> GetMovieInfo(long id)
        {
            Uri uri = new Uri(string.Format(Constants.BaseAddres +
                "/" + id +
                "?api_key=" + Constants.APIKey +
                "&language=" + Constants.Language, string.Empty));

            Movie movie = new Movie();

            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);
                movie = JsonConvert.DeserializeObject<Movie>(json.ToString());
                movie.ImageUrl = Constants.BaseAddresImg + Constants.OriginalSize + movie.ImageUrl;
                movie.Starts = (int)(float)Math.Floor(movie.VoteAverage / 2);
            }

            return movie;
        }

        private async Task<List<Character>> GetMovieCredits(long id)
        {
            Uri uri = new Uri(string.Format(Constants.BaseAddres +
                "/" + id +
                Constants.Credits +
                "?api_key=" + Constants.APIKey +
                "&language=" + Constants.Language, string.Empty));

            List<Character> Characters = new List<Character>();

            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);
                var cast = json["cast"].Children().Take(10);

                foreach (var item in cast)
                {
                    var character = JsonConvert.DeserializeObject<Character>(item.ToString());
                    character.ImageProfile = Constants.BaseAddresImg + Constants.MinimumSize + character.ImageProfile;
                    
                    Characters.Add(character);
                }
            }

            return Characters;
        }
    }
}
