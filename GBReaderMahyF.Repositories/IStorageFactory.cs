using GBReaderMahyF.Domains;

namespace GBReaderMahyF.Respositories;

public interface IStorageFactory
{
    
    /// <summary>
    /// Méthode qui permet de créer un nouveau SqlBookStorage
    /// </summary>
    /// <param name="managerReader">ManagerReader qui est le manager de l'application</param>
    /// <returns>ISqlStorage qui est l'interface implémentée par SqlBookStorage</returns>
    /// <exception cref="InvalidConnectionStringException">Exception lancée lorsque la string de connexion est incorrecte</exception>
    /// <exception cref="UnableToConnectException">Exception lancée lorsque la connection à la base de données échoue</exception>
    public IStorage NewStorage(ManagerReader managerReader);
}