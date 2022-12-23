namespace GBReaderMahyF.Domains;


/// <summary>
/// Class qui représente le manager de l'application
/// </summary>
public class ManagerReader
{
    private readonly LibraryBooks _libraryBooks = new LibraryBooks();
    private AllSessions _allSessions = new AllSessions();
    private Book? _currentBook;
    private Page? _currentPage;
    

    public LibraryBooks LibraryBooks
        =>this._libraryBooks;
    
    public AllSessions AllSessions
    {
        get =>  this._allSessions; 
        set => this._allSessions = value; 
    }
    
    public Dictionary<string, Session> SessionsDictonnary
    {
        get => this._allSessions.Sessions;
    }

    public Book? CurrentBook
    {
        get =>  this._currentBook; 
        set => this._currentBook = value; 
    }
    
    public Page? CurrentPage
    {
        get =>  this._currentPage; 
        set => this._currentPage = value; 
    }
}