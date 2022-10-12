namespace TextClassificationGUI._5_Foundation;

public static class StringOperations
{
    private const int EXTENSION_LENGTH = 4;

    public static string GetFileName(string path)
    {
        // a) skipping the extension .txt (4 chars)
        var name = path[..^EXTENSION_LENGTH];

        // b) skipping the front part
        var startPos = name.LastIndexOf('\\') + 1;
        name = name.Substring(startPos, name.Length - startPos);

        return name;
    }

    public static string Stem(string word)
    {
        return word;
        //return new EnglishPorter2Stemmer().Stem(word).Value;
    }
}