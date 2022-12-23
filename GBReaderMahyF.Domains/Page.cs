namespace GBReaderMahyF.Domains;

/// <summary>
/// Class qui représente la page d'un livre
/// </summary>
public record Page
{
    private readonly int _numPage;
    private readonly string _textPage;
    private List<Choice> _listChoices;

    /// <summary>
    /// Constructeur de Page
    /// </summary>
    /// <param name="numPage">int qui est le numéro de la page</param>
    /// <param name="textPage">string qui est le text de la page</param>
    public Page(int numPage, string textPage)
    {
        this._numPage = numPage;
        this._textPage = textPage;
        this._listChoices = new List<Choice>();
    }
    
    public int NumPage
        =>  this._numPage;
    
    public String TextPage
        => this._textPage;
    
    public List<Choice> ListChoices
    {
        get => this._listChoices;
        set => this._listChoices = value; 
    }

    /// <summary>
    /// Méthode qui permet de savoir si la page est une page de fin
    /// (qu'elle n'a pas de choix lié)
    /// </summary>
    /// <returns></returns>
    public bool IsAEndPage()
    {
        return (this._listChoices.Count == 0);
    }
}