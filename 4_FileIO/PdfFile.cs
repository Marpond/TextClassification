using IronPdf;

namespace TextClassificationGUI._4_FileIO;

public class PdfFile
{
    public static string GetText(string path)
    {
        var pdfDocument = PdfDocument.FromFile(path);
        return pdfDocument.ExtractAllText();
    }
}