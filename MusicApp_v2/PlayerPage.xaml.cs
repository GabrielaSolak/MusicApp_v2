namespace MusicApp_v2;

public partial class PlayerPage : ContentPage
{
    public PlayerPage(string title, string artist, string cover)
    {
        InitializeComponent();

        TitleLabel.Text = title;
        ArtistLabel.Text = artist;
        CoverImage.Source = cover;
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}