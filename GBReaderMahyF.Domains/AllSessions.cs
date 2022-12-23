namespace GBReaderMahyF.Domains;


/// <summary>
/// Class qui représente l'ensemble des sessions de lecture en cours d'un utilisateur
/// </summary>
public class AllSessions
{
    private Dictionary<string, Session> _sessions ;

    /// <summary>
    /// Constructeur de AllSessions
    /// </summary>
    /// <param name="sessionsModel">Dictionary<string, Session> qui est l'ensemble des sessions pour les livres qui sont en cours de lecture</param>
    public AllSessions(Dictionary<string, Session> sessionsModel)
    {
        this._sessions = sessionsModel;
    }
    
    /// <summary>
    /// Constructeur de base de AllSessions
    /// </summary>
    public AllSessions()
    {
        this._sessions = new Dictionary<string, Session>();
    }
    
    
    public Dictionary<string, Session> Sessions
    {
        get =>  this._sessions;
        set => this._sessions = value; 
    }
    
    /// <summary>
    /// Méthode qui permet de mettre à jour une session.
    /// Si une session pour le livre existe déjà elle sera mise à jour
    /// sinon la session sera crée. 
    /// </summary>
    /// <param name="isbn">string qui est l'isbn du livre auquel correspond la sessions</param>
    /// <param name="numCurrentPage">int qui est le numéro de la page à laquelle l'utilisateur est</param>
    public void UpdateSession(string isbn, int numCurrentPage)
    {
        if (_sessions.ContainsKey(isbn))
        {
            _sessions[isbn].UpdateSession(numCurrentPage, DateTime.Now);
        }
        else
        {
            _sessions.Add(isbn, new Session(numCurrentPage,  DateTime.Now,  DateTime.Now));
        }
    }

    /// <summary>
    /// Méthode qui permet de supprimer une session sur base du numéro isbn
    /// </summary>
    /// <param name="isbn">string qui est le numéro isbn du livre dont on souhaite supprimer la session</param>
    public void DeleteSession(string isbn)
    {
        _sessions.Remove(isbn);
    }
}