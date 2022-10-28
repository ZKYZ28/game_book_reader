namespace GBReaderMahyF.Respositories;
using GBReaderMahyF.Domains;
public interface IManagerRepository
{
    /// <summary>
    /// Méthode qui permet de lire tous les livres qui ont été créer par les utilisateurs
    /// </summary>
    /// <returns>List<Book> qui est la liste de tous les livres ayant un format isbn valide</returns>
    public  List<Book> ReadBooks();
}