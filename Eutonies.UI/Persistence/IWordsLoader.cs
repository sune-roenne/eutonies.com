namespace Eutonies.UI.Persistence;

public interface IWordsLoader
{

    Task<IReadOnlyCollection<string>> LoadWords(int numberOfWords, int? minNumberOfCharacters = null);

}
