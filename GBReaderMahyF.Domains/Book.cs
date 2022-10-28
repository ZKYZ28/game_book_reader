namespace GBReaderMahyF.Domains;

public class Book
{
    private Author _author;
    private String _title;
    private Isbn _isbn;
    private String _resume;


    /// <summary>
    /// Constructeur du Book
    /// </summary>
    /// <param name="title">String qui est le titre du livre</param>
    /// <param name="author">Authro qui est l'auteur du livre</param>
    /// <param name="isbn">Isbn qui est le numéro isbn du livre</param>
    /// <param name="resume">String qui est la description du livre</param>
    public Book(String title, Author author , Isbn isbn, String resume)
    {
        this._author = author;
        this._title = title;
        this._isbn = isbn;
        this._resume = resume;
    }

    public Author getAuthor()
    {
        return this._author;
    }

    public String getTitle()
    {
        return this._title;
    }

    public Isbn getIsbn()
    {
        return this._isbn;
    }

    public String getResume()
    {
        return this._resume;
    }
    
}