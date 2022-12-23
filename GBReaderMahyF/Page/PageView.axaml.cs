using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderMahyF.Presentations;
using GBReaderMahyF.Presentations.Events;

namespace GBReaderMahyF;

public partial class PageView : UserControl, IPageView
{
    /// <summary>
    /// Constructeur de la PageView
    /// </summary>
    public PageView()
    {
        InitializeComponent(true);
    }

    /// <summary>
    /// Méthode qui permet de mettre à jour les informations affichées sur la PageView
    /// </summary>
    /// <param name="numPage">string qui est le numéro de la page qu'on veut afficher</param>
    /// <param name="textPage">string qui est le texte de la page qu'on veut afficher</param>
    /// <param name="listChoices">ObservableCollection<string> qui est la liste des choix de la page</param>
    public void UpdatePageInformation(string numPage, string textPage, ObservableCollection<string> listChoices)
    {
        NumPage.Text = numPage;
        TextPage.Text = textPage;
        ListChoices.SelectedIndex = -1;
        ListChoices.Items = listChoices;
    }

    /// <summary>
    /// Méthode délanchée lorsque l'on clique sur un choix de la page
    /// </summary>
    /// <param name="sender">Item de la ListBox qui est le choix que l'on a cliqué</param>
    /// <param name="e">Les arguments de l'évenement</param>
    public void Choice_OnClick(object? sender, SelectionChangedEventArgs e)
    {
        int index = ListChoices.SelectedIndex;
        GoNextPage?.Invoke(sender, new GoNextPageArgs(index));
    }
    
    /// <summary>
    /// Méthode délanchée lorsque l'on clique sur le bouton interrompre la lecture
    /// </summary>
    /// <param name="sender">Button qui permet d'arrêter la lecture du livre</param>
    /// <param name="e">Les arguments de l'évenement</param>
    public void StopReading_OnClick(object? sender, RoutedEventArgs e)
    {
        GoStopReading?.Invoke(sender, e);
    }

    /// <summary>
    /// Méthode délanchée lorsque l'on clique sur le bouton retour à la liste des livres
    /// </summary>
    /// <param name="sender">Button qui permet de revenir à liste de tous les livres</param>
    /// <param name="e">Les arguments de l'évenement</param>
    public void BackListBook_OnClick(object? sender, RoutedEventArgs e)
    {
        GoListBook?.Invoke(sender, e);
    }
    
    /// <summary>
    /// Méthode délanchée lorsque l'on clique sur le bouton recommencer le livre
    /// </summary>
    /// <param name="sender">Button qui permet de recommencer la lecture du livre</param>
    /// <param name="e">Les arguments de l'évenement</param>
    public void Restart_OnClick(object? sender, RoutedEventArgs e)
    {
        
        GoRestartBook?.Invoke(sender, e);
    }

    /// <summary>
    /// Méthode qui permet de mettre à jour l'affichage lorsque l'on arrive à la fin d'un livre
    /// </summary>
    /// <param name="isDisplay">bool qui indique true si on veut rendre les élements visibles sinon false</param>
    public void DisplayEndBook(bool isDisplay)
    {
        BackToListBook.IsVisible = isDisplay;
        RestartBook.IsVisible = isDisplay;
        TextEndBook.IsVisible = isDisplay;
        StopReading.IsVisible = !isDisplay;
    }
    
    public event EventHandler<GoNextPageArgs> GoNextPage;
    public event EventHandler GoStopReading;
    public event EventHandler GoListBook;
    public event EventHandler GoRestartBook;
}