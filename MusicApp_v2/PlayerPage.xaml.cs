using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Media;
using CommunityToolkit.Maui.Views;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;

namespace MusicApp_v2;

public partial class PlayerPage : ContentPage
{
    private ObservableCollection<MainPage.Song> _songs;
    private int _currentIndex;

    private IAudioPlayer _player;
    private readonly IAudioManager _audioManager;

    public PlayerPage(ObservableCollection<MainPage.Song> songs, int index)
    {
        InitializeComponent();

        _songs = songs;
        _currentIndex = index;

        _audioManager = AudioManager.Current;

        LoadSongToUI();
        _ = PlaySong();
    }

    private void LoadSongToUI()
    {
        var song = _songs[_currentIndex];

        TitleLabel.Text = song.Title;
        ArtistLabel.Text = song.Artist ?? "";
        CoverImage.Source = song.Cover ?? "default_cover.png";
    }

    // Wstecz
    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        _player?.Stop();
        _player?.Dispose();
        await Navigation.PopAsync();
    }

    // Poprzednia piosenka
    private async void PrevButton_Clicked(object sender, EventArgs e)
    {
        _currentIndex = (_currentIndex - 1 + _songs.Count) % _songs.Count;
        LoadSongToUI();
        await PlaySong();
    }

    // Nastêpna
    private async void NextButton_Clicked(object sender, EventArgs e)
    {
        _currentIndex = (_currentIndex + 1) % _songs.Count;
        LoadSongToUI();
        await PlaySong();
    }

    // Play / Pause
    private void PlayPauseButton_Clicked(object sender, EventArgs e)
    {
        if (_player == null)
            return;

        if (_player.IsPlaying)
            _player.Pause();
        else
            _player.Play();
    }

    private async Task PlaySong()
    {
        try
        {
            var song = _songs[_currentIndex];

            _player?.Stop();
            _player?.Dispose();

            string audioFileName = $"{song.Cover.Replace(".jpg", ".mp3")}";

            using var stream = await FileSystem.OpenAppPackageFileAsync(audioFileName);
            _player = _audioManager.CreatePlayer(stream);
            _player.Play();
        }
        catch (Exception ex)
        {
            await DisplayAlert("B³¹d", $"Nie mo¿na odtworzyæ utworu: {ex.Message}", "OK");
        }
    }
}
