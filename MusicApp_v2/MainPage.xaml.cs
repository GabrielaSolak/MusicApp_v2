using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MusicApp_v2
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Song> Songs { get; set; }
        public ObservableCollection<Song> PopularSongs { get; set; }
        public ICommand OpenPlayerCommand { get; }

        private async void OnSearchCompleted(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            string text = entry?.Text?.Trim();

            if (string.IsNullOrEmpty(text))
                return;

            // wyszukiwanie tytulu
            var song = Songs.FirstOrDefault(s =>
                s.Title.Equals(text, StringComparison.OrdinalIgnoreCase));

            if (song != null)
            {
                int index = Songs.IndexOf(song);
                await Navigation.PushAsync(new PlayerPage(Songs, index));
            }
            else
            {
                await DisplayAlert("Brak wyniku", "Nie znaleziono takiego utworu.", "OK");
            }
        }


        public MainPage()
        {
            InitializeComponent();


            //popularne
            PopularSongs = new ObservableCollection<Song>
        {
            new Song { Cover = "cover1.jpg" },
            new Song { Cover = "cover2.jpg" },
            new Song { Cover = "cover3.jpg" },
            new Song { Cover = "cover4.jpg" },
        };

            //dostępne utwory
            Songs = new ObservableCollection<Song>
        {
            new Song { Title = "Happier Than Ever", Artist = "Billie Eilish", Cover = "cover1.jpg" },
            new Song { Title = "Here Comes The Sun", Artist = "The Beatles", Cover = "cover2.jpg" },
            new Song { Title = "Dancing Queen", Artist = "ABBA", Cover = "cover3.jpg" },
            new Song { Title = "To już było", Artist = "Maryla Rodowicz", Cover = "cover4.jpg" }
        };

            //otwarcie odtwarzacza i przeslanie informacji o utworze
            OpenPlayerCommand = new Command<Song>(async (song) =>
            {
                if (song != null)
                {
                    int index = Songs.IndexOf(song);
                    await Navigation.PushAsync(new PlayerPage(Songs, index));
                }
            });


            BindingContext = this;
        }

        public class Song
        {
            public string Title { get; set; }
            public string Artist { get; set; }
            public string Cover { get; set; }
        }
    }
}