namespace GBReaderMahyF.Domains;


/// <summary>
/// Class qui représente l'ensemble des livres créé et publié
/// </summary>
public class LibraryBooks
{
    private IDictionary<string, Book?> _libraryBooksAssociation = new Dictionary<string, Book?>();

    public IDictionary<string, Book?> LibraryBooksAssociation
        => _libraryBooksAssociation;

    /// <summary>
    /// Méthode qui permet de vérifier si il y a déjà des pages liées à un livre
    /// </summary>
    /// <param name="isbn">string qui est le numéro isbn du livre</param>
    /// <returns>True si la page existe déjà sinon false</returns>
    public bool CheckIfPageAlreadyExist(string isbn)
    {
        return (_libraryBooksAssociation[isbn]!.CheckIfPageAlreadyExist());
    }

    /// <summary>
    /// Méthode qui permet d'ajouter un livre à la librairie
    /// </summary>
    /// <param name="isbn">string qui est l'isbn du livre que l'on veut ajouter</param>
    /// <param name="book">Book qui est le livre que l'on veut ajouter</param>
    public void AddBook(string isbn, Book? book)
    {
        this._libraryBooksAssociation.Add(isbn, book);
    }
    
    /// <summary>
    /// Méthode qui permet de récupérer un livre sur base de son numéro isbn
    /// </summary>
    /// <param name="isbn">string numéro isbn du livre que l'on souhaite récupérer</param>
    /// <returns>Book qui est le livre auquel correspond le numéro isbn</returns>
    public Book? GetBookOnIsbn(string isbn)
    {
        return _libraryBooksAssociation[isbn];
    }
    
    
    /// <summary>
    /// Méthode qui permet de chercher les livres correspondant à la recherche encodée par l'utilisateur
    /// Si l'utilisateur n'encode rien alors on lui renvoie tous les livres
    /// </summary>
    /// <param name="toFind">String qui est ce que l'on souhaite rechercher</param>
    public List<Book?> FindBooks(string toFind)
    {
        return toFind.Equals("") ? this._libraryBooksAssociation.Values.ToList() : SearchBooks(toFind);
    }

    /// <summary>
    /// Méthode qui permet de recherhcer des livres sur base de leur isbn ou d'une sous-chaine de leur titre
    /// </summary>
    /// <param name="toFind">String qui est ce que l'on souhaite rechercher</param>
    private List<Book?> SearchBooks(string toFind)
    {
        var findBooks = new List<Book?>();
        foreach (var book in _libraryBooksAssociation.Values.ToList())
        {
            var bookTitle = book!.Title.ToUpper();
            toFind = toFind.ToUpper().Trim();
            if (bookTitle.Contains(toFind) || book.Isbn.IsbnNumber().Equals(toFind))
            {
                findBooks.Add(book);
            }
        }
        return findBooks;
    }

}