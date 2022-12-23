using GBReaderMahyF.Domains;
using GBReaderMahyF.Presentations.ModelView;
using GBReaderMahyF.Presentations.Routes;

namespace GBReaderMahyF.Presentations;

public class StatisticsPresenter
{
    private readonly IStatisticsView _statisticsView;
    private readonly ManagerReader _manager;
    private readonly IRouterToView _router;
    
    /// <summary>
    /// Constructeur de StatisticsView
    /// </summary>
    /// <param name="statisticsView">IStatisticsView qui est l'Interface implémentée par StatisticsView</param>
    /// <param name="manager">ManagerReader qui est le manager de l'application</param>
    /// <param name="router">IRouterToView  permet de changer de vue</param>
    public StatisticsPresenter(IStatisticsView statisticsView, ManagerReader manager, IRouterToView router)
    {
        this._statisticsView = statisticsView;
        this._manager = manager;
        this._router = router;

        this._statisticsView.GoListBook += BackToListBook;
    }
    
    /// <summary>
    /// Méthode appellée lors de la réception de GoListBook
    /// Méthode qui permet de revenir à la MenuView
    /// </summary>
    /// <param name="sender">Objet de la vue qui est envoyé</param>
    /// <param name="e">Argument lié à l'évenement</param>
    private void BackToListBook(object? sender, EventArgs e)
    {
        _router.GoTo("menuView");
    }

    /// <summary>
    /// Méthode qui permet d'afficher toutes les statistiques des sessions en cours
    /// </summary>
    public void DisplayAllStatistics()
    {
        _statisticsView.DisplayALlStatistics(MapperModelView.ConvertSessionsToSessionsMv(_manager.SessionsDictonnary));
    }

    /// <summary>
    /// Méthode qui permet d'aficher le nombre de session en cours
    /// </summary>
    public void DisplayNumberSession()
    {
        var nbrSession = _manager.SessionsDictonnary.Count;
        this._statisticsView.DisplayNumberSession(nbrSession.ToString());
    }
}