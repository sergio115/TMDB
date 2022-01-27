using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDB.Models;
using TMDB.Services;
using Xamarin.Forms;

namespace TMDB.ViewModels
{
    public class MovieDetailsViewModel : BaseViewModel
    {
        private readonly IMovieService _movieService;

        #region Properties
        private Movie _movieDetail;
        public Movie MovieDetail
        {
            get { return _movieDetail; }
            set { SetProperty(ref _movieDetail, value); }
        }

        private string _studio;
        public string Studio
        {
            get { return _studio; }
            set { SetProperty(ref _studio, value); }
        }

        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set { SetProperty(ref _genre, value); }
        }     

        private string _imageProfile;
        public string ImageProfile
        {
            get { return _imageProfile; }
            set { SetProperty(ref _imageProfile, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _releaseDateYear;
        public string ReleaseDateYear
        {
            get { return _releaseDateYear; }
            set { SetProperty(ref _releaseDateYear, value); }
        }
        #endregion

        #region Commands
        public ICommand NavigationToBackCommand { get; private set; }
        #endregion

        public MovieDetailsViewModel(long id)
        {
            _movieService = new MovieService();

            NavigationToBackCommand = new Command(async () => await NavigationToBack());
            Task.Run(() => GetMovieDetail(id)).Wait();
        }

        private Task NavigationToBack() => App.Current.MainPage.Navigation.PopAsync();

        private async Task GetMovieDetail(long id)
        {
            MovieDetail = await _movieService.GetMovieDetailsAsync(id);
            ReleaseDateYear = MovieDetail.ReleaseDate.Year.ToString();
            Studio = ConcatenateStudios(MovieDetail.ProductionCompanies);
            Genre = ConcatenateGenres(MovieDetail.Genres);
        }

        private string ConcatenateStudios(List<ProductionCompany> list)
        {
            string text = string.Empty;
         
            for (int i = 0; i < list.Count; i++)
            {
                if (i < list.Count-1)
                    text += $"{list[i].Name}, ";
                else
                    text += list[i].Name;
            }

            return text;
        }

        private string ConcatenateGenres(List<Genre> list)
        {
            string text = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                if (i < list.Count-1)
                    text += $"{list[i].Name}, ";
                else
                    text += list[i].Name;
            }

            return text;
        }
    }
}
