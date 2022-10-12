using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextClassificationGUI._3_Business;

public class Tokenization
{
    public static List<string> Tokenize(string originalText)
    {
        StringBuilder stringBuilder = new();
        foreach (var ch in originalText.Trim().Where(c => !char.IsPunctuation(c))) stringBuilder.Append(ch);
        return stringBuilder.ToString().ToLower().Split(" ").ToList();
    }
}