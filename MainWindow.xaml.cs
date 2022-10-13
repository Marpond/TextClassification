using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using TextClassificationGUI._1_Controller;
using TextClassificationGUI._2_Domain;
using TextClassificationGUI._4_FileIO;

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

    private void ButtonTrain_Click(object sender, RoutedEventArgs e)
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
        buttonGuess.IsEnabled = true;
        textBoxGuess.IsEnabled = true;
    }

    private void ButtonGuess_OnClick(object sender, RoutedEventArgs e)
    {
        // Open file explorer
        OpenFileDialog openFileDialog = new()
        {
            // Look for txt and pdf files
            Filter = "Text files (*.txt)|*.txt|PDF files (*.pdf)|*.pdf"
        };
        if (openFileDialog.ShowDialog() != true) return;
        var path = openFileDialog.FileName;
        // Get the text from the file
        var text = path[^3..] switch
        {
            "pdf" => PdfFile.GetText(path),
            "txt" => File.ReadAllText(path),
            _ => string.Empty
        };
        textBoxGuess.Text = path;
        var vector = knowledgeBuilder.VectorizeString(text);
        var guess = VectorOperations.GetClosestVectorType(knowledge.GetVectors(), vector, 3);
        _observableData.TextGuessClass = $"This file is most likely a {guess}";
    }
}