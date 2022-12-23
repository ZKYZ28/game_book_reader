namespace GBReaderMahyF.Respositories.Exception;

/// <summary>
/// Exception qui regroupe l'ensemble des exceptions avec le système de stockage des livres
/// </summary>
public class StorageException : System.Exception
{
    
    /// <summary>
    /// Constructeur de StorageException
    /// </summary>
    /// <param name="message">string qui est le message renvoyé par l'excpetion</param>
    /// <param name="ex">Exception de base</param>
    public StorageException(string message, System.Exception ex)
        : base(message, ex)
    { }
}