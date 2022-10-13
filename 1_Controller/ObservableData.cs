using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TextClassificationGUI._5_Foundation;

namespace TextClassificationGUI._1_Controller;

public class ObservableData : Bindable
{
    private ObservableCollection<string> _observableA;
    private ObservableCollection<string> _observableAB;
    private ObservableCollection<string> _observableB;
    private ObservableCollection<string> _observableDictionaryList;
    private ObservableCollection<int> _observableTokens;
    private string _trainingTime;
    private string _trainingAccuracy;
    private string _textGuessClass;


    public ObservableData()
    {
        var fileListBuilder = new FileListBuilder();
        fileListBuilder.GenerateFileNamesInA();
        fileListBuilder.GenerateFileNamesInB();
        var fileLists = fileListBuilder.GetFileLists();

        _observableA = new ObservableCollection<string>(fileLists.GetA().Select(StringOperations.GetFileName));
        _observableB = new ObservableCollection<string>(fileLists.GetB().Select(StringOperations.GetFileName));
        _observableAB = new ObservableCollection<string>(ObservableA.Concat(ObservableB));

        var knowledgeBuilder = new KnowledgeBuilder();
        knowledgeBuilder.BuildFileLists();
        var temp = new ObservableCollection<int>();
        for (var i = 0; i < _observableA.Count; i++) temp.Add(knowledgeBuilder.GetTokenCount("ClassA", ObservableA[i]));
        for (var i = 0; i < _observableB.Count; i++) temp.Add(knowledgeBuilder.GetTokenCount("ClassB", ObservableB[i]));
        _observableTokens = temp;
    }

    public ObservableCollection<string> ObservableA
    {
        get => _observableA;
        set
        {
            _observableA = value;
            PropertyIsChanged();
        }
    }

    public ObservableCollection<string> ObservableB
    {
        get => _observableB;
        set
        {
            _observableB = value;
            PropertyIsChanged();
        }
    }

    public ObservableCollection<string> ObservableAB
    {
        get => _observableAB;
        set
        {
            _observableAB = value;
            PropertyIsChanged();
        }
    }

    public ObservableCollection<int> ObservableTokens
    {
        get => _observableTokens;
        set
        {
            _observableTokens = value;
            PropertyIsChanged();
        }
    }

    public ObservableCollection<string> ObservableDictionaryList
    {
        get => _observableDictionaryList;

        set
        {
            _observableDictionaryList = value;
            PropertyIsChanged();
        }
    }
    
    public string TrainingTime
    {
        get => _trainingTime;
        set
        {
            _trainingTime = value;
            PropertyIsChanged();
        }
    }
    
    public string TrainingAccuracy
    {
        get => _trainingAccuracy;
        set
        {
            _trainingAccuracy = value;
            PropertyIsChanged();
        }
    }
    
    public string TextGuessClass
    {
        get => _textGuessClass;
        set
        {
            _textGuessClass = value;
            PropertyIsChanged();
        }
    }
}