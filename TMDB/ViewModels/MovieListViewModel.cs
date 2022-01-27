using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDB.Models;
using TMDB.Services;
using Xamarin.Forms;

namespace TMDB.ViewModels
{
    public class MovieListViewModel : BaseViewModel
    {
        private readonly IMovieService _movieService;

        #region Properties
        private MovieCard _movieCard;
        public MovieCard MovieCard
        {
            get { return _movieCard; }
            set { SetProperty(ref _movieCard, value); }
        }

        private ObservableCollection<MovieCard> _movieListTopRated;
        public ObservableCollection<MovieCard> MovieListTopRated
        {
            get
            {
                var moviesCollection = ChoseCollection(_movieListTopRated);
                IsVisibleMovieListTopRated = IsThereSomeElement(moviesCollection);
                IsVisibleWarningTopRated = !IsVisibleMovieListTopRated;

                return moviesCollection;
            }
            set { SetProperty(ref _movieListTopRated, value); }
        }

        private ObservableCollection<MovieCard> _movieListUpcoming;
        public ObservableCollection<MovieCard> MovieListUpcoming
        {
            get
            {
                var moviesCollection = ChoseCollection(_movieListUpcoming);
                IsVisibleMovieListUpcoming = IsThereSomeElement(moviesCollection);
                IsVisibleWarningUpcoming = !IsVisibleMovieListUpcoming;

                return moviesCollection;
            }
            set { SetProperty(ref _movieListUpcoming, value); }
        }

        private ObservableCollection<MovieCard> _movieListPopular;
        public ObservableCollection<MovieCard> MovieListPopular
        {
            get
            {
                var moviesCollection = ChoseCollection(_movieListPopular);
                IsVisibleMovieListPopular = IsThereSomeElement(moviesCollection);
                IsVisibleWarningPopular = !IsVisibleMovieListPopular;

                return moviesCollection;
            }
            set { SetProperty(ref _movieListPopular, value); }
        }

        private long _id;
        public long Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _titleName;
        public string TitleName
        {
            get { return _titleName; }
            set { SetProperty(ref _titleName, value); }
        }      

        private int _starts;
        public int Starts
        {
            get { return _starts; }
            set { SetProperty(ref _starts, value); }
        }
        private string _posterPath;
        public string PosterPath
        {
            get { return _posterPath; }
            set { SetProperty(ref _posterPath, value); }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value ?? string.Empty;
                    SetProperty(ref _searchText, value);
                    Task.Run(() => Search()).Wait();
                }
            }
        }

        private bool _isVisibleMovieListTopRated;
        public bool IsVisibleMovieListTopRated
        {
            get { return _isVisibleMovieListTopRated; }
            set { SetProperty(ref _isVisibleMovieListTopRated, value); }
        }
        
        private bool _isVisibleWarningTopRated;
        public bool IsVisibleWarningTopRated
        {
            get { return _isVisibleWarningTopRated; }
            set { SetProperty(ref _isVisibleWarningTopRated, value); }
        }

        private bool _isVisibleMovieListUpcoming;
        public bool IsVisibleMovieListUpcoming
        {
            get { return _isVisibleMovieListUpcoming; }
            set { SetProperty(ref _isVisibleMovieListUpcoming, value); }
        }

        private bool _isVisibleWarningUpcoming;
        public bool IsVisibleWarningUpcoming
        {
            get { return _isVisibleWarningUpcoming; }
            set { SetProperty(ref _isVisibleWarningUpcoming, value); }
        }

        private bool _isVisibleMovieListPopular;
        public bool IsVisibleMovieListPopular
        {
            get { return _isVisibleMovieListPopular; }
            set { SetProperty(ref _isVisibleMovieListPopular, value); }
        }

        private bool _isVisibleWarningPopular;
        public bool IsVisibleWarningPopular
        {
            get { return _isVisibleWarningPopular; }
            set { SetProperty(ref _isVisibleWarningPopular, value); }
        }
        #endregion

        #region Commands
        public ICommand ViewDetailsMovieCommand { get; private set; }
        public ICommand SearchCommand { get; }
        #endregion

        public MovieListViewModel()
        {
            _movieService = new MovieService();

            MovieCard = new MovieCard
            {
                Id = Id,
                TitleName = TitleName,
                Starts = Starts,
                PosterPath = PosterPath
            };

            ViewDetailsMovieCommand = new Command(async (movie) => await ViewDetailsMovie((MovieCard)movie));
            SearchCommand = new Command(async () => await Search());
            Task.Run(() => Init()).Wait();
        }

        private async Task ViewDetailsMovie(MovieCard movie)
        {
            if (movie != null)
                await NavigateToform(movie);
        }

        private async Task NavigateToform(MovieCard movie)
        {
            var vm = new MovieDetailsViewModel(movie.Id);
            var page = new Views.MovieDetailsPage
            {
                BindingContext = vm
            };

            await App.Current.MainPage.Navigation.PushAsync(page);
        }

        private async Task Search()
        {
            OnPropertyChanged("MovieListTopRated");
            OnPropertyChanged("MovieListUpcoming");
            OnPropertyChanged("MovieListPopular");
        }

        private async Task Init()
        {
            MovieListTopRated = await _movieService.GetMoviesAsync("topRated");
            MovieListUpcoming = await _movieService.GetMoviesAsync("upcoming");
            MovieListPopular = await _movieService.GetMoviesAsync("");
        }

        private ObservableCollection<MovieCard> ChoseCollection(ObservableCollection<MovieCard> movieCards)
        {
            try
            {
                if ((movieCards != null) && (_searchText.Length >= 3))
                {
                    var moviesCollection = FilterMovies(movieCards);
                    return moviesCollection;
                }
                else
                    return movieCards;
            }
            catch (Exception)
            {
                return movieCards;
            }
        }

        private ObservableCollection<MovieCard> FilterMovies(ObservableCollection<MovieCard> movieCards)
        {
            var moviesCollection = new ObservableCollection<MovieCard>();

            List<MovieCard> movies = (from movie in movieCards
                                      where movie.TitleName.ToLower().Contains(_searchText.ToLower())
                                      select movie).ToList();

            if (movies != null && movies.Any())
                moviesCollection = new ObservableCollection<MovieCard>(movies);

            return moviesCollection;
        }

        private bool IsThereSomeElement(ObservableCollection<MovieCard> moviesCollection)
        {
            if(moviesCollection.Count.Equals(0))
                return false;

            return true;
        }
    }
}
