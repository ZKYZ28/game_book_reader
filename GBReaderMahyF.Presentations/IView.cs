using GBReaderMahyF.Domains;
namespace GBReaderMahyF.Presentations;

public interface IView
{
    public void setPresenter(MainPresenter mainPresenter);
    
    /// <summary>
    /// Méthode qui permet d'afficher tous les livres
    /// </summary>
    /// <param name="BooksList">une liste de Book</param>
    public void DisplayAllBooks(List<Book> BooksList);

    /// <summary>
    ///Méthode qui permet d'afficher un message d'erreur
    /// </summary>
    /// <param name="error">String qui est mon message d'erreur</param>
    public void DisplayError(String error);

    /// <summary>
    /// Méthode qui permet d'afficher la vue détaillée du premier livre affiché à l'écran
    /// </summary>
    public void DisplayFirstDetailBook();
}