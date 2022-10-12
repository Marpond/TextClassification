using TextClassificationGUI._2_Domain;
using TextClassificationGUI._4_FileIO;

namespace TextClassificationGUI._1_Controller;

public class FileListBuilder : AbstractFileListsBuilder
{
    private const string A_FOLDER_NAME = "ClassA";
    private const string B_FOLDER_NAME = "ClassB";
    private const string A_BENCH_FOLDER_NAME = "BenchA";
    private const string B_BENCH_FOLDER_NAME = "BenchB";
    
    private readonly FileAdapter _fileAdapter;

    private readonly FileLists _fileLists;

    public FileListBuilder()
    {
        _fileLists = new FileLists();

        _fileAdapter = new TextFile("txt");
    }


    public override FileLists GetFileLists()
    {
        return _fileLists;
    }

    public override void GenerateFileNamesInA()
    {
        var fileNames = _fileAdapter.GetAllFileNames(A_FOLDER_NAME);
        _fileLists.SetA(fileNames);
    }

    public override void GenerateFileNamesInB()
    {
        var fileNames = _fileAdapter.GetAllFileNames(B_FOLDER_NAME);
        _fileLists.SetB(fileNames);
    }

    public override void GenerateFileNamesInBenchA()
    {
        var fileNames = _fileAdapter.GetAllFileNames(A_BENCH_FOLDER_NAME);
        _fileLists.SetBenchA(fileNames);
    }
    public override void GenerateFileNamesInBenchB()
    {
        var fileNames = _fileAdapter.GetAllFileNames(B_BENCH_FOLDER_NAME);
        _fileLists.SetBenchB(fileNames);
    }
}
