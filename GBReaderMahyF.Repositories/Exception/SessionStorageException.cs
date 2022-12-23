namespace GBReaderMahyF.Respositories.Exception;


/// <summary>
/// Exception lancée en cas de problème avec le fichier Json
/// </summary>
public class SessionStorageException : System.Exception
{
    /// <summary>
    /// Constructeur de SessionStorageException
    /// </summary>
    /// <param name="message">string qui est le message renvoyé par l'excpetion</param>
    /// <param name="ex">Exception de base</param>
    public SessionStorageException(string message, System.Exception ex)
        : base(message, ex)
    { }
    
}