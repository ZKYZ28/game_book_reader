namespace GBReaderMahyF.Domains;


/// <summary>
/// Class qui représente un choix
/// </summary>
public record Choice
{
    private readonly string _textChoice;
    private readonly int _numGoPage;

    /// <summary>
    /// Constructeur de Choice
    /// </summary>
    /// <param name="textChoice">string qui est le text du choix</param>
    /// <param name="numGoPage">int qui est le numéro de la page vers laquelle le choix pointe</param>
    public Choice(string textChoice, int numGoPage)
    {
        this._textChoice = textChoice;
        this._numGoPage = numGoPage;
    }
    
    public string TextChoice
        => this._textChoice;
    
    public int NumGoPage
        => this._numGoPage;
}