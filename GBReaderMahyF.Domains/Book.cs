namespace GBReaderMahyF.Domains;


/// <summary>
/// Class qui représente un livre
/// </summary>
public record Book
{
    private readonly Author _author;
    private readonly string _title;
    private readonly Isbn _isbn;
    private readonly string _resume;
    private List<Page?> _listPage;


    /// <summary>
    /// Constructeur du Book
    /// </summary>
    /// <param name="title">String qui est le titre du livre</param>
    /// <param name="author">Authro qui est l'auteur du livre</param>
    /// <param name="isbn">Isbn qui est le numéro isbn du livre</param>
    /// <param name="resume">String qui est la description du livre</param>
    public Book(string title, Author author, Isbn isbn, string resume)
    {
        this._author = author;
        this._title = title;
        this._isbn = isbn;
        this._resume = resume;
        this._listPage = new List<Page?>();
    }

    public string Title
        => this._title;
    
    public Author Author
        => this._author;

    public Isbn Isbn
        => this._isbn;
    
    public string IsbnNumber
        => this._isbn.IsbnNumber();

    public string Resume
        => this._resume;

    public List<Page?> ListPage
    {
        get  =>  this._listPage;
        set  => this._listPage = value; 
    }
    
    /// <summary>
    /// Méthode qui permet de vérifier si il y a déjà des pages liées à un livre
    /// </summary>
    /// <returns></returns>
    public bool CheckIfPageAlreadyExist()
    {
        return (this._listPage.Count != 0);
    }

    /// <summary>
    /// Méthode qui permet d'obtenir la page courrante sur base de l'index du choix sélectionné
    /// </summary>
    /// <param name="indexSelectedChoice">int qui est l'index du choix sélectionné</param>
    /// <param name="numCurrentPage">int qui est le numéro de la page courrante</param>
    /// <returns>Page qui est la page suivante</returns>
    public Page? GetNextPage(int indexSelectedChoice, int numCurrentPage)
    {
        var numCurrentChoice = this._listPage[numCurrentPage -1]!.ListChoices[indexSelectedChoice].NumGoPage;
        return this.ListPage[numCurrentChoice -1];;
    }
}