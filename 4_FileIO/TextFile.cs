using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextClassificationGUI._4_FileIO;

public class TextFile : FileAdapter
{
    private readonly string _projectPath = Path.GetFullPath(@"..\..\..\..\res\");

    public TextFile(string fileType) : base(fileType)
    {
    }

    public override List<string> GetAllFileNames(string folderName)
    {
        return Directory.GetFiles(_projectPath + folderName, "*." + GetFileType()).ToList();
    }

    public override string GetAllTextFromFileA(string fileName)
    {
        return File.ReadAllText(@"ClassA\" + fileName + ".txt");
    }

    public override string GetAllTextFromFileB(string fileName)
    {
        return File.ReadAllText(@"ClassB\" + fileName + ".txt");
    }

    public override string GetAllTextFromBenchFileA(string fileName)
    {
        return File.ReadAllText(@"BenchA\" + fileName + ".txt");
    }

    public override string GetAllTextFromBenchFileB(string fileName)
    {
        return File.ReadAllText(@"BenchB\" + fileName + ".txt");
    }
}