namespace SebastienDabertApp;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        LoadCarouselData();
    }

    private async void OnTrickButtonClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TrickPage));
    }

    private void LoadCarouselData()
    {
        var items = new List<SkiCarouselItem>
        {
            new()
            {
                Quote = "\"Le ski, c'est la liberté sur deux planches.\"",
                Author = "Jean-Claude Killy",
                Brand = "Rossignol"
            },
            new()
            {
                Quote = "\"La montagne n'est ni juste ni injuste, elle est dangereuse.\"",
                Author = "Reinhold Messner",
                Brand = "Salomon"
            },
            new()
            {
                Quote = "\"Chaque virage est une nouvelle aventure.\"",
                Author = "Luc Alphand",
                Brand = "Dynastar"
            },
            new()
            {
                Quote = "\"Le ski est le plus beau sport du monde.\"",
                Author = "Ingemar Stenmark",
                Brand = "Atomic"
            },
            new()
            {
                Quote = "\"La neige ne connaît qu'une seule couleur, mais elle offre mille sensations.\"",
                Author = "Proverbe montagnard",
                Brand = "Head"
            }
        };

        CarouselQuotes.ItemsSource = items;
    }
}

public class SkiCarouselItem
{
    public string Quote { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
}

