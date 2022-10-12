using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TextClassificationGUI._1_Controller;
using TextClassificationGUI._2_Domain;

namespace TextClassificationGUI;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ObservableData _observableData = new();
    private BagOfWords bof;
    private List<string> entries;
    private Knowledge knowledge;
    private KnowledgeBuilder knowledgeBuilder;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _observableData;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var t1 = TimeUtils.GetNanoseconds();
        knowledgeBuilder = new KnowledgeBuilder();
        knowledgeBuilder.Train();
        var duration = TimeUtils.GetNanoseconds() - t1;
        _observableData.TrainingTime = duration / 1000 / 1000 + "ms";

        knowledge = knowledgeBuilder.GetKnowledge();
        
        bof = knowledge.GetBagOfWords();
        entries = bof.GetEntriesInDictionary();
        _observableData.ObservableDictionaryList = new ObservableCollection<string>(entries);
        _observableData.TrainingAccuracy = knowledgeBuilder.GetTrainingAccuracy();
    }
}