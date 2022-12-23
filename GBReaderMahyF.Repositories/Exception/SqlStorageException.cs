namespace GBReaderMahyF.Respositories.Exception;


/// <summary>
/// Exception lancée en cas de problème lié à la base de données
/// </summary>
public class SqlStorageException : StorageException
{
    /// <summary>
    /// Constructeur de SqlStorageException
    /// </summary>
    /// <param name="message">string qui est le message renvoyé par l'excpetion</param>
    /// <param name="ex">Exception de base</param>
    public SqlStorageException(string message, System.Exception ex)
        : base(message, ex)
    { }
}