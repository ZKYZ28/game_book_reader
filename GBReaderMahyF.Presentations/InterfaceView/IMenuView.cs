using GBReaderMahyF.Presentations.Events;
using GBReaderMahyF.Presentations.ModelView;

namespace GBReaderMahyF.Presentations;

public interface IMenuView
{

    /// <summary>
    /// Méthode qui permet d'afficher tous les livres
    /// </summary>
    /// <param name="booksList">une liste de Book</param>
    public void DisplayAllBooks(List<ModelViewBook> booksList);

    /// <summary>
    ///Méthode qui permet d'afficher un message d'erreur
    /// </summary>
    /// <param name="error">String qui est mon message d'erreur</param>
    public void DisplayError(string error);

            
    /// <summary>
    /// Méthode qui permet de mettre à jour l'affichage de la vue détaillée du livre
    /// </summary>
    /// <param name="bookMv">ModelViewBook qui est le livre dont on souhaite afficher dans la vue détaillée</param>
    public void UpdateDetailBook(ModelViewBook bookMv);

    /// <summary>
    /// Méthode qui peremet de changer le statut du livre.
    /// Si une session existe pour le livre le bouton prend la valeur Reprendre sinon
    /// il prend la valeur Commencer
    /// </summary>
    /// <param name="textButton">string qui est la statut du livre</param>
    public void UpdateButtonDetailBook(string textButton);

    /// <summary>
    /// Evenement déclanché lors de la recherche d'un livre
    /// </summary>
    public event EventHandler<SearchBookArgs> SearchBook;
    
    /// <summary>
    /// Evenement déclanché lors du début de la lecture d'un livre
    /// </summary>
    public event EventHandler<StartReadingArgs> StartReadingBook;
    
    /// <summary>
    /// Evenement déclanché lorsque l'on veut aller sur la page des statistiques
    /// </summary>
    public event EventHandler SeeStatistics;
    
    /// <summary>
    /// Evenement déclanhcé pour changer la vue détaillé d'un livre
    /// </summary>
    public event EventHandler<SwitchDetailBookArgs> SwitchDetailBook;
}