
namespace Eutonies.UI.Persistence;

public class WordsLoader : IWordsLoader
{
    private readonly string[] _words;
    private readonly Dictionary<int, string[]> _sizeBuckets;
    private readonly Random _random = new Random();

    public WordsLoader()
    {
        var wordsFile = $"{AppContext.BaseDirectory}data/words_alpha.txt";
        var allLines = File.ReadAllLines(wordsFile);
        _words = allLines
            .Select(_ => _.Trim())
            .Where(_ => _.Length > 2)
//            .Where(_ => _random.Next(0,50) == 0)
            .Select(_ => _.Substring(0, 1).ToUpper() + _.Substring(1, _.Length - 1))
            .ToArray();
        _sizeBuckets = _words
            .GroupBy(_ => _.Length)
            .ToDictionary(_ => _.Key, _ => _.ToArray());

    }

    public async Task<IReadOnlyCollection<string>> LoadWords(int numberOfWords, int? minNumberOfCharacters = null)
    {
        await Task.CompletedTask;
        string[] elligebleWords = _words;
        if(minNumberOfCharacters != null)
        {
            elligebleWords = _sizeBuckets
                .Where(_ => _.Key >= minNumberOfCharacters.Value)
                .SelectMany(_ => _.Value)
                .ToArray();
        }
        var returnee = Enumerable.Range(0, numberOfWords)
            .Select(_ => _words[_random.Next(0, elligebleWords.Length)])
            .ToList();
        return returnee;
    }
}
