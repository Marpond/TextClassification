using System.Collections.Generic;

namespace TextClassificationGUI._4_FileIO;

public abstract class FileAdapter
{
    private readonly string _fileType;

    public FileAdapter(string fileType)
    {
        _fileType = fileType;
    }

    public abstract List<string> GetAllFileNames(string folderName);
    public abstract string GetAllTextFromFileA(string fileName);

    public abstract string GetAllTextFromFileB(string fileName);
    
    public abstract string GetAllTextFromBenchFileA(string fileName);
    
    public abstract string GetAllTextFromBenchFileB(string fileName);

    public string GetFileType()
    {
        return _fileType;
    }
}