using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TMDB.Models;

namespace TMDB.Services
{
    public interface IMovieService
    {
        Task<ObservableCollection<MovieCard>> GetMoviesAsync(string section);
        Task<Movie> GetMovieDetailsAsync(long id);
    }
}
