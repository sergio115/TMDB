using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMDB.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieCard : ContentView
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                nameof(Command),
                typeof(ICommand),
                typeof(MovieCard));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(
                nameof(ImageSource),
                typeof(string),
                typeof(MovieCard));

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(MovieCard));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty SelectedStarValueProperty =
            BindableProperty.Create(
                nameof(SelectedStarValue),
                typeof(int),
                typeof(MovieCard));

        public int SelectedStarValue
        {
            get { return (int)GetValue(SelectedStarValueProperty); }
            set { SetValue(SelectedStarValueProperty, value); }
        }
        
        public MovieCard()
        {
            InitializeComponent();
        }
    }
}