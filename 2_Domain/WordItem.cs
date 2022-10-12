// deprecated (THA)

namespace TextClassificationGUI._2_Domain;

public class WordItem
{
    private readonly string _word;
    private int _occurency;

    public WordItem(string word, int occurency)
    {
        _word = word;
        _occurency = occurency;
    }

    public string GetWord()
    {
        return _word;
    }
}