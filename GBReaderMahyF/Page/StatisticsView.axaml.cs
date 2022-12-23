using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderMahyF.Control;
using GBReaderMahyF.Presentations;
using GBReaderMahyF.Presentations.ModelView;

namespace GBReaderMahyF.Page;

public partial class StatisticsView : UserControl, IStatisticsView
{
    
    /// <summary>
    /// Constructeur de la StatisticsView
    /// </summary>
    public StatisticsView()
    {
        InitializeComponent(true);
    }

    /// <summary>
    /// Méthodes qui permet d'afficher les statistiques pour chaque livre qui a un session en cours
    /// </summary>
    /// <param name="sessions">Dictionary<string, ModelViewSession> qui est l'ensemble des sessions en cours</param>
    public void DisplayALlStatistics(Dictionary<string, ModelViewSession> sessions)
    {
        StatisticsContainer.Children.Clear();

        foreach(KeyValuePair<string, ModelViewSession> entry in sessions)  
        {
            StatisticView statisticView = new StatisticView();
            statisticView.SetStatisticInformation(entry.Key, entry.Value);
            StatisticsContainer.Children.Add(statisticView);
        }
    }

    /// <summary>
    /// Méthode qui permet d'afficher le nombre de session en cours
    /// </summary>
    /// <param name="nbrSession">int qui est le nombre de session en cours</param>
    public void DisplayNumberSession(string nbrSession)
    {
        NbrSession.Text = nbrSession;
    }
    

    /// <summary>
    /// Méthode déclanché lorsque l'on clique sur le bouton retour à la liste des livres
    /// </summary>
    /// <param name="sender">Button qui est le bouton qui permet de retourner à la liste des livres</param>
    /// <param name="e">Les arguments de l'évenement</param>
    public void BackToListBook_OnClick(object? sender, RoutedEventArgs e)
    {
        GoListBook?.Invoke(sender, e);
    }
    
    public event EventHandler GoListBook;
}