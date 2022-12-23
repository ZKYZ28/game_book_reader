using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using GBReaderMahyF.Presentations;
using GBReaderMahyF.Presentations.ModelView;

namespace GBReaderMahyF;

public partial class BookView : UserControl
{
    private ModelViewBook _bookMv;

    /// <summary>
    /// Constructeur de la BookView
    /// </summary>
    public BookView()
    {
        InitializeComponent(true);
    }

    /// <summary>
    /// Méthode qui permet de mettre à jour les inforamtions affichées pour un livre
    /// </summary>
    /// <param name="bookMv">ModelViewBook qui est la cover d'un livre</param>
    public void SetBookInformation(ModelViewBook bookMv)
    {
        _bookMv = bookMv;
        Title.Text = bookMv.Title;
        Author.Text = bookMv.Author;
        Isbn.Text = bookMv.Isbn;
    }
    
    /// <summary>
    /// Méthode déclanchée lorsque l'on clique sur la cover d'un livre
    /// </summary>
    /// <param name="sender">Button qui est le bouton qui permet de revenir à la liste des livres</param>
    /// <param name="args">Les arguments de l'évenement</param>
    public void Book_OnClick(object sender, PointerPressedEventArgs args)
    {
        OnBookClick?.Invoke(sender, _bookMv);
    }

    public event EventHandler<ModelViewBook> OnBookClick;
}