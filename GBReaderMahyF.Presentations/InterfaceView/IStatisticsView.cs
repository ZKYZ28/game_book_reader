using GBReaderMahyF.Presentations.ModelView;

namespace GBReaderMahyF.Presentations;

public interface IStatisticsView
{
    
    /// <summary>
    /// Méthode qui permet d'afficher toutes les statistiques des sessions en cours
    /// </summary>
    public void DisplayALlStatistics(Dictionary<string, ModelViewSession> sessions);

    /// <summary>
    /// Méthode qui permet d'aficher le nombre de session en cours
    /// </summary>
    public void DisplayNumberSession(string nbrSession);
    
    
    /// <summary>
    /// Evenement déclanché lorsque l'on souhaite se rendre à la page de la liste de tous les livres
    /// </summary>
    public event EventHandler GoListBook;
}