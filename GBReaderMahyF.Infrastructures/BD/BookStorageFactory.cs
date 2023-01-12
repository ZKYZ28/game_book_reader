using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using GBReaderMahyF.Domains;
using GBReaderMahyF.Respositories;
using GBReaderMahyF.Respositories.Exception;
using MySql.Data.MySqlClient;

namespace GBReaderMahyF.Infrastructures.BD;

public class BookStorageFactory : IStorageFactory
{
    private readonly DbProviderFactory _factory;
    private readonly string _connectionString;

    /// <summary>
    /// Constructeur de SqlBookStorageFactory
    /// </summary>
    /// <param name="providerName">string qui est le nom du fournisseur de données</param>
    /// <param name="connectionString">string qui est la chaine contenant les informations de connection à la base de données</param>
    /// <exception cref="ProviderNotFoundException">Exception lancée lorsque le providerName n'a pas pû être trouvé</exception>
    public BookStorageFactory(string providerName, string connectionString)
    {
        try
        {
            DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySqlClientFactory.Instance);
            _factory = DbProviderFactories.GetFactory(providerName);
            this._connectionString = connectionString;
        }
        catch (ArgumentException ex)
        {
            throw new ProviderNotFoundException($"Impossible de charger le provider {providerName}", ex);
        }
    }

    /// <summary>
    /// Méthode qui permet de créer un nouveau SqlBookStorage
    /// </summary>
    /// <param name="managerReader">ManagerReader qui est le manager de l'application</param>
    /// <returns>ISqlStorage qui est l'interface implémentée par SqlBookStorage</returns>
    /// <exception cref="InvalidConnectionStringException">Exception lancée lorsque la string de connexion est incorrecte</exception>
    /// <exception cref="UnableToConnectException">Exception lancée lorsque la connection à la base de données échoue</exception>
    public IStorage NewStorage(ManagerReader managerReader)
    {
        try
        {
            IDbConnection con = _factory.CreateConnection()!;
            con.ConnectionString = _connectionString;
            con.Open();
            return new SqlBookStorage(con, managerReader);
        }
        catch (ArgumentException ex)
        {
            throw new InvalidConnectionStringException(ex);
        }
        catch (SqlException ex)
        {
            throw new UnableToConnectException(ex);
        }
        catch (MySqlException ex)
        {
            throw new UnableToConnectException(ex);
        }
    }
}


/// <summary>
/// Excpetion lancée si la connexion à la base de données échoue
/// </summary>
public class UnableToConnectException : StorageException
{
    /// <summary>
    /// Constructeur de UnableToConnectException
    /// </summary>
    /// <param name="ex">Exception de base</param>
    public UnableToConnectException(Exception ex)
        : base("Impossible d'établir la connexion à la base de données", ex)
    { }
}


/// <summary>
/// Exception lancée si le providerName est incorrect
/// </summary>
public class ProviderNotFoundException : StorageException
{

    /// <summary>
    /// Constructeur de ProviderNotFoundException
    /// </summary>
    /// <param name="s">string qui est le message d'erreur</param>
    /// <param name="ex">Exception de base</param>
    public ProviderNotFoundException(string s, Exception ex)
        :base(s, ex)
    {
    }
}

/// <summary>
/// Exception lancée si la string de connexion à la base de données n'est pas bonne
/// </summary>
public class InvalidConnectionStringException : StorageException
{
    /// <summary>
    /// Constructeur de InvalidConnectionStringException
    /// </summary>
    /// <param name="ex">Exception de base</param>
    public InvalidConnectionStringException(Exception ex)
        : base("Unable to use this connection string", ex)
    {
    }
}