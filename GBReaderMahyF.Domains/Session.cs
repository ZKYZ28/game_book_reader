namespace GBReaderMahyF.Domains;


/// <summary>
/// Class qui représente une session de lecture en cours
/// </summary>
public class Session
{
    private int _numLastPage;
    private readonly DateTime _startReadingDate;
    private DateTime _endReadingDate;

    /// <summary>
    /// Constructeur de Session
    /// </summary>
    /// <param name="numLastPage">int qui est le numéro de la dernière page consultée par l'utilisateur</param>
    /// <param name="startReading">DateTime qui est le moment où l'utilisateur à commencer la session</param>
    /// <param name="endReading">DateTime qui est le moment où l'utilistateur à changer l'état de la session pour la dernière fois</param>
    public Session(int numLastPage, DateTime startReading, DateTime endReading)
    {
        this._numLastPage = numLastPage;
        this._startReadingDate = startReading;
        this._endReadingDate = endReading;
    }
    /// <summary>
    /// Méthode qui permet de mettre à jour une session
    /// Change le numéro de la dernière page consultée
    /// Change la date où la session a été modifiée pour la dernière fois
    /// </summary>
    /// <param name="numLastPage">int qui est le numéro de la dernière page consultée</param>
    /// <param name="endReading">DateTime qui est la dernière date où la session a été modifiée</param>
    public void UpdateSession(int numLastPage, DateTime endReading)
    {
        this._numLastPage = numLastPage;
        this._endReadingDate = endReading;
    }

    public int NumSessionPage 
        => this._numLastPage;
    
    
    public DateTime EndReadingDate
         => this._endReadingDate; 
    
    
    public DateTime StartReadingDate 
        => this._startReadingDate;
    
}