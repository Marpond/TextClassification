using System.Collections.Generic;

namespace TextClassificationGUI._2_Domain;

public class BagOfWords
{
    private readonly IDictionary<string, int> bagOfWords;

    public BagOfWords()
    {
        bagOfWords = new SortedDictionary<string, int>();
    }

    public void InsertEntry(string word)
    {
        word = word.Trim();
        if (word.Length == 0) return;

        if (bagOfWords.ContainsKey(word))
        {
            var count = bagOfWords[word];
            count++;
            bagOfWords[word] = count;
        }
        else
        {
            bagOfWords.Add(word, 1);
        }
    }

    public ICollection<string> GetAllWordsInDictionary()
    {
        return bagOfWords.Keys;
    }

    public List<string> GetEntriesInDictionary()
    {
        var entries = new List<string>();
        foreach (var key in bagOfWords.Keys) entries.Add(key + " " + bagOfWords[key]);
        return entries;
    }
}