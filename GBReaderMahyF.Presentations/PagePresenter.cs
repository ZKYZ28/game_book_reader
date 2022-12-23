using System.Collections.ObjectModel;
using GBReaderMahyF.Domains;
using GBReaderMahyF.Presentations.Events;
using GBReaderMahyF.Presentations.Notification;
using GBReaderMahyF.Presentations.Routes;
using GBReaderMahyF.Respositories;
using GBReaderMahyF.Respositories.Exception;

namespace GBReaderMahyF.Presentations;

public class PagePresenter
{
    private MainPresenter _mainPresenter = null!;
    private readonly IPageView _pageView;
    private readonly ManagerReader _manager;
    private readonly IRouterToView _router;
    private readonly ISession _session;
    
    
    /// <summary>
    /// Constructeur du PagePresenter
    /// </summary>
    /// <param name="pageView">IPageView qui est l'Interface implémentée par PageView</param>
    /// <param name="manager">ManagerReader qui est le manager de l'application</param>
    /// <param name="router">IRouterToView  permet de changer de vue</param>
    /// <param name="session">ISession qui est l'interface qui permet d'intéragir avec les sessions</param>
    public PagePresenter(IPageView pageView, ManagerReader manager, IRouterToView router, ISession session)
    {
        this._session = session;
        this._pageView = pageView;
        this._manager = manager;
        this._router = router;

        pageView.GoNextPage += UpdatePage;
        pageView.GoListBook += StopReading;
        pageView.GoRestartBook += RestartBook;
        pageView.GoStopReading += StopReading;
    }

    public MainPresenter MainPresenter
    {
        set { this._mainPresenter = value; }
    }

    /// <summary>
    /// Méthode utilisée lorsque l'on démarre la lecture d'un livre
    /// Permet de changer le livre courant
    /// Initliaser la page courante
    /// Vérifier si c'est une page de fin
    /// Mettre à jour les informations affichées à l'écran
    /// </summary>
    public void InitPages()
    {
        this._manager.CurrentBook = _manager.CurrentBook;
        InitCurrentPage();
        UpdateCurrentPage(true);
    }

    /// <summary>
    /// Méthodeq qui permet de mettre à jour la page courrante
    /// <param name="onInit">bool true si c'est pour initialiser la première page sinon false</param>
    /// </summary>
    private void UpdateCurrentPage(bool onInit)
    {
        CheckIfEndPage(onInit);
        var currentPage = _manager.CurrentPage;
        _pageView.UpdatePageInformation(currentPage!.NumPage.ToString(), currentPage.TextPage, new ObservableCollection<string>(currentPage.ListChoices.Select(c => c.TextChoice).ToList()));
    }

    /// <summary>
    /// Méthode utiliée lorsque de déterminer la page sur laquelle on va commencer une lecture
    /// Si une session existe la lecture commencera à la dernière page à laquelle on est arrivé
    /// sinon on commence à la page 1
    /// </summary>
    private void InitCurrentPage()
    {
        if (_manager.SessionsDictonnary.ContainsKey(_manager.CurrentBook!.IsbnNumber))
        {
            int index = _manager.SessionsDictonnary[_manager.CurrentBook.IsbnNumber].NumSessionPage - 1;
            _manager.CurrentPage = _manager.CurrentBook.ListPage[index];
        }
        else
        {
            _manager.CurrentPage = _manager.CurrentBook.ListPage[0];
        }
    }

    /// <summary>
    /// Méthode qui permet d'afficher des inforamtions en fonction de si la page est une page de fin ou non
    /// </summary>
    /// <param name="onInit">bool true si c'est pour initialiser la première page sinon false</param>
    private void CheckIfEndPage(bool onInit)
    {
        if (_manager.CurrentPage!.IsAEndPage())
        {
            
            if (!onInit)
            {
                _manager.AllSessions.DeleteSession(_manager.CurrentBook!.IsbnNumber);
                WriteSession();
            }
            _pageView.DisplayEndBook(true);
        }
        
        
            
        
    }

    /// <summary>
    /// Méthode appellée lors de la réception de GoNextPage
    /// Méthode qui permet de mettre à jour la page affichée
    /// De mettre à jour la session 
    /// </summary>
    /// <param name="sender">Objet de la vue qui est envoyé</param>
    /// <param name="goNextPageArgs">Argument lié à l'évenement</param>
    private void UpdatePage(object? sender, GoNextPageArgs goNextPageArgs)
    {
        if (goNextPageArgs.IndexChoice >= 0)
        {
            _manager.CurrentPage = _manager.CurrentBook!.GetNextPage(goNextPageArgs.IndexChoice, _manager.CurrentPage!.NumPage);
            _manager.AllSessions.UpdateSession(_manager.CurrentBook.IsbnNumber, _manager.CurrentPage!.NumPage);
            WriteSession();
            UpdateCurrentPage(false);
        }
    }

    /// <summary>
    /// Méthode qui permet d'écrire la session dans le fichier json
    /// </summary>
    private void WriteSession()
    {
        try
        {
            _session.WriteSession();
        }
        catch (SessionStorageException e)
        {
            _mainPresenter.PushNotification(NotificationSeverity.Error, e.Message, string.Empty); 
        }
    }

    /// <summary>
    /// Méthode appellée lors de la réception de GoStopReading/GoListBook
    /// Méthode qui appellée lorsque l'on souhaite arrêter la lecture d'un livre
    /// Permet de mettre à jour le statut du livre à l'affichage
    /// Supprimer les boutons spécifiques à la fin de page
    /// Revenir sur la liste de tous les livres
    /// </summary>
    /// <param name="sender">Objet de la vue qui est envoyé</param>
    /// <param name="e">Argument lié à l'évenement</param>
    private void StopReading(object? sender, EventArgs e)
    {
        _mainPresenter.UpdateSessionStatut(_manager.CurrentBook!.IsbnNumber);
        _pageView.DisplayEndBook(false);
        _router.GoTo("menuView");
    }
    
    /// <summary>
    /// Méthode appellée lors de la réception de GoRestartBook
    /// Méthode qui permet de recommencer le livre
    /// </summary>
    /// <param name="sender">Objet de la vue qui est envoyé</param>
    /// <param name="e">Argument lié à l'évenement</param>
    private void RestartBook(object? sender, EventArgs e)
    {
        _pageView.DisplayEndBook(false);
        InitPages();
    }
}