using Eutonies.UI.Persistence;
using Microsoft.AspNetCore.Components;

namespace Eutonies.UI.Pages.Home;

public partial class HomePage
{
    private IReadOnlyCollection<WordEntry> _words = [];
    private const int WordsPerRound = 16;
    private static readonly Random Random = new Random();

    [Inject]
    public IWordsLoader WordsLoader { get; set; }

    protected override Task OnInitializedAsync()
    {
        _ = StartAnimation();
        return base.OnInitializedAsync();
    }

    private async Task StartAnimation()
    {
        if (_words.Any())
            return;
        var words = 
        _words = (await WordsLoader.LoadWords(numberOfWords: WordsPerRound))
           .Select((wd, indx) => new WordEntry(wd, indx * 2, Random.Next(10,16)))
           .ToList();
        _ = InvokeAsync(StateHasChanged);
        var maxTime = words
            .Select(_ => _.InitialDelaySeconds + _.AnimationTime)
            .Max();
        await Task.Delay(TimeSpan.FromSeconds(maxTime + 2));
        _words = [];
        _ = InvokeAsync(StateHasChanged);
        await Task.Delay(TimeSpan.FromSeconds(1));
        _ = Task.Run(StartAnimation);
    }


    private record WordEntry(
        string Word,
        int InitialDelaySeconds,
        int AnimationTime
    );

}
