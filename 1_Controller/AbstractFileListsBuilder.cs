using TextClassificationGUI._2_Domain;

namespace TextClassificationGUI._1_Controller;

public abstract class AbstractFileListsBuilder
{
    public abstract void GenerateFileNamesInA();

    public abstract void GenerateFileNamesInB();
    
    public abstract void GenerateFileNamesInBenchA();
    
    public abstract void GenerateFileNamesInBenchB();

    //  get the complete FileLists-object
    public abstract FileLists GetFileLists();
}