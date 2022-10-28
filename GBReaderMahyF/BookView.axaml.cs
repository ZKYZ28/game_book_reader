using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GBReaderMahyF.Domains;
using Avalonia.Input;

namespace GBReaderMahyF;
public interface IDetailBookListener
{
    void OnDetailBookSelected(object sender, Book book);
}

public partial class BookView : UserControl
{
    private IDetailBookListener _listener;
    private Book _book;
    
    private TextBlock _title;
    private TextBlock _author;
    private TextBlock _isbn;

    public BookView()
    {
        InitializeComponent();
        LocateControls();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void LocateControls()
    {
        _title = this.FindControl<TextBlock>("Title");
        _author = this.FindControl<TextBlock>("Author");
        _isbn = this.FindControl<TextBlock>("Isbn");
    }

    public void SetBookInformation(Book book)
    {
        _book = book;
        _title.Text = book.getTitle();
        _author.Text = book.getAuthor().getFullName();
        _isbn.Text = book.getIsbn().getIsbnNumber();
    }
    
    public void SetListener(IDetailBookListener listener)
    {
        _listener = listener;
    }

    public void DetailBook_OnClick(object sender, PointerPressedEventArgs args)
    {
        _listener.OnDetailBookSelected(this, this._book);
    }
}