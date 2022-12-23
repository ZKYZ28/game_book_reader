using GBReaderMahyF.Respositories;
using GBReaderMahyF.Domains;
using GBReaderMahyF.Presentations.Events;
using GBReaderMahyF.Presentations.ModelView;
using GBReaderMahyF.Presentations.Routes;
using GBReaderMahyF.Respositories.Exception;

namespace GBReaderMahyF.Presentations
{
    public class MenuPresenter
    {
        private MainPresenter _mainPresenter = null!;
        private readonly ManagerReader _manager;
        private readonly IMenuView _menuView;
        private readonly IRouterToView _router;

        private readonly IStorageFactory _factory;

        /// <summary>
        /// Constructeur du MainPresenter
        /// <param name="menuView">IMenuView qui est l'Interface implémentée par MenuView</param>
        /// <param name="manager">ManagerReader qui est le manager de l'application</param>
        /// <param name="rooter">IRouterToView  permet de changer de vue</param>
        /// <param name="factory">IStorageFactory permet d'intéragir avec le système de stockage</param>
        ///</summary>
        public MenuPresenter(IMenuView menuView, ManagerReader manager, IRouterToView rooter, IStorageFactory factory)
        {
            //INIT COMPONENT
            this._menuView = menuView;
            this._manager = manager;
            this._router = rooter;
            this._factory = factory;

            //EVENT
            this._menuView.SearchBook += SearchBookEvent;
            this._menuView.StartReadingBook += StartReadingBook;
            this._menuView.SeeStatistics += GoToStatistics;
            this._menuView.SwitchDetailBook += UpdateDisplayDetailBookEvent;
            SetupMenuWindow();
        }

        public MainPresenter MainPresenter
        {
            set { this._mainPresenter = value; }
        }
        
        /// <summary>
        ///Méthode qui permet de mettre en place la mainWindow
        /// Si il y a des erreurs elles seront affichées et l'application sera quittée après 3secondes
        /// Sinon on affichera l'ensemble des livres lus ainsi que la vue détaillée du premier livre
        /// </summary>
        private void SetupMenuWindow()
        {
           if(LoadCoverBook())
           {
                if (!CheckIfBookExist())
                {
                    List<ModelViewBook> listBookMv = MapperModelView.ConvertListBookToListBookMv(_manager.LibraryBooks.LibraryBooksAssociation.Values.ToList());
                    ModelViewBook bookMv = listBookMv[0];
                    _menuView.DisplayAllBooks(listBookMv);
                    UpdateDisplayDetailBook(bookMv);
                    UpdateButtonDetailBook(bookMv.Isbn);
                }
                else
                {
                    Task.Delay(3000).ContinueWith(t =>  Environment.Exit(0));
                }
           }
        }
        
        /// <summary>
        /// Méhtode qui permet de load les covers des livres
        /// </summary>
        /// <returns>bool true si les covers sont chargées avec succès sinon false</returns>
        private bool LoadCoverBook()
        {
            try
            {
                using IStorage storage = _factory.NewStorage(_manager);
                storage.LoadCoverBook();
            }
            catch (StorageException e)
            {
                _menuView.DisplayError(e.Message);
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// Méthode appellée lors de la réception de SwitchDetailBook
        /// </summary>
        /// <param name="sender">Objet de la vue qui est envoyé</param>
        /// <param name="switchDetailBookArgs">Argument lié à l'évenement</param>
        private void UpdateDisplayDetailBookEvent(object? sender, SwitchDetailBookArgs switchDetailBookArgs)
        {
            UpdateDisplayDetailBook(switchDetailBookArgs.BookMv);
        }
        
        /// <summary>
        /// Méthode qui permet de mettre ç jour l'affichage de la vue détaillée du livre
        /// </summary>
        /// <param name="bookMv">ModelViewBook qui est le livre dont on souhaite afficher les détails</param>
        private void UpdateDisplayDetailBook(ModelViewBook bookMv)
        {
            this._menuView.UpdateDetailBook(bookMv);
            this.UpdateButtonDetailBook(bookMv.Isbn);
        }

        /// <summary>
        /// Méthode qui peremet de changer le statut du livre.
        /// Si une session existe pour le livre le bouton prend la valeur Reprendre sinon
        /// il prend la valeur Commencer
        /// </summary>
        /// <param name="isbn">string qui est l'isbn du livre dont on souhaite changer le statut</param>
        public void UpdateButtonDetailBook(string isbn)
        {
            if (_manager.SessionsDictonnary.ContainsKey(isbn))
            {
                this._menuView.UpdateButtonDetailBook("Reprendre");
            }
            else
            {
                this._menuView.UpdateButtonDetailBook("Commencer"); 
            }
        }

        /// <summary>
        /// Méthode appellée lors de la réception de StartReadingBook
        /// </summary>
        /// <param name="sender">Objet de la vue qui est envoyé</param>
        /// <param name="startReadingArgs">Argument lié à l'évenement</param>
        private void StartReadingBook(object? sender, StartReadingArgs startReadingArgs)
        {
            if (LoadPagesForBook(startReadingArgs.Isbn))
            {
                _manager.CurrentBook = _manager.LibraryBooks.GetBookOnIsbn(startReadingArgs.Isbn);
                _mainPresenter.StartReadingBook();
                _router.GoTo("pageView");
            }
        }

        /// <summary>
        /// Méthode qui permet de charger les pages d'un livre si elles ne le sont pas encore
        /// </summary>
        /// <param name="isbn">string qui est le numéor isbn du livre dont on souhaite lire le pages</param>
        /// <returns>True si les pages ont été chargées avec succès sinon false</returns>
        private bool LoadPagesForBook(string isbn)
        {
            if (!_manager.LibraryBooks.CheckIfPageAlreadyExist(isbn))
            {
                try
                {
                    using IStorage storage = _factory.NewStorage(_manager);
                    storage.LoadPages(isbn);
                }
                catch (StorageException e)
                {
                    _menuView.DisplayError(e.Message);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Méthode appellée lors de la réception de SearchBook
        /// </summary>
        /// <param name="sender">Objet de la vue qui est envoyé</param>
        /// <param name="searchBookArgs">Argument lié à l'évenement</param>
        private void SearchBookEvent(object? sender, SearchBookArgs searchBookArgs)
        {
            SearchBook(searchBookArgs.ToFind);
        }
        
        /// <summary>
        /// 
        /// Méthode qui permet de se rendre sur la vue des statistiques
        /// Méthode appellée lors de la réception de SeeStatistics
        /// </summary>
        /// <param name="sender">Objet de la vue qui est envoyé</param>
        /// <param name="e">Argument lié à l'évenement</param>
        private void GoToStatistics(object? sender, EventArgs e)
        {
            _mainPresenter.SwitchToStatistics();
            _router.GoTo("statisticsView");
        }

        /// <summary>
        /// Méthode qui permet de rechercher un livre sur base d'une chaine de caractères et de les afficher
        /// Si toFind est une chaine vide alors on réaffichera tous les livres
        /// </summary>
        /// <param name="toFind">String qui est ce que l'on souhaite rechercher</param>
        private void SearchBook(string toFind)
        {
           List<ModelViewBook> listBookMv = MapperModelView.ConvertListBookToListBookMv(_manager.LibraryBooks.FindBooks(toFind));
           if (listBookMv.Capacity == 0)
           { 
               _menuView.DisplayError( "Aucun résultat");
           }
           _menuView.DisplayAllBooks(listBookMv);
        }

        /// <summary>
        /// Méthode qui permet de vérifier que le fichier que le fichier existe bien et si c'est le cas qu'il n'est pas vide.
        /// En cas d'erreur il affichera un message
        /// </summary>
        /// <returns>Boolean, true si une erreur est trouvée sinon false</returns>
        private bool CheckIfBookExist()
        {
            if (_manager.LibraryBooks.LibraryBooksAssociation.Count == 0)
            {
                _menuView.DisplayError( "Aucun livre n'a été créé pour le moment.");
                return true;
            }
            return false;
        }
    }
}