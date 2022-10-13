using System;
using System.Collections.Generic;
using System.Linq;
using TextClassificationGUI._2_Domain;
using TextClassificationGUI._3_Business;
using TextClassificationGUI._4_FileIO;
using TextClassificationGUI._5_Foundation;

namespace TextClassificationGUI._1_Controller;

public class KnowledgeBuilder : AbstractKnowledgeBuilder
{
    private const int BENCHMARK_RANGE = 3;
    
    private readonly FileAdapter _fileAdapter;
    private readonly Knowledge _knowledge;

    private FileLists _fileLists;
    private Vectors _vectors;

    public KnowledgeBuilder()
    {
        _fileAdapter = new TextFile("txt");
        _knowledge = new Knowledge();
    }

    public override Knowledge GetKnowledge()
    {
        return _knowledge;
    }

    public override void Train()
    {
        BuildFileLists();
        BuildBagOfWords();
        BuildVectors();
    }

    public override void BuildFileLists()
    {
        var fileListBuilder = new FileListBuilder();

        fileListBuilder.GenerateFileNamesInA();
        fileListBuilder.GenerateFileNamesInB();
        fileListBuilder.GenerateFileNamesInBenchA();
        fileListBuilder.GenerateFileNamesInBenchB();

        _fileLists = fileListBuilder.GetFileLists();
        _knowledge.SetFileLists(_fileLists);
    }

    private void AddToBagOfWords(string folderName, BagOfWords bagOfWords)
    {
        var fileList = folderName switch
        {
            "ClassA" => _fileLists.GetA(),
            "ClassB" => _fileLists.GetB(),
            _ => throw new Exception("Invalid folder name")
        };

        foreach (var word in
                 // Get the text from file
                 fileList.Select(file => folderName switch
                     {
                         "ClassA" => _fileAdapter.GetAllTextFromFileA(StringOperations.GetFileName(file)),
                         "ClassB" => _fileAdapter.GetAllTextFromFileB(StringOperations.GetFileName(file)),
                         _ => throw new Exception("Invalid folder name")
                     })
                     // Tokenize and stem the text
                     .Select(text => Tokenization.Tokenize(text).Select(StringOperations.Stem))
                     .SelectMany(word => word).ToList()
                )
            bagOfWords.InsertEntry(word);
    }

    public override void BuildBagOfWords()
    {
        BagOfWords bagOfWords = new();

        AddToBagOfWords("ClassA", bagOfWords);
        AddToBagOfWords("ClassB", bagOfWords);

        _knowledge.SetBagOfWords(bagOfWords);
    }

    /// <summary>
    /// Returns a List of 0's and 1's representing the presence of a word in a file
    /// </summary>
    /// <param name="value">The text to be vectorized</param>
    /// <returns>A list of bits</returns>
    public List<int> VectorizeString(string value)
    {
        var bagOfWords = _knowledge.GetBagOfWords();
        var tokenizedAndStemmed = Tokenization.Tokenize(value).Select(StringOperations.Stem).ToList();

        var vector = bagOfWords.GetAllWordsInDictionary()
            .Select(word => tokenizedAndStemmed.Contains(word) ? 1 : 0).ToList();
        
        return vector;
    }
    
    private void AddToVectors(string folderName, VectorsBuilder vectorsBuilder)
    {
        // Get all file names
        var fileNameList = folderName switch
        {
            "ClassA" => _fileLists.GetA(),
            "ClassB" => _fileLists.GetB(),
            _ => throw new Exception("Invalid folder name")
        };

        foreach (var text in fileNameList
                     // Get the text from file
                     .Select(file => folderName switch
                     {
                         "ClassA" => _fileAdapter.GetAllTextFromFileA(StringOperations.GetFileName(file)),
                         "ClassB" => _fileAdapter.GetAllTextFromFileB(StringOperations.GetFileName(file)),
                         _ => throw new Exception("Invalid folder name")
                     })
                )
        {
            var vector = VectorizeString(text);
            // Add the vector to the appropriate vector list
            switch (folderName)
            {
                case "ClassA":
                    vectorsBuilder.AddVectorToA(vector);
                    break;
                case "ClassB":
                    vectorsBuilder.AddVectorToB(vector);
                    break;
            }
        }
    }

    public override void BuildVectors()
    {
        _vectors = new Vectors();
        var vectorsBuilder = new VectorsBuilder();

        AddToVectors("ClassA", vectorsBuilder);
        AddToVectors("ClassB", vectorsBuilder);

        _vectors = vectorsBuilder.GetVectors();
        _knowledge.SetVectors(_vectors);
    }

    public int GetTokenCount(string folderName, string filename)
    {
        List<string> list;
        int index;
        string text;
        var tokenCount = 0;
        switch (folderName)
        {
            case "ClassA":
                list = _fileLists.GetA().Select(StringOperations.GetFileName).ToList();
                index = list.IndexOf(filename);
                text = _fileAdapter.GetAllTextFromFileA(list[index]);
                tokenCount = Tokenization.Tokenize(text).Count;
                break;

            case "ClassB":
                list = _fileLists.GetB().Select(StringOperations.GetFileName).ToList();
                index = list.IndexOf(filename);
                text = _fileAdapter.GetAllTextFromFileB(list[index]);
                tokenCount = Tokenization.Tokenize(text).Count;
                break;
        }

        return tokenCount;
    }

    public string GetTrainingAccuracy()
    {
        var benchAFileNames = _fileLists.GetBenchA();
        var benchBFileNames = _fileLists.GetBenchB();

        int successA = 0;
        benchAFileNames.ForEach(file =>
        {
            var text = _fileAdapter.GetAllTextFromBenchFileA(StringOperations.GetFileName(file));
            var vector = VectorizeString(text);
            var type = VectorOperations.GetClosestVectorType(_knowledge.GetVectors(), vector, BENCHMARK_RANGE);
            if (type is "ClassA") successA++;
        });
        
        int successB = 0;
        benchBFileNames.ForEach(file =>
        {
            var text = _fileAdapter.GetAllTextFromBenchFileB(StringOperations.GetFileName(file));
            var vector = VectorizeString(text);
            var type = VectorOperations.GetClosestVectorType(_knowledge.GetVectors(), vector, BENCHMARK_RANGE);
            if (type is "ClassB") successB++;
        });
        
        return $"Class A benchmark succes rate: {successA / (double)benchAFileNames.Count * 100}%\n" +
               $"Class B benchmark succes rate: {successB / (double)benchBFileNames.Count * 100}%";
    }
    
}