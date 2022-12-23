using GBReaderMahyF.Presentations.Notification;

namespace GBReaderMahyF.Presentations;

public class MainPresenter
{
    private readonly MenuPresenter _menuPresenter;
    private readonly PagePresenter _pagePresenter;
    private readonly StatisticsPresenter _statisticsPresenter;
    private readonly IShowNotifications _notifications;

    /// <summary>
    /// Constructeur du MainPresenter
    /// </summary>
    /// <param name="menuPresenter">Menupresenter qui est le presenter de la vue de base</param>
    /// <param name="pagePresenter">PagePresenter qui est le presenter de la vue de la page</param>
    /// <param name="statisticsPresenter">StatisticsPresenter qui est le presenter de la vue des statistiques</param>
    /// <param name="notifications">IShowNotifications qui permet d'afficher une notification à l'utilisateur</param>
    public MainPresenter(MenuPresenter menuPresenter, PagePresenter pagePresenter, StatisticsPresenter statisticsPresenter, IShowNotifications notifications)
    {
        this._menuPresenter = menuPresenter;
        this._pagePresenter = pagePresenter;
        this._statisticsPresenter = statisticsPresenter;
        this._notifications = notifications;
        
        menuPresenter.MainPresenter = this;
        pagePresenter.MainPresenter = this;
    }

    /// <summary>
    /// Méthode utilisée lorsque l'on souhaite commencer à lire un livre
    /// Utilise la méthode InitPages du PagePresenter qui permet d'initialiser le début de lecture d'un livre
    /// </summary>
    public void StartReadingBook()
    {
        _pagePresenter.InitPages();;
    }

    /// <summary>
    /// Méthode utilisée lorsque l'on arrête de lire un livre et qu'on revient à la liste de tous les livres
    /// pour mettre à jour le statut dans la vue détaillée du livre (Commencer/Reprendre)
    /// Utilise UpdateButtonDetailBook du MenuPresenter qui permet de mettre à jour la statut du livre
    /// </summary>
    /// <param name="isbn">string qui est l'isbn du livre dont on souhaite mettre à jour le statut</param>
    public void UpdateSessionStatut(string isbn)
    {
        _menuPresenter.UpdateButtonDetailBook(isbn);
    }

    /// <summary>
    /// Méthode utilisée lorsque l'on se rend sur la Statistics
    /// Utilise DisplayNumberSession et DisplayAllStatistics du StatisticsPresenter
    /// pour afficher le nombre de livre avec une session en cours et afficher les statiques de
    /// toutes les sessions en cours
    /// </summary>
    public void SwitchToStatistics()
    {
        _statisticsPresenter.DisplayNumberSession();
        _statisticsPresenter.DisplayAllStatistics();
    }

    /// <summary>
    /// Méthode qui permet d'afficher une notification à l'utilisateur
    /// </summary>
    /// <param name="severity">NotificationSeverity qui est le type de notification que l'on veut afficher</param>
    /// <param name="title">string qui va être le titre de la notification</param>
    /// <param name="message">string qui va être le message de la notification</param>
    public void PushNotification(NotificationSeverity severity, string title, string message)
    {
        _notifications.Push(severity, title, message);
    }
}