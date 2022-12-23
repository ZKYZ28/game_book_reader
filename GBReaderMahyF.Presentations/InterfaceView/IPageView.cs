using System.Collections.ObjectModel;
using GBReaderMahyF.Presentations.Events;

namespace GBReaderMahyF.Presentations;

public interface IPageView
{
    
    /// <summary>
    /// Méthode qui permet de mettre à jour les informations affichées sur la PageView
    /// </summary>
    /// <param name="numPage">string qui est le numéro de la page qu'on veut afficher</param>
    /// <param name="textPage">string qui est le texte de la page qu'on veut afficher</param>
    /// <param name="listChoices">ObservableCollection<string> qui est la liste des choix de la page</param>
    public void UpdatePageInformation(string numPage, string textPage, ObservableCollection<string> listChoices);
    
    /// <summary>
    /// Méthode qui permet de mettre à jour l'affichage lorsque l'on arrive à la fin d'un livre
    /// </summary>
    /// <param name="isDisplay">bool qui indique true si on veut rendre les élements visibles sinon false</param>
    public void DisplayEndBook(bool isDisplay);
    
    /// <summary>
    /// Evenement déclanché lorsque l'on souhaite se rendre à la page suivante dnas le livre
    /// </summary>
    public event EventHandler<GoNextPageArgs> GoNextPage;
    
        
    /// <summary>
    /// Evenement déclanché lorsque l'on arrête la lecture d'un livre
    /// </summary>
    public event EventHandler GoStopReading;
    
        
    /// <summary>
    /// Evenement déclanché lorsque l'on souhaite se rendre à la page de la liste de tous les livres
    /// </summary>
    public event EventHandler GoListBook;
    
        
    /// <summary>
    /// Evenement déclanché lorsque l'on souhaite recommancer un livre
    /// </summary>
    public event EventHandler GoRestartBook;
}