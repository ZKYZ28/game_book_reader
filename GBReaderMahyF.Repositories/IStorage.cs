namespace GBReaderMahyF.Respositories;

public interface IStorage : IDisposable
{
    /// <summary>
    /// Méhtode qui permet de load la cover des livres depuis la base de données
    /// </summary>
    public void LoadCoverBook();

    /// <summary>
    /// Méthode qui permet de load les pages d'un livre depuis la base de données
    /// </summary>
    /// <param name="isbn">string qui est le numéro isbn du livre dont on souhaite lire les pages</param>
    public void LoadPages(string isbn);
}
