using GBReaderMahyF.Domains;

namespace GBReaderMahyF.Presentations.ModelView;

/// <summary>
/// Représente un Book sous la forme d'un ModelView
/// </summary>
public record ModelViewBook
{
    private readonly Book? _book;

    /// <summary>
    /// Constructeur de ModelViewBook
    /// </summary>
    /// <param name="book">Book qui est le livre que l'on transformer en ModelViewBook</param>
    public ModelViewBook(Book? book)
    {
        this._book = book;
    }
    
    public string Title
        => _book!.Title;
    
    public string Author
        => _book!.Author.GetFullName();

    public string Isbn
        => _book!.Isbn.IsbnNumber();

    public string Resume
        => _book!.Resume;
    
}